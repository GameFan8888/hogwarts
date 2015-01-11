﻿public class CharacterItem {

	protected string TABLE_NAME = "inventory";

	public int id;
	public int item;
	public int character;
	public int quantity;
	public int slot = 0;

	public void save () {
		Menu.db.Update (TABLE_NAME, this);
	}
	
	public bool create () {
		return Menu.db.Insert (TABLE_NAME, this);
	}
}
