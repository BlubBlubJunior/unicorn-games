using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offSetColor, highLighedColor;

    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private GameObject _highLight;

    [SerializeField] private GameObject InRange;
    
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offSetColor : _baseColor;
    }

    public void HighLight(bool isInRangeOff)
    {
        InRange.SetActive(isInRangeOff);
    }
    void OnMouseEnter()
    {
        _highLight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highLight.SetActive(false);
    }
}
