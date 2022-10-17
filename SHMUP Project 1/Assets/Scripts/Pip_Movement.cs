using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pip_Movement : MonoBehaviour
{
    [SerializeField]
    float drifSpeed, driftRange;

    [SerializeField]
    GameObject PipObject;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Text collectedPipsText;

    List<GameObject> pips = new List<GameObject>();

    public static int pipsGained = 0;
    float timeSinceGotPip = 1;

    static Camera cam;
    static float height = 0;
    static float width = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject pip in pips)
        {
            pip.GetComponent<PipInfo>().UpdateMove();
            pip.transform.position += Vector3.left * Time.deltaTime;
        }

        for (int i = 0; i<pips.Count; i++)
        {
            if (MovePips(pips[i], player.GetComponent<SpriteRenderer>()))
            {
                Destroy(pips[i]);
                pips.RemoveAt(i);
                i--;
                Collisions.score += 50;
                if (pipsGained < 50)
                {
                    pipsGained++;
                }
                timeSinceGotPip = 0;
                continue;
            }
            if (pips[i].transform.position.x < -width)
            {
                Destroy(pips[i]);
                pips.RemoveAt(i);
                i--;
                continue;
            }
        }

        collectedPipsText.text = pipsGained + "/50";
        if (timeSinceGotPip < 0.7)
        {
            collectedPipsText.color = Color.yellow;
        }
        else
        {
            collectedPipsText.color = Color.white;
        }
        timeSinceGotPip += Time.deltaTime;
    }

    public void CreatePips(int numOfPips, Transform shipPos)
    {
        for (int i = 0; i < numOfPips; i++)
        {
            GameObject pip = Instantiate(PipObject);
            pip.transform.position = shipPos.position;
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            pip.GetComponent<PipInfo>().direction = randomDirection;

            pips.Add(pip);

        }
    }

    bool MovePips(GameObject pipObject, SpriteRenderer play)
    {
        SpriteRenderer pip = pipObject.GetComponent<SpriteRenderer>();
        Vector3 pRadius = pip.bounds.size / 2;
        Vector3 eRadius = play.bounds.size / 4;
        Vector3 distance = play.bounds.center - pip.bounds.center;

        if (pRadius.magnitude + eRadius.magnitude > distance.magnitude)
        {
            return true;
        }
        if ((pRadius.magnitude + eRadius.magnitude) * driftRange > distance.magnitude)
        {
            distance.Normalize();
            pipObject.transform.position += distance * drifSpeed * Time.deltaTime;
        }

        return false;
    }

    public void ResetPips()
    {
        for (int i = 0; i < pips.Count; i++)
        {
            Destroy(pips[i]);
        }
        pips.Clear();
        pipsGained = 0;
        timeSinceGotPip = 1;
    }
}
