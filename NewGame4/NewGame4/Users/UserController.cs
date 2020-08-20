using System;
using System.Timers;
using NewGame4.Utilities;

namespace NewGame4.Users
{
    public class UserController : IController
    {
        private readonly ServerContext _context;
        private readonly UserModel _model;
        private Timer aTimer;
        

        public UserController(ServerContext context, UserModel model)
        {
            _context = context;
            _model = model;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _model.Serialize(_context);
            Console.WriteLine("user_OnEndTimer");
        }

        public void Activate()
        {
            _model.Deserialize(_context);
            aTimer = new Timer(12000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void Deactivate()
        {
            aTimer.Elapsed -= OnTimedEvent;
            aTimer.Enabled = false;
            aTimer = null;
        }
    }
}