using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    public GameObject backgroundInv;
    private bool isInvOpen = false;

    public List<GameObject> items;
    public List<GameObject> positions;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInvOpen == false)
        {
            isInvOpen = true;
            backgroundInv.SetActive(true);
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < positions.Count; j++)
                {
                    items[i].SetActive(true);
                    positions[j].SetActive(true);
                    items[i].transform.position = positions[j].transform.position;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && isInvOpen == true)
        {
            isInvOpen = false;
            backgroundInv.SetActive(false);
            
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < positions.Count; j++)
                {
                    items[i].SetActive(false);
                    positions[j].SetActive(false);
                    items[i].transform.position = positions[j].transform.position;
                }
            }
        }
    }
}
