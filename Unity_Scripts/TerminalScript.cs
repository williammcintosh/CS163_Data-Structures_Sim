using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalScript : MonoBehaviour
{
    public Canvas myCanvas;
    public float travelSpeed = 1.0f;
    public GameObject prefabEntryShooter;
    public GameObject [] blockers;
    public GameObject entryShooterPointer;
    public GameObject entryClassObject;
    public GameObject prefabListShooter;
    public GameObject listShooterPointer;
    public GameObject listClassObject;
    [HideInInspector]
    public LineRendererScript entryLR, listLR;
    public GameObject mainPointer;
    public GameObject prefabTextShooter;
    public GameObject tempEntryTitleText, tempEntryNotesText, terminalInputTitle, terminalInputNotes;
    public TMP_InputField titleInput, notesInput;
    // Start is called before the first frame update
    void Start()
    {
        entryLR = entryShooterPointer.GetComponent<LineRendererScript>();
        listLR = listShooterPointer.GetComponent<LineRendererScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EntryShooter()
    {
        StartCoroutine(EntryShooterRoutine());
    }
    public IEnumerator EntryShooterRoutine()
    {
        Vector3 startPos = entryShooterPointer.transform.position;
        Vector3 desPos = entryClassObject.transform.position;
        GameObject shooter = Instantiate(prefabEntryShooter, startPos, Quaternion.identity) as GameObject;
        Vector3 relativePos = desPos - startPos;
        shooter.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.back);
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime * travelSpeed;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            shooter.transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        shooter.transform.position = desPos;
        StartCoroutine(GrowRoutine(shooter));
        yield return null;
    }
    public void ListShooter()
    {
        StartCoroutine(ListShooterRoutine());
    }
    public IEnumerator ListShooterRoutine()
    {
        Vector3 startPos = listShooterPointer.transform.position;
        Vector3 desPos = listClassObject.transform.position;
        GameObject shooter = Instantiate(prefabListShooter, startPos, Quaternion.identity) as GameObject;
        Vector3 relativePos = desPos - startPos;
        shooter.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.back);
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime * travelSpeed;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            shooter.transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        shooter.transform.position = desPos;
        StartCoroutine(GrowRoutine(shooter));
        yield return null;
    }
    public void MovePointer(int pos)
    {
        StartCoroutine(MovePointerRoutine(pos));
    }
    public IEnumerator MovePointerRoutine(int pos)
    {
        Vector3 startPos = mainPointer.transform.position;
        Vector3 desPos = new Vector3(startPos.x, blockers[pos].transform.position.y, startPos.z);
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime * travelSpeed;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            mainPointer.transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        mainPointer.transform.position = desPos;
        yield return null;
    }
    public void DisappearBlock(int pos)
    {
        StartCoroutine(DisappearBlockRoutine(pos));
    }
    public IEnumerator DisappearBlockRoutine(int pos)
    {
        Vector3 startPos = blockers[pos].transform.position;
        Vector3 backward = blockers[pos].transform.TransformDirection(Vector3.back)*0.4f;
        Vector3 desPos = blockers[pos].transform.position-backward;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime * travelSpeed;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            blockers[pos].transform.position = Vector3.Lerp(startPos, desPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        blockers[pos].transform.position = desPos;
        yield return null;
    }
    public void TitleTextShooter()
    {
        StartCoroutine(TitleTextShooterRoutine());
    }
    public IEnumerator TitleTextShooterRoutine()
    {
        Vector3 startPos = terminalInputTitle.transform.position;
        Vector3 desPos = tempEntryTitleText.transform.position;
        GameObject shooter = Instantiate(prefabTextShooter, startPos, Quaternion.identity) as GameObject;
        shooter.transform.SetParent(myCanvas.transform);
        TextMeshProUGUI shooterText = shooter.GetComponentInChildren<TextMeshProUGUI>();
        shooterText.text = titleInput.text;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime * travelSpeed;
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
    public void NotesTextShooter()
    {
        StartCoroutine(NotesTextShooterRoutine());
    }
    public IEnumerator NotesTextShooterRoutine()
    {
        Vector3 startPos = terminalInputNotes.transform.position;
        Vector3 desPos = tempEntryNotesText.transform.position;
        GameObject shooter = Instantiate(prefabTextShooter, startPos, Quaternion.identity) as GameObject;
        shooter.transform.SetParent(myCanvas.transform);
        TextMeshProUGUI shooterText = shooter.GetComponentInChildren<TextMeshProUGUI>();
        shooterText.text = notesInput.text;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime * travelSpeed;
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
    public IEnumerator GrowRoutine(GameObject shooter)
    {
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            shooter.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        shooter.transform.localScale = Vector3.zero;
        yield return null;
        Destroy(shooter);
    }
}
