using UnityEngine;

public class VoxelPaletteDefault : IVoxelPalette
{
    private Color[] _colors = new Color[] 
    {
        Color.white,
        Color.black,
        Color.cyan,
        Color.red,
        Color.blue,
        Color.green
    };


    public int Length => _colors.Length;
    public Color UndefinedColor => Color.red;


    public Color GetColor(int voxelValue)
    {
        if( voxelValue >= 0 && voxelValue < Length)
            return _colors[voxelValue];

        return UndefinedColor;
    }

    public void SetPalette(Color[] palette) => _colors = palette;
}