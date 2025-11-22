namespace BombermanGame.Interpreters
{
    // INTERPRETER PATTERN - Abstract expression
    public interface IExpression
    {
        void Interpret(GameContext context);
    }
}

