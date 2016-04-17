﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Menu : MonoBehaviour {

	public List<GameObject> Menus;
	public GameObject ItemTooltipPanel;

	public static string defaultLevel = "Hogwarts";
	public static string debugLevel = "Test";
	public const string GAME_VERSION = "0.01";

	public static Menu _instance;
	
	public static Menu Instance {
		get
		{
			return _instance;
		}
	}

	void Start () {
		_instance = this;
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		DontDestroyOnLoad(GameObject.Find("EventSystem"));
	}

	public void OnLevelWasLoaded(int level) {
		switch (level) {
		case 0: // Main
			showPanel("MainPanel");
			break;
		default:
            gameObject.AddComponent<QuestManager>();

            showPanel ("PlayerPanel");
			showPanel ("ChatPanel", false);
			showPanel ("TopMenu", false);
			showPanel ("MiniMap", false);
            gameObject.GetComponent<CanvasScaler> ().enabled = false;
			break;
		}
	}

	public void showPanelVoid (string name) {
		showPanel(name);
	}
	
	public GameObject showPanel (string name, bool hidePanels = true) {
		if (hidePanels) {
			hideAllPanels ();
		}
		
		GameObject panel = this.getPanel (name);
		panel.SetActive (true);
		
		return panel;
	}

	public GameObject getPanel (string name) {
		foreach (GameObject panel in Menus) {
			if (panel.name == name) {
				return panel;
			}
		}
		throw new UnityException ("UI Panel "+ name +" not found");
	}
	
	public void togglePanel (string name) {
		GameObject panel = getPanel (name);
		
		panel.SetActive (!panel.GetActive());
	}
	
	public void hideAllPanels() {
		foreach (GameObject panel in Menus) {
			panel.SetActive(false);
		}
	}

	/**
		Shows a tooltip near to item slot
		@param Vector3 pos Position to show the tooltip
		@param Item item Item which we want to show its information

		@return void
	 */
	public void showTooltip (Vector3 pos, Item item) {
		ItemTooltipPanel.SetActive (true);
		ItemTooltipPanel.GetComponent<RectTransform> ().SetAsLastSibling ();
		ItemTooltipPanel.transform.position = pos;
		
		ItemTooltipPanel.transform.FindChild("TitleLabel").GetComponent<Text>().text = item.name;
		ItemTooltipPanel.transform.FindChild("TextLabel").GetComponent<Text>().text = item.description;
	}
	
	/**
		hides the tooltip

		@return void
	*/
	public void hideTooltip () {
		ItemTooltipPanel.SetActive (false);
	}
}