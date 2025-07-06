using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KillerType
{
    Scary,
    Mouth
};

public class Killer : MonoBehaviour
{
    public KillerType kType = KillerType.Scary;
    public LevelManager levelManager;
    public Room room;
    public bool preview = false;
    public GameObject gui;
    public GameObject gui2;
    public GameObject obj_power;
    public float time2power = 5.0f;
    public AudioClip SoundToScary;

    public Animation anim; // NEW - Legacy Animation component
    public AnimationClip killClip; // Assign your legacy animation here
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isPlayingAnimation = false;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        room.killer = this;

        /*
        if (anim == null)
            anim = GetComponent<Animation>();
        */

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        if (killClip != null && anim.GetClip(killClip.name) == null)
        {
            anim.AddClip(killClip, killClip.name);
        }
    }
    void Update()
    {

        if (isPlayingAnimation) return;
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
                            if (p.last_killer != this)
                            {
                                time2power = 5.0f;
                                if (kType == KillerType.Scary)
                                {
                                    p.kill(1);
                                    
                                    if(anim != null)
                                    {
                                        StartCoroutine(PlayKillAnimationLegacy());
                                    }                                    
                                }
                                if (kType == KillerType.Mouth)
                                {
                                    if (p.my_dir == 3)
                                    {
                                        p.kill(2);
                                    }
                                    else
                                    {
                                        p.kill(1);
                                    }                                    
                                }

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

    IEnumerator PlayKillAnimationLegacy()
    {
        isPlayingAnimation = true;

        if (anim != null && killClip != null)
        {
            anim.Play(killClip.name);
        }

        yield return new WaitForSeconds(4.0f);

        anim.Stop();
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        isPlayingAnimation = false;
    }
}
