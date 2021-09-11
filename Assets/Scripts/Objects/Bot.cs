
using UnityEngine;


namespace Scripts.Objects
{
    public class Bot : Player //������������� � ����
    {
        private Ball _ball;
        public Ball TargetBall => _ball;

        // Update is called once per frame
        private void Update()
        {
            BotMovement.Move(new Vector2(0, (Ball.transform.position.y - Body.transform.position.y)));
        }
    }
}
