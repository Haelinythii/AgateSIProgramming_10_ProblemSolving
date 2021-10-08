using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private TextMesh timerText;
    [SerializeField] private float timeToExplode;

    private Collider2D explosionCollider;

    private float explodeTimer;
    private bool hasExploded = false;

    private void OnEnable()
    {
        explodeTimer = timeToExplode;
        explosionCollider.enabled = false;
        hasExploded = false;
    }

    private void Awake()
    {
        timerText = GetComponentInChildren<TextMesh>();
        explosionCollider = GetComponent<Collider2D>();
        explosionCollider.enabled = false;

        explodeTimer = timeToExplode;
    }

    private void Update()
    {
        if(explodeTimer > 0f)
        {
            explodeTimer -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(explodeTimer).ToString();
        }
        else
        {
            if (hasExploded) return;

            hasExploded = true;
            StartCoroutine(DelayColliderAppearance());
        }
    }

    private IEnumerator DelayColliderAppearance()
    {
        explosionCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        explosionCollider.enabled = false;
    }
}
