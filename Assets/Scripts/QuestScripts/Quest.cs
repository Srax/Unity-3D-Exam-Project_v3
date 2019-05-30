
using UnityEngine;

[System.Serializable] //The Serializable attribute lets you embed a class with sub properties in the inspector.
public class Quest
{
    public bool isActive;

    public string title;
    public string description;
    public float expReward;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " has been completed.");
        PlayerPrefs.SetInt("questIndex", PlayerPrefs.GetInt("questIndex") + 1); //Count up questIndex by 1 each time a quest is completed.
    }
}
