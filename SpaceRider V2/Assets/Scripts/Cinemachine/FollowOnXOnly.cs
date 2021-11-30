using UnityEngine;
using Cinemachine;

public class FollowOnXOnly : CinemachineExtension
{
    [Tooltip("Lock the camera's Z position to this value")]
    public float m_yPos = 10;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.y = m_yPos;
            state.RawPosition = pos;
        }
    }
}
