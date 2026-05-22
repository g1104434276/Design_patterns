
using System.Drawing;
// ====================== 4. 客户端使用 ======================
var bulletFactory = new BulletFactory();
// 创建10000发黄色子弹
for (int i = 0; i < 1000; i++)
{
    // 从工厂获取享元（永远只返回同一个YellowBullet对象）
    var bullet = bulletFactory.GetBullet("Yellow");
    // 传入变化的外部状态
    bullet.Fly(i, i, 10);
}
// 内存占用：1个享元对象 + 10000次方法调用的参数
Console.WriteLine($"\n缓存中享元对象数量：{bulletFactory.GetCacheCount()}");
Console.WriteLine($"实际创建的子弹对象数量：{bulletFactory.GetCacheCount()}");


// ====================== 1. 抽象享元：定义统一接口 ======================
public interface IBullet
{
    // 方法接收变化的外部状态（坐标、速度）
    void Fly(int x, int y, int speed);
}

// ====================== 2. 具体享元：存储不变的内部状态 ======================
/// <summary>
/// 黄色子弹享元（所有黄色子弹共享这一个对象）
/// </summary>
public class YellowBullet : IBullet
{
    // 🔴 内部状态：不变的属性，只存储一次
    private readonly Color _color = Color.Yellow;
    private readonly int _size = 2;
    private readonly int _damage = 10;
    // 方法接收外部状态，不存储
    public void Fly(int x, int y, int speed)
    {
        Console.WriteLine($"黄色子弹 坐标({x},{y}) 速度{speed} 伤害{_damage}");
    }
}

public class RedBullet : IBullet
{
    private readonly Color _color = Color.Red;
    private readonly int _size = 5;
    private readonly int _damage = 15;
    

    public void Fly(int x, int y, int speed)
    {
        Console.WriteLine($"红色子弹 坐标({x},{y}) 速度{speed} 伤害{_damage}");
    }
}
// ====================== 3. 享元工厂：缓存和管理享元（核心） ======================
public class BulletFactory
{
    // 缓存享元对象的字典
    private readonly Dictionary<string, IBullet> _bullets = new Dictionary<string, IBullet>();

    /// <summary>
    /// 获取享元对象：如果缓存里有就直接返回，没有就创建并缓存
    /// </summary>
    public IBullet GetBullet(string type)
    {
        if (!_bullets.ContainsKey(type))
        {
            // 第一次请求时才创建，之后都从缓存取
            switch (type)
            {
                case "Yellow":
                    _bullets.Add(type, new YellowBullet());
                    break;
                // 以后加红色子弹，只需要加一个case
                case "Red":
                    _bullets.Add(type, new RedBullet());
                    break;
            }
            Console.WriteLine($"创建了新的{type}子弹享元");
        }
        return _bullets[type];
    }
    // 获取缓存中享元的数量
    public int GetCacheCount() => _bullets.Count;
}