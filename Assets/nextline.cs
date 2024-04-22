using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextline : MonoBehaviour
{
    public GameObject text;
    public GameObject thisgame;

    public void lines()
    {
        text.SetActive(true);
        thisgame.SetActive(false);
    }
}
