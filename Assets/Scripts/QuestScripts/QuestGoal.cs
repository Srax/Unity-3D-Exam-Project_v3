using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Again, we add this so we can edit the QuestGoal in the inspector/editor.
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool isReached()  {
        return (currentAmount >= requiredAmount); //If the currentAmount is equal to or higer than requiredAmount, return "true", else return "false".
    }

    //Enemy Killed
    public void EnemyKilled()
    {
        if(goalType == GoalType.Kill)
            currentAmount++;
    }

    public void BossKilled()
    {
        if (goalType == GoalType.BossKill)
            currentAmount++;
    }

    public void BossChestInteracted()
    {
        if (goalType == GoalType.Gather)
            currentAmount++;
    }

    //Any item collected
    public void ItemCollected()
    {
        if (goalType == GoalType.Gather)
            currentAmount++;
    }
}

//Types of quests we can do
public enum GoalType
{
    Kill,
    BossKill,
    Gather,
    Escort,
    MoveTo,
    Blank
}
