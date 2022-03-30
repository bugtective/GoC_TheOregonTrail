using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _resourceText;

    private Dictionary<ResourceType, int> _currentState = new Dictionary<ResourceType, int>();
    
    private void Awake()
    {
        _currentState.Add(ResourceType.Food, 0);
        _currentState.Add(ResourceType.Money, 700);
        _currentState.Add(ResourceType.Ammo, 0);
        _currentState.Add(ResourceType.SpareParts, 0);
        _currentState.Add(ResourceType.MedicalSupplies, 0);

        for (int i = 0; i < (int)ResourceType.Total; i++)
        {
            var resourceType = (ResourceType)i;
            _resourceText[(int)resourceType].text = $"{resourceType.ToString()}: {_currentState[resourceType]}";
        }
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        _currentState[resourceType] += amount;

        _resourceText[(int)resourceType].text = $"{resourceType.ToString()}: {_currentState[resourceType]}";
    }

    public void RemoveResource(ResourceType resourceType, int amount)
    {
        _currentState[resourceType] -= amount;

        _resourceText[(int)resourceType].text = $"{resourceType.ToString()}: {_currentState[resourceType]}";
    }

    public int GetResourceAmount(ResourceType resourceType)
    {
        return _currentState[resourceType];
    }

    public bool HasEnoughMoney(int amount)
    {
        return _currentState[ResourceType.Money] >= amount;
    }
}