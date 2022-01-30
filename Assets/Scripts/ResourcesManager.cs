using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] resourceText;

    private Dictionary<ResourceType, int> resourcesDict = new Dictionary<ResourceType, int>();
    
    private void Awake()
    {
        resourcesDict.Add(ResourceType.Food, 0);
        resourcesDict.Add(ResourceType.CryptoUnits, 700);
        resourcesDict.Add(ResourceType.EnergyCatridges, 0);
        resourcesDict.Add(ResourceType.SpareParts, 0);
        resourcesDict.Add(ResourceType.MedicalSupplies, 0);
        resourcesDict.Add(ResourceType.Sanity, 0);

        for (int i = 0; i < (int)ResourceType.Total; i++)
        {
            var resourceType = (ResourceType)i;
            resourceText[(int)resourceType].text = $"{resourceType.ToString()}: {resourcesDict[resourceType]}";
        }
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        resourcesDict[resourceType] += amount;

        resourceText[(int)resourceType].text = $"{resourceType.ToString()}: {resourcesDict[resourceType]}";
    }

    public void RemoveResource(ResourceType resourceType, int amount)
    {
        resourcesDict[resourceType] -= amount;

        resourceText[(int)resourceType].text = $"{resourceType.ToString()}: {resourcesDict[resourceType]}";
    }

    public int GetResourceAmount(ResourceType resourceType)
    {
        return resourcesDict[resourceType];
    }
}