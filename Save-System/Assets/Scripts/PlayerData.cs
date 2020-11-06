using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int level;
    private int health;
    private float[] position;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public float[] Position
    {
        get { return position; }
        set { position = value; }
    }

    public PlayerData (Player player)
    {
        level = player.Level;
        health = player.Health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
