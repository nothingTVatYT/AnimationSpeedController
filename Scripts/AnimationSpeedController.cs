using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationSpeedController : MonoBehaviour {

	public enum ControllerMode { Auto, Rigidbody, NavmeshAgent, CharacterController, None};
	public float animationSpeedMultiplier = 1f;
	public ControllerMode mode = ControllerMode.Auto;
	public float speed;
	public string animParameterSpeed = "speed";
	public string animParameterWalkRunRatio = "walkRunRatio";
	public string animParameterBackward = "backward";
	private Animator animator;
	private NavMeshAgent agent;
	private Rigidbody body;
	private CharacterController cc;
	private Vector3 v;
	private Vector3 vNorm;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		body = GetComponent<Rigidbody> ();
		cc = GetComponent<CharacterController> ();
		if (mode == ControllerMode.Auto) {
			if (agent != null)
				mode = ControllerMode.NavmeshAgent;
			else if (body != null)
				mode = ControllerMode.Rigidbody;
			else if (cc != null)
				mode = ControllerMode.CharacterController;
			else
				mode = ControllerMode.None;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (animator != null && mode != ControllerMode.None) {
			float relativeSpeed = 0f;
			float animationSpeed = 0f;
			float walkRunRatio = 0;
			float dotForward = 0;
			bool backward = false;
			switch (mode) {
			case ControllerMode.NavmeshAgent:
				relativeSpeed = agent.velocity.magnitude / speed;
				break;
			case ControllerMode.CharacterController:
				relativeSpeed = cc.velocity.magnitude / speed;
				dotForward = Vector3.Dot (cc.velocity, transform.forward);
				backward = dotForward < 0;
				break;
			case ControllerMode.Rigidbody:
				v = body.velocity;
				v.y = 0;
				vNorm = v;
				dotForward = Vector3.Dot (vNorm.normalized, transform.forward);
				relativeSpeed = v.magnitude / speed;
				backward = dotForward < 0;
				break;
			}
			animationSpeed = animationSpeedMultiplier * relativeSpeed;
			if (relativeSpeed > 1)
				walkRunRatio = relativeSpeed - 1f;
			if (!animParameterSpeed.Equals(""))
				animator.SetFloat (animParameterSpeed, animationSpeed);
			if (!animParameterWalkRunRatio.Equals(""))
				animator.SetFloat (animParameterWalkRunRatio, walkRunRatio);
			if (!animParameterBackward.Equals(""))
				animator.SetBool (animParameterBackward, backward);
		}
	}
}
