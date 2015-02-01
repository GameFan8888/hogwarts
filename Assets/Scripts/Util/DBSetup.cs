﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	This class is used to setup DB items
 **/

public class DBSetup : MonoBehaviour {

	public static void start () {
		insertItems ();
		insertTestItems ();
		insertNPCs ();
	}

	public static void insertItems () {
		ItemData item;

		item = new ItemData ();
		item.id = 1;
		item.name = "Queso de cabra";
		item.description = "Hecho en el mejor caserío muggle";
		item.price = 40;
		item.health = 20;
		item.type = Item.ItemType.Consumable;
		item.subType = Item.ItemSubType.Health;
		item.create ();
		
		
		item = new ItemData ();
		item.id = 2;
		item.name = "Colgante de Dermor";
		item.description = "No se me ocurre nada bueno";
		item.price = 2500;
		item.type = Item.ItemType.Armor;
		item.subType = Item.ItemSubType.Necklace;
		item.create ();

		item = new ItemData ();
		item.id = 3;
		item.name = "Varita de principiante";
		item.description = "La varita reglamentaria del centro";
		item.price = 40;
		item.type = Item.ItemType.Weapon;
		item.subType = Item.ItemSubType.Wand;
		item.create ();
	}

	public static void insertTestItems () {
		CharacterItem item = new CharacterItem ();
		item.id = 1;
		item.item = 1;
		item.character = 1;
		item.quantity = 2;
		item.create();
	}

	public static void insertNPCs () {

		NPCData npc;

		npc = new NPCData ();
		npc.id = 1;
		npc.name = "Mike Ghole";
		npc.level = 25;
		npc.type = NPCData.creatureType.Human;
		npc.subType = NPCData.creatureSubType.Seller;
		npc.create ();

		npc = new NPCData ();
		npc.id = 2;
		npc.name = "Araña del castillo";
		npc.level = 5;
		npc.type = NPCData.creatureType.Monster;
		npc.subType = NPCData.creatureSubType.Normal;
		npc.isAggresive = true;
		npc.create ();
	}
}
