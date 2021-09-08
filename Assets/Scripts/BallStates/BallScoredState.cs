using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BallStates
{
    public class BallScoredState : BallState
    {
        public delegate void BallScoredHandler(object sender, UnityEngine.Collider2D collidesWith);
        public static event BallScoredHandler BallScored;

        private UnityEngine.Collider2D playerCollider;
        private Player whichPlayerShot;
        private bool ballShot;
        public BallScoredState(Ball ball, StateMachine machine) : base(ball, machine)
        {
            Player.PlayerShot += (player, e) => whichPlayerShot = (Player)player;
        }
        public override void Enter()
        {
            base.Enter();
            whichPlayerShot = null;
            ballShot = false;

            playerCollider = ball.OverlappingWith;
            BallScored?.Invoke(this, ball.OverlappingWith);
            ball.ResetMovement();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (ballShot)
            {
                stateMachine.ChangeState(ball.movingState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            var xOffset = playerCollider.transform.position.x > 0 ? -0.5f : 0.5f;
            ball.transform.position = new UnityEngine.Vector2(playerCollider.transform.position.x + xOffset,
                playerCollider.transform.position.y);

            if(whichPlayerShot != null && whichPlayerShot.transform.position == playerCollider.transform.position)
            {
                ball.Body.velocity.Set(10, whichPlayerShot.Body.velocity.y * 10);

                ballShot = true;
            }
        }
    }
}
