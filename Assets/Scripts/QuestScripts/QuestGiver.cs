using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [Header("Objects")]
    public GameMasterScript gm;
    public GameObject player;
    public GameObject boss;
    public GameObject chest;
    public GameObject questWindow;

    [Header("UI")]
    public Text titleText;
    public Text descriptionText;
    public Text expText;
    public Text progressText;

    private Quest quest;
    public List<Quest> questList;

    private void Update()
    {
        //If the first quest is complete, spawn the boss.
        if(questList[0].isComplete == true)
        {
            if(boss && boss.activeSelf == false)
            {
                boss.SetActive(true);
            }
        }

        if(questList[0].isComplete && questList[1].isComplete)
        {
            if(chest && chest.activeSelf == false)
            {
                chest.SetActive(true);
            }
        }

        switch(PlayerPrefs.GetInt("questIndex"))
        {

            //Quest 1
            case 0:
                SetActiveQuest(0);
                UpdateQuestWindow();
                break;

            case 1:
                SetActiveQuest(1);
                UpdateQuestWindow();
                break;

            case 2:
                SetActiveQuest(2);
                UpdateQuestWindow();
                break;

            default:
                SetActiveQuest(3);
                UpdateQuestWindow();
                break;
        }

    }

    public void UpdateQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        expText.text = "Exp:" + quest.expReward.ToString();

        if(quest.goal.goalType == GoalType.Kill)
        {
            progressText.text = "Killed " + quest.goal.currentAmount + " of " + quest.goal.requiredAmount;
        }

        if(quest.goal.goalType == GoalType.Gather)
        {
            progressText.text = "Collected " + quest.goal.currentAmount + " of " + quest.goal.requiredAmount;
        }
    }

    public void SetActiveQuest(int num)
    {
        quest = questList[num]; //Set the active quest, to quest :)
        gm.quest = quest; //Give the GM the quest.
        quest.isActive = true;
    }
}
