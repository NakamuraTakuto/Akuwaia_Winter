//日本語対応
using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScore : MonoBehaviour
{
    public void ScoreLoad()
    {
        int rank;
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("ScoreClass");
        query.OrderByAscending("score");
        query.Limit = 10;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogWarning("error: " + e.ErrorMessage);
            }
            else
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    rank = i + 1;
                    Debug.Log("ScoreRanking " + rank + "位: " + objList[i]["name"] + " " + objList[i]["score"]);
                }
            }
        });
    }
}
