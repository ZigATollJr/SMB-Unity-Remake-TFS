using UnityEngine;
public class PlayerWalkState : PlayerBaseState
{
    readonly string smallWalkClip = "Small_Walk";
    readonly string largeWalkClip = "Large_Walk";
    public PlayerWalkState(PlayerStateMachine ctx, PlayerStateFactory factory) : base(ctx, factory) { }
    public override void CheckSwitchState()
    {
        if (ctx.isSkidding) SwitchState(factory.Skid());
        if (ctx.isJumping) SwitchState(factory.Jump());
        if (Mathf.Abs(ctx.xVel) == 0f) SwitchState(factory.Idle());


    }
    public override void EnterState()
    {
        Debug.Log("Entered Walk State");
        if (ctx.isLarge) ctx.Anim.Play(largeWalkClip);
        else ctx.Anim.Play(smallWalkClip);

        
    }
    public override void ExitState()
    {
        ctx.Anim.speed = 1f;
    }
    public override void UpdateState()
    {
        if (Mathf.Abs(ctx.xVel) > 6f) ctx.Anim.speed = 2f;
        else if (Mathf.Abs(ctx.xVel) > 4f) ctx.Anim.speed = 1.4f;
        else ctx.Anim.speed = 1f;
        CheckSwitchState();
    }
    public override void FixedUpdateState()
    {

    }
}
