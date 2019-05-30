using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public GameMasterScript gm;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text expText;

    private Quest quest;
    public List<Quest> questList;

    private void Update()
    {
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
    }

    public void SetActiveQuest(int num)
    {
        quest = questList[num]; //Set the active quest, to quest :)
        gm.quest = quest; //Give the GM the quest.
        quest.isActive = true;
    }
}
