using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private string keyboardText;
    [SerializeField] private string controllerText;

    public float disappearTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        ChangeInputText();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
            StartCoroutine(HideTutorial());

    }

    IEnumerator HideTutorial()
    {

        yield return new WaitForSeconds(disappearTime);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeInputText();
    }

    private void ChangeInputText()
    {
        if (GameManager.instance.currentControlScheme == GameManager.CurrentController.KEYBOARD)
        {
            textObject.text = keyboardText;
        }
        else if (GameManager.instance.currentControlScheme == GameManager.CurrentController.CONTROLLER)
        {
            textObject.text = controllerText;
        }
    }
}
