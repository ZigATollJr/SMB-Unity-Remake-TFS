
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    readonly string smallIdleClip = "Small_Idle";
    readonly string largeIdleClip = "Large_Idle";
    public PlayerIdleState(PlayerStateMachine ctx, PlayerStateFactory factory) : base(ctx, factory) { }
    public override void CheckSwitchState()
    {
        if (Mathf.Abs(ctx.xVel) > 0f) SwitchState(factory.Walk());
        if (ctx.isJumping) SwitchState(factory.Jump());
        if (ctx.changeSize && !ctx.isLarge) SwitchState(factory.Grow());
        if (ctx.changeSize && ctx.isLarge) SwitchState(factory.Shrink());
    }
    public override void EnterState()
    {
        Debug.Log("Entered Idle State");
        if (ctx.isLarge) ctx.Anim.Play(largeIdleClip);
        else ctx.Anim.Play(smallIdleClip);
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
