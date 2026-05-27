// ====================== 4. 客户端使用 ======================
// 创建处理者
var teamLeader = new TeamLeader();
var manager = new Manager();
var director = new Director();
var generalManager = new GeneralManager();

// 构建责任链：组长→经理→总监→总经理
teamLeader.setApprover(manager);
manager.setApprover(director);
director.setApprover(generalManager);

// 提交不同的请假申请
Console.WriteLine("=== 张三请假1天 ===");
teamLeader.ProcessRequest(new LeaveRequest("张三", 1));

Console.WriteLine("\n=== 李四请假3天 ===");
teamLeader.ProcessRequest(new LeaveRequest("李四", 3));

Console.WriteLine("\n=== 王五请假7天 ===");
teamLeader.ProcessRequest(new LeaveRequest("王五", 7));

Console.WriteLine("\n=== 赵六请假10天 ===");
teamLeader.ProcessRequest(new LeaveRequest("赵六", 10));

// ====================== 1. 请求：请假申请 ======================
public class LeaveRequest
{
    public string Name;
    public int Days;
    public LeaveRequest(string name, int days)
    {
        this.Name = name;
        this.Days = days;
    }
}
// ====================== 2. 抽象处理者：审批人 ======================
public abstract class Approver
{
    // 下一个处理者
    protected Approver _nextApprover;
    // 设置下一个处理者
    public void setApprover(Approver nextApprover)
    {
        _nextApprover = nextApprover;
    }
    // 处理请求的抽象方法
    public abstract void ProcessRequest(LeaveRequest  request);
}
// ====================== 3. 具体处理者 ======================
public class TeamLeader : Approver
{
    public override void ProcessRequest(LeaveRequest request)
    {
        if (request.Days <= 1)
        {
            Console.WriteLine($"✅ 组长审批通过：{request.Name}请假{request.Days}天");
        }
        else
        {
            // 处理不了，传给下一个处理者
            Console.WriteLine($"➡️ 组长无权审批，转交给经理");
            _nextApprover.ProcessRequest(request);
        }
    }
}

public class Manager :Approver
{
    public override void ProcessRequest(LeaveRequest request)
    {
        if (request.Days > 1 && request.Days <= 3)
        {
            Console.WriteLine($"✅ 经理审批通过：{request.Name}请假{request.Days}天");
        }
        else
        {
            Console.WriteLine($"➡️ 经理无权审批，转交给总监");
            _nextApprover.ProcessRequest(request);
        }
    }
}

public class Director :Approver
{
    public override void ProcessRequest(LeaveRequest request)
    {
        if (request.Days > 3 && request.Days <= 7)
        {
            Console.WriteLine($"✅ 总监审批通过：{request.Name}请假{request.Days}天");
        }
        else
        {
            Console.WriteLine($"➡️ 总监无权审批，转交给总裁");
            _nextApprover.ProcessRequest(request);
        }
    }
}

public class GeneralManager : Approver
{
    public override void ProcessRequest(LeaveRequest request)
    {
        Console.WriteLine($"✅ 总裁审批通过：{request.Name}请假{request.Days}天");
    }
}