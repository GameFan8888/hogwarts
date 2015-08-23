﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class SellPanel : MonoBehaviour {

	const int MAX_ITEMS = 3;

	public static Item _selectedItem;
	public static Item selectedItem {
		set {
			// check if player can buy it
			if (value == null || value.price > Player.Instance.money) {
				SellPanel.Instance.transform.FindChild("BuyButton").GetComponent<Button>().interactable = false;
				return;
			} else {
				_selectedItem = value;
				SellPanel.Instance.transform.FindChild("BuyButton").GetComponent<Button>().interactable = true;
			}
		}
		get {return _selectedItem;}
	}
	
	public AudioSource sound;

	public static SellPanel _instance;
	public static SellPanel Instance {
		get
		{
			return _instance;
		}
	}

	private List<Item> itemList = new List<Item>();
	
	void OnEnable () {
		_instance = this;

		// @ToDo: select items depending on NPC level and area
		foreach (Item itm in Service.db.Select<Item>("FROM item limit 0," + MAX_ITEMS)) {
			itemList.Add(itm);
		}

		ScrollableList scroll = gameObject.transform.FindChild("ContainerPanel/Panel").GetComponent<ScrollableList>();
		scroll.itemCount = MAX_ITEMS;
		scroll.load(delegate (GameObject newItem, int num) {

			Item tItem = itemList[num];
			Vector3 price = Util.formatMoney (tItem.price);

			newItem.SetActive(true);
			newItem.gameObject.transform.FindChild("Button/NameLabel").GetComponent<Text>().text = tItem.name;
			newItem.gameObject.transform.FindChild ("Button/GalleonLabel").GetComponent<Text> ().text = price.x.ToString();
			newItem.gameObject.transform.FindChild ("Button/SickleLabel").GetComponent<Text> ().text = price.y.ToString();
			newItem.gameObject.transform.FindChild ("Button/KnutLabel").GetComponent<Text> ().text = price.z.ToString();
			newItem.gameObject.transform.FindChild ("Icon").GetComponent<RawImage> ().texture = tItem.icon;

			newItem.gameObject.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(
				delegate {
				selectedItem = tItem;
			});
			
			return newItem;
		});

		updateMoney();
	}

	void OnDisable () {
		selectedItem = null;
	}

	public void updateMoney () {
		Vector3 money = Util.formatMoney (Player.Instance.money);
		transform.FindChild ("GalleonLabel").GetComponent<Text> ().text = money.x.ToString();
		transform.FindChild ("SickleLabel").GetComponent<Text> ().text = money.y.ToString();
		transform.FindChild ("KnutLabel").GetComponent<Text> ().text = money.z.ToString();
	}
	
	public void buyButton () {
		if (selectedItem == null || selectedItem.price > Player.Instance.money) {
			return;
		}

		sound.clip = SoundManager.get(SoundManager.Effect.Buy);
		sound.Play();

		Player.Instance.money -= selectedItem.price;
		Player.Instance.addItem (selectedItem.id);
		updateMoney ();
	}
}
