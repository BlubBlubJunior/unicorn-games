using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class dialogos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textcomponent;
    [SerializeField] private TextMeshProUGUI nameComp;
    [SerializeField] private Image Character;
    [SerializeField] private Sprite[] mysprites;


    [SerializeField] private  string[] Text;
    [SerializeField] private string[] characterName;

    [SerializeField] private float textspeed = 0.5f;

    private int index;
    void Start()
    {
        textcomponent.text = string.Empty;
        nameComp.text = string.Empty;
        startDailogo();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textcomponent.text == Text[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textcomponent.text = Text[index];
                nameComp.text = characterName[index];
                updateCharacterInfo();
            }
        }
    }

    void startDailogo()
    {
        index = 0;
        StartCoroutine(TypeLine());
        
        updateCharacterInfo();
    }
    IEnumerator TypeLine()
    {
        foreach (char c in Text[index].ToCharArray())
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }
    void NextLine()
    {
        if (index < Text.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine(TypeLine());
            
            updateCharacterInfo();
        }
    }

    void updateCharacterInfo()
    {
        if (index < characterName.Length)
        {
            nameComp.text = characterName[index];
        }

        if (index < mysprites.Length)
        {
            Character.sprite = mysprites[index];
        }
    }
}

