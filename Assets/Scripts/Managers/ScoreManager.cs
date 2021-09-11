
using Scripts.BallStates;

using Scripts.Objects;

using UnityEngine;

namespace Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static event System.EventHandler GameOver;

        [SerializeField]
        private TMPro.TextMeshProUGUI p1ScoreMesh;
        [SerializeField]
        private TMPro.TextMeshProUGUI p2ScoreMesh;

        private int _p1Score = 0;
        private int _p2Score = 0;
        public int P1Score
        {
            get => _p1Score;
            set
            {
                _p1Score = value;
                p1ScoreMesh.text = value.ToString();
            }
        }
        public int P2Score
        {
            get => _p2Score;
            set
            {
                _p2Score = value;
                p2ScoreMesh.text = value.ToString();
            }
        }

        private void Start()
        {
            p1ScoreMesh.text = P1Score.ToString();
            p2ScoreMesh.text = P2Score.ToString();

            BallScoredState.BallScored += BallScored;
        }

        public void BallScored(object sender, Goal goal)
        {
            if (goal.transform.position.x < 0)
            {
                P1Score++;
            }
            else
            {
                P2Score++;
            }
            if (P1Score == 10 || P2Score == 10)
            {
                P1Score = 0;
                P2Score = 0;
                GameOver?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}