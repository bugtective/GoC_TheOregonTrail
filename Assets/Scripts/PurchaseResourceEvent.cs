
public struct EventResult
{
    public bool Success;
    public string Message;
}

public class PurchaseResourceEvent
{
    private ResourceType _resourceType;
    private ResourcesManager _resourcesManager;

    public PurchaseResourceEvent(ResourceType resourceType, ResourcesManager resourcesManager)
    {
        _resourceType = resourceType;
        _resourcesManager = resourcesManager;
    }

    public string GetMessage()
    {
        return $"How much do you want to spend on your {_resourceType.ToString()}";
    }

    public EventResult ProcessInput(string input)
    {
        EventResult result = new EventResult();

        if (int.TryParse(input, out int moneyToSpend))
        {
            if (_resourcesManager.HasEnoughMoney(moneyToSpend))
            {
                _resourcesManager.RemoveResource(ResourceType.Money, moneyToSpend);

                // TODO: Calculate costs
                int amount = moneyToSpend;

                _resourcesManager.AddResource(_resourceType, amount);

                result.Success = true;
            }
            else
            {
                result.Message = $"No enough money!\n{GetMessage()}";
            }
        }

        return result;
    }
}