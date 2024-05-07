using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态
/// </summary>
public enum Transition
{
	NullTransition=0,
	SeePlayer,
	LostPlayer
}
/// <summary>
/// 行为ID
/// </summary>
public enum StateID
{
	NullStateID=0,
	Patrol,//巡逻
	Chase//追逐
}

/// <summary>
/// 基类
/// </summary>
public abstract class FSMState{

	protected StateID stateID;
	public StateID ID { get { return stateID; } }
	protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
	protected FSMSystem fsm;

	public FSMState(FSMSystem fsm)
	{
		this.fsm = fsm;
	}


	public void AddTransition(Transition trans,StateID id)
	{
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("不允许NullTransition");return;
		}
		if (id == StateID.NullStateID)
		{
			Debug.LogError("不允许NullStateID"); return;
		}
		if (map.ContainsKey(trans))
		{
			Debug.LogError("添加转换条件的时候，" + trans + "已经存在于map中");return;
		}
		map.Add(trans, id);
	}
	public void DeleteTransition(Transition trans)
	{
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("不允许NullTransition"); return;
		}
		if (map.ContainsKey(trans)==false)
		{
			Debug.LogError("删除转换条件的时候，" + trans + "不存在于map中"); return;
		}
		map.Remove(trans);
	}
	/// <summary>
	/// 获取状态
	/// </summary>
	/// <param name="trans"></param>
	/// <returns></returns>
	public StateID GetOutputState(Transition trans)
	{
		if (map.ContainsKey(trans))
		{
			return map[trans];
		}
		return StateID.NullStateID;
	}

	public virtual void DoBeforeEntering() { }
	public virtual void DoAfterLeaving() { }
	public abstract void Act(GameObject npc);
	public abstract void Reason(GameObject npc);
}
