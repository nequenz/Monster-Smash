using UnityEngine;


public struct VoxelData
{
    private byte _x;
    private byte _y;
    private byte _z;
    private Color _color;


    public byte X => _x;
    public byte Y => _y;
    public byte Z => _z;
    public Color Color => _color;


    public VoxelData(byte x, byte y, byte z, Color color)
    {
        _x = x;
        _y = y;
        _z = z;
        _color = color;
    }
}


public enum VoxelPrefabs
{
    TestPlane8x1x8,
    TestPlane16x1x16,
    TestPlane32x1x32,
    TestPlane8x1x16,
    Length
}

public static partial class VoxelModels
{
    private static IVolumeReadOnly<Color>[] _prefabs = new IVolumeReadOnly<Color>[(int)VoxelPrefabs.Length + 1];
    private static bool _isInited = false;



    private static void LoadPrefabs()
    {
        _prefabs[(int)VoxelPrefabs.TestPlane8x1x8] = CreatePrefab(8, 1, 8, Color.white);
        _prefabs[(int)VoxelPrefabs.TestPlane16x1x16] = CreatePrefab(16, 1, 16, Color.white);
        _prefabs[(int)VoxelPrefabs.TestPlane32x1x32] = CreatePrefab(32, 1, 32, Color.white);
        _prefabs[(int)VoxelPrefabs.TestPlane8x1x16] = CreatePrefab(8, 1, 16, Color.white);

        _isInited = true;
    }

    private static IVolumeReadOnly<Color> CreatePrefab(int xSize, int ySize, int zSize, Color initColor)
    {
        ColorVolume prefab = new ColorVolume();

        prefab.Allocate(new Vector3Int(xSize,ySize,zSize), initColor);

        return new VolumeReadOnly<Color>(prefab);
    }

    public static IVolumeReadOnly<Color> GetPrefab(VoxelPrefabs id)
    {
        if (_isInited == false)
            LoadPrefabs();

        return _prefabs[(int)id];
    }

    public static IVoxelVolume GetMatchedVoxels(VoxelPrefabs id)
    {
        IVoxelVolume volume = new VoxelVolume();

        volume.SetVolumeModelToBuild(GetPrefab(id));
        volume.Allocate();
        volume.MatchToModel();

        return volume;
    }
}