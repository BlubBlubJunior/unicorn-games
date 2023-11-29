using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textcomponent;
    [SerializeField] private TextMeshProUGUI nameComp;
    [SerializeField] private Image Character;
    [SerializeField] private Sprite[] mysprites;


    [SerializeField] private  string[] lines;
    [SerializeField] private string[] name;

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
            if (textcomponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textcomponent.text = lines[index];
                nameComp.text = name[index];
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
        foreach (char c in lines[index].ToCharArray())
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine(TypeLine());
            
            updateCharacterInfo();
        }
    }

    void updateCharacterInfo()
    {
        if (index < name.Length)
        {
            nameComp.text = name[index];
        }

        if (index < mysprites.Length)
        {
            Character.sprite = mysprites[index];
        }
    }
}

