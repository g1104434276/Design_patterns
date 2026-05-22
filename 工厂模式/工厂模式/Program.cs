
Food food1 = new ShreddedPorkWithPotatoesFactory().CreateFood();
food1.Print();
Food food2 = new TomatoScrambledEggsFactory().CreateFood();
food2.Print();

public abstract class Food
{
    public abstract void Print();
}

public class TomatoScrambledEggs : Food
{
    public override void Print() { Console.WriteLine("Tomato Scrambled eggs"); }
}

public class ShreddedPorkWithPotatoes : Food
{
    public override void Print() { Console.WriteLine("Shredded Pork with Potatoes"); }
}


public abstract class Creator { public abstract Food CreateFood(); }

public class TomatoScrambledEggsFactory : Creator
{
    public override Food CreateFood() => new TomatoScrambledEggs(); 
}

public class ShreddedPorkWithPotatoesFactory : Creator
{
    public override Food CreateFood() => new ShreddedPorkWithPotatoes();
}