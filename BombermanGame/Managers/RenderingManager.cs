using System;
using System.Drawing;
using System.Collections.Generic;
using BombermanGame.Entities;
using BombermanGame.Bridges;

namespace BombermanGame.Managers
{
    public class RenderingManager
    {
        private RenderingSystem renderingSystem;
        private const int TILE_SIZE = 32;
        
        public RenderingManager(bool useAdvancedRenderer = true)
        {
            IRenderer renderer = useAdvancedRenderer ? new AdvancedRenderer() : new BasicRenderer();
            renderingSystem = useAdvancedRenderer ? new EnhancedRenderingSystem(renderer) : new StandardRenderingSystem(renderer);
        }
        
        public void SetRenderingSystem(RenderingSystem system)
        {
            renderingSystem = system;
        }
        
        public void Render(Graphics g, Player player, List<Bomb> bombs, List<Explosion> explosions, Tile[,] board, List<PowerUp>? powerUps = null, Dictionary<string, Player>? remotePlayers = null, List<Enemy>? enemies = null)
        {
            if (renderingSystem != null)
            {
                renderingSystem.RenderGame(g, player, bombs, explosions, board, TILE_SIZE, powerUps, remotePlayers, enemies);
            }
        }
    }
}

