// ====================== 使用：自由组合 ======================
var clieckPen = new ClieckPen(new BlackCore());
clieckPen.Write("测试!");
var capPen = new CapPen(new RedCore());
capPen.Write("测试");


public interface IpenCore
{
    // 笔芯的功能：出墨水写字
    void Draw(String text);
}
// 具体实现层：黑笔芯
public class BlackCore : IpenCore
{
    public void Draw(String text)
    {
        Console.WriteLine("[黑色]:"+text);
    }
}
// 具体实现层：红笔芯
public class RedCore : IpenCore
{
    public void Draw(String text)
    {
        Console.WriteLine("[红色]:"+text);
    }
}
// ====================== 2. 抽象层：笔杆（不干活，只定义怎么用） ======================
public abstract class Pen
{
    // ========== 这就是桥 ==========
    // 🔴 核心桥梁：笔杆里的卡槽（就是之前说的impl字段）
    // 持有笔芯的引用，把笔杆和笔芯连起来
    private IpenCore _core;

    // 构造函数：把笔芯塞进卡槽里
    public Pen(IpenCore core)
    {
        _core = core;// 接入桥梁
    }
    public abstract void Write(String text);
}

// 扩展抽象层：拔盖笔杆
public class CapPen : Pen
{
    public CapPen(IpenCore core) : base(core){}
    public override void Write(String text)
    {
        Console.WriteLine("拔下笔帽...");
        // 借助桥梁，调用另一边的功能
        // 笔杆不用自己出墨水，让卡槽里的笔芯写
        Console.WriteLine(text);
    }
}

public class ClieckPen :Pen
{
    public ClieckPen(IpenCore core) : base(core)
    {
        
    }

    public override void Write(string text)
    {
        Console.WriteLine("按一下笔杆，出笔芯...");
        // 借助桥梁，调用另一边的功能
        // 笔杆不用自己出墨水，让卡槽里的笔芯写
        Console.WriteLine(text);
    }
}