using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCampOnTrigger : MonoBehaviour
{
    public ParticleSystem fireSpark;
    public ParticleSystem fireSpark2;
    public ParticleSystem fireSpark3;
    public float fireDuration = 60f;
    public bool isFireOn = false;
    private List<SparkOnCollision> frictionObjectsInTrigger = new List<SparkOnCollision>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FrictionObject"))
        {
            SparkOnCollision sparkObject = other.GetComponent<SparkOnCollision>();
            if (sparkObject != null && !frictionObjectsInTrigger.Contains(sparkObject))
                frictionObjectsInTrigger.Add(sparkObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FrictionObject"))
        {
            SparkOnCollision sparkObject = other.GetComponent<SparkOnCollision>();
            if (sparkObject != null && frictionObjectsInTrigger.Contains(sparkObject))
                frictionObjectsInTrigger.Remove(sparkObject);
        }
    }

    private void Update()
    {
        if (!isFireOn && frictionObjectsInTrigger.Count >= 2)
            if (AreBothSparking(frictionObjectsInTrigger))
                StartCoroutine(StartFire());
    }

    private bool AreBothSparking(List<SparkOnCollision> objects)
    {
        int sparkingCount = 0;
        foreach (SparkOnCollision obj in objects)
        {
            if (obj.isSparking)
                sparkingCount++;
        }
        return sparkingCount >= 2;
    }

    private IEnumerator StartFire()
    {
        isFireOn = true;
        fireSpark.Play();
        fireSpark2.Play();
        fireSpark3.Play();
        yield return new WaitForSeconds(fireDuration);
        ForceStopFire();
    }
    public void ForceStopFire()
    {
        fireSpark.Stop();
        fireSpark2.Stop();
        fireSpark3.Stop();
        isFireOn = false;
        StopAllCoroutines();
    }

}
