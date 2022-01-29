using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : Collidable
{
    public string sceneName;
    // Start is called before the first frame update
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            GameManager.instance.SaveState();
            //
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
