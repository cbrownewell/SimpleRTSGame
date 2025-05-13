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
        var container = new ResourceContainer(maxCap, 0);

        int extracted = container.RetrieveResources(request);
        
        Assert.AreEqual(request, extracted);
    }

    [Test]
    [TestCase(100, 190)]
    [TestCase(50, 100)]
    public void ResourceContainer_ExtractResource_Failure(int maxCap, int request)
    {
        var container = new ResourceContainer(maxCap, 0);

        int extracted = container.RetrieveResources(request);

        Assert.AreEqual(extracted, 0);
    }

    [Test]
    [TestCase(50, 30, 30)]
    public void ResourceContainer_RegenResource_Success(int maxCap, int request, int regenAmount)
    {
        var container = new ResourceContainer(maxCap, regenAmount);

        int extracted = container.RetrieveResources(request);
        
        Assert.AreEqual(request, extracted);

        container.RegenResources();

        int extracted2 = container.RetrieveResources(request);

        Assert.AreEqual(extracted2, request);
    }
}
