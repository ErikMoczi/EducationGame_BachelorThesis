using Bachelor.Game.Controllers.GM.Base;
using Bachelor.Game.Controllers.Spawn.Base;
using Bachelor.MyExtensions.Managers;
using Bachelor.Utilities.Unity;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bachelor.Game.Controllers
{
    public class SpawnController : MonoBehaviour, INotification, IGameObjectPool
    {
        [SerializeField]
        private RandomPercentageGenerator m_RandomPercentageGenerator;

        [SerializeField]
        private Vector2 m_SpawnPositionX = new Vector2(-8f, 8f);

        [SerializeField]
        private int m_StartingHazardCount = 1;

        [SerializeField]
        private Vector2 m_IncrementHazardCount = new Vector2(0.1f, 0.25f);

        [SerializeField]
        private Vector2 m_IncrementHazzardWait = new Vector2(5f, 10f);

        [SerializeField]
        private float m_StartWait = 1f;

        [SerializeField]
        private Vector2 m_SpawnWait = new Vector2(0.5f, 1.25f);

        [SerializeField]
        private Vector2 m_WaveWait = new Vector2(1f, 1.5f);

        [SerializeField]
        [ReadOnlyWhenPlaying]
        private float m_CurrentHazzardCount = 0f;

        private Vector3 m_SpawnPositionObject;
        private bool m_Spawning = false;

        private void OnEnable()
        {
            this.AddObserver(OnStartSpawn, PlayingState.GamePlayingNotification);
            this.AddObserver(OnEndSpawn, EndingState.GameEndingNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnStartSpawn, PlayingState.GamePlayingNotification);
            this.RemoveObserver(OnEndSpawn, EndingState.GameEndingNotification);
        }

        private void Awake()
        {
            m_SpawnPositionObject = transform.position;
            m_CurrentHazzardCount = m_StartingHazardCount;            
        }

        private IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(m_StartWait);
            while (m_Spawning)
            {
                for (int i = 0; i < (int)m_CurrentHazzardCount; i++)
                {                    
                    GameObject randomHazard = m_RandomPercentageGenerator.GetItem();
                    Vector3 randomSpawnPosition = new Vector3(Random.Range(m_SpawnPositionX.x, m_SpawnPositionX.y), m_SpawnPositionObject.y, m_SpawnPositionObject.z);
                    this.Spawn(this.FindPoolName(randomHazard), randomSpawnPosition);
                    yield return new WaitForSeconds(Random.Range(m_SpawnWait.x, m_SpawnWait.y));
                }
                yield return new WaitForSeconds(Random.Range(m_WaveWait.x, m_WaveWait.y));
            }
        }

        private IEnumerator CalculateHazzardCount()
        {
            while (m_Spawning)
            {
                yield return new WaitForSeconds(Random.Range(m_IncrementHazzardWait.x, m_IncrementHazzardWait.y));
                m_CurrentHazzardCount += Random.Range(m_IncrementHazardCount.x, m_IncrementHazardCount.y);
            }            
        }

        private void SpawnCycle()
        {
            StartCoroutine(CalculateHazzardCount());
            StartCoroutine(SpawnWaves());
        }

        private void OnStartSpawn(object arg1, object arg2)
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            if (!m_Spawning)
            {
                m_Spawning = true;
                SpawnCycle();
            }
        }

        private void StopSpawn(bool stopImmediately = false)
        {
            if (stopImmediately)
            {
                StopAllCoroutines();
            }
            m_Spawning = false;
        }

        private void OnEndSpawn(object arg1, object arg2)
        {
            StopSpawn(true);
        }
    }
}