using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BallStates
{
    public class BallScoredState : BallState
    {
        public delegate void BallScoredHandler(object sender, Goal collidesWith);
        public static event BallScoredHandler BallScored;

        private Goal goal;
        private Player whichPlayerShot;
        private bool ballShot;
        private float distance;
        public BallScoredState(Ball ball, StateMachine machine) : base(ball, machine)
        {
            Player.PlayerShot += (player, e) => whichPlayerShot = (Player)player;
        }
        public override void Enter()
        {
            base.Enter();
            whichPlayerShot = null;

            goal = Goal.Instances.First(g => g.Name == ball.CollidingWith.name);
            distance = goal.transform.position.x > 0 ? 0.5f : -0.5f;
            BallScored?.Invoke(this, goal);
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

            ball.Body.position = new UnityEngine.Vector2(ball.LastCollidedPlayer.transform.position.x + distance,
                ball.LastCollidedPlayer.transform.position.y);
            
            if(whichPlayerShot != null && ball.LastCollidedPlayer == whichPlayerShot)
            {
                ballShot = true;
                ball.Body.velocity = new UnityEngine.Vector2(10f, whichPlayerShot.Velocity);
            }
        }
    }
}
