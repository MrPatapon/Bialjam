using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public GameObject roomParent;
    public GameObject edgeParent;
    void Start()
    {
        Dictionary<Vector2Int, Room> plan=new();
        Dictionary<Vector2Int, edge> plane = new();

        foreach (Room r in roomParent.GetComponentsInChildren<Room>())
        {
            Vector3 pos=r.transform.position;
            int xi = (int)(pos.x / 2.0);
            int yi = (int)(pos.z / 2.0);
            plan[new Vector2Int(xi,yi)]= r;
            Debug.Log("new room " + xi.ToString()+" "+ yi.ToString());
        }
        foreach (edge r in edgeParent.GetComponentsInChildren<edge>())
        {
            Vector3 pos = r.transform.position;
            int xi = (int)(pos.x);
            int yi = (int)(pos.z);
            plane[new Vector2Int(xi, yi)] = r;
            Debug.Log("new door " + xi.ToString() + " " + yi.ToString());
        }



        int i = 0;
        foreach(Vector2Int pi in plan.Keys)
        {
            List<Vector2Int> dirs = new();
            dirs.Add(new Vector2Int(-1, 0));
            dirs.Add(new Vector2Int(0, -1));
            dirs.Add(new Vector2Int(1,0));
  
            dirs.Add(new Vector2Int(0, 1));
            foreach (Vector2Int dir in dirs)
            {
                Side s = new Side();
                s.type = SideType.Nothing;
                if (plan.ContainsKey(pi + dir))
                {
                    s.other_room=plan[pi + dir];
                }
                if (plane.ContainsKey(pi + pi + dir)) {
                    s.wall = plane[pi + pi + dir];
                    s.type = plane[pi + pi + dir].type;
                }
                i += 1;
                plan[pi].near.Add(s);
                plan[pi].dir.Add(dir);
            }
        }
        Debug.Log("ala " + i.ToString());



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
