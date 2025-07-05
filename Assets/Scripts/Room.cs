using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public enum SideType
{
    Wall,
    CDoor,
    ODoor,
    Nothing
}


[Serializable]
public struct Side
{
    public SideType type;
    public Room other_room;
    public edge wall;
};


public class Room : MonoBehaviour
{
    public Camera camera;

    public GameObject active_gui;

    public List<Side> near = new();
    public List<Vector2Int> dir=new();
    public bool can_rotate = true;

    public bool is_rot=false;
    public KeyCode trigger = KeyCode.Q;
    LevelManager level;
    bool pre = false;
    bool last_m = false;
    public Killer killer=null;
    public bool use_auto_mesh = false;
    public GameObject mesh1;
    public GameObject mesh2;


    void Start()
    {
        if (use_auto_mesh)
        {
            if (can_rotate)
            {
                mesh1.active = true;
                mesh2.active = false;
            }
            else
            {
                mesh2.active = true;
                mesh1.active = false;
            }
        }

        level = FindObjectOfType<LevelManager>();
   
    }
    void Update()
    {
        if (is_rot)
        {
            if ( Input.GetKeyDown(trigger) && pre == false ) {
                pre=true;
                Debug.Log("ROTATE!!!!!!!!!!");
                rotate();
            }
            else if( Input.GetKeyDown(trigger) == false && pre )
            {
                pre = false;
            }
        }
        camera= FindObjectOfType<Camera>();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        var v=ray.origin - ray.direction * (ray.origin.y / ray.direction.y);
        if (killer != null)
        {
            killer.preview = false;
        }
        if ((transform.position - v).magnitude<1.0) {
            if (level == null) { level = FindObjectOfType<LevelManager>(); };
            if (level.stamina.v > 0.3f)
            {
                if (can_rotate)
                {
                    if (killer == null)
                    {
                        if (Input.GetMouseButton(0) && (!last_m))
                        {
                            Debug.Log("ROTATE!!!!!!!!!!");
                            level.stamina.v -= 0.3f;
                            rotate();

                        }
                    }
                    if (killer != null)
                    {
                        killer.preview = true;
                    }
                    if (killer == null)
                    {
                        active_gui.active = true;
                    }
                        
                }
            }
                
        }
        else
        {
            active_gui.active = false;
        }
        last_m = Input.GetMouseButton(0);
    }
    void OnMouseOver()
    {
        Debug.Log("ROTATE!!!!!!!!!!");

    }

     private void OnMouseDown()
    {
        Debug.Log("ROTATE!!!!!!!!!!");
        rotate();
    }
    void rotate()
    {
        List<Side> nearO = new();
        nearO.Add(near[0]);
        nearO.Add(near[1]);
        nearO.Add(near[2]);
        nearO.Add(near[3]);

        for(int idO=0;idO<4;idO++){
            int id = (idO + 1) % 4;
            Side s;
            s.other_room = nearO[id].other_room;
            s.wall = nearO[idO].wall;
            s.type = nearO[idO].type;
            near[id] = s;
            int nid = id ^ 2;
            if (s.other_room != null)
            {
                Side sn = s.other_room.near[nid];
                sn.wall= s.wall;
                sn.type = s.type;
                s.other_room.near[nid] = sn;
            }

            if(nearO[idO].wall!= null)
            {
                edge e = nearO[idO].wall;
                e.transform.RotateAround(new Vector3(0,1,0),-(float)Math.PI/2.0f);
                Vector2Int d = dir[id];
                e.transform.position = this.transform.position+new Vector3(d.x,0,d.y);
            }
        }
        


    }
}
