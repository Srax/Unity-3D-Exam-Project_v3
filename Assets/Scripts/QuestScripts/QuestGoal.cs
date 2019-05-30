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

    public void EnemyKilled()
    {
        if(goalType == GoalType.Kill)
            currentAmount++;
    }

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
    Gather,
    Escort,
    MoveTo
}
