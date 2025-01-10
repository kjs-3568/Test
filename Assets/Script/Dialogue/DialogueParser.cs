using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); // 대사를 저장할 Dialogue 클래스의 리스트를 생성

        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);  // CSV 파일을 로드

        string[] data = csvData.text.Split(new char[] { '\n' });  // 줄바꿈 기준으로 데이터 분리

        for (int i = 1; i < data.Length;)  // 첫 번째 줄은 헤더이므로 i = 1부터 시작
        {
            string[] row = data[i].Split(new char[] { ',' });  // 각 행을 쉼표로 분리(row[0]: ID, row[1]: 캐릭터이름, row[2]: 대사)

            Dialogue dialogue = new Dialogue(); // Dialogue 클래스의 변수를 생성해서,
            dialogue.name = row[1]; // 쉼표로 분리된 데이터를 삽입
            // dialogue.contexts = row[2];  contexts는 배열타입이기에 배열에 맞는 데이터가 아니면 오류남
            List<string> contextList = new List<string>(); // 그래서 contexts에 데이터를 넣기 위한 리스트 생성

            do // 한 캐릭터의 대사가 여러줄인 경우가 있어서, 대사 줄의 갯수만큼 반복하여 데이터에 저장
            {
                if (row.Length > 2) // 엑셀을 파싱할 때 생기는 오류방지용
                {
                    contextList.Add(row[2]); // 대사 추가
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

            dialogue.contexts = contextList.ToArray();  // 리스트를 배열로 바꿔 저장

            dialogueList.Add(dialogue);  // 대사를 리스트에 추가
        }

        return dialogueList.ToArray();  // 최종적으로 대사 배열을 반환
    }
}

