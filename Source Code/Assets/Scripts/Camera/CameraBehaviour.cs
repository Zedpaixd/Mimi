﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public Transform target;
	public float dampingTime = 0.25f;
	public float PPU = 64f; //Pixels per unit

	private Vector3 velocity;

	private Vector3 proxyPosition;

	private void LateUpdate() {

		proxyPosition = Vector3.SmoothDamp(proxyPosition, target.position, ref velocity, dampingTime);
		transform.position = new Vector3(
		Mathf.Round(proxyPosition.x * PPU)/PPU,
		Mathf.Round(proxyPosition.y * PPU)/PPU,
		-10f

		);
	}
}
