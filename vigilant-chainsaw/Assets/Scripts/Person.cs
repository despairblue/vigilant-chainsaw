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

    // States
    public Transform normalWaiting;
    public Transform veryWaiting;
    public Transform happy;
    public Transform angry;

    // Needs
    public GameObject bubble;
    public GameObject toiletPaper;
    public GameObject soap;
    public GameObject flour;

    // Other Stuff
    public GameObject scoreTemplate;

    // GameLoop
    public float timeBetweenStateChangesSeconds = 3f;
    public float variance = 0.5f;
    public float speedUpFactor = 0.95f;
    public float speedUpTimer = 20f;
    public float chanceOfStateChange = 0.5f;

    private List<Transform> stateGameObjectsList;
    private List<GameObject> needsGameObjectsList;
    private GameState gameState;
    private BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        var gameStateGameObject = GameObject.FindGameObjectWithTag("GameState");
        gameState = gameStateGameObject.GetComponent<GameState>();

        stateGameObjectsList = new List<Transform> { normalWaiting, veryWaiting, happy, angry };
        needsGameObjectsList = new List<GameObject> { toiletPaper, soap, flour };

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        StartCoroutine(ChangeState());
        StartCoroutine(ChangeSpeed());
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
                // gameState.IncreaseScore(100);
                CreateScoreObject();
            }
            else
            {
                Debug.Log("Hey!");
                state = State.Angry;
                gameState.ReduceLife();
            }
            // Disables the speech bubble as well.
            need = Groceries.Nothing;
            boxCollider.enabled = false;
        }
    }

    private void CreateScoreObject()
    {
        var score = Instantiate(scoreTemplate);
        score.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        score.SetActive(true);
    }

    private void Render()
    {
        var currentStateGameObject = GetCurrentStatesGameObject();
        var currentNeedGameObject = GetCurrentNeedsGameObject();

        // RENDER STATE
        stateGameObjectsList.ForEach(delegate (Transform stateTransform)
        {
            if (stateTransform.gameObject == currentStateGameObject)
            {
                SetActiveIfChanged(stateTransform.gameObject, true);
            }
            else
            {
                SetActiveIfChanged(stateTransform.gameObject, false);

            }
        });

        // RENDER NEEDS
        if (currentNeedGameObject == null)
        {
            // Disable bubble if we don't have any needs.
            SetActiveIfChanged(bubble, false);
        }
        else
        {
            // Enable bubble if we have needs.
            SetActiveIfChanged(bubble, true);
            // Disable all but the current need.
            needsGameObjectsList.ForEach(delegate (GameObject needGameObject)
            {
                if (needGameObject == currentNeedGameObject)
                {
                    SetActiveIfChanged(needGameObject, true);
                }
                else
                {
                    SetActiveIfChanged(needGameObject, false);
                }

            });
        }
    }

    private void SetActiveIfChanged(GameObject gameObject, bool active)
    {
        if (gameObject.activeSelf != active)
        {
            gameObject.SetActive(active);
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

    private GameObject GetCurrentNeedsGameObject()
    {
        switch (need)
        {
            case Groceries.Desinfectant:
                return soap;
            case Groceries.ToiletPaper:
                return toiletPaper;
            case Groceries.Yeast:
                return flour;
            default:
                return null;
        }
    }

    private IEnumerator ChangeState()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStateChangesSeconds + Random.Range(-variance, variance));

            var chance = Random.Range(0f, 1f);

            if (chance < chanceOfStateChange)
            {
                switch (state)
                {
                    case State.Complacent:
                        need = (Groceries)Random.Range(1, System.Enum.GetNames(typeof(Groceries)).Length);
                        state = State.NormalWaiting;
                        boxCollider.enabled = true;
                        break;
                    case State.NormalWaiting:
                        state = State.VeryWaiting;
                        break;
                    case State.VeryWaiting:
                        state = State.Angry;
                        need = Groceries.Nothing;
                        gameState.ReduceLife();
                        break;
                    case State.Happy:
                    case State.Angry:
                        need = Groceries.Nothing;
                        state = State.Complacent;
                        boxCollider.enabled = false;
                        break;
                }
            }
        }

    }

    private IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedUpTimer);

            timeBetweenStateChangesSeconds = timeBetweenStateChangesSeconds * speedUpFactor;
            variance = variance * speedUpFactor;
        }
    }
}
