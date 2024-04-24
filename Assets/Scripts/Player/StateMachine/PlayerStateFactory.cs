public class PlayerStateFactory
{
    readonly PlayerStateMachine ctx;
    PlayerIdleState idleState;
    PlayerWalkState walkState;
    PlayerJumpState jumpState;
    PlayerSkidState skidState;
    PlayerGrowState growState;
    PlayerShrinkState shrinkState;

    public PlayerStateFactory(PlayerStateMachine ctx)
    {
        this.ctx = ctx;
        idleState = new PlayerIdleState(ctx, this);
        walkState = new PlayerWalkState(ctx, this);
        jumpState = new PlayerJumpState(ctx, this);
        skidState = new PlayerSkidState(ctx, this);
        growState = new PlayerGrowState(ctx, this);
        shrinkState = new PlayerShrinkState(ctx, this);
    }
    public PlayerBaseState Idle() 
    {
        return idleState;
    }
    public PlayerBaseState Walk()
    {
        return walkState;
    }
    public PlayerBaseState Jump()
    {
        return jumpState;
    }
    public PlayerSkidState Skid()
    {
        return skidState;
    }
    public PlayerGrowState Grow()
    {
        return growState;
    }
    public PlayerShrinkState Shrink()
    {
        return shrinkState;
    }
}
