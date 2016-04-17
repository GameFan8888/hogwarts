﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Task
{
    protected string TABLE_NAME = "tasks";

    public enum ActionType
    {
        Kill = 1,
        Talk = 2,
        GetItem = 3,
        RemoveItem = 4,
        Visit = 5
    }

    public enum ActorType
    {
        NPC = 1,
        Place = 2,
        Item = 3
    }

    public int id;
    public int quest;
    public int taskId; // iBoxDB forces to have a unique id
    public int _type;
    public int _action;
    public ActorType type
    {
        get
        {
            return (ActorType)_type;
        }
        set
        {
            _type = (int)value;
        }
    }
    public ActionType action
    {
        get
        {
            return (ActionType)_action;
        }
        set
        {
            _action = (int)value;
        }
    }

    public int quantity = 0;
    public int currentQuantity = 0;
    public bool isCompleted = false;
    public TaskUI ui;

    public string buildName ()
    {
        string name = "";

        switch (action) {
            case ActionType.Kill:
                name += "Mata";
                break;
            case ActionType.Visit:
                name += "Ve a";
                break;
            case ActionType.Talk:
                name += "Habla con";
                break;
            default:
                break;
        }
        name += " ";

        switch (type) {
            case ActorType.NPC:
                var data = NPC.get(id);
                name += data.name;
                break;
        }
        name += " ";

        if (quantity > 1) {
            name += currentQuantity + "/" + quantity;
        }
        return name;
    }

    public void save()
    {
        Service.db.Update(TABLE_NAME, this);
    }

    public bool create()
    {
        return Service.db.Insert(TABLE_NAME, this);
    }
}