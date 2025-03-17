using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Search;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    /*Basic Operations of Queue
   
    Enqueue: Adds an item to the end of the queue.
    Dequeue: Removes and returns the item at the front of the queue.
    Peek: Returns the item at the front of the queue without removing it.
    Count: Gets the number of elements in the queue. 

    */

    public TMP_Text DialogText;
    public Button NextButton;
    public float TypingSpeed = 0.5f;

    private Queue<string> sentences;
    public bool IsDialogActive = false;
    private Coroutine typingCoroutine;
    public string[] DialogSentences;
    public string[] DialogNormalBPM;
    public string[] DialogHighBPM;
    
    // Start is called before the first frame update
    public void Start()
    {
        sentences = new Queue<string>();
        //NextButton.onClick.AddListener(DisplayNextSentence);
        //StartDialog(DialogSentences);
    }

    // Update is called once per frame
    public void Update()
    {

    }


    public void StartDialog(string[] dialog)
    {
        IsDialogActive = true;
        sentences.Clear();

        foreach (string sentence in dialog)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if(typingCoroutine != null) 
        {
            StopCoroutine(typingCoroutine);
        }
        
        if(sentences.Count == 0) 
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        typingCoroutine = StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence) 
    {
        DialogText.text = "";
        foreach(char letter in sentence.ToCharArray()) 
        {
            DialogText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
    }

    public void EndDialog() 
    {
        IsDialogActive = false;
        DialogText.text = "";
        // Optionally disable the dialog UI Here
    }
}
