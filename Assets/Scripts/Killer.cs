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
    public GameObject obj_power;
    public float time2power = 5.0f;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        room.killer = this;
    }
    void Update()
    {
        time2power -= Time.deltaTime;
        obj_power.active = time2power < 0.0;
        if (time2power < 0.0)
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
                            if(p.last_killer != this)
                            {
                                time2power = 5.0f;
                                p.kill(1);
                            }

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
        else
        {
            gui.active = false;
            gui2.active = false;
        }
        
        
    }
}
