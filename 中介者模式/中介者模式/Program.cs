var weChatChatRoom = new WeChatChatRoom();
var normalUserZ = new NormalUser("张三",weChatChatRoom);
var normalUserL = new NormalUser("李四",weChatChatRoom);
var normalUserW = new NormalUser("王五",weChatChatRoom);
var normalUserM = new NormalUser("王麻子",weChatChatRoom);

weChatChatRoom.Register(normalUserZ);
weChatChatRoom.Register(normalUserL);
weChatChatRoom.Register(normalUserW);
weChatChatRoom.Register(normalUserM);

normalUserZ.Send("李四","我喜欢你!");
normalUserZ.Send("所有人","我喜欢你!");

normalUserZ.Send("李四","脏话");

public interface IChatRoom
{
    public void Register(IUser user);
    public bool SendMessgae(string form, string to, string message);
}

public class WeChatChatRoom :IChatRoom
{
    private readonly Dictionary<string,IUser> _users = new();
    
    public void Register(IUser user)
    {
        if (!_users.ContainsKey(user._Name))
        {
            _users.Add(user._Name, user);
            Console.WriteLine($"{user._Name} 加入了聊天室");
        }
    }

    public bool SendMessgae(string from, string to, string message)
    {
        if (message.Contains("脏话"))
        {
            Console.WriteLine($"❌ {from} 发送了敏感词，消息被拦截");
            return false;
        }

        if (_users.TryGetValue(to, out IUser targetUser))
        {
            targetUser.Register(from, message);
            return true;
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


public abstract class IUser
{
    public string _Name { get; protected set; }
    protected readonly IChatRoom _chatRoom;

    protected IUser(string name, IChatRoom chatRoom)
    {
        _Name = name;
        _chatRoom = chatRoom;
    }

    public abstract void Send(string to, string message);
    public abstract void Register(string name, string message);
}

public class NormalUser : IUser
{
    public NormalUser(string name, IChatRoom chatRoom) :base(name,chatRoom){}
    //这个函数需要自己来调
    public override void Send(string to, string message)
    {
        _chatRoom.SendMessgae(_Name,to,message);
    }

    //这个函数需要中介者来调用
    public override void Register(string name, string message)
    {
        Console.WriteLine($"{_Name} is registered as {message}");
    }
}

