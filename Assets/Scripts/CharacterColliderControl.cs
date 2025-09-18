using Assets.Scripts.Obstacles;
using UnityEngine;


namespace Assets.Scripts
{
    /// <summary>
    /// Karakterin collider ile ilgili kontrolleri burada yap.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]

    public class CharacterColliderControl : MonoBehaviour
    {


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                float rndDamage = Random.Range(5f, 20f);
                CharacterHealthControl.Instance.TakeDamage(rndDamage);
                Debug.Log("Karakter düþmana çarptý!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EndGame"))
            {
                Debug.Log("Karakter bir eþya aldý!");
            }
        }
    }
}
