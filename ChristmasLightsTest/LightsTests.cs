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
                (coordsStartX, coordsStartY),
                (coordsEndX, coordsEndY))
            , Is.EqualTo(numOfActiveLights));
    }

    [Test]
    [TestCase(499, 499, 500, 500, 0)]
    public void TestTurnOffSetOfLights(int coordsStartX, int coordsStartY,
        int coordsEndX, int coordsEndY, int numOfinactiveLights)
    {
        Assert.That(_lightsController.TurnOffSet(
                (coordsStartX, coordsStartY),
                (coordsEndX, coordsEndY))
            , Is.EqualTo(numOfinactiveLights));
    }

    [Test]
    [TestCase(0, 0, 2, 2, 9)]
    public void TestToggleSetOfLights(int coordsStartX, int coordsStartY,
        int coordsEndX, int coordsEndY, int numOfActiveLights)
    {
        Assert.That(_lightsController.ToggleSet(
                (coordsStartX, coordsStartY),
                (coordsEndX, coordsEndY))
            , Is.EqualTo(numOfActiveLights));
    }

    [Test]
    public void TestMultipleActions()
    {
        _lightsController.TurnOnSet((887, 9),
            (959, 629));
        _lightsController.TurnOnSet((454, 398),
            (844, 448));

        _lightsController.TurnOffSet((539, 243),
            (559, 965));
        _lightsController.TurnOffSet((370, 819),
            (676, 868));
        _lightsController.TurnOffSet((145, 40),
            (370, 997));
        _lightsController.TurnOffSet((301, 3),
            (808, 453));

        _lightsController.TurnOnSet((351, 678),
            (951, 908));

        _lightsController.ToggleSet((720, 196),
            (897, 994));
        _lightsController.ToggleSet((831, 394),
            (904, 860));

        Assert.That(_lightsController.GetActiveLightsCount(), Is.EqualTo(230022));
    }

    [Test]
    public void TestValidInputs()
    {
        Assert.That(() => _lightsController.TurnOnSet(
                (10000, 10000),
                (1093203, 209123)),
            Throws.ArgumentException);

        Assert.That(() => _lightsController.TurnOffSet(
                (10000, 10000),
                (1093203, 209123)),
            Throws.ArgumentException);

        Assert.That(() => _lightsController.ToggleSet(
                (10000, 10000),
                (1093203, 209123)),
            Throws.ArgumentException);

        Assert.That(() => _lightsController.ToggleSet(
                (999, 1000),
                (1000, 999)),
            Throws.ArgumentException);

        // Sanity check for my bounds logic
        Assert.That(() => _lightsController.ToggleSet(
                (999, 999),
                (999, 999)),
            Is.EqualTo(1));
    }

    [Test]
    public void TotalBrightnessAfterTurnOn()
    {
        _lightsController.TurnOnSet((0, 0), (0, 0));
        Assert.That(_lightsController.GetTotalBrightness(), Is.EqualTo(1));
    }

    [Test]
    public void TotalBrightnessAfterToggle()
    {
        _lightsController.ToggleSet((0, 0), (999, 999));
        Assert.That(_lightsController.GetTotalBrightness(), Is.EqualTo(2000000));
    }
    
    [Test]
    public void BrightnessShouldBeNoLessThanZero()
    {
        _lightsController.TurnOnSet((0, 0), (0, 0));
        _lightsController.TurnOffSet((0, 0), (0, 0));
        _lightsController.TurnOffSet((0, 0), (0, 0));
        Assert.That(_lightsController.GetTotalBrightness(), Is.EqualTo(0));
    }
}