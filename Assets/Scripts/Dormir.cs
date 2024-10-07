using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormir : MonoBehaviour
{
    public TimeManager timeManager;
    public Animator panelAnimator;
    public string sleepAnimationName = "Sleep";
    public string wakeUpAnimationName = "WakeUp";
    public GameObject player; // Referencia al jugador
    private PlayerStatus playerStatus;
    private Collider playerCollider;
    private List<FireCampOnTrigger> fireCampScripts = new List<FireCampOnTrigger>();
    private List<Collider> sleepZoneColliders = new List<Collider>();

    private void Start()
    {
        Debug.Log("Dormir script initialized.");

        if (player != null)
        {
            playerStatus = player.GetComponent<PlayerStatus>();
            playerCollider = player.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("Player is not assigned in the inspector!");
        }

        GameObject[] campfiresObjects = GameObject.FindGameObjectsWithTag("Campfires");
        foreach (var campfireObject in campfiresObjects)
        {
            FireCampOnTrigger fireCampScript = campfireObject.GetComponent<FireCampOnTrigger>();
            if (fireCampScript != null)
            {
                fireCampScripts.Add(fireCampScript);
            }
        }
        if (fireCampScripts.Count == 0)
        {
            Debug.LogError("No Campfires found! Check the tag.");
        }

        GameObject[] cavesObjects = GameObject.FindGameObjectsWithTag("Caves");
        foreach (var cave in cavesObjects)
        {
            var sleepZoneCollider = cave.GetComponent<Collider>();
            if (sleepZoneCollider != null)
            {
                sleepZoneColliders.Add(sleepZoneCollider);
            }
        }
    }

    private void Update()
    {
        if (fireCampScripts.Count > 0 && timeManager != null && sleepZoneColliders.Count > 0)
        {
            bool isNight = timeManager.Hours >= 22 || timeManager.Hours < 6;
            bool isFireOn = IsAnyFireOn();

            if (isFireOn && isNight && AreBothColliding())
            {
                TriggerSleepAnimation();
            }
        }
    }

    private bool IsAnyFireOn()
    {
        foreach (var fireCampScript in fireCampScripts)
        {
            if (fireCampScript.isFireOn)
                return true;
        }
        return false;
    }

    private bool AreBothColliding()
    {
        if (playerCollider != null)
        {
            foreach (var sleepZoneCollider in sleepZoneColliders)
            {
                bool isPlayerInCave = sleepZoneCollider.bounds.Intersects(playerCollider.bounds);
                foreach (var fireCampScript in fireCampScripts)
                {
                    bool isFireInCave = sleepZoneCollider.bounds.Intersects(fireCampScript.GetComponent<Collider>().bounds);
                    if (isPlayerInCave && isFireInCave)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void TriggerSleepAnimation()
    {
        if (panelAnimator != null)
        {
            panelAnimator.Play(sleepAnimationName);

            if (playerStatus != null)
            {
                playerStatus.UpdateSleepPosition(player.transform.position);
            }

            StartCoroutine(WaitAndWakeUp(5f));
        }
    }

    private IEnumerator WaitAndWakeUp(float waitTime)
    {

        if (timeManager != null)
        {
            timeManager.SetTime(0, 8, 0);
        }

        yield return new WaitForSeconds(waitTime);

        foreach (var fireCampScript in fireCampScripts)
        {
            if (fireCampScript != null)
            {
                fireCampScript.ForceStopFire();
            }
        }

        if (panelAnimator != null)
        {
            panelAnimator.Play(wakeUpAnimationName);
        }
    }
}
