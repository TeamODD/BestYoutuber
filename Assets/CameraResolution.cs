using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void awake()
    {
        Camera cam = GetCoponent<Camera>();

        Rect rt = cam.rect;

        float scale_height=((float)screen.width/screen.height)/ ((float)9 / 16);
        float scale_width=lf/scale_height;

        if (scale_height<1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width=scale_width;
            rt.x = (1f - scale_width)/2f;
        }

        cam.rect = rt;
    }
}
