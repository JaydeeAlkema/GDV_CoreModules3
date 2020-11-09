using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Steered Cohesion is practically the same as the Cohesion behaviour. With the addition of SmoothDamp.
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FlockBehaviour
{
	Vector3 currentVelocity;
	public float agentSmoothTime = 0.5f;

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
		cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);

		return cohesionMove;
	}
}
