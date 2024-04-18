using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransParent : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public float transparencyDistance = 5f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (player == null)
            player = FindObjectOfType<playerController>()?.transform;

        if (mainCamera == null)
            mainCamera = Camera.main;
        
        
        if (player == null || mainCamera == null)
            return;
        
        Vector3 directionToPlayer = player.position - mainCamera.transform.position;
        
        Ray ray = new Ray(mainCamera.transform.position, directionToPlayer.normalized);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, directionToPlayer.magnitude))
        {
            if (hit.collider.gameObject == gameObject)
                return;
            
            float transparency = Mathf.InverseLerp(0, transparencyDistance, hit.distance);
            
            Color materialColor = rend.material.color;
            materialColor.a = transparency;
            rend.material.color = materialColor;
        }
        else
        {
            Color materialColor = rend.material.color;
            materialColor.a = 1f;
            rend.material.color = materialColor;
        }
    }
}
