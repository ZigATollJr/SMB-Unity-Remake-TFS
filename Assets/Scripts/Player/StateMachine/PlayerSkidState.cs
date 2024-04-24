using System;
public class PlayerSkidState : PlayerBaseState
{
    readonly string smallSkidClip = "Small_Skid";
    readonly string largeSkidClip = "Large_Skid";
    public PlayerSkidState(PlayerStateMachine ctx, PlayerStateFactory factory) : base(ctx, factory) { }
    public override void CheckSwitchState()
    {
        if (ctx.xVel == 0f) SwitchState(factory.Idle());
        if (!ctx.isSkidding) SwitchState(factory.Walk());
        if (ctx.isJumping) SwitchState(factory.Jump());
    }
    public override void EnterState()
    {
        if (ctx.isLarge) ctx.Anim.Play(largeSkidClip);
        else ctx.Anim.Play(smallSkidClip);
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
