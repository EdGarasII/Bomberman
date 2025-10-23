using System;
using BombermanGame.Entities;

namespace BombermanGame.Commands
{
    // COMMAND PATTERN - Encapsulate requests as objects
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    
    // Move command - encapsulates player movement
    public class MoveCommand : ICommand
    {
        private Player player;
        private int deltaX;
        private int deltaY;
        private int previousX;
        private int previousY;
        
        public MoveCommand(Player player, int deltaX, int deltaY)
        {
            this.player = player;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
        }
        
        public void Execute()
        {
            previousX = player.X;
            previousY = player.Y;
            player.Move(deltaX, deltaY);
        }
        
        public void Undo()
        {
            player.X = previousX;
            player.Y = previousY;
        }
    }
    
    // Place bomb command - encapsulates bomb placement
    public class PlaceBombCommand : ICommand
    {
        private Player player;
        private bool wasExecuted;
        
        public PlaceBombCommand(Player player)
        {
            this.player = player;
        }
        
        public void Execute()
        {
            if (player.BombCount > 0)
            {
                player.PlaceBomb();
                wasExecuted = true;
            }
        }
        
        public void Undo()
        {
            if (wasExecuted)
            {
                player.BombCount++;
                wasExecuted = false;
            }
        }
    }
}

