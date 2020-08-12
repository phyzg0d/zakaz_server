﻿using NewGame4.Commands;
using NewGame4.Utilities;

namespace NewGame4
{
    public class ServerContext
    {
        public CommandModel CommandModel;
        public Factory Factory;
        public BdConnection BdConnection;
    }
}