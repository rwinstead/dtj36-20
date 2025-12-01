using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class WebglVideo : MonoBehaviour
{
    [Header("Video Components")]
    public VideoPlayer player;
    public string videoFileName = "intro.mp4";

    void Start()
    {
        // Assign RT to player
        player.source = VideoSource.Url;

        // Build streaming path
        string path = Path.Combine(Application.streamingAssetsPath, videoFileName);
        player.url = path;

        // Prepare video asynchronously
        player.Prepare();
        player.prepareCompleted += OnPrepared;
    }

    void OnPrepared(VideoPlayer p)
    {
        player.Play();
    }
}
