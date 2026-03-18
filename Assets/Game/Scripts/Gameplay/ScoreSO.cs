using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Game Data/PlayerScore")]
public class ScoreSO : ScriptableObject // probably i should use Json
{
    public float money;
    public int day;

    public void AddRemoveMoney(float _money, int _day)
    {
        day += _day;
        money += _money;
    }

    private void Reset()
    {
        money = 0;
        day = 0;
    }
}