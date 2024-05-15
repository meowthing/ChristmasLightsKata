namespace ChristmasLights;

public class LightsController
{
    private readonly Light[,] _lightArray;

    public LightsController(int sizeX, int sizeY)
    {
        _lightArray = new Light[sizeX, sizeY];

        for (int i = 0; i < _lightArray.GetLength(0); i++)
        {
            for (int j = 0; j < _lightArray.GetLength(1); j++)
            {
                _lightArray[i, j] = new Light();
            }
        }
    }

    private bool IsValidInput((int X, int Y) coordsStart, (int X, int Y) coordsEnd)
    {
        if (coordsStart.X >= _lightArray.GetLength(0) ||
            coordsEnd.X >= _lightArray.GetLength(0) ||
            coordsStart.Y >= _lightArray.GetLength(1) ||
            coordsEnd.Y >= _lightArray.GetLength(1))
        {
            throw new ArgumentException("Specified coordinates are out of array bounds.");
        }

        return true;
    }

    private void IterateThroughLightSet((int X, int Y) coordsStart, (int X, int Y) coordsEnd, LightAction action)
    {
        if (!IsValidInput(coordsStart, coordsEnd))
            return;

        for (int i = coordsStart.X; i <= coordsEnd.X; i++)
        {
            for (int j = coordsStart.Y; j <= coordsEnd.Y; j++)
            {
                switch (action)
                {
                    case (LightAction.ON):
                        _lightArray[i, j].Active = true;
                        _lightArray[i, j].Brightness += 1;
                        break;
                    case (LightAction.OFF):
                        _lightArray[i, j].Active = false;
                        _lightArray[i, j].Brightness -= 1;
                        break;
                    case (LightAction.TOGGLE):
                        _lightArray[i, j].Active = !_lightArray[i, j].Active;
                        _lightArray[i, j].Brightness += 2;
                        break;
                }
            }
        }
    }

    public int GetActiveLightsCount()
    {
        int activeLights = 0;
        foreach (var light in _lightArray)
        {
            if (light.Active)
                activeLights++;
        }

        return activeLights;
    }

    public int GetTotalBrightness()
    {
        int totalBrightness = 0;
        foreach (var light in _lightArray)
        {
            totalBrightness += light.Brightness;
        }

        return totalBrightness;
    }

    public int TurnOnSet((int, int) coordsStart, (int, int) coordsEnd)
    {
        IterateThroughLightSet(coordsStart, coordsEnd, LightAction.ON);
        return GetActiveLightsCount();
    }

    public int TurnOffSet((int, int) coordsStart, (int, int) coordsEnd)
    {
        IterateThroughLightSet(coordsStart, coordsEnd, LightAction.OFF);
        return GetActiveLightsCount();
    }

    public int ToggleSet((int, int) coordsStart, (int, int) coordsEnd)
    {
        IterateThroughLightSet(coordsStart, coordsEnd, LightAction.TOGGLE);
        return GetActiveLightsCount();
    }
}