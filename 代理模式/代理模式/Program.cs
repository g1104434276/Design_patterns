// ====================== 4. 客户端（主办方） ======================
// 主办方永远只和经纪人打交道，不知道明星本人的存在
Agent agent = new Agent();
Console.WriteLine("=== 主办方邀请唱歌 ===");
agent.Sing();
Console.WriteLine("\n=== 主办方邀请演戏 ===");
agent.Sing();

public interface IArtist
{
    void Sing();
    void Act();
}

public class Star : IArtist
{
    public void Sing()
    {
        Console.WriteLine("🎤 明星：演唱《七里香》");
    }

    public void Act()
    {
        Console.WriteLine("🎬 明星：出演《无间道》");
    }
}

public class Agent : IArtist
{
    private readonly Star _star;

    public Agent()
    {
        _star  = new  Star();
    }
    // 代理唱歌：处理所有前置后置逻辑
    public void Sing()
    {
        // 前置逻辑：经纪人处理琐事
        Console.WriteLine("📝 经纪人：谈合同、谈价格、排日程");
        Console.WriteLine("🚗 经纪人：安排车辆、酒店、安保");
        // 只有核心工作才交给明星本人
        _star.Sing();
        // 后置逻辑：经纪人收尾
        Console.WriteLine("💰 经纪人：结款、开发票");
        Console.WriteLine("📞 经纪人：处理媒体采访和售后");
    }

    // 代理演戏：同样的逻辑
    public void Act()
    {
        Console.WriteLine("📝 经纪人：谈剧本、谈片酬、排档期");
        Console.WriteLine("🚗 经纪人：安排剧组住宿、随行人员");
        
        _star.Act();
        
        Console.WriteLine("💰 经纪人：结款、处理宣传事务");
    }
}