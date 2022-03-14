using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = FindObjectOfType<PlayerController>().gameObject;
        playerObj.transform.position = transform.Find(GameManager.instance.playerSpawnName).position;
    }

}
