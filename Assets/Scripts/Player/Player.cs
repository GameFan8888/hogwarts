﻿using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this

	public int health
	{
		get {return characterData.health;}
		set {
			//prevent negative health values
			if (value <  0) {
				value = 0;
			}

			characterData.health = value;

			if (photonView.isMine) {
				HealthBar.Instance.updateUI(characterData.health, characterData.maxHealth);
			}
		}
	}

	public CharacterData characterData;

	Animator anim;
	bool gotFirstUpdate = false;
	public TextMesh nick;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (!photonView.isMine) {
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
		} else {
			if (!gotFirstUpdate) {
				this.GetComponent<PhotonView>().RPC("setNick", PhotonTargets.Others, PhotonNetwork.player.name);
				HealthBar.Instance.updateUI(characterData.health, characterData.maxHealth);
			}

			//looks like player is falling
			if (transform.position.y < -100) {
				transform.position = GameObject.Find("SpawnPoints/FirstJoin").transform.position;
			}
		}
	}

	public bool isDead () {
		if (health < 1) {
			return true;
		}
		return false;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(anim.GetFloat("Speed"));
			stream.SendNext(anim.GetBool("Jumping"));
		}
		else
		{
			// Network player, receive data
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();

			if (anim) {
				anim.SetFloat("Speed", (float)stream.ReceiveNext());
				anim.SetBool("Jumping", (bool)stream.ReceiveNext());
			}

			
			if(gotFirstUpdate == false) {
				transform.position = this.correctPlayerPos;
				transform.rotation = this.correctPlayerRot;
				gotFirstUpdate = true;
			}
			
		}
	}

	[RPC]
	void setNick (string name) {
		nick.text = name;
	}

	private static Vector3 getVector3(string rString){
		string[] temp = rString.Substring(1, rString.Length-2).Split(',');
		float x = float.Parse(temp[0]);
		float y = float.Parse(temp[1]);
		float z = float.Parse(temp[2]);
		Vector3 rValue = new Vector3(x,y,z);
		return rValue;
	}
}
