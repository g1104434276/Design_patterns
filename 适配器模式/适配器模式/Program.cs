var oldRoundHead = new OldRoundHead();
var adapter = new HeadAdapter(oldRoundHead);
adapter.play("古典音乐");

// 目标接口:手机通用耳机接口
public interface IHeadPhone
{
    void play(string sound);
}

// 2. 适配者：老式圆孔耳机（不能修改源码）
public class OldRoundHead
{
    public void playMusic(string voice)
    {
        Console.WriteLine($"圆孔耳机播放：{voice}");
    }
}

// 3. 适配器：转接头，做接口转换
public class HeadAdapter : IHeadPhone
{
    private readonly OldRoundHead _oldRoundHead;
    public HeadAdapter(OldRoundHead oldRoundHead)
    {
        _oldRoundHead = oldRoundHead;
    }
    public void play(string sound)
    {
        _oldRoundHead.playMusic(sound);
    }
}