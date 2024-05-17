using IG;
using System;
using TMPro;
using UnityEngine;

namespace Kosmos6
{
    public class ManagerScore : SingletonManager<ManagerScore>
    {
        [SerializeField] private int Score;
        [SerializeField] private TextMeshProUGUI ScoreText;
        public Action<int> OnAddScore;
        public Action<int> OnNewScore;


        private void OnEnable()
        {
            if (ScoreText == null)
            {
                ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            }
            else
                ScoreText.text = Score.ToString();

            OnAddScore += IncreaseScore;
        }

        private void OnDisable() => OnAddScore -= IncreaseScore;

        public void AddScore(int scoreDelta) => OnAddScore?.Invoke(scoreDelta);

        private void IncreaseScore(int scoreDelta)
        {
            Score += scoreDelta;
            ScoreText.text = Score.ToString();
            OnNewScore?.Invoke(Score);
        }
    }

}
