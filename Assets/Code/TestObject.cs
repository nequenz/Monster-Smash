using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
public class TestObject : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private VoxelBody _voxelBody = new();
    [SerializeField] private VoxelParticle _voxelParticlePrefab;


    public VoxelBody AttachedVoxelBody => _voxelBody;


    private void Awake()
    {
        _voxelBody.Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody body;

        if(collision.collider.Is<Projectile>())
        {
            //VoxelParticle part = Instantiate(_voxelParticlePrefab, collision.collider.transform.position, Quaternion.identity);
            
            _voxelBody.SetVoxel(collision.collider.transform.position, IVoxelVolume.Empty);

            //_voxelBody.CalibrateParticle(part, collision.collider.transform.position);

            if(AttachedVoxelBody.GetVoxel(collision.collider.transform.position) == IVoxelVolume.Empty)
            {
                body = collision.collider.GetComponent<Rigidbody>();
                Vector3 deepPosition = transform.position + body.velocity * _voxelBody.AttachedVoxelMesh.FaceSize * 2;

                _voxelBody.SetVoxel(deepPosition, IVoxelVolume.Empty);
            }
        }
    }

    private void Update()
    {
        _voxelBody.Update(Time.deltaTime);
    }


}