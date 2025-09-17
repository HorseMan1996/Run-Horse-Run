using Assets.Scripts.Managers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class Bullet : MonoBehaviour
    {
        [Tooltip("Otomatik geri koyma s�resi (saniye)")]
        public float lifeTime = 5f;

        private Rigidbody _rb;
        private Coroutine _autoReturnCoroutine;

        private Quaternion _initialQuaternion;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _initialQuaternion = transform.rotation;
            // E�er pool'dan �ekildi�inde konum/rotasyon resetlemek istersen burada yapma, caller yaps�n daha esnek.
        }

        private void OnEnable()
        {
            // Aktive edildi�inde (pool'dan �ekildi�inde) otomatik geri say�m� ba�lat
            _autoReturnCoroutine = StartCoroutine(AutoReturnAfterSeconds(lifeTime));
        }

        private void OnDisable()
        {
            // Devre d��� kal�nca h�z� s�f�rla ve coroutine'i temizle
            _rb.linearVelocity = Vector3.zero;
            if (_autoReturnCoroutine != null)
            {
                StopCoroutine(_autoReturnCoroutine);
                _autoReturnCoroutine = null;
            }
        }

        /// <summary>
        /// Kur�unu belirtilen y�nde ve h�zda ate�le.
        /// caller: bullet = ObjectPool.Instance.GetObject(); bullet.GetComponent<Bullet>().Fire(dir, speed);
        /// </summary>
        public void Fire(Vector3 direction, float speed)
        {
            if (direction.sqrMagnitude < 0.0001f) direction = transform.forward;
            direction.Normalize();
            _rb.linearVelocity = direction * speed;
            // �stersen angular velocity veya rotation da ayarla:
            // transform.rotation = Quaternion.LookRotation(direction);
        }

        private IEnumerator AutoReturnAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ReturnToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Vuru� efekti, hasar hesaplama vs. ekle
            // �rn: var hit = collision.gameObject.GetComponent<Health>(); if (hit) hit.TakeDamage(damage);

            // �arp���nca hemen havuza geri koy
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            // Pool'unuzun metoduna g�re �a��r�n
            // E�er ObjectPool.Instance kullan�yorsan�z:
            _rb.linearVelocity = Vector3.zero; // H�z�n� s�f�rla
            _rb.angularVelocity = Vector3.zero; // D��nme h�z�n� s�f�rla
            transform.rotation = _initialQuaternion; // Rotasyonu resetle
            ObjectPool.Instance.ReturnObject(this.gameObject);
        }
    }
}
