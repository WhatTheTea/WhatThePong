
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Ball : MonoBehaviour
    {
        #region Consts
        [SerializeField]
        public const int maxSpeed = 15;
        #endregion
        #region Vars
        //TODO: Добавить стейты
        public StateMachine stateMachine;
        public BallStates.BallMovingState movingState;
        public BallStates.BallScoredState scoredState;

        private Rigidbody2D body;
        [SerializeField]
        private float collisionOverlapRadius = 0.1f;
        [SerializeField]
        private Collider2D hitBox;
        #endregion
        #region Properties
        public Rigidbody2D Body { get => body; private set => body = value; }
        public Collider2D HitBox { get => hitBox; private set => hitBox = value; }
        public float CollisionOverlapRadius { get => collisionOverlapRadius; }
        public Collision2D CollidedWith { get; private set; }
        public Rigidbody2D LastCollidedPlayerBody { get; private set; }
        #endregion
        #region Methods
        public void ResetMovement()
        {
            Body.position = Vector2.zero;
            Body.velocity = Vector2.zero;
        }
        private void ThrowRandom()
        {
            float direction = Random.Range(0, 2) == 0
                          ? 10
                          : -10; // Случайно определить куда кинуть мячик
            Body.position = Vector2.zero;
            Body.velocity = Vector2.zero;
            Body.velocity = new Vector2(direction, 0);
        }
        #endregion
        #region UnityCallbacks
        private void Start()
        {
            body = GetComponent<Rigidbody2D>();

            stateMachine = new StateMachine();
            movingState = new BallStates.BallMovingState(this, stateMachine);
            scoredState = new BallStates.BallScoredState(this, stateMachine);

            stateMachine.Init(movingState);
            ThrowRandom();
        }
        private void Update()
        {
            stateMachine.CurrentState.LogicUpdate();
        }
        private void FixedUpdate()
        {
            stateMachine.CurrentState.PhysicsUpdate();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollidedWith = collision;
            if(collision.transform.tag == "Player")
            {
                LastCollidedPlayerBody = collision.rigidbody;
            }
        }
        /*private void OnCollisionExit2D(Collision2D collision)
        {
            CollidedWith = null;
        }*/
        #endregion
    }
}
