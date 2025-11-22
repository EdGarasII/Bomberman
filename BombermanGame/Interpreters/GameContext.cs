using System.Collections.Generic;

namespace BombermanGame.Interpreters
{
    // INTERPRETER PATTERN - Context for interpreter
    public class GameContext
    {
        private Dictionary<string, object> variables;
        
        public GameContext()
        {
            variables = new Dictionary<string, object>();
        }
        
        public void SetVariable(string name, object value)
        {
            variables[name] = value;
        }
        
        public object GetVariable(string name)
        {
            return variables.ContainsKey(name) ? variables[name] : null;
        }
        
        public bool HasVariable(string name)
        {
            return variables.ContainsKey(name);
        }
    }
}

