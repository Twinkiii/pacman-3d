using System;
using UnityEngine;

namespace Pacman
{
    public class MinimapMarker : MonoBehaviour
    {
        public enum MarkerType { Player, Enemy, Wall, Pellet }

        [SerializeField] private MarkerType m_Type;
        [SerializeField] private Color m_PlayerColor = Color.green;
        [SerializeField] private Color m_EnemyColor = Color.red;
        [SerializeField] private Color m_WallColor = Color.blue;
        [SerializeField] private Color m_PelletColor = Color.yellow;

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
            
            m_Marker = GameObject.CreatePrimitive(PrimitiveType.Quad);
            m_Marker.name = $"MinimapMarker_{m_Type}";

            
            Destroy(m_Marker.GetComponent<Collider>());

            
            m_Marker.layer = GetLayer();

            
            var renderer = m_Marker.GetComponent<Renderer>();
            var mat = new Material(Shader.Find("Unlit/Color"));
            mat.color = GetColor();
            renderer.material = mat;

            
            float size = GetSize();
            m_Marker.transform.localScale = new Vector3(size, size, size);

            
            m_Marker.transform.rotation = Quaternion.Euler(90, 0, 0);

            m_MarkerTransform = m_Marker.transform;
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
                25f, 
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

        private Color GetColor()
        {
            switch (m_Type)
            {
                case MarkerType.Player: return m_PlayerColor;
                case MarkerType.Enemy: return m_EnemyColor;
                case MarkerType.Wall: return m_WallColor;
                case MarkerType.Pellet: return m_PelletColor;
                default: return Color.white;
            }
        }

        private float GetSize()
        {
            switch (m_Type)
            {
                case MarkerType.Player: return 1.5f;
                case MarkerType.Enemy: return 1.2f;
                case MarkerType.Wall: return 2f;
                case MarkerType.Pellet: return 0.85f;
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