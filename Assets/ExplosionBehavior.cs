using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public float secondsToDisappear;

    float secondsSinceAppear;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceAppear = 0.0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        secondsSinceAppear += Time.fixedDeltaTime;

        float lifeFraction = secondsSinceAppear / secondsToDisappear;
        Vector3 maxScale = Vector3.one * 5;
        transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, lifeFraction);

        if (secondsSinceAppear >= secondsToDisappear)
        {
            Destroy(gameObject);
            secondsSinceAppear = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem theirHealthSystem = other.gameObject.GetComponent<HealthSystem>();
        if (theirHealthSystem != null)
        {
            theirHealthSystem.TakeDamage(10);
        }
    }
}
