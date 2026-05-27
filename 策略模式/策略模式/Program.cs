var taxiStrategy = new TaxiStrategy();
var subwayStrategy = new SubwayStrategy();

// 客户端选择策略：今天打车
var travelPlanner = new TravelPlanner(taxiStrategy);
travelPlanner.StartTravel();
// 动态切换策略：明天坐地铁
travelPlanner.SetStrategy(subwayStrategy);
travelPlanner.StartTravel();

// ====================== 1. 抽象策略接口 ======================
public interface ITrategy
{
    // 统一的算法执行方法
    void Travel();
}

// ====================== 2. 具体策略类 ======================
public class SubwayStrategy :ITrategy
{
    public void Travel()
    {
        Console.WriteLine("地铁策略.🚇 乘坐地铁：耗时30分钟，费用5元");
    }
}

public class TaxiStrategy : ITrategy
{
    public void Travel()
    {
        Console.WriteLine("打车策略.🚕 打车：耗时15分钟，费用20元");
    }
}

public class BicycleStrategy : ITrategy
{
    public void Travel()
    {
        Console.WriteLine("自行车策略.🚲 骑自行车：耗时45分钟，费用1元");
    }
}
// ====================== 3. 上下文：行程规划器 ======================
public class TravelPlanner
{
    // 持有策略接口的引用
    private ITrategy _travelPlanner;
    // 构造函数注入策略
    public TravelPlanner(ITrategy travelPlanner)
    {
        _travelPlanner = travelPlanner;
    }
    // 动态切换策略（运行时随时换）
    public void SetStrategy(ITrategy travelPlanner)
    {
        _travelPlanner = travelPlanner;
    }
    // 对外统一接口，委托给策略执行
    public void StartTravel()
    {
        _travelPlanner.Travel();
    }
    
}