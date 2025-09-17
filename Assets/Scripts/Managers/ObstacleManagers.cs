using Assets.Scripts.Obstacles;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ObstacleManagers : MonoBehaviour
    {
        public float bulletSpeed = 20f;
        [Header("Ateşleme Ayarları")]
        [SerializeField] private float fireRate = 0.5f;
        private float _fireTimer;

        void Update()
        {
            if (GameManager.Instance.GameState != Enums.GameState.Playing) return;

            _fireTimer += Time.deltaTime;

            if (_fireTimer >= fireRate)
            {
                Shoot();
                _fireTimer = 0f;
            }
        }

        private void Shoot()
        {
            GameObject bulletGO = ObjectPool.Instance.GetObject();

            float rnd = Random.Range(0f, 4f);
            bulletGO.transform.position = CharacterControl.Instance.transform.position + new Vector3(20f, rnd, 0f);
            //bulletGO.transform.rotation = Quaternion.LookRotation(muzzle.forward); // istenirse

            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Fire(Vector3.left, bulletSpeed);
        }

    }
}