using NUnit.Framework;

[TestFixture]
public class ResourceContainerTests
{
    
    [Test]
    [TestCase(100, 90)]
    [TestCase(100, 10)]
    [TestCase(45, 45)]
    public void ResourceContainer_ExtractResource_Success(int maxCap, int request)
    {
        var container = new ResourceContainer(maxCap, ResourceType.WOOD);

        ResourcePackage extracted = container.ExtractResources(request);
        
        Assert.AreEqual(request, extracted.Amount);
    }

    [Test]
    [TestCase(100, 190)]
    [TestCase(50, 100)]
    public void ResourceContainer_ExtractResource_Failure(int maxCap, int request)
    {
        var container = new ResourceContainer(maxCap, ResourceType.WOOD);

        ResourcePackage extracted = container.ExtractResources(request);

        Assert.AreEqual(extracted.Amount, 0);
    }

    [Test]
    [TestCase(50, 30, 30)]
    public void ResourceContainer_RegenResource_Success(int maxCap, int request, int regenAmount)
    {
        var container = new ResourceContainer(maxCap, ResourceType.WOOD, regenAmount);

        ResourcePackage extracted = container.ExtractResources(request);
        
        Assert.AreEqual(request, extracted);

        container.RegenResources();

        ResourcePackage extracted2 = container.ExtractResources(request);

        Assert.AreEqual(extracted2.Amount, request);
    }
}
