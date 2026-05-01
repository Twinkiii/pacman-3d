using UnityEngine;
using Unity.AI.Navigation;
using Pacman.Gameplay.Spawning;

namespace Pacman.Gameplay.Maze
{
    public class GameLevelBootstrap : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private MazeBuilder m_MazeBuilder;
        [SerializeField] private EntitySpawner m_EntitySpawner;
        [SerializeField] private NavMeshSurface m_NavMeshSurface;

        [Header("Settings")]
        [SerializeField] private int m_Width = 13;
        [SerializeField] private int m_Height = 13;

        private void Awake()
        {
            
            var generator = new MazeGenerator(m_Width, m_Height);
            generator.Generate();

            
            m_MazeBuilder.Build(generator.Map);

            
            m_NavMeshSurface?.BuildNavMesh();

            
            m_EntitySpawner.Initialise(
                generator.FreeCells,
                m_Width, m_Height,
                m_MazeBuilder);

            m_EntitySpawner.SpawnAll();
        }
    }
}