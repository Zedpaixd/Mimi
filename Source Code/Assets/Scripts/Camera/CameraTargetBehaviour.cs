using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetBehaviour : MonoBehaviour {

	public float distance = 4;

	private PlayerMovement player;

	void Start() {
		player = PlayerMovement.instance;
	}

	void Update() {

		if (player.GetDirection() != 0)
		{
		Vector3 localPos = new Vector3(
			player.GetDirection() < 0 ? -1f : 1.6f,
			0f,
			0f
		);
		transform.localPosition = localPos * distance;
		}

	}

}
