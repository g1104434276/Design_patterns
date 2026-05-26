BookList bookList = new BookList();
bookList.Add(new Book("西游记"));
bookList.Add(new Book("红楼梦"));
bookList.Add(new Book("三国演义"));

var enumerator = bookList.GetEnumerator();
while (enumerator.MoveNext())
{
    Book book = (Book)enumerator.Current;
    Console.WriteLine(book._Name);
}


// ====================== 1. 抽象迭代器接口 ======================
public interface IEnumerator
{
    // 移动到下一个元素，返回是否还有更多元素
    bool MoveNext();

    // object Current();
    // 获取当前元素
    object Current { get; }

    // 重置迭代器到第一个元素之前
    void Reset();
}

// ====================== 2. 具体迭代器：书籍列表迭代器 ======================
public class BookListEnumerator :IEnumerator
{
    private readonly BookList _bookList;
    private int _currentIndex = -1; // 初始位置在第一个元素之前

    public BookListEnumerator(BookList bookList)
    {
        _bookList = bookList;
    }
    
    // 移动到下一个元素
    public bool MoveNext()
    {
        _currentIndex++;
        return _currentIndex < _bookList.Count;
        
    }

    // public object Current()
    // {
    //     return _bookList.GetBook(_currentIndex);
    // }
    // 获取当前元素
    public object Current
    {
        get
        {
            // 防护：索引无效时直接抛异常（标准迭代器行为）
            if (_currentIndex < 0 || _currentIndex >= _bookList.Count)
                throw new InvalidOperationException("迭代器越界");
            
            return _bookList.GetBook(_currentIndex);
        }
    }
    // 重置迭代器
    public void Reset()
    {
        _currentIndex = -1;
    }
}

// ====================== 3. 抽象集合接口 ======================
public interface IEnumeerable
{
    // 获取迭代器
    IEnumerator GetEnumerator();
}

// 书籍类
public class Book 
{
    public string _Name { get; set; }
    public Book (string name) => _Name = name;
}

// ====================== 4. 具体集合：书籍列表 ======================
public class BookList :IEnumeerable
{
    private readonly List<Book> _books = new ();
    
    //添加书籍
    public void Add(Book book) => _books.Add(book);
    // 获取书籍数量
    public int Count => _books.Count;

    // 获取指定位置的书籍
    public Book GetBook(int index) => _books[index];
    
    // 返回对应的具体迭代器
    public IEnumerator GetEnumerator()
    {
        return new BookListEnumerator(this);
    }
}

