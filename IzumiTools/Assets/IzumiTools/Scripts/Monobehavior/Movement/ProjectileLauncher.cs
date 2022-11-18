using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IzumiTools
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField]
        Transform fireAnchor;
        [SerializeField]
        Projectile projectilePrefab;
        public UnityEvent<Projectile> onFire;
        public Projectile Fire()
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.SetPositionAndRotation(fireAnchor.position, fireAnchor.rotation);
            projectile.Init(this);
            onFire.Invoke(projectile);
            return projectile;
        }
    }

}