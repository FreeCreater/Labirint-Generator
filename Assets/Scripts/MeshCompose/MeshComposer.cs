using UnityEngine;

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshComposer : MonoBehaviour
    {
        public MeshFilter MyFilter;
        public MeshRenderer MyRenderer;
        public MeshFilter[] InternalFilters;
        public Material SharedMaterial;
        [SerializeField] private bool _useMeshCollider;


        public void Compose()
        {
            var combineMeshes = new CombineInstance[InternalFilters.Length];
            for (int i = 0; i < combineMeshes.Length; i++)
            {
                combineMeshes[i].mesh = InternalFilters[i].mesh;
                combineMeshes[i].transform = InternalFilters[i].transform.localToWorldMatrix;
                InternalFilters[i].gameObject.SetActive(false);
            }

            MyFilter.mesh = new Mesh();
            MyFilter.mesh.CombineMeshes(combineMeshes, true, true, false);
            MyRenderer.material = SharedMaterial;

            if (_useMeshCollider)
            {
                var collider = gameObject.AddComponent<MeshCollider>();
                collider.sharedMesh = MyFilter.mesh;
            }
        }
    }
