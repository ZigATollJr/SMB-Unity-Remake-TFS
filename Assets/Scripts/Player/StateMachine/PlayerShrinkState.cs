using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerShrinkState : PlayerBaseState
{
    readonly string shrinkClip = "Large_To_Small";
    Color tempColor;
    bool flipFlop = false;
    public PlayerShrinkState(PlayerStateMachine ctx, PlayerStateFactory factory) : base(ctx, factory) { }
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
        ctx.Anim.Play(shrinkClip);
        timer = 0.733f;
        ctx.changeSize = false;
    }
    public override void ExitState()
    {
        ctx.isLarge = false;
    }
    public override void FixedUpdateState()
    {
        
        tempColor.a = (flipFlop ? 1f : 0f);
        flipFlop = !flipFlop;
        Debug.Log(flipFlop + " guh " + tempColor + " guh " + ctx.Sr.color);
        ctx.sr.color = tempColor;
    }
}
