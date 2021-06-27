using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntryClassScript : MonoBehaviour
{
    public TextMeshProUGUI myTitle, myNotes;
    [HideInInspector]
    public LineRenderer nextLR;
    public GameObject nextLRPointer;
    public bool isTemp = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!isTemp)
            nextLR = nextLRPointer.GetComponent<LineRenderer>();
    }
    public void UpdateTitleText(string newString)
    {
        myTitle.text = newString;
    }
    public void UpdateNotesText(string newString)
    {
        myNotes.text = newString;
    }
}
