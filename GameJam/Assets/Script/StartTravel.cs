using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTravel : MonoBehaviour
{
    public GameObject WinWindow;
    public GameObject RestartWindow;
    // Static reference to the instance of the singleton
    private static StartTravel instance;

    private bool time = false;
    public float timer = 8;
    // Public access to the singleton instance
    public static StartTravel Instance
    {
        get { return instance; }
    }

    [SerializeField]
    List<Node> Path = new List<Node>();
    public GameObject Chara;

    private void Update()
    {
        if (time)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopAllCoroutines();
                RestartWindow.SetActive(true);
                time = false;
            }
        }
    }


    public void StartPathTravel()
    {
        Path.Clear();
        Path.AddRange(PathfindingControl.Instance.FindAndPrintPath());
        foreach (Node n in Path)
        {
            positions.Add(n.transform.position);
            n.transform.GetComponent<SpriteRenderer>().color = Color.green;
        }
        StartMovement();
    }

    public List<Vector2> positions;
    public float moveSpeed = 5f;

    private Transform charaTransform;
    private int currentPositionIndex = 0;

    private void StartMovement()
    {
        charaTransform = Chara.transform;
        time = true;
        StartCoroutine(MoveCharaThroughPositions());
    }

    private IEnumerator MoveCharaThroughPositions()
    {
        while (currentPositionIndex < positions.Count)
        {
            Vector2 targetPosition = positions[currentPositionIndex];
            while ((Vector2)charaTransform.position != targetPosition)
            {
                charaTransform.position = Vector2.MoveTowards(charaTransform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            currentPositionIndex++;
        }

        Debug.Log("Chara reached all positions.");
        time = false;
        WinWindow.SetActive(true);
    }
}
