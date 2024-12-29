using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    public dialogueSystem dialogue;
    public float pauseBetweenLines = 1.0f;

    public void StartAnnoyedDialogue()
    {
        // 대화 1단계: 놀라는 대사를 시작
        StartCoroutine(PlayDialogueSequence(new string[]
        {
            "으악! 뭐야 갑자기!"
        }));
        
        // 대화 2단계: 짜증 난 상태의 중얼중얼한 대사
        StartCoroutine(PlayDialogueSequence(new string[]
        {
            "진짜… 이런 건 예상도 못 했잖아.",
            "계속 이런 식이면 피곤해진다니까… (투덜투덜)"
        }));
    }
    
    public void StartSighDialogue()
    {
        // 대화 3단계: 한숨을 쉰 후 마무리
        StartCoroutine(PlayDialogueSequence(new string[]
        {
            "(한숨을 푹 쉰다)",
            "그럼, 빨리 가자!"
        }));
    }
    
    private IEnumerator PlayDialogueSequence(string[] lines)
    {
        dialogue.Ondialogue(lines); // 대화 시작
        yield return new WaitForSeconds(pauseBetweenLines * lines.Length); // 대사가 출력될 시간을 대략 계산
    }
}
