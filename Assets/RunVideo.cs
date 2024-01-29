using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using OpenCover.Framework.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class RunVideo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private StudioEventEmitter FModSound;
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField] private bool _hideOnEnd;
    [SerializeField] private bool _skipable;

    [SerializeField]
    UnityEvent onEndVideo;
    public void Awake()
    {
        videoPlayer.targetCamera = Camera.main;
    }

    public void Play()
    {
        videoPlayer.loopPointReached += EndReached;
        FModSound.Play();
        videoPlayer.Play();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TrySkip();
        }
    }

    private void TrySkip()
    {
        if (_skipable) EndReached(videoPlayer);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        if (_hideOnEnd)
            vp.targetCamera = null;
        onEndVideo?.Invoke();
    }
}
