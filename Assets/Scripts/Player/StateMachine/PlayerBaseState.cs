using System.Threading;

public abstract class PlayerBaseState
{
    // Class objects to call on
    protected PlayerStateFactory factory;
    protected PlayerStateMachine ctx;
    public bool animFinished;
    // Constructor
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        ctx = currentContext;
        factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();


    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();

        ctx.SetCurrentState(newState);
    }
}