public class ResourceContainer
{
    public ResourceContainer(int cap, int regen)
    {
        resourceCap = amount = cap;
        regenAmount = regen;
    }

    int amount;
    int resourceCap;
    int regenAmount;
    public bool IsEmpty { get { return amount == 0; } }

    /// <summary>
    /// Changes the amoutn stored in the resource
    /// </summary>
    /// <param name="val">value of change</param>
    /// <param name="subtract">whether we are subtracting or adding</param>
    /// <returns>if -1 is returned then something went wrong</returns>
    int ChangeAmount(int val, bool subtract = false)
    {
        if (subtract)
        {
            int postWithdraw = amount - val;
            if (postWithdraw < 0)
                return -1;//returning -1 because it should never be negative
            amount = postWithdraw;
        }
        else 
        {
            int postRegen = amount + val;
            if (postRegen > resourceCap)
                return -1;
            amount = postRegen;
        }

       return val;
    }

    public void RegenResources() 
    {
        ChangeAmount(regenAmount, false);
    }

    public int RetrieveResources(int requested)
    {
        int foo, fin;
        if (!IsEmpty || requested != 0)
        {
            foo = ChangeAmount(requested, true);

            fin = foo == -1 ? 0 : foo;
        }
        else 
        {
            fin = 0;
        }

        
        return fin;
    }


}
