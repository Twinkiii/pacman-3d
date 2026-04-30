using System.Collections.Generic;
using UnityEngine;


namespace Pacman
{
    public class EntitySpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject m_PlayerPrefab;
        [SerializeField] private GameObject m_DirectChasePrefab;
        [SerializeField] private GameObject m_PatrolChasePrefab;
        [SerializeField] private GameObject m_AmbushPrefab;

        private List<Vector2Int> m_FreeCells;
        private int m_Width;
        private int m_Height;
        private float m_CellSize;
        private MazeBuilder m_Builder;

        public void Initialise(
            List<Vector2Int> freeCells,
            int width, int height,
            MazeBuilder builder)
        {
            m_FreeCells = freeCells;
            m_Width = width;
            m_Height = height;
            m_Builder = builder;
            m_CellSize = builder.CellSize;
        }

        public void SpawnAll()
        {
            SpawnPlayer();
            SpawnAmbushEnemy();
            SpawnDirectChaseEnemy();
            SpawnPatrolChaseEnemy();
        }

        private void SpawnPlayer()
        {
            if (m_PlayerPrefab == null) return;

            Vector2Int center = new Vector2Int(m_Width / 2, m_Height / 2);
            Vector2Int cell = GetNearestFreeCell(center);
            Vector3 pos = CellToWorld(cell);
            pos.y = 1f;

            var player = Instantiate(m_PlayerPrefab, pos, Quaternion.identity);
            var marker = player.AddComponent<MinimapMarker>();
        }

        private void SpawnAmbushEnemy()
        {
            if (m_AmbushPrefab == null) return;

            Vector2Int[] corners = {
                new Vector2Int(1, 1),
                new Vector2Int(m_Width - 2, 1),
                new Vector2Int(1, m_Height - 2),
                new Vector2Int(m_Width - 2, m_Height - 2)
            };

            Vector2Int corner = corners[Random.Range(0, corners.Length)];
            Vector2Int cell = GetNearestFreeCell(corner);
            Vector3 pos = CellToWorld(cell);
            pos.y = 1f;

            var enemy = Instantiate(m_AmbushPrefab, pos, Quaternion.identity);
            enemy.AddComponent<MinimapMarker>();
        }

        private void SpawnDirectChaseEnemy()
        {
            if (m_DirectChasePrefab == null) return;

            Vector2Int cell = GetFarFreeCell();
            Vector3 pos = CellToWorld(cell);
            pos.y = 1f;

            var enemy = Instantiate(m_DirectChasePrefab, pos, Quaternion.identity);
            enemy.AddComponent<MinimapMarker>();
        }

        private void SpawnPatrolChaseEnemy()
        {
            if (m_PatrolChasePrefab == null) return;

            Vector2Int cell = GetRandomFreeCell();
            Vector3 pos = CellToWorld(cell);
            pos.y = 1f;

            var enemy = Instantiate(m_PatrolChasePrefab, pos, Quaternion.identity);
            enemy.AddComponent<MinimapMarker>();

            var patrol = enemy.GetComponent<PatrolChaseEnemy>();
            if (patrol != null)
                patrol.SetPatrolPoints(GeneratePatrolPoints(4));
        }

        private Transform[] GeneratePatrolPoints(int count)
        {
            var points = new Transform[count];
            for (int i = 0; i < count; i++)
            {
                Vector2Int cell = GetRandomFreeCell();
                Vector3 pos = CellToWorld(cell);
                pos.y = 0.5f;

                var point = new GameObject($"PatrolPoint_{i}");
                point.transform.position = pos;
                point.transform.SetParent(transform);
                points[i] = point.transform;
            }
            return points;
        }

        private Vector3 CellToWorld(Vector2Int cell)
        {
            return m_Builder.CellToWorld(cell, m_Width, m_Height);
        }

        private Vector2Int GetNearestFreeCell(Vector2Int target)
        {
            Vector2Int best = m_FreeCells[0];
            float bestDist = float.MaxValue;

            foreach (var cell in m_FreeCells)
            {
                float dist = Vector2Int.Distance(cell, target);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = cell;
                }
            }
            return best;
        }

        private Vector2Int GetFarFreeCell()
        {
            Vector2Int center = new Vector2Int(m_Width / 2, m_Height / 2);
            Vector2Int best = m_FreeCells[0];
            float bestDist = 0f;

            foreach (var cell in m_FreeCells)
            {
                float dist = Vector2Int.Distance(cell, center);
                if (dist > bestDist)
                {
                    bestDist = dist;
                    best = cell;
                }
            }
            return best;
        }

        private Vector2Int GetRandomFreeCell()
        {
            return m_FreeCells[Random.Range(0, m_FreeCells.Count)];
        }
    }
}