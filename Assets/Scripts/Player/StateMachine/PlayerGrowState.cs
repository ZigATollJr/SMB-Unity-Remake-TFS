using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerGrowState : PlayerBaseState
{
    readonly string growClip = "Small_To_Large";
    public PlayerGrowState(PlayerStateMachine ctx, PlayerStateFactory factory) : base(ctx, factory) { }
    private float timer = 0.733f;
    public override void CheckSwitchState()
    {
        if (timer <= 0) SwitchState(factory.Idle());
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        timer -= Time.deltaTime;
    }
    public override void EnterState()
    {
        ctx.Anim.Play(growClip);
        timer = 0.733f;
        ctx.changeSize = false;
    }
    public override void ExitState()
    {
        ctx.isLarge = true;
    }
    public override void FixedUpdateState()
    {

    }
}
