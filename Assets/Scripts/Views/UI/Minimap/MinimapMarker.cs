using UnityEngine;
using Pacman.Views.Collectibles;

namespace Pacman.Views.UI.Minimap
{
    public class MinimapMarker : MonoBehaviour
    {
        public enum MarkerType { Player, Enemy, Wall, Pellet }

        [Header("Marker Type")]
        [SerializeField] private MarkerType m_Type;

        [Header("Marker Materials")]
        [SerializeField] private Material m_PlayerMaterial;
        [SerializeField] private Material m_EnemyMaterial;
        [SerializeField] private Material m_WallMaterial;
        [SerializeField] private Material m_PelletMaterial;

        [Header("Marker Sizes")]
        [SerializeField] private float m_PlayerSize = 1.5f;
        [SerializeField] private float m_EnemySize = 1.2f;
        [SerializeField] private float m_WallSize = 2f;
        [SerializeField] private float m_PelletSize = 0.85f;

        [Header("Marker Height")]
        [SerializeField] private float m_MarkerHeight = 25f;

        private GameObject m_Marker;
        private Transform m_MarkerTransform;

        private void Start()
        {
            CreateMarker();

            if (m_Type == MarkerType.Pellet)
            {
                var collectible = GetComponent<CollectibleView>();
                if (collectible != null)
                    collectible.OnCollected += RemoveMarker;
            }
        }

        private void CreateMarker()
        {
            int layer = GetLayer();

            if (layer == -1)
            {
                Debug.LogError($"MinimapMarker: Layer NOT FOUND for type={m_Type}! Check Tags and Layers settings!");
                return;
            }

            Material markerMaterial = GetMaterial();
            if (markerMaterial == null)
            {
                Debug.LogError($"MinimapMarker: Material is not assigned for type={m_Type}! Assign it in the Inspector.");
                return;
            }

            m_Marker = GameObject.CreatePrimitive(PrimitiveType.Quad);
            m_Marker.name = $"MinimapMarker_{m_Type}";

            Destroy(m_Marker.GetComponent<Collider>());

            m_Marker.layer = layer;

            var renderer = m_Marker.GetComponent<Renderer>();
            renderer.sharedMaterial = markerMaterial;

            float size = GetSize();
            m_Marker.transform.localScale = new Vector3(size, size, size);
            m_Marker.transform.rotation = Quaternion.Euler(90, 0, 0);

            m_MarkerTransform = m_Marker.transform;

            Debug.Log($"MinimapMarker: Created {m_Type} on layer {layer}");
        }

        private void RemoveMarker()
        {
            if (m_Marker != null)
                Destroy(m_Marker);
        }

        private void LateUpdate()
        {
            if (m_MarkerTransform == null) return;

            m_MarkerTransform.position = new Vector3(
                transform.position.x,
                m_MarkerHeight,
                transform.position.z);
        }

        public void Initialise(MarkerType type)
        {
            m_Type = type;
        }

        private int GetLayer()
        {
            switch (m_Type)
            {
                case MarkerType.Player: return LayerMask.NameToLayer("MinimapPlayer");
                case MarkerType.Enemy: return LayerMask.NameToLayer("MinimapEnemy");
                case MarkerType.Wall: return LayerMask.NameToLayer("MinimapWall");
                case MarkerType.Pellet: return LayerMask.NameToLayer("MinimapPellet");
                default: return 0;
            }
        }

        private Material GetMaterial()
        {
            switch (m_Type)
            {
                case MarkerType.Player: return m_PlayerMaterial;
                case MarkerType.Enemy: return m_EnemyMaterial;
                case MarkerType.Wall: return m_WallMaterial;
                case MarkerType.Pellet: return m_PelletMaterial;
                default: return null;
            }
        }

        private float GetSize()
        {
            switch (m_Type)
            {
                case MarkerType.Player: return m_PlayerSize;
                case MarkerType.Enemy: return m_EnemySize;
                case MarkerType.Wall: return m_WallSize;
                case MarkerType.Pellet: return m_PelletSize;
                default: return 1f;
            }
        }

        private void OnDestroy()
        {
            if (m_Marker != null)
                Destroy(m_Marker);
        }
    }
}