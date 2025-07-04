using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    public LevelManager levelManager;
    public Room room;
    void Start()
    {
        
    }
    void Update()
    {
        foreach(Person p in levelManager.person)
        {
            if (p.room == room)
            {
                p.kill();
            }
        }
    }
}
