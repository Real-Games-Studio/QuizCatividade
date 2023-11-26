using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FeelManager : MonoBehaviour
{
    [SerializeField] private MMF_Player _buttonFeels, _addPoints, _removePoints;

    public void PlayFeelButton()
    {
        _buttonFeels.PlayFeedbacks();
    }
    public void PlayFeelAddPoints()
    {
        _addPoints.PlayFeedbacks();
    }
    public void PlayFeelRemovePoints()
    {
        _removePoints.PlayFeedbacks();
    }
}
