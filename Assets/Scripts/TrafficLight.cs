using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* New TrafficLight script using our generic StateMachine script. */
public class TrafficLight : MonoBehaviour {
    /* The amount of time each state will last. */
	public float redStateTime = 5.0f;
    public float yellowStateTime = 2.0f;
    public float greenStateTime = 3.0f;

	/* Materials. */
	public MeshRenderer redMesh;
	public MeshRenderer yellowMesh;
	public MeshRenderer greenMesh;

	private LightState state = LightState.None;
    private float stateElapsedTime = 0.0f;

	private StateMachine stateMachine = new StateMachine();
	//private Dictionary<LightState, System.Action> states = new Dictionary<LightState, System.Action>();
	private void Start() {
		this.states.Add("Red", this.RedState);
		this.states.Add("Yellow", this.YellowState);
		this.states.Add("Green", this.GreenState);

		this.stateMachine.StateId = "Red";
		//this.TransitionState(LightState.Red);
	}

	void RedState() {
		if(stateElapsedTime >= this.redStateTime) {
			/*
			this.state = LightState.Green;
			this.stateElapsedTime = 0.0f; */
			this.TransitionState(LightState.Green);
			return;
		}
		/*
		Color color = this.redMesh.material.color;
		color.a = 1.0f;
		this.redMesh.material.color = color;

		color = yellowMesh.material.color;
		color.a = .3f;
		this.yellowMesh.material.color = color;
		color = greenMesh.material.color;
		color.a = .3f;
		this.greenMesh.material.color = color; */

		this.stateElapsedTime += Time.deltaTime;
	}

	void YellowState() {
		if(this.stateElapsedTime >= this.yellowStateTime) {
			this.TransitionState(LightState.Red);
			return;
		}

		/*
		Color color = this.yellowMesh.material.color;
		color.a = 1.0f;
		this.yellowMesh.material.color = color;

		color = redMesh.material.color;
		color.a = .3f;
		this.redMesh.material.color = color;
		color = greenMesh.material.color;
		color.a = .3f;
		this.greenMesh.material.color = color; */

		this.stateElapsedTime += Time.deltaTime;
	}

	void GreenState() {
		if (this.stateElapsedTime >= this.greenStateTime) {
			this.TransitionState(LightState.Yellow);
			return;
		}

		/*
		Color color = this.redMesh.material.color;
		color.a = 1.0f;
		this.redMesh.material.color = color;

		color = yellowMesh.material.color;
		color.a = .3f;
		this.yellowMesh.material.color = color;
		color = redMesh.material.color;
		color.a = .3f;
		this.redMesh.material.color = color; */

		this.stateElapsedTime += Time.deltaTime;
	}

	private void TransitionState(LightState next) {
		this.state = next;
		this.stateElapsedTime = 0.0f;

		Color color;

		switch (this.state) {
			case LightState.Red:
				color = this.redMesh.material.color;
				color.a = 1.0f;
				this.redMesh.material.color = color;

				color = yellowMesh.material.color;
				color.a = .3f;
				this.yellowMesh.material.color = color;
				color = greenMesh.material.color;
				color.a = .3f;
				this.greenMesh.material.color = color;
				break;
			case LightState.Yellow:
				color = this.yellowMesh.material.color;
				color.a = 1.0f;
				this.yellowMesh.material.color = color;

				color = redMesh.material.color;
				color.a = .3f;
				this.redMesh.material.color = color;
				color = greenMesh.material.color;
				color.a = .3f;
				this.greenMesh.material.color = color;
				break;
			case LightState.Green:
				color = this.redMesh.material.color;
				color.a = 1.0f;
				this.redMesh.material.color = color;

				color = yellowMesh.material.color;
				color.a = .3f;
				this.yellowMesh.material.color = color;
				color = redMesh.material.color;
				color.a = .3f;
				this.redMesh.material.color = color;
				break;

		}
	}

	void Update() {
		/* By using a Dictionary we can avoid this:
		switch (this.state) {
			case LightState.Red:
				this.RedState();
				break;
			case LightState.Yellow:
				this.YellowState();
				break;
			case LightState.Green:
				this.GreenState();
				break;
		} */
		System.Action currentState = this.states[this.state];
		currentState();
	}
}

public enum LightState {
	None = 0,
	Red = 1,
	Yellow = 2,
	Green = 3
}