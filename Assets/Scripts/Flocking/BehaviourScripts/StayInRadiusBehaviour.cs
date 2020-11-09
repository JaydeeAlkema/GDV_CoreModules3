using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Stay In Radius Behaviour makes sure the agents dont wander of into the abyss.
/// When nearing the edge of the invisible radius, the agents will start to move back towards the center.
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
	public Vector3 center = Vector3.zero;
	public float radius = 25f;

	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		Vector3 centerOffset = center - agent.transform.position;
		float t = centerOffset.magnitude / radius;

		if(t < 0.9f)
		{
			return Vector3.zero;
		}

		return centerOffset * t * t;
	}
}
