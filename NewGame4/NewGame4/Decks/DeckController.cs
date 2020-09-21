using System;
using System.Timers;
using NewGame4.Utilities;

namespace NewGame4.Decks
{
    public class DeckController : IController
    {
        private readonly ServerContext _context;
        private readonly DeckModel _model;
        private Timer _aTimer;


        public DeckController(ServerContext context, DeckModel model)
        {
            _context = context;
            _model = model;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
        }

        public void Activate()
        {
            _aTimer = new Timer(6000);
            _aTimer.Elapsed += OnTimedEvent;
            _aTimer.AutoReset = true;
            _aTimer.Enabled = true;
        }

        public void Deactivate()
        {
            _aTimer.Elapsed -= OnTimedEvent;
            _aTimer.Enabled = false;
            _aTimer = null;
        }
    }
}