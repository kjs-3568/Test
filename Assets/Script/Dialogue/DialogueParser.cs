using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);  // CSV 파일을 로드

        string[] data = csvData.text.Split(new char[] { '\n' });  // 줄바꿈 기준으로 데이터 분리

        for (int i = 1; i < data.Length;)  // 첫 번째 줄은 헤더이므로 i = 1부터 시작
        {
            string[] row = data[i].Split(new char[] { ',' });  // 각 행을 쉼표로 분리

            Dialogue dialogue = new Dialogue();
            dialogue.name = row[1];  // 캐릭터 이름은 두 번째 컬럼
            Debug.Log(row[1]);

            List<string> contextList = new List<string>();

            do
            {
                if (row.Length > 2)
                {
                    contextList.Add(row[2]);  // 대사는 세 번째 컬럼에 있음
                    Debug.Log(row[2]);
                }

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });  // 다음 행으로 이동
                }
                else
                {
                    break;
                }

            } while (row[0].ToString() == "");  // 첫 번째 컬럼이 비어있다면, 이 대사는 여러 줄로 이어진 것

            dialogue.contexts = contextList.ToArray();  // 여러 줄의 대사를 배열로 저장

            dialogueList.Add(dialogue);  // 대사를 리스트에 추가
        }

        return dialogueList.ToArray();  // 최종적으로 대사 배열을 반환
    }
}

