// ====================== 4. 客户端使用 ======================
Console.WriteLine("=== 制作咖啡 ===");
var coffee = new Coffee();
coffee.Make();

Console.WriteLine("\n=== 制作茶 ===");
var tea = new Tea();
tea.Make();

// ====================== 1. 抽象模板类（核心） ======================
/// <summary>
/// 饮品抽象类：定义制作饮品的固定模板
/// </summary>
public abstract class Beverage
{
    // 🔴 模板方法：固定算法的执行顺序，用sealed防止子类重写
    public void Make()
    {
        BoilWater();
        Brew();// 变化步骤：子类实现
        PourIntoCup();
        if (NeedCondiments())// 钩子方法：子类可以选择性控制
        {
            AddCondiments();// 变化步骤：子类实现
        }
    }
    
    // 固定步骤：所有饮品都一样，父类实现
    private void BoilWater()=> Console.WriteLine("1. 把水烧开");
    private void PourIntoCup()=> Console.WriteLine("3. 倒入杯子");

    // 抽象方法：变化的步骤，子类必须实现
    protected abstract void Brew();// 冲泡
    protected abstract void AddCondiments();// 加配料
    // 钩子方法：可选步骤，子类可以选择性覆盖
    protected virtual bool NeedCondiments()=> true ;
    
}

// ====================== 2. 具体子类：咖啡 ======================
public class Coffee : Beverage
{
    // 实现自己的冲泡方法
    protected override void Brew()
    {
        Console.WriteLine("2. 冲泡咖啡");
    }
    // 实现自己的加配料方法
    protected override void AddCondiments()
    {
        Console.WriteLine("4. 盖上盖子");
    }
}

// ====================== 3. 具体子类：茶 ======================
public class Tea :Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("2. 浸泡茶叶");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("4. 加柠檬");
    }
    // 覆盖钩子方法：茶不需要加配料
    protected override bool NeedCondiments() => false;
}





