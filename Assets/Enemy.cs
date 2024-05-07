using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用怪物脚本来驱动游戏
/// </summary>
public class Enemy : MonoBehaviour {

	private FSMSystem fsm;

	void Start () {
		InitFSM();//初始化
	}

	void InitFSM()
	{
		fsm = new FSMSystem();

		FSMState patrolState = new PatrolState(fsm);//巡逻状态对象
		patrolState.AddTransition(Transition.SeePlayer, StateID.Chase);

		FSMState chaseState = new ChaseState(fsm);
		chaseState.AddTransition(Transition.LostPlayer, StateID.Patrol);

		fsm.AddState(patrolState);
		fsm.AddState(chaseState);
	}

	void Update () {
		fsm.Update(this.gameObject);
	}
}
