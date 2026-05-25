public interface IEnumerator
{
    bool MoveNext();
    
    object Current { get; }
    
    void Reset();
}

public interface IEnumeerable
{
    //获取迭代器
    IEnumerator GetEnumerator();
}