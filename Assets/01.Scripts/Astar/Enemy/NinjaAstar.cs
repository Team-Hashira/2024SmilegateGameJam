//using System.Collections;
//using System.Collections.Generic;
//using Gondr.Astar;
//using UnityEngine;

//public class NinjaAstar : AstarEnemy
//{
    
//    protected override void Awake()
//    {
//        base.Awake();
//        stateMachine.AddState("Idle", new EnemyIdleState(this, stateMachine, "Idle"));
//        //stateMachine.AddState("Move", new EnemyMoveState(this, stateMachine, "Move"));
//        stateMachine.AddState("Move", new EnemyAstarMoveState(this, stateMachine, "Move"));
//        stateMachine.AddState("Attack", new EnemyAttackState(this, stateMachine, "Attack"));
//    }

//    private void Start()
//    {
//        stateMachine.Initialize("Idle");
//    }
//}
