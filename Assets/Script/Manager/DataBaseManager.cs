using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;  // 싱글톤 인스턴스

    [SerializeField] string csv_FileName;  // 사용할 CSV 파일 이름

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();  // 대사 데이터를 저장할 딕셔너리

    public static bool isFinish = false;  // 대사 데이터 로딩이 완료되었는지 여부

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();  // DialogueParser 컴포넌트 가져오기
            Dialogue[] dialogues = theParser.Parse(csv_FileName);  // 대사 파싱

            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);  // 대사 목록을 딕셔너리에 추가
            }

            isFinish = true;  // 로딩 완료 표시
        }
    }

    public Dialogue[] GetDialogue(int _startNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for(int i = 0; i <= _EndNum - _startNum; i++)
        {
            dialogueList.Add(dialogueDic[_startNum + i]);
        }

        return dialogueList.ToArray();
    }
}

