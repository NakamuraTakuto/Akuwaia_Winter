//日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class SaveScore : MonoBehaviour
{
    public void ScoreSave(float Score, string Name)
    {
        NCMBObject scoreClass = new NCMBObject("ScoreClass");
        scoreClass["score"] = Score;
        scoreClass["name"] = Name;
        scoreClass.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("Error: " + e.ErrorMessage);
            }
            else
            {
                Debug.Log("success");
            }
        });
    }
}
