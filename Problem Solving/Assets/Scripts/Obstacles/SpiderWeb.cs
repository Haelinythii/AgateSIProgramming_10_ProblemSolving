using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    [SerializeField] private Vector2 maxSize;
    [SerializeField] private float timeOnScreen, timeOnWarning;
    [SerializeField] private GameObject warning, web;

    private float onScreenTimer, warningTimer;
    private bool canExpand = false;

    private void Start()
    {
        warning.SetActive(true);

        StartCoroutine(ExpandOrShrink(warning.transform, maxSize, timeOnWarning, () => {
            warning.SetActive(false);
            ProcessExpandWeb();
        }));
    }

    private void ProcessExpandWeb()
    {
        StartCoroutine(ExpandOrShrink(web.transform, maxSize, timeOnScreen, ProcessShrinkWeb));
    }

    private void ProcessShrinkWeb()
    {
        StartCoroutine(ExpandOrShrink(web.transform, Vector2.zero, timeOnScreen, null));
    }

    private IEnumerator ExpandOrShrink(Transform _object, Vector2 targetSize, float time, System.Action OnCompleted)
    {
        float expandTimer = time;
        while (expandTimer > 0f)
        {
            _object.localScale = Vector2.Lerp(_object.localScale, targetSize, Time.fixedDeltaTime * 5f);
            expandTimer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        OnCompleted?.Invoke();
    }
}
