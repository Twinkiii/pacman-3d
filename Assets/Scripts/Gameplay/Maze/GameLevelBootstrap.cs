using UnityEngine;
using Unity.AI.Navigation;
using Pacman.Gameplay.Spawning;
using System.Collections;

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

            StartCoroutine(InitMinimapNextFrame());
        }

        private IEnumerator InitMinimapNextFrame()
        {

            yield return null;


            var minimapCam = GameObject.Find("MinimapCamera")
                ?.GetComponent<Camera>();

            if (minimapCam != null)
            {
                minimapCam.enabled = false;
                minimapCam.enabled = true;
                Debug.Log("MinimapCamera restarted after spawn!");
            }
        }
    }
}