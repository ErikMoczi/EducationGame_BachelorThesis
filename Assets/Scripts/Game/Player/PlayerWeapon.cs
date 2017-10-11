using Bachelor.Game.Base;
using Bachelor.MyExtensions.Managers;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Bachelor.Game
{
    public class PlayerWeapon : BaseWeapon, IGameObjectPool
    {
        protected override void Fire()
        {
            if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > Delay)
            {
                Delay = Time.time + FireRate;
                foreach (Transform shotSpawn in ShotSpawns)
                {
                    GameObject spawnShot = this.Spawn(this.FindPoolName(Shot), shotSpawn.position, shotSpawn.rotation);
                    /*
                    SpawnShots.Add(spawnShot);
                    spawnShot.GetComponent<PlayerBoltController>().SpawnByName = gameObject.name;
                    */
                }
                PlayWeaponSound();
            }
        }
    }
}