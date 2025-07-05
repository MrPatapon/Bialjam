using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    public LevelManager levelManager;
    public Room room;
    public bool preview=false;
    public GameObject gui;
    public GameObject gui2;
    void Start()
    {
        room.killer = this;
    }
    void Update()
    {
        bool ac = false;
        foreach (Person p in levelManager.person)
        {
            if (p.room == room)
            {
                if (p.live)
                {
                    ac = true;
                }
            }
        }
        
        if (preview && ac)
        {
            if (Input.GetMouseButton(0))
            {
                foreach (Person p in levelManager.person)
                {
                    if (p.room == room)
                    {
                        p.kill();
                    }
                }

            }
                
        }
        if (preview)
        {
            if (ac)
            {
                gui.active = true;
                gui2.active = false;
            }
            else
            {
                gui.active = false;
                gui2.active = true;
            }
        }
        else
        {
            gui.active = false;
            gui2.active = false;
        }
        
    }
}
