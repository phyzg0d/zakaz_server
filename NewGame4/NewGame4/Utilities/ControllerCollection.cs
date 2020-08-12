﻿using System.Collections.Generic;

namespace NewGame4.Utilities
{
    public class ControllerCollection
    {
        private List<IController> _controllers = new List<IController>();

        public void Activate()
        {
            foreach (var controller in _controllers)
            {
                controller.Activate();
            }
        }
        
        public void Deactivate()
        {
            foreach (var controller in _controllers)
            {
                controller.Deactivate();
            }
        }

        public void Add(IController controller)
        {
            _controllers.Add(controller);
        }

        public void Clear()
        {
            _controllers.Clear();
        }
    }
}