using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class PlayerTrigger : MonoBehaviour
{
    public PlayableDirector director;
    public ²¾°Ê PlayerMove;
    public bool PlayerTouch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerTouch)
            return;
        director.Play();
        PlayerTouch = true;
        PlayerMove.enabled = false;
        PlayerMove.GetComponent<Animator>().speed = 0;
        Invoke("FinishInvoke", (float)director.duration);
    }
    void FinishInvoke()
    {
        PlayerMove.enabled = true;
        PlayerMove.GetComponent<Animator>().speed = 1;
    }
}
