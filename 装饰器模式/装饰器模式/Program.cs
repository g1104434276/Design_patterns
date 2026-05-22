// 基础原味
MilkTea tea1 = new OriginalTea();
Console.WriteLine($"{tea1.GetDesc()} 价格：{tea1.GetPrice()}");

// 套娃包装：原味+珍珠
MilkTea tea2 = new Pearl(new OriginalTea());
Console.WriteLine($"{tea2.GetDesc()} 价格：{tea2.GetPrice()}");

// 多层叠加：原味+珍珠+椰果
MilkTea tea3 = new Coconut(new Pearl(new OriginalTea()));
Console.WriteLine($"{tea3.GetDesc()} 价格：{tea3.GetPrice()}");


// 1. 抽象组件：奶茶统一规范
public abstract class MilkTea
{
    public abstract string GetDesc();
    public abstract double GetPrice();
}

// 2. 具体组件：基础原味奶茶（原始对象）
public class OriginalTea : MilkTea
{
    public override string GetDesc()
    {
        return "原味基础奶茶";
    }

    public override double GetPrice()
    {
        return 8;
    }
}
// 3. 抽象装饰器：配料装饰父类
public abstract class CondimentDecorator : MilkTea
{
    protected MilkTea _milkTea;

    protected CondimentDecorator(MilkTea milkTea)
    {
        _milkTea = milkTea;
    }
}
// 4. 具体装饰器1：加珍珠
public class Coconut : CondimentDecorator
{
    public Coconut(MilkTea milkTea) : base(milkTea)
    {
        
    }

    public override string GetDesc()
    {
        return  _milkTea.GetDesc() + "椰果";
    }

    public override double GetPrice()
    {
        return _milkTea.GetPrice() + 1;
    }
}
// 具体装饰器2：加椰果
public class Pearl : CondimentDecorator
{
    public Pearl(MilkTea milkTea) : base(milkTea)
    {
        
    }

    public override string GetDesc()
    {
        return _milkTea.GetDesc() + "珍珠";
    }

    public override double GetPrice()
    {
        return _milkTea.GetPrice() + 1;
    }
}




