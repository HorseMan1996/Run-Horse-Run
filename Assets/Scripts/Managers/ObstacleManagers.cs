using Assets.Scripts.Obstacles;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ObstacleManagers : MonoBehaviour
    {
        [SerializeField] private float _minbulletSpeed;
        [SerializeField] private float _maxbulletSpeed;
        [Header("Ateşleme Ayarları")]
        [SerializeField] private float fireRate;
        private float _fireTimer;

        [SerializeField] private Vector3 muzzleOffset;


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

            float rndY = Random.Range(0f, 0.1f);
            muzzleOffset = new Vector3(-1f, rndY, 0f);

            float bulletSpeed = Random.Range(_minbulletSpeed, _maxbulletSpeed);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Fire(muzzleOffset, bulletSpeed);
        }

    }
}