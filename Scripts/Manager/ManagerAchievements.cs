using IG;
using UnityEngine;

namespace Kosmos6
{
    public class ManagerAchievements : SingletonManager<ManagerAchievements>
    {
        private void OnEnable() => ManagerScore.Instance.OnNewScore += CheckAchievements;
        private void OnDisable() => ManagerScore.Instance.OnNewScore -= CheckAchievements;
        private void CheckAchievements(int newScore)
        {
            if (newScore > 9)
                Debug.Log("You are the Champion!");
        }
    }
}