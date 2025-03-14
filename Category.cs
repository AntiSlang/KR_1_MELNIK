namespace KR_1_MELNIK;

public class Category
{
    public int id { get; set; }
    public bool type { get; set; }
    public string name { get; set; }
    
    public Category(int _id, bool _type, string _name) => (id, type, name) = (_id, _type, _name);
    
    public Category() => (id, type, name) = (new Random().Next(100000, 1000000), true, "Category");

    public override string ToString()
    {
        return name;
    }
    public void Accept(IExportVisitor visitor)
    {
        visitor.Visit(this);
    }
}