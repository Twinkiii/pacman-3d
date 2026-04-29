using UnityEngine;

namespace Pacman
{
    public class MazeBuilder : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject m_WallPrefab;
        [SerializeField] private GameObject m_PelletPrefab;
        [SerializeField] private GameObject m_FloorPrefab;

        [Header("Settings")]
        [SerializeField] private float m_CellSize = 2f;

        public float CellSize => m_CellSize;

        public void Build(int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            float offsetX = -(cols * m_CellSize) / 2f;
            float offsetZ = -(rows * m_CellSize) / 2f;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    float x = col * m_CellSize + offsetX;
                    float z = row * m_CellSize + offsetZ;

                    SpawnFloor(x, z);

                    if (map[row, col] == 1)
                        SpawnWall(x, z);
                    else
                        SpawnPellet(x, z);
                }
            }
        }

        public Vector3 CellToWorld(Vector2Int cell, int cols, int rows)
        {
            float offsetX = -(cols * m_CellSize) / 2f;
            float offsetZ = -(rows * m_CellSize) / 2f;

            return new Vector3(
                cell.x * m_CellSize + offsetX,
                0f,
                cell.y * m_CellSize + offsetZ);
        }

        private void SpawnFloor(float x, float z)
        {
            if (m_FloorPrefab != null)
                Instantiate(m_FloorPrefab,
                    new Vector3(x, 0f, z),
                    Quaternion.identity, transform);
        }

        private void SpawnWall(float x, float z)
        {
            if (m_WallPrefab != null)
                Instantiate(m_WallPrefab,
                    new Vector3(x, 1f, z),
                    Quaternion.identity, transform);
        }

        private void SpawnPellet(float x, float z)
        {
            if (m_PelletPrefab != null)
                Instantiate(m_PelletPrefab,
                    new Vector3(x, 0.5f, z),
                    Quaternion.identity, transform);
        }
    }
}
