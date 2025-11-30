using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash Instance;

    public Image flashImage; // full-screen white image

    void Awake()
    {
        Instance = this;
        flashImage.gameObject.SetActive(false); // start hidden
    }

    public void FlashWhite()
    {
        flashImage.gameObject.SetActive(true);
    }

    public void ClearFlash()
    {
        flashImage.gameObject.SetActive(false);
    }
}
