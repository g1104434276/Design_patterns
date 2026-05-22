

var folder = new Folder("我的文档");
folder.Add(new File("简历",1000));
folder.Add(new File("照片",1000));

Folder projectFolder = new Folder("项目代码");
projectFolder.Add(new File("Program.cs", 1000));
projectFolder.Add(new File("README.md", 1000));

Folder srcFolder = new Folder("src");
srcFolder.Add(new File("Utils.cs", 1000));
projectFolder.Add(srcFolder);

var folderALL = new Folder("集合");
folderALL.Add(folder);
folderALL.Add(projectFolder);
folderALL.Add(srcFolder);

Console.WriteLine(folderALL.GetSize());
folderALL.DisPlay();


// ====================== 1. 抽象组件：所有节点的统一接口 ======================
public abstract class FileSystemNode
{
    public string _name { get; protected set; }
    
    // 统一方法：所有节点都能显示自己的信息
    public abstract void DisPlay(int depth = 0);
    // 统一方法：所有节点都能计算自己的大小
    public abstract long GetSize();
    // 组合节点特有的方法：添加/删除子节点（透明式写法，叶子节点空实现或抛异常）
    public virtual void Add(FileSystemNode node)
    {
        throw new NotSupportedException("叶子节点不能添加子节点");
    }


    public virtual void Remove(FileSystemNode node)
    {
        throw new NotSupportedException("叶子节点不能删除子节点");
    }
}

// ====================== 2. 叶子组件：文件（没有子节点） ======================
public class File : FileSystemNode
{
    public readonly long _size; // 文件大小（字节）

    public File(string name, long size)
    {
        _size = size;
        _name = name;
    }
    // 显示文件信息
    public override void DisPlay(int depth = 0)
    {
        Console.WriteLine($"{new String(' ', depth * 2)}名称 :{_name},大小 : {_size}字节");
    }
    // 返回文件大小
    public override long GetSize()
    {
        return _size;
    }
}

// ====================== 3. 组合组件：文件夹（包含子节点） ======================
public class Folder : FileSystemNode
{
    // 🔴 核心：持有子节点列表，可以是文件也可以是其他文件夹
    private readonly List<FileSystemNode> _children = new List<FileSystemNode>();
    public Folder(String name)
    {
        _name = name;
    }
    // 实现添加子节点
    public override void Add(FileSystemNode node)
    {
        _children.Add(node);
    }
    // 实现删除子节点
    public override void Remove(FileSystemNode node)
    {
        _children.Remove(node);
    }
    
    
    // 递归显示文件夹和所有子节点
    public override void DisPlay(int depth = 0)
    {
        Console.WriteLine($"{new string(' ',depth * 2)},名称:{_name}");
        // 递归显示所有子节点
        foreach (var child in _children)
        {
            child.DisPlay(depth + 1);
        }
    }

    // 递归计算文件夹总大小
    public override long GetSize()
    {
        long totalSize = 0;
        // 递归累加所有子节点的大小
        foreach (var child in _children)
        {
            totalSize += child.GetSize();
        }
        return totalSize;
    }
}