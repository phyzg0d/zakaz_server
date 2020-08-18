namespace NewGame4.Sessions
{
    public class SessionUnitModel : ISessionUnitModel
    {
        public string id { get; set; }
        public string name { get; set;}
        public string second_name { get; set;}
        public string email { get; set;}
        public string password { get; set;}
        public string session { get; set;}
        public bool is_new { get; set; }
    }
}