using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Groceries
{
    Nothing,
    ToiletPaper,
    Desinfectant,
    Yeast,
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
    public float timeBetweenStateChangesSeconds = 3f;
    public float chanceOfStateChange = 0.5f;

    private List<Transform> stateGameObjectsList;


    // Start is called before the first frame update
    void Start()
    {
        stateGameObjectsList = new List<Transform> { normalWaiting, veryWaiting, happy, angry };
        StartCoroutine(ChangeState());
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

    // private void SetState(State newState) {


    // }

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

    private IEnumerator ChangeState()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStateChangesSeconds);

            var chance = Random.Range(0f, 1f);

            if (chance > chanceOfStateChange)
            {
                switch (state)
                {
                    case State.Complacent:
                        need = (Groceries)Random.Range(1, System.Enum.GetNames(typeof(Groceries)).Length);
                        state = State.NormalWaiting;
                        break;
                    case State.NormalWaiting:
                        state = State.VeryWaiting;
                        break;
                    case State.VeryWaiting:
                        state = State.Angry;
                        break;
                    case State.Happy:
                    case State.Angry:
                        need = Groceries.Nothing;
                        state = State.Complacent;
                        break;
                }
            }
        }

    }
}
