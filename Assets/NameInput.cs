using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameInput : MonoBehaviour
{
   public TMP_InputField InputField;

   public string getName()
   {
      return InputField.text;
   }
}
