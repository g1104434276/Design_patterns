Console.WriteLine("=== 1. 自定义配置鸭脖 ===");
YaBo MyYaBo = new YaBo.Builder(5, 500).addCoriander().addGarlic().addVinegar().build();
MyYaBo.eat();
Console.WriteLine("=== 1. 指挥者预配置鸭脖 ===");
var makeExtraSpicyYaBo = new YaBoDirector().MakeExtraSpicyYaBo();
makeExtraSpicyYaBo.eat();

// ====================== 指挥者（可选）：封装固定的构建流程 ======================
// 当有多个标准化套餐时使用，客户端不需要知道具体配置
public class YaBoDirector
{
    // 标准微辣鸭脖套餐
    public YaBo MakeMildYaBo(int spicyLevel, int weight)
    {
        return new YaBo.Builder(2, 200)
            .addCoriander()
            .addGarlic()
            .addVinegar()
            .packType()
            .build();   
    }
    // 特辣鸭脖套餐
    public YaBo MakeExtraSpicyYaBo()
    {
        return new YaBo.Builder(5,500)
            .addCoriander()
            .addGarlic()
            .addVinegar()
            .packType()
            .build();
    }
    
}

// ====================== 产品：不可变的鸭脖对象 ======================
// 所有属性只读，构造函数私有，只能通过建造者创建
public class YaBo
{
    // 🔴 必选参数（没有就不是一个合法的鸭脖）
    public int SpicyLevel { get; } // 辣度 0-10
    public int Weight { get; }     // 重量 克

    // 🟡 可选参数（可以有默认值）
    public bool AddCoriander { get; } // 加香菜
    public bool AddVinegar { get; }   // 加醋
    public bool AddGarlic { get; }    // 加蒜
    public string PackType { get; }   // 包装类型：塑料/纸质
    
    // 🔴 私有构造函数：彻底禁止外部直接new
    // 只有内部的Builder类可以调用，保证对象创建的唯一性
    private YaBo(int spicyLevel, int weight, bool addCoriander, 
        bool addVinegar, bool addGarlic, string packType)
    {
        SpicyLevel = spicyLevel;
        Weight = weight;
        
        AddCoriander = addCoriander;
        AddVinegar = addVinegar;
        AddGarlic = addGarlic;
        PackType = packType;
    }
    
    //业务方法：只有完整的鸭脖才能被食用
    public void eat()
    {
        Console.WriteLine($"正在吃 {Weight}克、{SpicyLevel}度辣 的鸭脖");
        Console.WriteLine($"配料：{(AddCoriander ? "香菜" : "")} {(AddVinegar ? "醋" : "")} {(AddGarlic ? "蒜" : "")}");
        Console.WriteLine($"包装：{PackType}\n");
    }
    
    
    // ====================== 内嵌建造者（C#工业界标准写法） ======================
    // 内嵌类可以直接访问外部类的私有构造函数
    public class Builder
    {
        // 🔴 必选参数：通过建造者构造函数强制传入
        // 不允许创建没有辣度和重量的鸭脖
        private readonly int _spicyLevel;
        private readonly int _weight;

        // 🟡 可选参数：设置合理的默认值
        private bool _addCoriander = false;
        private bool _addVinegar = false;
        private bool _addGarlic = false;
        private string _packType = "塑料包装";
        
        public Builder(int spicyLevel, int weight)
        {
            _spicyLevel = spicyLevel;
            _weight = weight;
        }

        public Builder addCoriander()
        {
            _addCoriander = true;
            return this;
        }

        public Builder addVinegar()
        {
            _addVinegar = true;
            return this;
        }

        public Builder addGarlic()
        {
            _addGarlic = true;
            return this;
        }

        public Builder packType()
        {
            _packType = "纸质包装";
            return this;
        }
        public YaBo build()
        {
            if (_spicyLevel < 0 ||  _spicyLevel > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(_spicyLevel), "辣度必须在0-10之间"); 
            }
            if (_weight < 0 || _weight > 500)
            {
                throw new ArgumentOutOfRangeException(nameof(_weight), "重量必须在1-500克之间");
            }
            return new YaBo(_spicyLevel, _weight, _addCoriander, 
                _addVinegar, _addGarlic, _packType);
        }
    }
}