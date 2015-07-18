﻿using UnityEngine;
using System.Collections;

public class Broomstick : MonoBehaviour
{
	public Transform playerPos;
	public static Broomstick Instance;

	private float speed = 0.5f;
	private Player driver;
	private bool inUse {
		get {
			if (driver == null) {
				return false;
			}
			return true;
		}
	}

	void FixedUpdate ()
	{
		if (!inUse) {
			return;
		}
		Vector3 newPos;

		if (Input.GetAxis("Vertical") != 0)
		{
			if (Input.GetKey (KeyCode.Mouse0) && Input.GetKey (KeyCode.Mouse1)) {
				newPos = Camera.main.transform.forward;
			} else {
				newPos = Vector3.back;
			}
			transform.Translate(newPos * speed * Input.GetAxis("Vertical"));
		}
		if (Input.GetAxis("Horizontal") != 0)
		{
			transform.Translate(Vector3.left * speed * Input.GetAxis("Horizontal"));
		}
	}


	// When a player collides, he joins the bromstick
	public void OnCollisionEnter (Collision collision)
	{
		Player player = collision.transform.GetComponent<Player>();

		if (inUse || player == null || !player.isMine) {
			return;
		}

		driver = player;
		driver.transform.position = playerPos.position;
		driver.transform.eulerAngles = new Vector3(0, 180f, 0);

		driver.transform.SetParent(transform);
		driver.freeze();
		driver.isFlying = true;

		Instance = this;
	}

	public void leave ()
	{
		driver.transform.SetParent(null);
		driver.unfreeze();
		driver.isFlying = false;
		driver = null;
	}
}
