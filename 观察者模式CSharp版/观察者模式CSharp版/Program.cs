
var weChatOfficialAccount = new WeChatOfficialAccount();

var weChatUser = new WeChatUser();
weChatUser.userName = "微信王一";
var smsUser = new SmsUser();
smsUser.userName = "sms李二";

weChatOfficialAccount.userUpDate += weChatUser.Update;
weChatOfficialAccount.userUpDate += smsUser.Update;

weChatOfficialAccount.PublishArticle("公众号发布最新的消息");

public interface ISubject
{
    public void PublishArticle(string title);
}

public class WeChatOfficialAccount : ISubject
{
    public event Action<String> userUpDate;
    public void PublishArticle(string title)
    {
        Console.WriteLine("公众号发布消息!");
        userUpDate.Invoke(title);
    }
}

public interface IObserver
{
    void Update(string title);
}

public class WeChatUser : IObserver
{
    public string userName { get; set; }

    public void Update(String title)
    {
        Console.WriteLine($"用户{userName}阅读{title}");
    }
}

public class SmsUser :IObserver
{
    public string userName { get; set; }
    public void Update(string title)
    {
        Console.WriteLine($"用户{userName}阅读{title}");
    }
}

