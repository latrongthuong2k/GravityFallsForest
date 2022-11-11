﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalScript : MonoBehaviour
{
	public Gamemodes Gamemode;
	public Speeds Speed;
	public Gravity gravity;
	public int State;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();
            movement.ChangeThroughPortal(Gamemode, Speed, gravity, State);
        }
        catch { }
    }
}

