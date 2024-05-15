namespace ChristmasLights;

public class LightsController
{
    public Light[,] LightArray;

    public LightsController(int sizeX, int sizeY)
    {
        LightArray = new Light[sizeX, sizeY];

        for (int i = 0; i < LightArray.GetLength(0); i++)
        {
            for (int j = 0; j < LightArray.GetLength(1); j++)
            {
                LightArray[i, j] = new Light();
            }
        }
    }

    private bool IsValidInput((int X, int Y) coordsStart, (int X, int Y) coordsEnd)
    {
        if (coordsStart.X >= LightArray.GetLength(0) ||
            coordsEnd.X >= LightArray.GetLength(0) ||
            coordsStart.Y >= LightArray.GetLength(1) ||
            coordsEnd.Y >= LightArray.GetLength(1))
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
                        LightArray[i, j].Active = true;
                        LightArray[i, j].Brightness += 1;
                        break;
                    case (LightAction.OFF):
                        LightArray[i, j].Active = false;
                        LightArray[i, j].Brightness -= 1;
                        break;
                    case (LightAction.TOGGLE):
                        LightArray[i, j].Active = !LightArray[i, j].Active;
                        LightArray[i, j].Brightness += 2;
                        break;
                }
            }
        }

        return;
    }

    public int GetActiveLightsCount()
    {
        int activeLights = 0;
        foreach (var light in LightArray)
        {
            if (light.Active)
                activeLights++;
        }

        return activeLights;
    }

    public int GetTotalBrightness()
    {
        int totalBrightness = 0;
        foreach (var light in LightArray)
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