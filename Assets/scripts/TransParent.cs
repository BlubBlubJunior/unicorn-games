using System.Collections;
using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;

public class TransParent : MonoBehaviour
{
    
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();


            if (rend)
            {
                rend.material.shader = Shader.Find("Hidden/UnlitTransparentColored"); 
                Color tempColor = rend.material.color;
            
                tempColor.a = 0.3f;
            
                rend.material.color = tempColor;
            }
            else
            {
                rend.material.shader = Shader.Find("lit"); 
                Color tempColor = rend.material.color;
            
                tempColor.a = 0.3f;
            
                rend.material.color = tempColor;
                
            }
        }

    }
}
