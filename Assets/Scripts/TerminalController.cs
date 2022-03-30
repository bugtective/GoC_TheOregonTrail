using System;
using UnityEngine;
using TMPro;

public class TerminalController : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField inputField;

    public Action<string> InputReceievedCallback = null;

    private void Awake()
    {
        // Hiding Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        inputField.onSubmit.AddListener(OnInputEntered);
    }

    private void OnInputEntered(string text)
    {
        inputField.text = "";
        inputField.enabled = false;

        InputReceievedCallback?.Invoke(text);
    }

    public void ShowMessage(string text)
    {
        messageText.text = text;
    }

    public void DisableInput()
    {
        inputField.enabled = false;
    }
    
    public void EnableInput()
    {
        inputField.enabled = true;
        inputField.ActivateInputField();
    }
}
