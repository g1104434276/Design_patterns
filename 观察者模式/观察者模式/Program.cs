// ====================== 5. 客户端使用 ======================
// 创建公众号（主题）
var wechatOfficialAccount = new WechatOfficialAccount();
// 用户订阅
var weChatUser = new WeChatUser();
weChatUser.name = "微信用户";
var smsUser = new SmsUser();
smsUser.name = "短信用户";
var smsUser0 = new SmsUser();
smsUser0.name = "临时用户";
// 发布文章，自动通知所有订阅者
wechatOfficialAccount.Attach(weChatUser);
wechatOfficialAccount.Attach(smsUser);
wechatOfficialAccount.Attach(smsUser0);

wechatOfficialAccount.LatestArticle("第一次的测试通知");
// 李四取消订阅
wechatOfficialAccount.Detach(smsUser0);
// 再发布一篇文章，李四收不到了
wechatOfficialAccount.LatestArticle("第二次测试通知");
// ====================== 1. 抽象主题（被观察者） ======================
public interface ISubject
 {
     //订阅
     void Attach(IObserver observer);
     //取消订阅
     void Detach(IObserver observer);
     //通知所有观察者
     void Notify(string message);
 }
// ====================== 2. 抽象观察者 ======================
 public interface IObserver
 {
     //接收通知的方法
     void Update(string message);
 }
 // ====================== 3. 具体主题：微信公众号 ======================
 public class WechatOfficialAccount : ISubject
 {
     // 维护观察者列表
     private List<IObserver> _observers = new();
     // 公众号最新文章
     public void LatestArticle(string article)
     {
         Console.WriteLine($"公众号发布一篇新文章!文章标题{article}");
         Notify(article);// 自动通知所有订阅者
     }
     
     // 订阅
     public void Attach(IObserver observer)
     {
         _observers.Add(observer);
         Console.WriteLine("新用户订阅了公众号");
     }
     // 取消订阅
     public void Detach(IObserver observer)
     {
         _observers.Remove(observer);
         Console.WriteLine("新用户取消了订阅");
     }
     // 通知所有观察者
     public void Notify(string message)
     {
         foreach (var observer in _observers)
         {
             observer.Update(message);
         }
     }
 }
 // ====================== 4. 具体观察者：不同类型的用户 ======================
// 微信用户
 public class WeChatUser :IObserver
 {
     public string name;
     public void Update(string message)
     {
         Console.WriteLine($"通知{name}: {message}");
     }
 }
 // 短信用户
 public class SmsUser : IObserver
 {
     public string name;

     public void Update(string message)
     {
         Console.WriteLine($"通知{name}: {message}");
     }
 }
 
 