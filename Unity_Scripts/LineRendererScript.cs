using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    public GameObject targetObject;
    LineRenderer myLine;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
        myLine.SetWidth(0.0F, 0.0F);
        myLine.SetVertexCount(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            PointAtStuff();
    }
    public void PointAtStuff()
    {
        myLine.SetPosition(0, this.transform.position);
        myLine.SetPosition(1, targetObject.transform.position);
    }
    public void FormLine()
    {
        active = true;
        StartCoroutine(FormLineRoutine());
    }
    public IEnumerator FormLineRoutine()
    {
        float startThickness = 0.0f;
        float desThickness = 0.1f;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            float myThickness = Mathf.Lerp(startThickness, desThickness, normalizedValue);
            myLine.SetWidth(myThickness, myThickness);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        myLine.SetWidth(desThickness, desThickness);
        yield return null;
    }
    public void ShrinkLine()
    {
        StartCoroutine(ShrinkLineRoutine());
    }
    public IEnumerator ShrinkLineRoutine()
    {
        float startThickness = 0.1f;
        float desThickness = 0.0f;
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            float myThickness = Mathf.Lerp(startThickness, desThickness, normalizedValue);
            myLine.SetWidth(myThickness, myThickness);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        myLine.SetWidth(desThickness, desThickness);
        yield return null;
    }
}
