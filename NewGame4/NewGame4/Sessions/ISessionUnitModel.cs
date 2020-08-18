namespace NewGame4.Sessions
{
    public interface ISessionUnitModel
    {
        string id { get;  }
        string name { get; }
        string second_name { get; }
        string email { get; }
        string password { get; }
        string session { get; }
        bool is_new { get; set; }
    }
}