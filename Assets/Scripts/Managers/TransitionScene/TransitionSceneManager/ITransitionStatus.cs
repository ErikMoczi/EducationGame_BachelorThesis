namespace Bachelor.Managers.Transition
{
    public interface ITransitionStatus
    {
        bool InTransition { get; }
        float TransitionPercent { get; }
        bool TransitionFading { get; }
    }
}