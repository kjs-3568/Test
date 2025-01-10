using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueName;
    [SerializeField] TextMeshProUGUI text_Dialogue;
    [SerializeField] TextMeshProUGUI text_Name;

    private Dialogue[] dialogues;

    private bool isDialouge = false; // 대화중일 경우 true
    private bool isNext = false; // 특정키 입력대기용
    int lineCount = 0; // 대화카운트
    int contextCount = 0; // 대사 카운트
    private float textDelay = 0.03f; // 텍스트 딜레이 수치

    private InteractionController theIC;

    private void Start()
    {
        text_Dialogue.text = "";
        text_Name.text = "";
        theIC = FindObjectOfType<InteractionController>();
    }

    private void Update()
    {
        if (isDialouge)
        {
            if(isNext)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    text_Dialogue.text = "";

                    if(++contextCount < dialogues[lineCount].contexts.Length) 
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else // 한 캐릭터의 대사가 끝나면,
                    {
                        contextCount = 0; // 초기화
                        if (++lineCount < dialogues.Length) // 다음 캐릭터 대사로.
                        {
                            StartCoroutine(TypeWriter());
                        }
                        else // 대사가 모두 끝나면,
                        {
                            EndDialogue();
                        }
                    }
                    
                }
            }
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialouge=true;
        dialogues = p_dialogues;
        theIC.SettingMousePoinerUI(false);

        StartCoroutine(TypeWriter());
    }

    private void EndDialogue()
    {
        isDialouge = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        SettingUI(false);
        theIC.SettingMousePoinerUI(true);
    }

    IEnumerator TypeWriter() // <,>사이는 출력이 되지 않게.
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("' ", ", "); // '를 ,로 변환

        text_Name.text = dialogues[lineCount].name;

        //t_ReplaceText = Regex.Replace(t_ReplaceText, "<.*?>", ""); 수정필요

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            //ChangeTextColor(t_ReplaceText[i]);
            text_Dialogue.text += t_ReplaceText[i];

            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
        yield return null;
    }

    //private void ChangeTextColor(char letter)
    //{
    //    bool t_white = false, t_red = false, t_blue = false;

    //    switch(letter)
    //    {
    //        case 'ⓦ': t_white = true; t_red = false; t_blue = false; t_ignore = true; break;
    //        case 'ⓡ': t_white = false; t_red = true; t_blue = false; t_ignore = true; break;
    //        case 'ⓑ': t_white = false; t_red = false; t_blue = true; t_ignore = true; break;
    //    }

    //    string t_letter = letter.ToString();

    //    if(!t_ignore)
    //    {
    //        if (t_white)
    //            t_letter = "<color=#ffffff>" + t_letter + "</color>";
    //        else if (t_red)
    //            t_letter = "<color=#FF0000>" + t_letter + "</color>";
    //        else if (t_blue)
    //            t_letter = "<color=#0000FF>" + t_letter + "</color>";
    //    }
    //    t_ignore = false;
    //}

    private void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueName.SetActive(p_flag);
    }

}
