// ====================== 5. 客户端使用 ======================
// 创建接收者
TV tv = new TV();
Light light = new Light();

// 创建命令
TVOnCommand tvOn = new TVOnCommand(tv);
TVOffCommand tvOff = new TVOffCommand(tv);
LightOnCommand lightOn = new LightOnCommand(light);
LightOffCommand lightOff = new LightOffCommand(light);

// 创建遥控器，设置按钮
var remoteControl = new RemoteControl();
remoteControl.setCommand(0,tvOn,tvOff);
remoteControl.setCommand(1,lightOn,lightOff);
// 使用遥控器
remoteControl.PressOnButton(0);
remoteControl.PressOnButton(1);
remoteControl.PressUndoButton();

// ====================== 2. 接收者：真正干活的对象 ======================
// 电视
public class TV
{
    public void TurnOn() => Console.WriteLine("📺 电视开机");
    public void TurnOff() => Console.WriteLine("📺 电视关机");
    public void VolumeUp() => Console.WriteLine("📺 音量+1");
    public void VolumeDown() => Console.WriteLine("📺 音量-1");
}
// 电灯
public class Light
{
    public void TurnOn() => Console.WriteLine("💡 电灯打开");
    public void TurnOff() => Console.WriteLine("💡 电灯关闭");
}

// ====================== 1. 抽象命令接口 ======================
public interface ICommand
{
    void Execute();
    void Undo();
}

class noCommand:ICommand
{
    public void Execute(){}
    public void Undo(){}
}
// ====================== 3. 具体命令：封装每个动作 ======================
// 电视开机命令
public class TVOnCommand : ICommand
{
    private TV _command;
    
    public TVOnCommand(TV command) => _command = command;
    
    public void Execute()=>_command.TurnOn();
    public void Undo() =>_command.TurnOff();// 关机的撤销就是开机
}
// 电视关机命令
public class TVOffCommand : ICommand
{
    private TV _command;
    
    public TVOffCommand(TV command) => _command = command;
    
    public void Execute()=>_command.TurnOff();
    public void Undo() =>_command.TurnOn();
}

// 电灯打开命令
public class LightOnCommand :ICommand
{
    private Light _command;
    public LightOnCommand(Light command) => _command = command;
    public void Execute()=>_command.TurnOn();
    public void Undo()=>_command.TurnOff();
}

// 电灯关闭命令
public class LightOffCommand :ICommand
{
    private Light _command;
    public LightOffCommand(Light command) => _command = command;
    public void Execute()=>_command.TurnOff();
    public void Undo()=>_command.TurnOn();
}
//遥控器 
public class RemoteControl
{
    // 存储每个按钮对应的命令
    private readonly ICommand[] _onButtons = new ICommand[5];
    private readonly ICommand[] _offButtons = new ICommand[5];
    // 存储最后执行的命令，用于撤销
    public ICommand _lastCommand;

    public RemoteControl()
    {
        // 初始化空命令，避免空指针
        var noCommand = new noCommand();
        for (int i = 0; i < _onButtons.Length; i++)
        {
            _onButtons[i] =  noCommand;
            _offButtons[i] =  noCommand;
        }
    }
    // 设置按钮对应的命令
    public void setCommand(int slot ,ICommand onButtons, ICommand offButtons)
    {
        _onButtons[slot] = onButtons;
        _offButtons[slot] = offButtons;
    }
    // 按开机按钮
    public void PressOnButton(int slot)
    {
        _onButtons[slot].Execute();
        _lastCommand = _onButtons[slot];
    }
    // 按关机按钮
    public void PressOffButton(int slot)
    {
        _offButtons[slot].Execute();
        _lastCommand = _offButtons[slot];
    }
    // 按撤销按钮
    public void PressUndoButton()
    {
        Console.WriteLine("执行撤销操作!");
        _lastCommand.Undo();
    }
    
}










