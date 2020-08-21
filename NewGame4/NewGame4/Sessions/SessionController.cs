using System;
using System.Timers;
using NewGame4.Utilities;

namespace NewGame4.Sessions
{
    public class SessionController : IController
    {
        private readonly ServerContext _context;
        private readonly SessionModel _model;
        private Timer aTimer;
        

        public SessionController(ServerContext context, SessionModel model)
        {
            _context = context;
            _model = model;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _model.Serialize(_context);
        }

        public void Activate()
        {
            _model.Deserialize(_context);
            aTimer = new Timer(6000);
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