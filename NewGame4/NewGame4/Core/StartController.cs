using NewGame4.Commands;
using NewGame4.Utilities;

namespace NewGame4.Core
{
    public class StartController
    {
        private readonly ServerContext _context;
        private ControllerCollection _controllerCollection = new ControllerCollection();

        public StartController(ServerContext context)
        {
            _context = context;
            
            _context.BdConnection = new BdConnection();
            _context.BdConnection.Connect();
            
            CreateModels();
            CreateControllers();
            _controllerCollection.Activate();
        }

        private void CreateModels()
        {
            _context.CommandModel = new CommandModel();
            _context.Factory = new Factory(_context);
        }

        private void CreateControllers()
        {
            _controllerCollection.Add(new CommandController(_context, _context.CommandModel));
        }
    }
}