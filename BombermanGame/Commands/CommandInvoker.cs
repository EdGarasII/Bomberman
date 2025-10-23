using System;
using System.Collections.Generic;

namespace BombermanGame.Commands
{
    // COMMAND PATTERN - Command Invoker for undo/redo functionality
    public class CommandInvoker
    {
        private Stack<ICommand> commandHistory;
        private Stack<ICommand> undoHistory;
        
        public CommandInvoker()
        {
            commandHistory = new Stack<ICommand>();
            undoHistory = new Stack<ICommand>();
        }
        
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandHistory.Push(command);
            undoHistory.Clear();
        }
        
        public void Undo()
        {
            if (commandHistory.Count > 0)
            {
                ICommand command = commandHistory.Pop();
                command.Undo();
                undoHistory.Push(command);
            }
        }
        
        public void Redo()
        {
            if (undoHistory.Count > 0)
            {
                ICommand command = undoHistory.Pop();
                command.Execute();
                commandHistory.Push(command);
            }
        }
    }
}

