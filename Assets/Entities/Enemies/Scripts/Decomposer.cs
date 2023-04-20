using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decomposer : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    [SerializeField] float duration = 3f;
    [SerializeField] float depth = 3f;

    public void BeginDecomposing() {
        StartCoroutine(Decompose());
    }

    IEnumerator Decompose() {
        float delayTimeElapsed = 0f;
        while (delayTimeElapsed < delay) {
            delayTimeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // prepare decay variables
        float decayTimeElapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3();
        endPos.Set(startPos.x, startPos.y - depth, startPos.z);
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        while (decayTimeElapsed < duration) {
            decayTimeElapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, decayTimeElapsed/duration);
            transform.localScale = Vector3.Lerp(startScale, endScale, decayTimeElapsed/duration);
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }


}
