using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : BasePanel
{
    public GameObject dialogueBox;
    public Text dialogueText, nameText;
    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;
    [SerializeField] private float textSpeed;

    private bool isScrolling;

    public Questable currentQuestable;

    public QuestTarget questTarget;
   

    private void Start()
    {
        //dialogueText.text = dialogueLines[currentLine];
    }

    private void Update()
    {
        # region 直接一段一段显示
        /*if(dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                currentLine++;
                if (currentLine < dialogueLines.Length)
                {
                    dialogueText.text = dialogueLines[currentLine];
                }
                else
                {
                    dialogueBox.SetActive(false);
                    FindObjectOfType<PlayerMovement>().canMove = true;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                currentLine--;
                if (currentLine >= 0)
                {
                    dialogueText.text = dialogueLines[currentLine];
                }
                else
                {
                    currentLine = 0;
                }
            }
        }*/
        # endregion 

        if(dialogueBox.activeInHierarchy)
        {
            if(Input.GetMouseButtonUp(0))
            {
                if(isScrolling)
                {
                    isScrolling = false;
                    StopCoroutine("ScrollingText");
                    dialogueText.text = dialogueLines[currentLine];
                    
                }
                else
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        StartCoroutine("ScrollingText");
                    }
                    else
                    {
                        //dialogueBox.SetActive(false);
                        UIManager.GetInstance().PopPanel("Dialogue Panel");
                        FindObjectOfType<PlayerMovement>().canMove = true;

                        if(currentQuestable!=null)
                        {
                            currentQuestable.DelegateQuest();
                            if(currentQuestable.quest.questStatus==Quest.QuestStatus.Completed&&!currentQuestable.isgetRewards)
                            {
                                currentQuestable.getRewards();
                                currentQuestable.isgetRewards = true;
                            }
                        }
                        
                        if(questTarget!=null)
                        {
                            questTarget.TaskCompleted();
                        }
                    }
                }
            }


        }
    }

    public void ShowDialogue(string[] _newLines)
    {
        
            dialogueLines = _newLines;
            currentLine = 0;
            //dialogueText.text = dialogueLines[currentLine];
            StartCoroutine("ScrollingText");
            dialogueBox.SetActive(true);
            FindObjectOfType<PlayerMovement>().canMove = false;
        
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);

        }

        isScrolling = false;
    }


}
