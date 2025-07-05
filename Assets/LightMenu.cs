using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMenu : MonoBehaviour
{
    public Material materialA; // e.g. "Lit"
    public Material materialB; // e.g. "Unlit"

    void Start()
    {
        
        foreach (Transform child in transform)
        {
            Renderer rend = child.GetComponent<Renderer>();
            if (rend != null && child.childCount > 0)
            {
                Transform nested = child.GetChild(0); 
                StartCoroutine(SwapMaterialIndividually(rend, nested));
            }
        }
    }

    IEnumerator SwapMaterialIndividually(Renderer rend, Transform nestedChild)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 10f));

            Material chosenMat = (Random.value > 0.5f) ? materialA : materialB;
            rend.material = chosenMat;

            bool shouldBeActive = chosenMat == materialA;
            nestedChild.gameObject.SetActive(shouldBeActive);
        }
    }
}