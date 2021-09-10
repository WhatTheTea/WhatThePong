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

        private Goal goal;
        private Player whichPlayerShot;
        private bool ballShot;
        public BallScoredState(Ball ball, StateMachine machine) : base(ball, machine)
        {
            Player.PlayerShot += (player, e) => whichPlayerShot = (Player)player;
        }
        public override void Enter()
        {
            base.Enter();

            goal = Goal.Instances.First(g => g.Name == ball.OverlappingWith.name);
            BallScored?.Invoke(this, ball.OverlappingWith);
            ball.ResetMovement();
        }

        public override void Exit()
        {
            base.Exit();

            whichPlayerShot = null;
            ballShot = false;
            goal = null;
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

            var xOffset = goal.transform.position.x > 0 ? 0.3f : -0.3f; //TODO: Можно определить в начале 
            ball.transform.position = new UnityEngine.Vector2(ball.LastCollidedPlayer.transform.position.x + xOffset,
                ball.LastCollidedPlayer.transform.position.y);

            if(whichPlayerShot != null)
            {
                ball.Body.velocity = new UnityEngine.Vector2(10f, (whichPlayerShot.Velocity + 0.001f) * 10);

                ballShot = true;
            }
        }
    }
}
