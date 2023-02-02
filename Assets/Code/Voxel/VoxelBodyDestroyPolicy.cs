using System;

public class VoxelBodyDestroyPolicy : IVoxelBodyDestroyPolicy
{
    public float MinimumDestroyPersent => throw new NotImplementedException();


    public event Action Destoryed;


    public void HandleChanges()
    {
        //TO-DO
    }

    public void SetMinimumDestroyPersent(float value)
    {
        //TO-DO
    }
}