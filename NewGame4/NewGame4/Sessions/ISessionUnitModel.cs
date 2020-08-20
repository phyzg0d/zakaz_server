using System.Collections.Generic;

namespace NewGame4.Sessions
{
    public interface ISessionUnitModel
    {
         string Id { get; set; }
         string[] Keys { get; set; }
         string UserIdOwner { get; set;}
         Dictionary<string, CameraData> Cameras { get; }
    }
}