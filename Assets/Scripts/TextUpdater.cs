using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    public InterfaceVariable chooseVariable;
    private TMP_Text textUpdate;
   
    // Start is called before the first frame update
    void Start()
    {
        textUpdate = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (chooseVariable)
        {
            case InterfaceVariable.SCORE:
                textUpdate.text = "Score: " + GameManager.instance.GetScore(); break;
            case InterfaceVariable.TIME:
                textUpdate.text = "Time: " + GameManager.instance.GetTime().ToString("000"); break;
            case InterfaceVariable.LIVES:
                textUpdate.text = "Lives: " + GameManager.instance.GetLives(); break;
        }
    }
}
