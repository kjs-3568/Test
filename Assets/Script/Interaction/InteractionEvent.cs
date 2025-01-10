using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] GetDialogue() // 왜 햇갈리게 DataBaseManager랑 클래스 명이 같은지..?
    {
        dialogue.dialogues = DataBaseManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        return dialogue.dialogues;
    }
}
