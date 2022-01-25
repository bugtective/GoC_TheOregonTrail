using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField inputField;

    void Awake()
    {
        // Hiding Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        messageText.text = "This is a text! Write Something and press Enter";

        inputField.onSubmit.AddListener(OnEndEdit);

        inputField.ActivateInputField();
    }

    void OnEndEdit(string text)
    {
        messageText.text = text;

        inputField.text = "";
        inputField.ActivateInputField();
    }
}
