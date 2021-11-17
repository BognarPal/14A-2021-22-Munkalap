namespace munkalap.data
{
    public interface IEmployee: IIdentity
    {
        string Name { get; set; }
        bool IsDeleted { get; set; }
    }
}


