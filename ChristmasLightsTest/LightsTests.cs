using System.Reflection;
using ChristmasLights;

namespace ChristmasLightsTest;

public class Tests
{
    private LightsController _lightsController;

    [SetUp]
    public void Setup()
    {
        _lightsController = new LightsController(1000, 1000);
    }

    [Test]
    [TestCase(0, 0, 2, 2, 9)]
    [TestCase(0, 0, 999, 0, 1000)]
    [TestCase(499, 499, 500, 500, 4)]
    public void TestTurnOnSetOfLights(int coordsStartX, int coordsStartY,
        int coordsEndX, int coordsEndY, int numOfActiveLights)
    {
        Assert.That(_lightsController.TurnOnSet(
                new LightCoordinates(coordsStartX, coordsStartY),
                new LightCoordinates(coordsEndX, coordsEndY))
            , Is.EqualTo(numOfActiveLights));
    }

    [Test]
    [TestCase(499, 499, 500, 500, 0)]
    public void TestTurnOffSetOfLights(int coordsStartX, int coordsStartY,
        int coordsEndX, int coordsEndY, int numOfinactiveLights)
    {
        Assert.That(_lightsController.TurnOffSet(
                new LightCoordinates(coordsStartX, coordsStartY),
                new LightCoordinates(coordsEndX, coordsEndY))
            , Is.EqualTo(numOfinactiveLights));
    }

    [Test]
    [TestCase(0, 0, 2, 2, 9)]
    public void TestToggleSetOfLights(int coordsStartX, int coordsStartY,
        int coordsEndX, int coordsEndY, int numOfActiveLights)
    {
        Assert.That(_lightsController.ToggleSet(
                new LightCoordinates(coordsStartX, coordsStartY),
                new LightCoordinates(coordsEndX, coordsEndY))
            , Is.EqualTo(numOfActiveLights));
    }

    [Test]
    public void TestMultipleActions()
    {
        _lightsController.TurnOnSet(new LightCoordinates(887, 9),
            new LightCoordinates(959, 629));
        _lightsController.TurnOnSet(new LightCoordinates(454, 398),
            new LightCoordinates(844, 448));

        _lightsController.TurnOffSet(new LightCoordinates(539, 243),
            new LightCoordinates(559, 965));
        _lightsController.TurnOffSet(new LightCoordinates(370, 819),
            new LightCoordinates(676, 868));
        _lightsController.TurnOffSet(new LightCoordinates(145, 40),
            new LightCoordinates(370, 997));
        _lightsController.TurnOffSet(new LightCoordinates(301, 3),
            new LightCoordinates(808, 453));

        _lightsController.TurnOnSet(new LightCoordinates(351, 678),
            new LightCoordinates(951, 908));

        _lightsController.ToggleSet(new LightCoordinates(720, 196),
            new LightCoordinates(897, 994));
        _lightsController.ToggleSet(new LightCoordinates(831, 394),
            new LightCoordinates(904, 860));

        Assert.That(_lightsController.GetActiveLightsCount(), Is.EqualTo(230022));
    }

    [Test]
    public void TestValidInputs()
    {
        Assert.That(() => _lightsController.TurnOnSet(
                new LightCoordinates(10000, 10000),
                new LightCoordinates(1093203, 209123)),
            Throws.ArgumentException);

        Assert.That(() => _lightsController.TurnOffSet(
                new LightCoordinates(10000, 10000),
                new LightCoordinates(1093203, 209123)),
            Throws.ArgumentException);

        Assert.That(() => _lightsController.ToggleSet(
                new LightCoordinates(10000, 10000),
                new LightCoordinates(1093203, 209123)),
            Throws.ArgumentException);
        
        Assert.That(() => _lightsController.ToggleSet(
                new LightCoordinates(999, 1000),
                new LightCoordinates(1000, 999)),
            Throws.ArgumentException);

        // Sanity check for my bounds logic
        Assert.That(() => _lightsController.ToggleSet(
                new LightCoordinates(999, 999),
                new LightCoordinates(999, 999)),
            Is.EqualTo(1));
    }
}