using System.Collections.Generic;

namespace NewGame4.Sessions
{
    public class SessionUnitModel : ISessionUnitModel
    {
        public string Id { get; set; }
        public string[] Keys { get; set; }
        public string UserIdOwner { get; set;}
        public Dictionary<string, CameraData> Cameras { get; }

        public SessionUnitModel()
        {
            Cameras = new Dictionary<string, CameraData>();
        }
    }
    
    public class CameraData
    {
        public byte[] Camera;
        public int X;
        public int Y;
    }
}