using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RunVideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.loopPointReached += EndReached;

        videoPlayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        vp.targetCamera = null;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
