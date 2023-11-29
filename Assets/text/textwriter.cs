using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textwriter : MonoBehaviour
{

    private static textwriter instance;

    private List <TextWriterSingle> textWriterSinglelist;

    private void Awake()
    {
        instance = this;
        textWriterSinglelist = new List<TextWriterSingle>();
    }
    public static void Addwriter_static (Text uitext, string TextTowrite, float timepercharacter, bool invisiblecharacters)
    {
        instance.addwriter(uitext, TextTowrite, timepercharacter, invisiblecharacters);
    }
    public void addwriter(Text uitext, string TextTowrite, float timepercharacter, bool invisiblecharacters)
    {
        textWriterSinglelist.Add(new TextWriterSingle( uitext,  TextTowrite,  timepercharacter,  invisiblecharacters));
        
    }
    private void Update()
    {
        for(int i = 0; i < textWriterSinglelist.Count; i++)
        {
            Debug.Log(textWriterSinglelist.Count);
            bool destroyinstance = textWriterSinglelist[i].Update();
            if (destroyinstance)
            {
                textWriterSinglelist.RemoveAt(i);
                i--;            
            }
        }
        
    }


    public class TextWriterSingle
    {
        private Text uitext;
        private string TextTowrite;
        private int characterindex;
        private float timepercharacter;
        private float timer;
        private bool invisiblecharacters;
        public TextWriterSingle(Text uitext, string TextTowrite, float timepercharacter, bool invisiblecharacters)
        {
            this.uitext = uitext;
            this.TextTowrite = TextTowrite;
            this.timepercharacter = timepercharacter;
            this.invisiblecharacters = invisiblecharacters;
            characterindex = 0;
        }
        public bool Update()
        {
            
           timer -= Time.deltaTime;
           
           {
                timer += timepercharacter;
                characterindex++;
                string text = TextTowrite.Substring(0, characterindex);
                if (invisiblecharacters)
                {
                   text += "<color=#00000000>" + TextTowrite.Substring(characterindex) + "</color>";
                }
                

                if (characterindex >= TextTowrite.Length)
                {
                    return true;
                }
                return false;
            }
        
        }
    }
}

