using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FlockAgent : MonoBehaviour
{
	SphereCollider agentCollider;
	public SphereCollider AgentCollider { get { return agentCollider; } }

	private void Start()
	{
		agentCollider = GetComponent<SphereCollider>();
	}

	public void Move(Vector3 velocity)
	{
		transform.forward = velocity;
		transform.position += velocity * Time.deltaTime;
	}
}
