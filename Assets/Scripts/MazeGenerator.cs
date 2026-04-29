using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace Pacman
{
    public class MazeGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject m_WallPrefab;
        [SerializeField] private GameObject m_PelletPrefab;
        [SerializeField] private GameObject m_FloorPrefab;


        [Header("Settings")]
        [SerializeField] private float m_CellSize = 2f;

        [Header("NavMesh")]
        [SerializeField] private NavMeshSurface m_NavMeshSurface;


        private readonly int[,] m_Map = new int[,]
        {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,1,0,0,0,0,0,1 },
            { 1,0,1,1,0,0,0,0,0,1,1,0,1 },
            { 1,0,1,0,0,1,0,1,0,0,1,0,1 },
            { 1,0,0,0,1,0,0,0,1,0,0,0,1 },
            { 1,0,1,0,0,0,1,0,0,0,1,0,1 },
            { 1,0,0,1,0,1,1,1,0,1,0,0,1 },
            { 1,1,0,0,0,0,1,0,0,0,0,1,1 },
            { 1,0,0,1,0,1,1,1,0,1,0,0,1 },
            { 1,0,1,0,0,0,1,0,0,0,1,0,1 },
            { 1,0,0,0,1,0,0,0,1,0,0,0,1 },
            { 1,0,1,0,0,1,0,1,0,0,1,0,1 },
            { 1,0,1,1,0,0,0,0,0,1,1,0,1 },
            { 1,0,0,0,0,0,1,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,1,1,1,1 },
        };

        private void Awake()
        {
            GenerateMaze();
            BakeNavMesh();
        }

        private void BakeNavMesh()
        {
            if (m_NavMeshSurface != null)
                m_NavMeshSurface.BuildNavMesh();
        }

        private void GenerateMaze()
        {
            int rows = m_Map.GetLength(0);
            int cols = m_Map.GetLength(1);

            float offsetX = -((float)cols * m_CellSize) / 2f;
            float offsetZ = -((float)rows * m_CellSize) / 2f;


            for(int row = 0; row < rows; row++)
            {
                for(int col = 0; col < cols; col++)
                {
                    float x = col * m_CellSize + offsetX;
                    float z = row * m_CellSize + offsetZ;

                    if(m_FloorPrefab != null)
                    {
                        Instantiate(m_FloorPrefab, new Vector3(x, 0f, z), 
                                    Quaternion.identity, transform);
                    }

                    if (m_Map[row, col] == 1)
                    {
                        if (m_WallPrefab != null)
                        {
                            Instantiate(m_WallPrefab, new Vector3(x, 1f, z), 
                                        Quaternion.identity, transform);
                        }
                    }
                    else
                    {
                        if (m_PelletPrefab != null)
                        {
                            Instantiate(m_PelletPrefab, new Vector3(x, 0.5f, z),
                                        Quaternion.identity, transform);
                        }
                    }
                }
            }
        }
    }
}
