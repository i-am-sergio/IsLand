using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormir : MonoBehaviour
{
    public TimeManager timeManager;
    public Animator panelAnimator;
    public string sleepAnimationName = "Sleep";
    public string wakeUpAnimationName = "WakeUp";
    public Collider playerCollider;

    private FireCampOnTrigger fireCampScript;
    private Collider sleepZoneCollider;
    private Collider fireCampCollider;

    private void Start()
    {
        GameObject campiresObject = GameObject.FindGameObjectWithTag("Campfires");
        if (campiresObject != null)
        {
            fireCampScript = campiresObject.GetComponent<FireCampOnTrigger>();
            fireCampCollider = campiresObject.GetComponent<Collider>();
        }

        GameObject cavesObject = GameObject.FindGameObjectWithTag("Caves");
        if (cavesObject != null)
        {
            sleepZoneCollider = cavesObject.GetComponent<Collider>();
        }
    }

    private void Update()
    {
        if (fireCampScript != null && timeManager != null && sleepZoneCollider != null && fireCampCollider != null)
        {
            bool isNight = timeManager.Hours >= 22 || timeManager.Hours < 6;
            bool isFireOn = fireCampScript.isFireOn;
            if (isFireOn && isNight && AreBothColliding())
            {
                TriggerSleepAnimation();
            }
        }
    }

    private bool AreBothColliding()
    {
        bool isPlayerInCave = sleepZoneCollider.bounds.Intersects(playerCollider.bounds);
        bool isFireInCave = sleepZoneCollider.bounds.Intersects(fireCampCollider.bounds);
        bool result = isPlayerInCave && isFireInCave;
        return result;
    }

    private void TriggerSleepAnimation()
    {
        if (panelAnimator != null)
        {
            panelAnimator.Play(sleepAnimationName);
            StartCoroutine(WaitAndWakeUp(5f));
        }
    }

    private IEnumerator WaitAndWakeUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (timeManager != null)
        {
            timeManager.SetTime(0, 8, 0);
        }
        if (fireCampScript != null)
        {
            fireCampScript.ForceStopFire();
        }

        if (panelAnimator != null)
        {
            panelAnimator.Play(wakeUpAnimationName);
        }
    }
}
