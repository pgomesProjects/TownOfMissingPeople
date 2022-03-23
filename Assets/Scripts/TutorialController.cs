using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameManager.Tutorial activeTut;

    public float disappearTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.tutorialsViewed[(int)activeTut])
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
            StartCoroutine(HideTutorial());

    }

    IEnumerator HideTutorial()
    {

        yield return new WaitForSeconds(disappearTime);
        GameManager.instance.tutorialsViewed[(int)activeTut] = true;
        Destroy(gameObject);
    }
}
