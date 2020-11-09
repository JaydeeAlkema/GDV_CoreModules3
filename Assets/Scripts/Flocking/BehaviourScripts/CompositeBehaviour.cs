using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The composite behaviour is made to be able to add multiple behaviours together and parse them through to the Flock.
/// This is done so extra beahviour methods can be very easily added, removed and edited.
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
	public FlockBehaviour[] behaviours;
	public float[] weights;

	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		// Handle Data Missmatch
		if(weights.Length != behaviours.Length)
		{
			Debug.LogError("Data missmatch in " + name, this);
			return Vector3.zero;
		}

		// Setup move
		Vector3 move = Vector3.zero;

		// Iterate through behaviours
		for(int i = 0; i < behaviours.Length; i++)
		{
			Vector3 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];

			if(partialMove != Vector3.zero)
			{
				if(partialMove.sqrMagnitude > weights[i] * weights[i])
				{
					partialMove.Normalize();
					partialMove *= weights[i];
				}

				move += partialMove;
			}
		}

		return move;
	}
}
