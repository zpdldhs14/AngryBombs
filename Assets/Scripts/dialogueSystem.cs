using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class dialogueSystem : MonoBehaviour, IPointerDownHandler
    {
        public TMP_Text dialogueText;
        public GameObject nextText;
        public CanvasGroup canvasGroup;
        public Queue<string> sentences;
        public float typingSpeed = 0.1f;
        public string currentSentence;
        public bool isTyping;

        void Start()
        {
            sentences = new Queue<string>();
        }

        public void Ondialogue(string[] lines)
        {
            sentences.Clear();
            foreach (string line in lines)
            {
                sentences.Enqueue(line);
            }
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            
            NextSentence();
        }
        
        public void NextSentence(bool autoNext = false)
        {
            if (sentences.Count != 0)
            {
                currentSentence = sentences.Dequeue();
                isTyping = true;
                nextText.SetActive(false);
                StartCoroutine(Typing(currentSentence));
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
            }
            
        }

        IEnumerator Typing(string line, bool autoNext = false)
        {
            dialogueText.text = "";
            foreach (char letter in line.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;

            if (autoNext)
            {
                yield return new WaitForSeconds(1.0f); // 다음 대사로 넘어가기 전 대기 시간
                NextSentence(true);
            }
        }

        void Update()
        {
            if(dialogueText.text.Equals(currentSentence))
            {
                nextText.SetActive(true);
                isTyping = false;
            }
            else
            {
                nextText.SetActive(false);
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isTyping && canvasGroup.alpha > 0)
            {
                NextSentence();
            }
            
        }
    }
}