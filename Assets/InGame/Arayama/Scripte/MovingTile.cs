using UnityEngine;
public class MovingTile : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    private void Update()
    {
        // 現在の床の位置が目的地に非常に近い場合
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            // 目的地を次のポイントにセットする
            currentWaypointIndex++;
            // 最後まで行ったら、一番最初のポイントを目的地とする
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        // 現在の床の位置から、目的地の位置まで移動する
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}