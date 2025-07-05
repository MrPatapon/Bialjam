using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Person> person;
    public Stamina stamina;
    public float gameSpeed = 1.0f;
    // Start is called before the first frame update
    bool ending = false;
    int kills = 0;
    float t = 1.0f;
    public bool on = true;
    public string current_lv = "";//"more_pok"
    public string next_lv = "";//"EndScene"
    public void OnKill()
    {
        kills += 1;
        if (kills == person.Count)
        {
            ending = true;
        }
        


    }

    public void OnRun()
    {
        Debug.Log("END!!!!!!!!!!!!");
        SceneManager.LoadScene(current_lv, LoadSceneMode.Single);
    } 
    void Start()
    {
        Debug.Assert(person.Count > 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (ending)
        {
            t -= Time.deltaTime;
            if (t < 0.0f)
            {
                Debug.Log("WIN!!!!!!!!!!");
                if (on)
                {
                    SceneManager.LoadScene(next_lv, LoadSceneMode.Single);

                }
                    
            }
        }
        

    }
}
