namespace ChristmasLights;

public record Light
{
    public bool Active { get; set; }

    private int _brightness = 0;

    public int Brightness
    {
        get => _brightness;
        set => _brightness = value < 0
            ? 0
            : value;
    }
}