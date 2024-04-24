public class PlayerJumpState : PlayerBaseState
{
    readonly string smallJumpClip = "Small_Jump";
    readonly string largeJumpClip = "Large_Jump";
    public PlayerJumpState(PlayerStateMachine ctx, PlayerStateFactory factory) : base(ctx, factory) { }
    public override void CheckSwitchState()
    {
        if (ctx.isGrounded)
        {
            if (ctx.xVel == 0f) SwitchState(factory.Idle());
            else SwitchState(factory.Walk());
        }
    }
    public override void EnterState()
    {
        if (ctx.isLarge) ctx.Anim.Play(largeJumpClip);
        else ctx.Anim.Play(smallJumpClip);
    }
    public override void ExitState()
    {

    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void FixedUpdateState()
    {

    }
}
