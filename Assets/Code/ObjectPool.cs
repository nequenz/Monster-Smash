using System;
using System.Collections.Generic;
using UnityEngine;

public struct Pool
{
    private MonoBehaviour[] _pool;
    private int _maxCount;
    private int _lastFreeIndex;


    public Pool(int maxCount)
    {
        _pool = new MonoBehaviour[maxCount];
        _maxCount = maxCount;
        _lastFreeIndex = 0;
    }

    public GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation)
    {
        //GameObject.Instantiate

        return null;
    }

    public Type GetPoolType()
    {
        return _pool.GetType();
    }
}

public static class ObjectPool 
{
    public const int ProjectileCountMax = 32;
    public const int VoxelParticleCountMax = 32;


    private static Projectile[] _projectilePool = new Projectile[ProjectileCountMax];
    private static VoxelParticle[] _voxelPool = new VoxelParticle[VoxelParticleCountMax];
    private static object[] _pools = 
    {
        _projectilePool,
        _voxelPool
    };

    public static T Instantiate<T>(Vector3 position, Quaternion rotation)
    {
        for(int i = 0; i < _pools.Length; i ++)
        {

        }

        return default;
    }
}


public static class PoolCoordinator
{
    private static List<List<object>> _pools = new();

    //TO-DO

    //public static Instantiate(GameObject original, Vector3 position, Quaternion rotation)
}