/// This code is writen using the Unity Flock Algorithm Guide: https://www.youtube.com/watch?v=i_XinoVBqt8
/// I Added my own touch to the code, cleaned it up, and optimilised where possible.

using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
	#region Private
	[SerializeField] private FlockAgent agentPrefab;
	[SerializeField] private FlockBehaviour behaviour;
	[Space]
	[SerializeField] private const float AgentDensity = 0.08f;
	[SerializeField] [Range(10, 500)] private int startingCount = 250;
	[SerializeField] [Range(1f, 100f)] private float driveFactor = 10f;
	[SerializeField] [Range(1f, 100f)] private float maxSpeed = 5f;
	[SerializeField] [Range(1f, 10f)] private float neighbourRadius = 1.5f;
	[SerializeField] [Range(0f, 1f)] private float avoidanceRadiusMultiplier = 0.5f;
	[Space]
	[SerializeField] private List<FlockAgent> agents = new List<FlockAgent>();

	private float squareMaxSpeed;
	private float squareNeighbourRadius;
	private float squareAvoidanceRadius;
	#endregion

	#region Properties
	public float SquareAvoidanceRadius { get => squareAvoidanceRadius; set => squareAvoidanceRadius = value; }
	#endregion

	// Start is called before the first frame update
	void Start()
	{
		// Utility.
		squareMaxSpeed = maxSpeed * maxSpeed;
		squareNeighbourRadius = neighbourRadius * neighbourRadius;
		squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

		InstantiateFlockAgents();
	}

	/// <summary>
	/// Instantiate all the Flock Agents within the scene.
	/// </summary>
	private void InstantiateFlockAgents()
	{
		for(int i = 0; i < startingCount; i++)
		{
			FlockAgent newAgent = Instantiate(
			agentPrefab,
			Random.insideUnitSphere * startingCount * AgentDensity,
			Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
			transform
			);
			newAgent.name = "Agent " + i;

			// This gives each agent their own color. Might be a bit of an eyesore tho...
			Color agentColor = Random.ColorHSV(0f, 0.75f);
			newAgent.GetComponent<MeshRenderer>().material.SetColor("_Color", agentColor);
			newAgent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", agentColor * 2f);

			agents.Add(newAgent);
		}
	}

	void Update()
	{
		foreach(FlockAgent agent in agents)
		{
			List<Transform> context = GetNearbyObjects(agent);

			//DEMO MODE ONLY. VERY HEAVY PERFORMANCE-WISE


			Vector3 move = behaviour.CalculateMove(agent, context, this);
			move *= driveFactor;
			if(move.sqrMagnitude > squareMaxSpeed)
			{
				move = move.normalized * maxSpeed;
			}
			agent.Move(move);
		}
	}

	/// <summary>
	/// Return a list with all the neighbouring agents.
	/// </summary>
	/// <param name="agent"> From where we check for neighbouring Agents. </param>
	/// <returns> A List of all the neighbouring Agents. "</returns>
	List<Transform> GetNearbyObjects(FlockAgent agent)
	{
		List<Transform> context = new List<Transform>();
		Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
		// This gives it that nice glowing colors.

		foreach(Collider c in contextColliders)
		{
			if(c != agent.AgentCollider)
			{
				context.Add(c.transform);
			}
		}

		return context;
	}
}
