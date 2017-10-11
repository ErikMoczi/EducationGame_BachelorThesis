using Bachelor.Managers.Transition.Base;

namespace Bachelor.Managers.Transition
{
    public interface ITransitionElement
    {
        ITransitionDetails TransitionDetails { get; }
        string Name { get; }
    }
}