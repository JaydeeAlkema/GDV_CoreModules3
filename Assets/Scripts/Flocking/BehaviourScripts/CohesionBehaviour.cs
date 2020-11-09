using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Cohesion behaviour makes it so the Agents move together to the relative center of their Flock.
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		// if no neighbours are close, do nothing. (not adjustment)
		if(context.Count == 0)
		{
			return Vector3.zero;
		}

		// Add all points together and average.
		Vector3 cohesionMove = Vector3.zero;
		foreach(Transform item in context)
		{
			cohesionMove += item.position;
		}
		cohesionMove /= context.Count;

		// Create offset from agent position.
		cohesionMove -= agent.transform.position;

		return cohesionMove;
	}
}
