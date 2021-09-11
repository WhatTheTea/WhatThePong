using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.BallStates
{
    public class BallMovingState : BallState
    {
        private Collider2D collidedWith;
        private bool scored;
        public BallMovingState(Ball ball, StateMachine machine) : base(ball, machine)
        {
        }
        public override void Enter() => base.Enter();

        public override void Exit()
        {
            base.Exit();

            scored = false;
            collidedWith = null;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (scored)
            {
                stateMachine.ChangeState(ball.scoredState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            collidedWith = ball.CollidingWith;
            switch (collidedWith?.tag)
            {
                case "Player":
                    BounceBack(collidedWith);
                    break;
                case "Finish":
                    scored = true;
                    break;
                case "Wall":
                    ball.Body.velocity.Scale(new Vector2(2, 1));
                    break;
            }
        }
        public void BounceBack(Collider2D from)
        {
            if (!scored)
            {
                var yDistance = from.transform.position.y - ball.Body.position.y;
                var velocity = new Vector2(-from.transform.position.x * 2,
                            -yDistance * 10);
                ball.Body.velocity = velocity;

                if (yDistance > -0.5f && yDistance < 0.5f)
                {
                    ball.Body.AddForce(new Vector2(0,
                        UnityEngine.Random.Range(0, 2) == 0 ? 10 : -10));
                }
            }
        }
    }
}
