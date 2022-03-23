using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMotel : MonoBehaviour
{
    public GameObject indicator;
    private NewLevelController motelController;
    // Start is called before the first frame update
    void Start()
    {
        motelController = GetComponent<NewLevelController>();

        bool canUnlock = true;

        foreach (var i in GameManager.instance.talkedToNPCs)
            if (!i)
            {
                canUnlock = false;
                break;
            }

        if (canUnlock)
        {
            motelController.isUnlocked = true;
            indicator.SetActive(true);
        }
    }
}
