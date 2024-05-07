using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机系统管理类  集中管理行为状态
/// </summary>
public class FSMSystem  {
	private Dictionary<StateID, FSMState> states = new Dictionary<StateID, FSMState>();//库   有库就有增删改查  其实这个是负责管理行为状态对象的

	/// <summary>
	/// 行为ID
	/// </summary>
	private StateID currentStateID;
	/// <summary>
	/// 行为状态
	/// </summary>
	private FSMState currentState;

	public void Update(GameObject npc)
	{
		currentState.Act(npc);
		currentState.Reason(npc);
	}

	public void AddState(FSMState s)
	{
		if (s == null)
		{
			Debug.LogError("FSMState不能为空");return;
		}
		if (currentState == null)
		{
			currentState = s;
			currentStateID = s.ID;
		}
		if (states.ContainsKey(s.ID))
		{
			Debug.LogError("状态" + s.ID + "已经存在，无法重复添加");return;
		}
		states.Add(s.ID, s);
	}
	public void DeleteState(StateID id)
	{
		if (id == StateID.NullStateID)
		{
			Debug.LogError("无法删除空状态");return;
		}
		if (states.ContainsKey(id) == false)
		{
			Debug.LogError("无法删除不存在的状态：" + id);return;
		}
		states.Remove(id);
	}
	/// <summary>
	/// 转换状态
	/// </summary>
	/// <param name="trans"></param>
	public void PerformTransition(Transition trans)
	{
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("无法执行空的转换条件");return;
		}
		StateID id= currentState.GetOutputState(trans);
		if (id == StateID.NullStateID)
		{
			Debug.LogWarning("当前状态" + currentStateID + "无法根据转换条件" + trans + "发生转换");return;
		}
		if (states.ContainsKey(id) == false)
		{
			Debug.LogError("在状态机里面不存在状态" + id + "，无法进行状态转换！");return;
		}
		FSMState state = states[id];
		currentState.DoAfterLeaving();
		currentState = state;
		currentStateID = id;
		currentState.DoBeforeEntering();
	}
} 
