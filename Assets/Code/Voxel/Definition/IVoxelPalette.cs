using UnityEngine;


public interface IVoxelPalette
{
    public Color UndefinedColor { get; }
    public int Length { get; }


    public void SetPalette(Color[] palette);
    public Color GetColor(int voxelValue);
}