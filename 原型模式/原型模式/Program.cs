using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

Console.WriteLine("=== 原型模式演示 ===");
var recipe = new Recipe
{
    Name = "湖南特辣配方",
    Ingredients = new List<string> { "辣椒", "花椒", "八角" }
};

var yaBo = new YaBo( 50,  100,  recipe);
var yaboClone = yaBo.Clone();
yaboClone._SpicyLevel = 0;
yaboClone._Weight = 0;
yaboClone._Recipe = null;

Console.WriteLine(yaBo._SpicyLevel);
Console.WriteLine(yaBo._Weight);
Console.WriteLine(yaBo._Recipe);



// 原型接口：定义克隆方法 
public interface IPrototype<T>
{
    T Clone();
}

//抽象类实现接口的方法
public abstract class PrototypeBase<T> : IPrototype<T>
{
     
    //二进制序列化在20-24年之间慢慢的被禁止使用
    // [Obsolete("Obsolete")]
    // public YaBo Clone()
    // {
    //     using (var stream = new MemoryStream())
    //     {
    //         var formatter = new BinaryFormatter();
    //         formatter.Serialize(stream, this);
    //         stream.Position = 0;
    //         return (YaBo)formatter.Deserialize(stream);
    //     }
    // }
    //现在的克隆使用更安全的JSON序列化做深拷贝
    public T Clone()
    {
        // 配置：支持循环引用 + 包含所有字段
        var options = new JsonSerializerOptions
        {
            IncludeFields = true,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        };
        // 序列化 + 反序列化 = 深拷贝
        String json = JsonSerializer.Serialize(this, options);
        return JsonSerializer.Deserialize<T>(json, options);
    }
}

// 产品：鸭脖（必须标记为可序列化）
// [Serializable] [已过时]
public class YaBo : PrototypeBase<YaBo>
{
    // 值类型
    public int _SpicyLevel { get; set; }
    public int _Weight { get; set; }
    
    // 引用类型
    public Recipe _Recipe { get; set; }

    public YaBo()
    {
        
    }
    

    public YaBo(int SpicyLevel, int Weight, Recipe Recipe)
    {
        _SpicyLevel = SpicyLevel;
        _Weight = Weight;
        _Recipe = Recipe;
        
        //卤制的过程非常的耗时,最好跳过
        Console.WriteLine("正在卤制鸭脖...（耗时2小时）");
        System.Threading.Thread.Sleep(2000);
    }
}

// 引用类型：卤制配方（也必须标记为可序列化[已过时]）
// [Serializable][已过时]
public class Recipe
{
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
}