namespace Candy.Framework.Infrastructure
{
    public interface IBeginRequestTask
    {
        void Execute();

        int Order { get; }
    }
}