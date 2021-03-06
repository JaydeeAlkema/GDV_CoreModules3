﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Avoidance behaviour avoids the agent accordingly from a different Flock of Agents.
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		// if no neighbours are close, do nothing. (not adjustment)
		if(context.Count == 0)
		{
			return Vector3.zero;
		}

		// Add all points together and average.
		Vector3 avoidanceMove = Vector3.zero;
		int nAvoid = 0;
		foreach(Transform item in context)
		{
			if(Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
			{
				nAvoid++;
				avoidanceMove += agent.transform.position - item.position;
			}
		}
		if(nAvoid > 0)
		{
			avoidanceMove /= nAvoid;
		}

		return avoidanceMove;
	}
}
