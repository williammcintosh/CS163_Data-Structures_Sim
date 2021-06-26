using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimulationManagerScript : MonoBehaviour
{
    public GameObject panel;
    public Toggle LLL, Queue, Stack;
    public GameObject terminal;
    TerminalScript terminalScript;
    public GameObject entryClassObject, listClassObject, theNULL;
    GrowScript entryGrowScript, listGrowScript, nullGrowScript;
    ListManagerScript listManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(true);
        terminalScript = terminal.GetComponent<TerminalScript>();
        entryGrowScript = entryClassObject.GetComponent<GrowScript>();
        listGrowScript = listClassObject.GetComponent<GrowScript>();
        nullGrowScript = theNULL.GetComponent<GrowScript>();
        listManagerScript = listClassObject.GetComponent<ListManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Begin()
    {
        panel.SetActive(false);
        StartCoroutine(MakeEntryClass());
    }
    public IEnumerator MakeEntryClass()
    {
        yield return new WaitForSeconds(2f);
        terminalScript.MovePointer(0);
        yield return new WaitForSeconds(1f);
        terminalScript.DisappearBlock(0);
        yield return new WaitForSeconds(1f);
        terminalScript.blockers[0].SetActive(false);
        yield return new WaitForSeconds(1f);
        terminalScript.EntryShooter();
        yield return new WaitForSeconds(1f);
        entryGrowScript.Grow();
        yield return new WaitForSeconds(1f);
        terminalScript.entryLR.FormLine();
        StartCoroutine(MakeListClass());
    }
    public IEnumerator MakeListClass()
    {
        yield return new WaitForSeconds(1f);
        terminalScript.MovePointer(1);
        yield return new WaitForSeconds(1f);
        terminalScript.entryLR.ShrinkLine();
        terminalScript.DisappearBlock(1);
        yield return new WaitForSeconds(1f);
        terminalScript.blockers[1].SetActive(false);
        yield return new WaitForSeconds(1f);
        terminalScript.ListShooter();
        yield return new WaitForSeconds(1f);
        listGrowScript.Grow();
        yield return new WaitForSeconds(1f);
        terminalScript.listLR.FormLine();
        yield return new WaitForSeconds(1f);
        nullGrowScript.Grow();
        yield return new WaitForSeconds(1f);
        listManagerScript.headLR.FormLine();
        yield return new WaitForSeconds(1f);
        listManagerScript.tailLR.FormLine();
        yield return new WaitForSeconds(1f);
        terminalScript.listLR.ShrinkLine();
        StartCoroutine(MakeInputsAppear());
    }
    public IEnumerator MakeInputsAppear()
    {
        yield return new WaitForSeconds(1f);
        terminalScript.MovePointer(2);
        yield return new WaitForSeconds(1f);
        terminalScript.DisappearBlock(2);
        yield return new WaitForSeconds(1f);
        terminalScript.blockers[2].SetActive(false);
        yield return new WaitForSeconds(1f);
        terminalScript.MovePointer(3);
        yield return new WaitForSeconds(1f);
        terminalScript.DisappearBlock(3);
        yield return new WaitForSeconds(1f);
        terminalScript.blockers[3].SetActive(false);
        yield return new WaitForSeconds(1f);
        terminalScript.MovePointer(4);
        yield return new WaitForSeconds(1f);
        terminalScript.DisappearBlock(4);
        yield return new WaitForSeconds(1f);
        terminalScript.blockers[4].SetActive(false);
        yield return new WaitForSeconds(1f);
        listManagerScript.theCamScript.setup = true;
    }
}
