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
            collidedWith = ball.OverlappingWith;
            switch (collidedWith?.tag)
            {
                case "Player":
                    BounceBack(collidedWith);
                    break;
                case "Finish":
                    scored = true;
                    break;
                case "Wall":
                    ball.Body.velocity.Scale(new Vector2(2, 0.5f));
                    break;
            }
        }
        public void BounceBack(Collider2D from)
        {
            var velocity = new Vector2(-from.transform.position.x * 2,
                        -(from.transform.position.y - ball.Body.position.y) * 5); // попробовать заменить тело на transform
            ball.Body.velocity = velocity;

            if (from.transform.position.y - ball.Body.position.y > -0.5f 
             && from.transform.position.y - ball.Body.position.y < 0.5f)
            {
                ball.Body.AddForce(new Vector2(0, 
                    UnityEngine.Random.Range(0, 2) == 0 ? 10 : -10));
            }
        }
    }
}
