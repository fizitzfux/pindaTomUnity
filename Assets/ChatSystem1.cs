
using System.Collections.Generic;
using UnityEngine;

public class ChatSystem1 : MonoBehaviour {
    
    [SerializeField]  
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SendMessageToChat("You pressed the space key!");
    }

    public void SendMessageToChat(string text)
    {
        Message newMessage = new Message();

        newMessage.text = text;

        messageList.Add(newMessage);
    }
}


 [System.Serializable]
public class Message
{
    public string text;
}