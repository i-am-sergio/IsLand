using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRestart : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public Transform xrOrigin;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Players");
        if (player != null)
        {
            playerStatus = player.GetComponent<PlayerStatus>();
        }
    }

    public void RespawnPlayer()
    {
        if (playerStatus != null)
        {
            xrOrigin.position = playerStatus.lastSleepPosition;
            Debug.Log("Reapareciendo al jugador en la última posición de descanso: " + playerStatus.lastSleepPosition);
        }
    }
}