using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpeedControll : MonoBehaviour
{
    // Start is called before the first frame update
    public SpawnManagerScriptableObject config;
    public UnityEngine.UI.Button b_plus;
    public UnityEngine.UI.Button b_minus;

    public TMPro.TMP_Text label;
    void Start()
    {
        Upd();
        b_plus.onClick.AddListener(() =>
        {
            config.speed = config.speed + 0.1f;
            Upd();
        });
        b_minus.onClick.AddListener(() => {
            config.speed = Mathf.Max( config.speed - 0.1f,0.1f);
            Upd();
        });
    }

    public void Upd()
    {
        label.text = ((int)Mathf.Round(100.0f*config.speed)).ToString()+"%";
    }

}
