Elevator elevator = new Elevator();
Console.WriteLine("=== 乘客操作电梯 ===");
elevator.Open();   // 停止状态 → 开门
elevator.Run();    // 开门状态不能运行
elevator.Close();  // 开门状态 → 关门
elevator.Run();    // 关门状态 → 运行
elevator.Open();   // 运行状态不能开门
elevator.Stop();   // 运行状态 → 停止
elevator.Open();   // 停止状态 → 开门

// ====================== 1. 抽象状态接口 ======================
public abstract class ElevatorState
{
    // 持有上下文引用，用于状态转换
    protected Elevator _elevator;

    public void SetContext(Elevator elevator)
    {
        _elevator = elevator;
    }
    
    // 定义所有状态都能执行的行为
    public abstract void Open();
    public abstract void Close();
    public abstract void Run();
    public abstract void Stop();
    
}
// ====================== 2. 具体状态类 ======================
public class OpenState :ElevatorState
{
    public override void Open()
    {
        Console.WriteLine("❌ 电梯已经是开门状态，不能再开门");
    }

    public override void Close()
    {
        // 状态转换：开门 → 关门
        _elevator.SetState(_elevator.closeState);
        Console.WriteLine("✅ 电梯关门");
        
    }

    public override void Run()
    {
        Console.WriteLine("❌ 电梯开门状态，不能运行");
    }

    public override void Stop()
    {
        Console.WriteLine("❌ 电梯已经是停止状态");
    }
}

public class CloseState:ElevatorState
{
    public override void Open()
    {
        _elevator.SetState(_elevator.openState);
        Console.WriteLine("✅ 电梯开门");
    }

    public override void Close()
    {
        Console.WriteLine("❌ 电梯已经是关门状态");
    }

    public override void Run()
    {
        Console.WriteLine("✅ 电梯开始运行");
        _elevator.SetState(_elevator.runningState);
    }

    public override void Stop()
    {
        Console.WriteLine("✅ 电梯停止");
        _elevator.SetState(_elevator.closeState);
    }
}

public class RunningState:ElevatorState
{
    public override void Open()
    {
        Console.WriteLine("❌ 电梯正在运行，不能开门");
    }

    public override void Close()
    {
        Console.WriteLine("❌ 电梯正在运行，不能关门");
    }

    public override void Run()
    {
        Console.WriteLine("❌ 电梯已经在运行");
    }

    public override void Stop()
    {
        Console.WriteLine("✅ 电梯停止运行");
        _elevator.SetState(_elevator.stopState);
    }
}

public class StopState:ElevatorState
{
    public override void Open()
    {
        Console.WriteLine("✅ 电梯开门");
        _elevator.SetState(_elevator.openState);
    }

    public override void Close()
    {
        Console.WriteLine("✅ 电梯关门");
        _elevator.SetState(_elevator.closeState);
    }

    public override void Run()
    {
        Console.WriteLine("✅ 电梯开始运行");
        _elevator.SetState(_elevator.runningState);
    }

    public override void Stop()
    {
        Console.WriteLine("❌ 电梯已经是停止状态");
    }
}


public class Elevator
{
    // 预定义所有状态（单例，避免重复创建）
    public OpenState openState { get;} = new OpenState();
    public CloseState  closeState{get;} = new CloseState();
    public RunningState runningState{get;} = new RunningState();
    public StopState stopState{get;} = new StopState();
    
    // 当前状态
    public ElevatorState _currentState;

    
    public Elevator()
    {
        // 初始状态：停止
        _currentState = stopState;
        _currentState.SetContext(this);
    }

    // 设置当前状态
    public void SetState(ElevatorState state)
    {
        _currentState = state;
        _currentState.SetContext(this);
        Console.WriteLine($"🔄 电梯状态切换为：{state.GetType().Name}");
    }
    
    // 对外统一接口，所有请求委托给当前状态处理
    public void Open()=>_currentState.Open();
    public void Close()=>_currentState.Close();
    public void Run()=>_currentState.Run();
    public void Stop()=>_currentState.Stop();
    
    
}