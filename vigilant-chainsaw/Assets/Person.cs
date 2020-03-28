using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Groceries
{
    Nothing,
    ToiletPaper,
}

public enum State
{
    Complacent,
    NormalWaiting,
    VeryWaiting,
    Happy,
    Angry,
}

public class Person : MonoBehaviour
{
    public Groceries need = Groceries.Nothing;
    public State state = State.Complacent;
    public Transform normalWaiting;
    public Transform veryWaiting;
    public Transform happy;
    public Transform angry;

    private List<Transform> stateGameObjectsList;


    // Start is called before the first frame update
    void Start()
    {
        stateGameObjectsList = new List<Transform> { normalWaiting, veryWaiting, happy, angry };
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger something");

        if (state == State.NormalWaiting || state == State.VeryWaiting)
        {
            if (collision.transform.CompareTag(need.ToString()))
            {
                Debug.Log("Thanks");
                state = State.Happy;
            }
            else
            {
                Debug.Log("Hey!");
                state = State.Angry;
            }
        }
    }

    private void Render()
    {
        var go = GetCurrentStatesGameObject();

        stateGameObjectsList.ForEach(delegate (Transform transform)
        {
            // Don't deactivate the current go, so that particle effects are not
            // reset and keep running.
            if (transform.gameObject != go)
            {
                transform.gameObject.SetActive(false);
            }
        });

        if (go && !go.activeSelf)
        {
            go.SetActive(true);
        }
    }

    private GameObject GetCurrentStatesGameObject()
    {
        switch (state)
        {
            case State.Angry:
                return angry.gameObject;
            case State.Happy:
                return happy.gameObject;
            case State.NormalWaiting:
                return normalWaiting.gameObject;
            case State.VeryWaiting:
                return veryWaiting.gameObject;
            default:
                return null;
        }
    }
}
