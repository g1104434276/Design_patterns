/// 顾客充当客户端，负责调用简单工厂来生产对象
/// 即客户点菜，厨师（相当于简单工厂）负责烧菜(生产的对象)
Food food1  = FoodSimpleFactory.Factory("TomatoScrambledEggs");
food1.print();
Food food2 = FoodSimpleFactory.Factory("ShrededPorkWithPotatoes");
food2.print();

//两个食物
public abstract class Food { public abstract void print(); }

public class TomatoScrambledEggs : Food
{
    public override void print() { Console.WriteLine("Tomato Scrambled eggs"); }
}

public class ShrededPorkWithPotatoes :Food
{
    public override void print() { Console.WriteLine("Shreded Pork with Potatoes"); }
}

//创建一个静态工厂类
public class FoodSimpleFactory
{
    public static Food Factory(String name)
    {
        Food food = null;
        if (name == "TomatoScrambledEggs")
        {
            food =  new TomatoScrambledEggs();
        }
        else if (name == "ShrededPorkWithPotatoes")
        {
            food = new ShrededPorkWithPotatoes();
        }
        return food;
    }
    
}