using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField inputField;

    private GameStates currentState = GameStates.None;

    private int currentRound = 0;

    private string decisionsStr = null;
    private List<Desicions> desicionsExpected = new List<Desicions>();

    private void Awake()
    {
        // Hiding Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ChangeState(GameStates.Loading);
    }

    private void Start()
    {
        inputField.onSubmit.AddListener(OnInputEntered);
        
        ShowRoundMessage();
    }

    private void ChangeState(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.None:
            case GameStates.Loading:
            {
                inputField.enabled = false;
            }
            break;
            
            case GameStates.PrintingMessage:
            {
                inputField.enabled = false;
            }
            break;

            case GameStates.WaitingInput:
            {
                inputField.enabled = true;
                inputField.ActivateInputField();
            }
            break;
        }

        currentState = newState;
    }

    private void OnInputEntered(string text)
    {
        inputField.text = "";
        inputField.enabled = false;

        if (int.TryParse(text, out int textInt))
        {
            Desicions desicion = (Desicions)textInt;

            switch (desicion)
            {
                case Desicions.Continue:
                {
                    currentRound++;
                    ShowRoundMessage();
                    return;
                }
            }
        }
        
        messageText.text = $"Sorry, I didn't get that\n{decisionsStr}";
        ChangeState(GameStates.WaitingInput);
    }

    private void ShowRoundMessage()
    {
        ChangeState(GameStates.PrintingMessage);

        string narrativeStr = null;
        decisionsStr = "";

        if (currentRound == 0)
        {
            narrativeStr = "This is the first round. Live long and prosper";
        }
        else
        {
            narrativeStr = $"Round number {currentRound}";
        }

        decisionsStr = "These are your options: (1) Continue.";

        // NOTE: This does not makes much sense now the we only have one option,
        // but it will later on
        desicionsExpected.Clear();
        desicionsExpected.Add(Desicions.Continue);

        messageText.text = $"{narrativeStr}\n{decisionsStr}";

        ChangeState(GameStates.WaitingInput);
    }
}
