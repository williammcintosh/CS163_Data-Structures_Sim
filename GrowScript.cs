using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowScript : MonoBehaviour
{
    public float growSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Grow()
    {
        StartCoroutine(GrowRoutine());
    }
    public IEnumerator GrowRoutine()
    {
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime * growSpeed;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time
            this.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        this.transform.localScale = Vector3.one;
        yield return null;
    }
}
