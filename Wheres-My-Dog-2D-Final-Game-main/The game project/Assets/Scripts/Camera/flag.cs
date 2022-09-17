using UnityEngine;

public class flag: MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private ControllerBG cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "temp")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom);
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }
}