namespace NewGame4.Users
{
    public interface IUserUnitModel
    {
        string Id { get;  }
        string Name { get; }
        string SecondName { get; }
        string Email { get; }
        string Password { get; }
        string Session { get; }
        bool IsNew { get; set; }
    }
}