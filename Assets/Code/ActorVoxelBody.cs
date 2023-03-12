using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
public class ActorVoxelBody : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private VoxelBody _voxelBody = new();
    [SerializeField] private VoxelParticle _voxelParticlePrefab;


    public VoxelBody AttachedVoxelBody => _voxelBody;


    private void Awake()
    {
        _voxelBody.Init();
    }

    private void OnEnable()
    {
        _voxelBody.AttachedVolume.Changed += OnVoxelBodyChanged;
    }

    private void OnDisable()
    {
        _voxelBody.AttachedVolume.Changed -= OnVoxelBodyChanged;
    }

    private void Update()
    {
        _voxelBody.Update(Time.deltaTime);
    }

    private void OnVoxelBodyChanged(Vector3Int position)
    {
        Vector3 worldPosition = _voxelBody.AttachedTransform.CalculateWorldPosition(position);
        VoxelParticle particle = Instantiate(_voxelParticlePrefab, worldPosition, Quaternion.identity);

        _voxelBody.CalibrateParticle(particle, position);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube
    }

}