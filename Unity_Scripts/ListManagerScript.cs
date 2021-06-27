using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListManagerScript : MonoBehaviour
{
    public Canvas myCanvas;
    public GameObject theCam;
    [HideInInspector]
    public CameraMovementScript theCamScript;
    public GameObject headPointer, tailPointer;
    [HideInInspector]
    public LineRendererScript headLR, tailLR;
    public GameObject theNULL;
    public List<GameObject> objectList = new List<GameObject>();
    public TMP_InputField titleInput, notesInput;
    public Button addButton;
    public GameObject tempEntry;
    public GameObject terminal;
    public GameObject prefabNodeClass;
    TerminalScript terminalScript;
    EntryClassScript tempEntryScript;
    public GameObject prefabTextShooter;
    public GameObject simManager;
    [HideInInspector]
    public SimulationManagerScript simManagerScript;
    bool titleReady = false, notesReady = false, currentlyMakingANode = false;
    // Start is called before the first frame update
    void Start()
    {
        headLR = headPointer.GetComponent<LineRendererScript>();
        tailLR = tailPointer.GetComponent<LineRendererScript>();
        terminalScript = terminal.GetComponent<TerminalScript>();
        tempEntryScript = tempEntry.GetComponent<EntryClassScript>();
        simManagerScript = simManager.GetComponent<SimulationManagerScript>();
        headLR.targetObject = theNULL;
        tailLR.targetObject = theNULL;
        objectList.Add(theNULL);
        theCamScript = theCam.GetComponent<CameraMovementScript>();
        addButton.interactable = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (titleReady && notesReady && !currentlyMakingANode)
            addButton.interactable = true;
        else
            addButton.interactable = false;
    }
    public void CheckTerminalTitleInputValid(string titleString)
    {
        if (titleString.Length > 0 && titleString.Length < 12)
            titleReady = true;
        else
            titleReady = false;
    }
    public void CheckTerminalNotesInputValid(string notesString)
    {
        if (notesString.Length > 0 && notesString.Length < 12)
            notesReady = true;
        else
            notesReady = false;
    }
    public void MoveAll()
    {
        StartCoroutine(MoveAllOver());
    }
    public IEnumerator MoveAllOver()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < objectList.Count; i++) {
            Vector3 startPos = objectList[i].transform.position;
            Vector3 rightward = objectList[i].transform.TransformDirection(Vector3.right)*7f;
            Vector3 desPos = objectList[i].transform.position+rightward;
            float timeOfTravel = 1f;
            float currentTime = 0;
            float normalizedValue;
            while (currentTime <= timeOfTravel) {
                currentTime += Time.deltaTime;
                normalizedValue = currentTime / timeOfTravel; // we normalize our time
                objectList[i].transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            objectList[i].transform.position = desPos;
        }
        StartCoroutine(TransferText());
        yield return null;
    }
    public IEnumerator MoveNullOver()
    {
        yield return new WaitForSeconds(1f);
        Vector3 startPos = objectList[0].transform.position;
        Vector3 rightward = objectList[0].transform.TransformDirection(Vector3.right)*7f;
        Vector3 desPos = objectList[0].transform.position+rightward;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            objectList[0].transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        objectList[0].transform.position = desPos;
        StartCoroutine(TransferText());
        yield return null;
    }

    public void CreateNew()
    {
        theCamScript.setup = false;
        titleInput.interactable = false;
        notesInput.interactable = false;
        currentlyMakingANode = true;
        StartCoroutine(NewNodeRoutine());
    }
    public IEnumerator NewNodeRoutine()
    {
        if (objectList.Count >1)
            StartCoroutine(MoveCamera());
        yield return new WaitForSeconds(1f);
        if (simManagerScript.Stack.isOn)
            StartCoroutine(MoveAllOver());
        else if (simManagerScript.Queue.isOn)
            StartCoroutine(MoveAllOver());
        else //LLL
            StartCoroutine(MoveNullOver());
        yield return null;
    }
    public IEnumerator TransferText()
    {
        //Shoot Text to temp object
        yield return new WaitForSeconds(1f);
        terminalScript.TitleTextShooter();
        yield return new WaitForSeconds(1f);
        tempEntryScript.UpdateTitleText(titleInput.text);
        yield return new WaitForSeconds(1f);
        terminalScript.NotesTextShooter();
        yield return new WaitForSeconds(1f);
        tempEntryScript.UpdateNotesText(notesInput.text);
        //Create new Node
        Vector3 location = Vector3.zero;
        if (simManagerScript.LLL.isOn && objectList.Count > 1) {
            Vector3 rightward = objectList[objectList.Count-1].transform.TransformDirection(Vector3.right)*7f;
            location = objectList[objectList.Count-1].transform.position+rightward;
        }
        GameObject newObj = Instantiate(prefabNodeClass, location, Quaternion.identity) as GameObject;
        headLR.ShrinkLine();
        if (objectList.Count == 1) {
            tailLR.ShrinkLine();
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        SetHeadAndTailPointers(newObj);
        headLR.FormLine();
        if (objectList.Count == 1) {
            tailLR.FormLine();
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        newObj.GetComponent<GrowScript>().Grow();
        //Shoot text to new object
        yield return new WaitForSeconds(1f);
        EntryClassScript newObjScript = newObj.GetComponent<EntryClassScript>();
        TitleTextShooter(newObjScript.myTitle.gameObject);
        yield return new WaitForSeconds(1f);
        newObjScript.UpdateTitleText(titleInput.text);
        yield return new WaitForSeconds(1f);
        NotesTextShooter(newObjScript.myNotes.gameObject);
        yield return new WaitForSeconds(1f);
        newObjScript.UpdateNotesText(notesInput.text);
        yield return new WaitForSeconds(1f);
        //Update new object's Next pointer
        StartCoroutine(SetNewObjectsNextPointer(newObjScript));
        objectList.Add(newObj);
        //Clear things
        ClearInputs();
    }
    public void TitleTextShooter(GameObject obj)
    {
        StartCoroutine(TitleTextShooterRoutine(obj));
    }
    public IEnumerator TitleTextShooterRoutine(GameObject obj)
    {
        Vector3 startPos = tempEntryScript.myTitle.transform.position;
        Vector3 desPos = obj.transform.position;
        GameObject shooter = Instantiate(prefabTextShooter, startPos, Quaternion.identity) as GameObject;
        shooter.transform.SetParent(myCanvas.transform);
        TextMeshProUGUI shooterText = shooter.GetComponentInChildren<TextMeshProUGUI>();
        shooterText.text = titleInput.text;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            shooter.transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        shooter.transform.position = desPos;
        yield return new WaitForSeconds(1f);
        Destroy(shooter);
        yield return null;
    }
    public void NotesTextShooter(GameObject obj)
    {
        StartCoroutine(NotesTextShooterRoutine(obj));
    }
    public IEnumerator NotesTextShooterRoutine(GameObject obj)
    {
        Vector3 startPos = tempEntryScript.myNotes.transform.position;
        Vector3 desPos = obj.transform.position;
        GameObject shooter = Instantiate(prefabTextShooter, startPos, Quaternion.identity) as GameObject;
        shooter.transform.SetParent(myCanvas.transform);
        TextMeshProUGUI shooterText = shooter.GetComponentInChildren<TextMeshProUGUI>();
        shooterText.text = notesInput.text;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            shooter.transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        shooter.transform.position = desPos;
        yield return new WaitForSeconds(1f);
        Destroy(shooter);
        yield return null;
    }
    public void ClearInputs()
    {
        titleInput.interactable = true;
        notesInput.interactable = true;
        currentlyMakingANode = false;
        titleInput.text = "";
        notesInput.text = "";
        tempEntryScript.myTitle.text = "";
        tempEntryScript.myNotes.text = "";
        theCamScript.setup = true;
    }
    public IEnumerator SetNewObjectsNextPointer(EntryClassScript newObjScript)
    {
        if (simManagerScript.LLL.isOn && objectList.Count > 1) {
            newObjScript.nextLRPointer.GetComponent<LineRendererScript>().targetObject = objectList[0]; //newObj.next == null
            //Get the node right before the new one and set it's next pointer to this new obj
            LineRendererScript lastNextLR = objectList[objectList.Count-1].GetComponent<EntryClassScript>().nextLRPointer.GetComponent<LineRendererScript>();
            lastNextLR.ShrinkLine();
            yield return new WaitForSeconds(1f);
            lastNextLR.targetObject = newObjScript.gameObject;
            lastNextLR.FormLine();
            yield return new WaitForSeconds(1f);
        } else {
            newObjScript.nextLRPointer.GetComponent<LineRendererScript>().targetObject = objectList[objectList.Count-1];
        }
        newObjScript.nextLRPointer.GetComponent<LineRendererScript>().FormLine();
        yield return null;
    }
    public void SetHeadAndTailPointers(GameObject newObj)
    {
        //insert at head
        if (simManagerScript.Stack.isOn) {
            headLR.targetObject = newObj;
        } else if (simManagerScript.Queue.isOn) {
            headLR.targetObject = newObj;
        } else {//LLL - insert at last
            if (objectList.Count == 1) {
                headLR.targetObject = newObj;
            } else if (objectList.Count > 1) {
                headLR.targetObject = objectList[1];
            }
        }
        if (objectList.Count == 1) {
            tailLR.targetObject = newObj;
        } else if (objectList.Count > 1) {
            if (simManagerScript.LLL.isOn) {
                tailLR.targetObject = newObj;
            }
        }
    }
    public IEnumerator MoveCamera()
    {
        Vector3 startPos = theCam.transform.position;
        Vector3 desPos = theCam.transform.position;
        if (objectList.Count == 2)
            desPos = new Vector3(3, theCam.transform.position.y, -15);
        else if (objectList.Count == 3)
            desPos = new Vector3(4, theCam.transform.position.y, -19);
        else if (objectList.Count == 4)
            desPos = new Vector3(6, theCam.transform.position.y, -24);
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            theCam.transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        theCam.transform.position = desPos;
        yield return null;
    }
}
