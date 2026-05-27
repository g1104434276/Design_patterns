// 创建中介者（聊天室）
var weChatChatRoom = new WeChatChatRoom();

// 创建用户，注册到聊天室
var normalUserZ = new NormalUser("张三",weChatChatRoom);
var normalUserL = new NormalUser("李四",weChatChatRoom);
var normalUserW = new NormalUser("王五",weChatChatRoom);
var normalUserM = new NormalUser("王麻子",weChatChatRoom);

weChatChatRoom.Register(normalUserZ);
weChatChatRoom.Register(normalUserL);
weChatChatRoom.Register(normalUserW);
weChatChatRoom.Register(normalUserM);

// 张三给李四发私信
normalUserZ.Send("李四","我喜欢你!");
// 张三给所有人发群消息
normalUserZ.Send("所有人","我喜欢你!");
// 测试敏感词过滤
normalUserZ.Send("李四","脏话");

// ====================== 1. 抽象中介者 ======================
public interface IChatRoom
{
    // 注册用户
    public void Register(IUser user);
    // 转发消息
    public bool SendMessgae(string form, string to, string message);
}

// ====================== 3. 具体中介者：聊天室 ======================
public class WeChatChatRoom :IChatRoom
{
    // 维护所有注册的用户
    private readonly Dictionary<string,IUser> _users = new();
    // 用户加入聊天室
    public void Register(IUser user)
    {
        if (!_users.ContainsKey(user._Name))
        {
            _users.Add(user._Name, user);
            Console.WriteLine($"{user._Name} 加入了聊天室");
        }
    }
    // 核心：转发消息
    public bool SendMessgae(string from, string to, string message)
    {
        // 统一控制逻辑：敏感词过滤
        if (message.Contains("脏话"))
        {
            Console.WriteLine($"❌ {from} 发送了敏感词，消息被拦截");
            return false;
        }
        // 转发给目标用户
        if (_users.TryGetValue(to, out IUser targetUser))
        {
            targetUser.Register(from, message);
            return true;
            // 如果目标是"所有人"，转发给所有用户
        }else if (to == "所有人")
        {
            foreach (var user in _users)
            {
                if (user.Key != from)
                {
                    user.Value.Register(from, message);
                }
            }
        }
        
        return false;
    }
}

// ====================== 2. 抽象同事 ======================
public abstract class IUser
{
    public string _Name { get; protected set; }
    // 持有中介者的引用，所有通信都通过中介者
    protected readonly IChatRoom _chatRoom;

    protected IUser(string name, IChatRoom chatRoom)
    {
        _Name = name;
        _chatRoom = chatRoom;
    }

    // 发消息：只发给中介者(中介者去找用户去做转发)
    public abstract void Send(string to, string message);
    // 收消息：从中介者接收(中介者找到用户来接收)
    public abstract void Register(string name, string message);
}

// ====================== 4. 具体同事：普通用户 ======================
public class NormalUser : IUser
{
    public NormalUser(string name, IChatRoom chatRoom) :base(name,chatRoom){}
    
    //这个函数需要自己来调
    // 发消息：只需要告诉中介者发给谁、发什么
    public override void Send(string to, string message)
    {
        _chatRoom.SendMessgae(_Name,to,message);
    }

    //这个函数需要中介者来调用,从中介者接收
    public override void Register(string name, string message)
    {
        Console.WriteLine($"{_Name} is registered as {message}");
    }
}

