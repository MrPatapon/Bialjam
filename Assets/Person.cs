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
    public Vector3 vel;
    public Killer last_killer=null;
    public GameObject hub_scary;
    public bool scary=false;
    private Vector3 velocity;
    public float time2heal;
    [SerializeField] private Transform visualChild;
    [SerializeField] private Animation animationComponent;
    private AnimationState walkState;
    void Start()
    {
        //animationComponent = GetComponentInChildren<Animation>();
    }
    /*Vector3 get_d()
    {

        return new Vector3(room.dir[i].x, 0, room.dir[i].y);
    }*/
    // Update is called once per frame
    void Update()
    {
        hub_scary.active = scary;
        if (inside)
        {
            timeleft -= Time.deltaTime* levelmanager.gameSpeed;
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
                            bool emp = true;

                            foreach(Person p in levelmanager.person)
                            {
                                if(p.room== room.near[i].other_room)
                                {
                                    if (p.live)
                                    {
                                        emp = false;
                                    }
                                }
                            }
                            if (emp)
                            {
                                my_dir = i;
                                Room nroom = room.near[i].other_room;
                                room = nroom;
                                break;
                            }
                            
                        }
                        else{
                            inside = false;
                            vel =new Vector3(room.dir[i].x,0,room.dir[i].y)*5.0f;

                        }
                    }
                }
                timeleft = maxtimeleft;
            }
            Vector3 pos = transform.position;
            Vector3 npos = Vector3.Lerp(pos, room.transform.position, Time.deltaTime * 4.0f* levelmanager.gameSpeed);
            npos.y = pos.y;
            transform.position = npos;
            velocity = (room.transform.position - transform.position) * 4.0f;
        }
        else
        {
            transform.position += vel*Time.deltaTime * levelmanager.gameSpeed;
            timeleft -= Time.deltaTime * levelmanager.gameSpeed;
            if (timeleft < 0.0f)
            {
                vel *= 0.1f;
                levelmanager.OnRun();
            }
            velocity = vel;
        }

        Vector3 movementDir;

        if (inside)
        {
            movementDir = (room.transform.position - transform.position);
        }
        else
        {
            movementDir = vel;
        }

        // Only rotate if moving
        if (movementDir.magnitude > 0.1f && visualChild != null)
        {
            Vector3 lookDir = new Vector3(movementDir.x, 0, movementDir.z);
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                visualChild.rotation = Quaternion.Slerp(visualChild.rotation, targetRotation, Time.deltaTime * 100f);
            }
        }
        float speed = new Vector3(velocity.x, 0, velocity.z).magnitude;

        if (speed > 0.1f)
        {
            if(inside)
            {
                walkState = animationComponent["Walking"];
                walkState.speed = speed * 0.5f;
                if (!animationComponent.IsPlaying("Walking"))
                {
                    animationComponent.CrossFade("Walking");
                }
            }
            else
            {
                walkState = animationComponent["Running"];
                walkState.speed = speed * 0.3f;
                if (!animationComponent.IsPlaying("Running"))
                {
                    animationComponent.CrossFade("Running");
                }
            }                    
        }
        else
        {   
            AnimationState idleState = animationComponent["T POSE"];
            if (idleState != null)
            {
               idleState.speed = 1.0f;
               if (!animationComponent.IsPlaying("T POSE"))
               {
                    animationComponent.CrossFade("T POSE");
               }               
            }            
        }
        //AnimationState anim = animationComponent["Walking"];
        //anim.speed = speed * 0.5f;
        if (!live)
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
        if (scary)
        {
            time2heal -= Time.deltaTime * levelmanager.gameSpeed;
            if(time2heal < 0.0f)
            {
                scary = false;
            }
        }
    }
    public void kill(int damage)
    {
        if (damage < 2 && !scary)
        {
            if (live)
            {
                scary = true;
                time2heal = 4.0f;
            }
        }
        else
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
}
