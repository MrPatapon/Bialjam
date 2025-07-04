using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public Room room;
    public float maxtimeleft = 0.5f;
    public float timeleft = 0.5f;
    public int my_dir = 0;
    public bool inside = true;
    public LevelManager levelmanager;
    public bool live = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inside)
        {
            timeleft -= Time.deltaTime;
            if (timeleft < 0.0f && live)
            {
                int[] d = { my_dir, (my_dir + 1) % 4, (my_dir + 3) % 4, (my_dir + 2) % 4 };
                for (int ii = 0; ii < 4; ii++)
                {
                    int i = d[ii];
                    if (room.near[i].type != SideType.Wall)
                    {
                        if (room.near[i].other_room != null)
                        {
                            my_dir = i;
                            Room nroom = room.near[i].other_room;
                            room = nroom;
                            break;
                        }
                        else{



                        }
                    }
                }
                timeleft = maxtimeleft;
            }
            Vector3 pos = transform.position;
            Vector3 npos = Vector3.Lerp(pos, room.transform.position, Time.deltaTime * 4.0f);
            npos.y = pos.y;
            transform.position = npos;
        }
        
    }
    public void kill()
    {
        if (live)
        {
            live = false;
            levelmanager.OnKill();
        }
        else
        {
            Debug.Log("ERr kill again");
        }

    }
}
