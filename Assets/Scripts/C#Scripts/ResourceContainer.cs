public class ResourceContainer
{
    public ResourceContainer(int cap, ResourceType resourceType, int regen = 0)
    {
        _resourceCap = _amount = cap;
        _regenAmount = regen;
        _resourceType = resourceType;
    }

    int _amount;
    int _resourceCap;
    int _regenAmount;
    ResourceType _resourceType;
    public bool IsEmpty { get { return _amount == 0; } }

    /// <summary>
    /// Changes the amount stored in the resource
    /// </summary>
    /// <param name="val">value of change</param>
    /// <param name="subtract">whether we are subtracting or adding</param>
    /// <returns>if -1 is returned then something went wrong</returns>
    int ChangeAmount(int val, bool subtract = false)
    {
        if (subtract)
        {
            int postWithdraw = _amount - val;
            if (postWithdraw < 0)
                return -1;//returning -1 because it should never be negative
            _amount = postWithdraw;
        }
        else 
        {
            int postRegen = _amount + val;
            if (postRegen > _resourceCap)
                return -1;
            _amount = postRegen;
        }

       return val;
    }

    /// <summary>
    /// Restore resources
    /// </summary>
    public void RegenResources() 
    {
        ChangeAmount(_regenAmount, false);
    }

    /// <summary>
    /// Extract Resources from container
    /// </summary>
    /// <param name="requested"></param>
    /// <returns></returns>
    public ResourcePackage ExtractResources(int requested)
    {
        int newValue, finalAmount;
        if (!IsEmpty || requested != 0)
        {
            newValue = ChangeAmount(requested, true);

            finalAmount = newValue == -1 ? 0 : newValue;
        }
        else 
        {
            finalAmount = 0;
        }

        return new ResourcePackage(_resourceType, finalAmount);
    }


}
