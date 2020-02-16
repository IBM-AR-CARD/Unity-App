using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoFade : MonoBehaviour
{
    public GameObject logo1;
    public GameObject logo2;
    private int alpha = 800;
    private bool decreasing = true;
    private bool logo1IsCurrent = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("startFadingLogo", 0, 0.01f);
    }

    // Update is called once per frame
    void startFadingLogo()
    {
        Debug.Log(alpha);
        alpha = alpha + (decreasing ? -1 : 1);
        if (alpha <= 0)
        {
            decreasing = false;
            logo1IsCurrent = !logo1IsCurrent;
        }
        if (alpha >= 800)
        {
            decreasing = true;
        }
        GameObject current = logo1IsCurrent ? logo1 : logo2;
        current.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha / 255f);

    }
}
