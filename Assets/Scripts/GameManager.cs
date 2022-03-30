using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        None,
        Loading,
        PrintingMessage,
        WaitingInput
    }
    
    private TerminalController _terminalController;
    private ResourcesManager _resourcesManager;

    private GameStates _currentState = GameStates.None;

    private int _currentRound = 0;

    private PurchaseResourceEvent[] _purchaseEvents = null;
    private int _currentResourcePurchase = 0;

    private void Awake()
    {
        _terminalController = GetComponent<TerminalController>();
        _terminalController.InputReceievedCallback = InputReceived;

        _resourcesManager = GetComponent<ResourcesManager>();

        _purchaseEvents = new PurchaseResourceEvent[]
        {
            new PurchaseResourceEvent(ResourceType.Food, _resourcesManager),
            new PurchaseResourceEvent(ResourceType.Ammo, _resourcesManager),
            new PurchaseResourceEvent(ResourceType.SpareParts, _resourcesManager),
            new PurchaseResourceEvent(ResourceType.MedicalSupplies, _resourcesManager)
        };

        ChangeState(GameStates.Loading); 
    }

    private void Start()
    {
        StartRound(0);
    }

    private void Update()
    {
        if (_currentRound > 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRound(1);
        }
    }

    private void ChangeState(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.None:
            case GameStates.Loading:
            case GameStates.PrintingMessage:
            {
                _terminalController.DisableInput();
            }
            break;
            
            case GameStates.WaitingInput:
            {
               _terminalController.EnableInput();
            }
            break;
        }

        _currentState = newState;
    }

    private void InputReceived(string textInput)
    {
        bool wrongInput = false;
        string retryMessage = "Sorry, I didn't get that.";

        switch (_currentRound)
        {
            // Purchase Resources
            case 1:
            {
                EventResult result = _purchaseEvents[_currentResourcePurchase].ProcessInput(textInput);

                if (result.Success)
                {
                    _currentResourcePurchase++;

                    if (_currentResourcePurchase == _purchaseEvents.Length)
                    {
                        StartRound(_currentRound + 1);
                    }
                    else
                    {
                        ShowRoundMessage();
                    }
                }
                else
                {
                    wrongInput = true;

                    if (!string.IsNullOrEmpty(result.Message))
                    {
                        retryMessage = result.Message;
                    }
                }
            }
            break;
        }

        if (wrongInput)
        {
            _terminalController.ShowMessage(retryMessage);
            ChangeState(GameStates.WaitingInput);
        }
    }

    private void StartRound(int roundNumber)
    {
        _currentRound = roundNumber;

        if (_currentRound == 1)
        {
            _currentResourcePurchase = 0;
        }

        ShowRoundMessage();
    }

    private void ShowRoundMessage()
    {
        ChangeState(GameStates.PrintingMessage);

        switch (_currentRound)
        {
            // Game Intro
            case 0:
            {
                _terminalController.ShowMessage("This is the first round. Live long and prosper. Press Enter.");
                return;
            }
            
            // Purchase Resources
            case 1:
            {
               _terminalController.ShowMessage(_purchaseEvents[_currentResourcePurchase].GetMessage());
            }
            break;

            // Food = 0,
            // Money,
            // Ammo,
            // SpareParts, 
            // MedicalSupplies,

            default:
            {
                _terminalController.ShowMessage($"Round number {_currentRound}");
            }
            break;
        }

        ChangeState(GameStates.WaitingInput);
    }
}
