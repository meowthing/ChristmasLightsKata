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

    private bool IsValidInput(LightCoordinates coordsStart, LightCoordinates coordsEnd)
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

    private void IterateThroughLightSet(LightCoordinates coordsStart, LightCoordinates coordsEnd, LightAction action)
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
                        LightArray[i, j].active = true;
                        break;
                    case (LightAction.OFF):
                        LightArray[i, j].active = false;
                        break;
                    case (LightAction.TOGGLE):
                        LightArray[i, j].active = !LightArray[i, j].active;
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
            if (light.active)
                activeLights++;
        }

        return activeLights;
    }

    public int TurnOnSet(LightCoordinates coordsStart, LightCoordinates coordsEnd)
    {
        IterateThroughLightSet(coordsStart, coordsEnd, LightAction.ON);
        return GetActiveLightsCount();
    }

    public int TurnOffSet(LightCoordinates coordsStart, LightCoordinates coordsEnd)
    {
        IterateThroughLightSet(coordsStart, coordsEnd, LightAction.OFF);
        return GetActiveLightsCount();
    }

    public int ToggleSet(LightCoordinates coordsStart, LightCoordinates coordsEnd)
    {
        IterateThroughLightSet(coordsStart, coordsEnd, LightAction.TOGGLE);
        return GetActiveLightsCount();
    }
}