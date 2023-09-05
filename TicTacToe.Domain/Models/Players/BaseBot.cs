namespace TicTacToe.Domain.Models.Players;
public abstract class BaseBot : BasePlayer
{
    protected BaseBot(string name) 
        : base(name)
    { }
}
