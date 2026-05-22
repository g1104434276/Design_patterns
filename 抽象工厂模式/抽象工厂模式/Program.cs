HuNanFactory huNanFactory = new HuNanFactory();
var orderPage = new OrderPage();
orderPage.creatFood(huNanFactory);

class OrderPage
{
    public void creatFood(AbstractFactory factory)
    {
        factory.CreateYaBoFood().Creat();
        factory.CreateYaJaFood().Creat();
    }
}
//使用抽象方法来创建工厂(工厂接口生产的 是两种抽象的食物)
public abstract class AbstractFactory
{
    public abstract YaJa CreateYaJaFood();
    public abstract YaBo CreateYaBoFood();
}
public class HuNanFactory : AbstractFactory
{
    public override YaJa CreateYaJaFood() =>  new HuNanYaJa();
    public override YaBo CreateYaBoFood() => new HuNanYaBo();
}

public class ShangHiFactory :AbstractFactory
{
    public override YaJa CreateYaJaFood() => new ShangHiYaJa();
    public override YaBo CreateYaBoFood() => new ShangHiYaBo();
}

public abstract class YaBo
{
    public abstract void Creat();
}

public abstract class YaJa
{
    public abstract void Creat();
}
//创建产品,用的同一种方法
public class HuNanYaBo : YaBo { public override void Creat() => Console.WriteLine("HuNanYaBo"); }
public class HuNanYaJa : YaJa { public override void Creat() => Console.WriteLine("HuNanYaJa"); }
public class ShangHiYaJa:YaJa { public override void Creat() => Console.WriteLine("ShangHiYaJa"); }
public class ShangHiYaBo:YaBo { public override void Creat() => Console.WriteLine("ShangHiYaBo"); }


