public interface IHarvestable 
{
    Resource Extract(int extractAmount = 1);
    bool IsEmpty();
}
