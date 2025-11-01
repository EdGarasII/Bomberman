using System;
using System.Collections.Generic;
using BombermanGame.Entities;

namespace BombermanGame.Network
{
    // Network message types for multiplayer communication
    public enum MessageType
    {
        // Client to Server
        JoinGame,
        PlayerMove,
        PlaceBomb,
        Disconnect,
        
        // Server to Client
        GameStateUpdate,
        PlayerJoined,
        PlayerLeft,
        GameStart,
        GameEnd,
        Error
    }
    
    // Base message class
    [Serializable]
    public class NetworkMessage
    {
        public MessageType Type { get; set; }
        public string? PlayerId { get; set; }
        public long Timestamp { get; set; }
        
        public NetworkMessage()
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            PlayerId = "";
        }
    }
    
    // Join game message
    [Serializable]
    public class JoinGameMessage : NetworkMessage
    {
        [System.Text.Json.Serialization.JsonPropertyName("PlayerName")]
        public string PlayerName { get; set; } = "";
        
        public JoinGameMessage()
        {
            Type = MessageType.JoinGame;
            PlayerName = "";
        }
    }
    
    // Player movement message
    [Serializable]
    public class PlayerMoveMessage : NetworkMessage
    {
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
        
        public PlayerMoveMessage()
        {
            Type = MessageType.PlayerMove;
        }
    }
    
    // Place bomb message
    [Serializable]
    public class PlaceBombMessage : NetworkMessage
    {
        public PlaceBombMessage()
        {
            Type = MessageType.PlaceBomb;
        }
    }
    
    // Game state update message (server -> clients)
    [Serializable]
    public class GameStateMessage : NetworkMessage
    {
        public List<PlayerData> Players { get; set; }
        public List<BombData> Bombs { get; set; }
        public List<ExplosionData> Explosions { get; set; }
        // Note: Board is not sent - each client generates its own level
        
        public GameStateMessage()
        {
            Type = MessageType.GameStateUpdate;
            Players = new List<PlayerData>();
            Bombs = new List<BombData>();
            Explosions = new List<ExplosionData>();
        }
    }
    
    // Player joined message
    [Serializable]
    public class PlayerJoinedMessage : NetworkMessage
    {
        [System.Text.Json.Serialization.JsonPropertyName("PlayerName")]
        public string PlayerName { get; set; } = "";
        
        [System.Text.Json.Serialization.JsonPropertyName("AssignedPlayerId")]
        public string AssignedPlayerId { get; set; } = "";
        
        public PlayerJoinedMessage()
        {
            Type = MessageType.PlayerJoined;
            PlayerName = "";
            AssignedPlayerId = "";
        }
    }
    
    // Game start message
    [Serializable]
    public class GameStartMessage : NetworkMessage
    {
        public List<string> PlayerIds { get; set; }
        
        public GameStartMessage()
        {
            Type = MessageType.GameStart;
            PlayerIds = new List<string>();
        }
    }
    
    // Serializable data classes for network transmission
    [Serializable]
    public class PlayerData
    {
        public string? PlayerId { get; set; }
        public string? PlayerName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int BombCount { get; set; }
        public int MaxBombs { get; set; }
        public int BombRange { get; set; }
        public bool IsAlive { get; set; }
    }
    
    [Serializable]
    public class BombData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Timer { get; set; }
        public int Range { get; set; }
        public string? OwnerId { get; set; }
    }
    
    [Serializable]
    public class ExplosionData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Duration { get; set; }
    }
    
    [Serializable]
    public class TileData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public TileType Type { get; set; }
    }
}

