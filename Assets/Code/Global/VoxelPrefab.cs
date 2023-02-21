using UnityEngine;


public enum VoxelPrefabs
{
    TestHead8x8x8,
    TestBody10x12x5,
    TestArm4x12x4,
    Length
}


public static class VoxelPrefab
{
    private static IVolumeReadOnly<Color>[] _prefabs = new IVolumeReadOnly<Color>[(int)VoxelPrefabs.Length + 1];
    private static bool _isInited = false;


    private static void LoadPrefabs()
    {
        _prefabs[(int)VoxelPrefabs.TestHead8x8x8] = CreatePrefab(8, 8, 8, Color.white);
        _prefabs[(int)VoxelPrefabs.TestBody10x12x5] = CreatePrefab(10, 12, 8, Color.white);
        _prefabs[(int)VoxelPrefabs.TestArm4x12x4] = CreatePrefab(2, 2, 2, Color.white);

        _isInited = true;
    }

    private static IVolumeReadOnly<Color> CreatePrefab(int x, int y, int z, Color initColor)
    {
        ColorVolume prefab = new ColorVolume();

        prefab.Allocate(new Vector3Int(x,y,z), initColor);

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

        volume.SetVolumePrefabToBuild(GetPrefab(id));
        volume.Allocate();
        volume.MatchToPrefab();

        return volume;
    }
}