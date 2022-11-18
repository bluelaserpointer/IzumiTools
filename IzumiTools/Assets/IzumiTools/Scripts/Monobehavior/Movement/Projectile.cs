using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IzumiTools
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public float speed = 10;
        public float range = 500;
        public ValueChangeWatcher<string> delt;
        public UnityEvent<Collider> onHit;

        Timestamp fireTimestamp = new Timestamp();
        public Rigidbody Rigidbody { get; private set; }
        public ProjectileLauncher Projector { get; private set; }
        public float FlightDistance { get; private set; }
        public float FireTime => fireTimestamp.lastStampTime;
        public float FlightTime => fireTimestamp.PassedTime;
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
        public void Init(ProjectileLauncher projector)
        {
            fireTimestamp.Stamp();
            Projector = projector;
        }
        private void Update()
        {
            if(FlightDistance > range)
                Destroy(gameObject);
        }
        private void FixedUpdate()
        {
            FlightDistance += Rigidbody.velocity.magnitude * Time.fixedDeltaTime;
            Rigidbody.MovePosition(transform.forward * speed * Time.fixedDeltaTime);
        }
        private void OnCollisionEnter(Collision collision)
        {
            onHit.Invoke(collision.collider);
        }
        private void OnTriggerEnter(Collider other)
        {
            onHit.Invoke(other);
        }
    }

}