using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCUIButtonTest : MonoBehaviour
{
    public Button hostButton, clientButton;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        hostButton.onClick.AddListener(TaskOnClick);
        //m_YourSecondButton.onClick.AddListener(delegate { TaskWithParameters("Hello"); });
        //m_YourThirdButton.onClick.AddListener(() => ButtonClicked(42));
        clientButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }
}
