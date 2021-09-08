using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BallStates
{
    public abstract class BallState : IState
    {
        protected Ball ball;
        protected StateMachine stateMachine;

        protected BallState(Ball ball, StateMachine machine)
        {
            this.ball = ball;
            this.stateMachine = machine;
        }
        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            ball.Body.velocity = UnityEngine.Vector2.ClampMagnitude(ball.Body.velocity, Ball.maxSpeed);
        }
    }
}
