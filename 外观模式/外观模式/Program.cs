// ====================== 客户端（极简） ======================
// 客户端只需要知道外观类，不用知道任何子系统
var computerFacade = new ComputerFacade();
computerFacade.start();// 一键开机

// ====================== 子系统（完全不用改，和之前一样） ======================
// 子系统1：CPU
public class Cpu
{
    public void Init() => Console.WriteLine("初始化CPU");
    public void Start() => Console.WriteLine("启动CPU");
}

// 子系统2：内存
public class Memory
{
    public void SelfCheck() => Console.WriteLine("内存自检");
    public void Load() => Console.WriteLine("加载内存数据");
}

// 子系统3：硬盘
public class HardDisk
{
    public void ReadBootSector() => Console.WriteLine("读取硬盘引导扇区");
    public void LoadOS() => Console.WriteLine("加载操作系统");
}

// 子系统4：显卡
public class GraphicsCard
{
    public void Init() => Console.WriteLine("初始化显卡");
    public void OutputDisplay() => Console.WriteLine("输出显示信号");
}
// ====================== 外观类（核心） ======================
/// 电脑外观类：封装所有开机的复杂逻辑
public class ComputerFacade
{
    // 持有所有子系统的引用
    private readonly Cpu _cpu  = new Cpu();
    private readonly Memory _memory = new Memory(); 
    private readonly HardDisk _hardDisk = new HardDisk();
    private readonly GraphicsCard _graphicsCard = new GraphicsCard();

    /// 统一的开机方法：客户端只需要调用这一个方法
    public void start()
    {
        Console.WriteLine("=== 按下电源键 ===");
        // 所有复杂的调用顺序和逻辑都封装在这里
        _cpu.Init();
        _cpu.Start();
        _memory.Load();
        _memory.SelfCheck();
        _hardDisk.ReadBootSector();
        _hardDisk.LoadOS();
        _graphicsCard.Init();
        _graphicsCard.OutputDisplay();
        Console.WriteLine("=== 电脑开机成功 ===");
    }
}