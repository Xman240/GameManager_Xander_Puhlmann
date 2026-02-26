using UnityEngine;

public class GhostDataRecorder : MonoBehaviour
{
    public GhostData ghostData;
    bool isRecording;
    JSonSaving save;
    public Animator animator;

    [ContextMenu("animate")]
    public void StartAnimation()
    {
        animator.SetTrigger("nextCar");

    }

    private void Start()
    {
        StartRecording();
    }
    public void StopRecording() {
        isRecording = false;
        SaveProfile profile = new SaveProfile("sujan", 10);
        profile.ghostData = ghostData;
       // save.SaveData(profile);
    }
    public void StartRecording() { isRecording = true; }
    private void FixedUpdate()
    {
        if (!isRecording) return;

        Vector3 position = transform.position;
        Vector3 rotation = transform.eulerAngles;

        GhostDataFrame frame = new GhostDataFrame(position, rotation);

        ghostData.ghostDataFrames.Add(frame);
    }
}
