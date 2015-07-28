﻿using UnityEngine;
using System.Collections;

public class PlayerHotkeys : MonoBehaviour
{	
	public static bool isClickingATarget = false;

	void Update () {
		if (Input.GetKey (KeyCode.F) && Player.Instance.isFlying) {
			Broomstick.Instance.leave();
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			Menu.Instance.togglePanel("BagPanel");
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			Menu.Instance.togglePanel("CharacterPanel");
		}

		if (Player.Instance.target)
		{
			// unselect target, this should also check if is clicking over an NPC/Player
			if (Input.GetKey(KeyCode.Mouse0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
				if(isClickingATarget) {
					isClickingATarget = false;
				} else {
					Player.Instance.target = null;
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				PlayerCombat.Instance.spellCast(0);
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				PlayerCombat.Instance.spellCast(1);
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				PlayerCombat.Instance.spellCast(2);
			}
		}
	}
}
