using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BallStates
{
    public class BallScoredState : BallState
    {
        public delegate void BallScoredHandler(object sender, UnityEngine.Collision2D collidesWith);
        public static event BallScoredHandler BallScored;

        private UnityEngine.Collision2D wall;
        private Player whichPlayerShot;
        private bool ballShot;
        public BallScoredState(Ball ball, StateMachine machine) : base(ball, machine)
        {
            Player.PlayerShot += (player, e) => whichPlayerShot = (Player)player;
        }
        public override void Enter()
        {
            base.Enter();

            wall = ball.CollidedWith;
            BallScored?.Invoke(this, ball.CollidedWith);
            ball.ResetMovement();
        }

        public override void Exit()
        {
            base.Exit();

            whichPlayerShot = null;
            ballShot = false;
            wall = null;
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
            var xOffset = wall.transform.position.x > 0 ? -0.3f : 0.3f;
            ball.transform.position = new UnityEngine.Vector2(ball.LastCollidedPlayerBody.position.x + xOffset,
                ball.LastCollidedPlayerBody.position.y);

            if(whichPlayerShot != null)
            {
                ball.Body.velocity = new UnityEngine.Vector2(10f, (whichPlayerShot.Body.velocity.y + 0.001f) * 10);

                ballShot = true;
            }
        }
    }
}
