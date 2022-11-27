using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    [SerializeField]
    private float speed = 1;

    void movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
