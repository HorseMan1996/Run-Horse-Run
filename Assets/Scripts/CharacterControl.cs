using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterControl : MonoBehaviour
    {

        [Header("Movement")]
        [Tooltip("Sürekli sağa doğru hareket hızı (units/sec)")]
        public float moveSpeed = 5f;

        [Header("Jump")]
        [Tooltip("Zıplama kuvveti (impulse)")]
        public float jumpForce = 7f;
        [Tooltip("Zemin katmanları (Ground)")]
        public LayerMask groundLayer;
        [Tooltip("Yer kontrolü için ayak altı nokta")]
        public Transform groundCheck;
        [Tooltip("Yer kontrol yarıçapı")]
        public float groundCheckRadius = 0.2f;

        private Rigidbody _rb;
        private bool _wantsToJump;
        private bool _isGrounded;

        [SerializeField] private Vector3 _characterStartPosition;    
        public static CharacterControl Instance { get; private set; }
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotation; // devrilmeyi engelle
            _rb.interpolation = RigidbodyInterpolation.Interpolate;

            if (groundCheck == null)
            {
                var go = new GameObject("GroundCheck");
                go.transform.SetParent(transform);
                go.transform.localPosition = new Vector3(0f, -0.5f, 0f);
                groundCheck = go.transform;
            }
        }

        private void Start()
        {
            GameManager.Instance.EndGameAct += ResetCharacter;
        }

        private void OnDisable()
        {
            GameManager.Instance.EndGameAct -= ResetCharacter;
        }
        private void ResetCharacter(bool isWinner)
        {
            _rb.linearVelocity = Vector3.zero;
            transform.position = _characterStartPosition;
        }

        void Update()
        {
            if (GameManager.Instance.GameState == Enums.GameState.Playing)
            {
                // input oku
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    _wantsToJump = true;
                }

                // yerde mi?
                _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);

            }
        }

        void FixedUpdate()
        {
            if (GameManager.Instance.GameState == Enums.GameState.Playing)
            {
                // Sürekli sağa doğru hareket
                Vector3 vel = _rb.linearVelocity;
                vel.x = moveSpeed;
                _rb.linearVelocity = vel;

                // Zıplama
                if (_wantsToJump && _isGrounded)
                {
                    _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z); // Y hızını sıfırla
                    _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }

                _wantsToJump = false;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }

}
