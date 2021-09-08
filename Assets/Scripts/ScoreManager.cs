using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public partial class ScoreManager : MonoBehaviour
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
    void Start()
    {
        p1ScoreMesh.text = P1Score.ToString();
        p2ScoreMesh.text = P2Score.ToString();

        //BallScript.BallScored += BallScored;
    }

    public void BallScored(object sender, Walls wall)
    {
        switch (wall)
        {
            case Walls.Left:
                P1Score++;
                break;
            case Walls.Right:
                P2Score++;
                break;
        }
        if(P1Score == 10 || P2Score == 10)
        {
            P1Score = 0;
            P2Score = 0;
            GameOver?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
