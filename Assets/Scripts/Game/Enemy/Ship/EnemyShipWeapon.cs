using Bachelor.Game.Base;
using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.Game
{
    public class EnemyShipWeapon : BaseWeapon, IGameObjectPool
    {
        /*
        private void OnDestroy()
        {
            foreach (GameObject spawnShot in SpawnShots)
            {
                Destroy(spawnShot);
            }
        }
        */

        protected override void Fire()
        {
            if (Time.time > Delay)
            {
                Delay = Time.time + FireRate;
                foreach (Transform shotSpawn in ShotSpawns)
                {
                    GameObject spawnShot = this.Spawn(this.FindPoolName(Shot), shotSpawn.position, shotSpawn.rotation);
                    /*
                    SpawnShots.Add(spawnShot);
                    spawnShot.GetComponent<EnemyShipBoltController>().SpawnByName = gameObject.name;
                    */
                }
                PlayWeaponSound();
            }
        }
    }
}