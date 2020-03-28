using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceriesGun : MonoBehaviour
{
    public List<Transform> groceries;
    public Transform canon;
    public Transform groceriesGroup;

    public float activeOffset = 0.17f;
    public float shootVelocity = 5f;
    public float canonRotationSpeed = 10;
    private int active = -1;

    // Start is called before the first frame update
    void Start()
    {
        SelectGrocery(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            SelectGrocery(active - 1);
        }

        if (Input.GetKeyDown("d"))
        {
            SelectGrocery(active + 1);
        }

        if (Input.GetKeyDown("e"))
        {
            canon.Rotate(0, 0, -canonRotationSpeed);
        }

        if (Input.GetKeyDown("q"))
        {
            canon.Rotate(0, 0, canonRotationSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
            var selectedGrocery = groceries[active];
            var preset = selectedGrocery.GetChild(0);
            var copy = Instantiate(preset);
            var rigidBody2d = copy.GetComponent<Rigidbody2D>();

            copy.gameObject.SetActive(true);

            copy.SetPositionAndRotation(selectedGrocery.position, Quaternion.identity);
            rigidBody2d.rotation = canon.rotation.eulerAngles.z;
            rigidBody2d.AddRelativeForce(new Vector2(0, shootVelocity), ForceMode2D.Impulse);
        }
    }

    private void SelectGrocery(int index)
    {
        if (index == active)
        {
            return;
        }

        if (index == -1)
        {
            index = groceries.Count - 1;
        }

        var oldActive = active >= 0 ? groceries[active] : null;
        if (oldActive)
        {
            oldActive.Translate(new Vector2(0f, -activeOffset));
        }

        active = index % groceries.Count;
        var newActive = groceries[active];
        newActive.Translate(new Vector2(0f, activeOffset));
    }
}
