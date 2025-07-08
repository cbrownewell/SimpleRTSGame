public interface IHarvestable 
{
    ResourcePackage Extract(int extractAmount = 1);
    bool IsEmpty();
}
