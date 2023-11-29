using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_assistant : MonoBehaviour
{
    
    private Text messageText;
    
     private void Awake()
    {
        
            messageText = transform.Find("Message").Find("MessageText").GetComponent<Text>();
        

        
        
        Application.targetFrameRate = 3;
    }

    private void Start()
    {
       
        textwriter.Addwriter_static(messageText, "hello world!", 1f, true);
    }
}
