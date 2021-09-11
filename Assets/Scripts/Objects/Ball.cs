
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Ball : MonoBehaviour
    {
        #region Consts
        public const int maxSpeed = 15;
        #endregion
        #region fields
        public StateMachine stateMachine;
        public BallStates.BallMovingState movingState;
        public BallStates.BallScoredState scoredState;

        private Rigidbody2D body;
        [SerializeField]
        private Collider2D hitBox;
        private Player _lastCollidedPlayer;
        #endregion
        #region Properties
        public Rigidbody2D Body { get => body; private set => body = value; }
        public Collider2D HitBox { get => hitBox; private set => hitBox = value; }
        public Player LastCollidedPlayer => _lastCollidedPlayer;
        public Collider2D CollidingWith
        {
            get
            {
                Collider2D result;
                List<Collider2D> colliders = new List<Collider2D>();
                Body.OverlapCollider(new ContactFilter2D(), colliders);
                //result = Physics2D.OverlapCircle(Body.position, 1.5f);

                result = colliders.FirstOrDefault(c => c.name != "Ball");
                if (result != default && result.tag == "Player")
                {
                    _lastCollidedPlayer = Player.Instances.First(p => p.name == result.name);
                }
                return result;
            }
        }
        #endregion
        #region Methods
        public void ResetMovement()
        {
            Body.position = Vector2.zero;
            Body.velocity = Vector2.zero;
            Body.angularVelocity = 0f;
        }
        private void ThrowRandom()
        {
            ResetMovement();

            float direction = Random.Range(0, 2) == 0
                          ? 10
                          : -10; // Случайно определить куда кинуть мячик

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
        private void Update() => stateMachine.CurrentState.LogicUpdate();
        private void FixedUpdate() => stateMachine.CurrentState.PhysicsUpdate();
        #endregion
    }
}
