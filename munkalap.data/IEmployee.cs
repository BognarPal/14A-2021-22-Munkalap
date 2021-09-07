namespace munkalap.data
{
    public interface IEmployee
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsDeleted { get; set; }
    }
}


