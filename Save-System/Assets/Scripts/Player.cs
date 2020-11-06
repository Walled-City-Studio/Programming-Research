using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int level = 4;
    [SerializeField]
    private int health = 40;

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

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.Level;
        health = data.Health;

        Vector3 position;
        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];
        transform.position = position;
    }
}
