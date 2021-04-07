using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarPrint : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Radar radar;
    [SerializeField]
    private RectTransform arrowRect;
    [SerializeField]
    private RectTransform canvasRect;
    [SerializeField]
    private Image radarImg;

    private Vector3 dirArrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!radar.IsColossus())
        {
            SetVisible(false);
            return;
        }

        //Vector3 dir = radar.GetDirVector();
        //float angle = Vector3.Angle(dir, cam.transform.forward);
        //arrowRect.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 screenPoint = GetColossusScreenPoint();
        //Vector3 cappedScreenPoint = Capping(screenPoint);

        Vector3 arrowPos;
        float xlimit = canvasRect.sizeDelta.x / 2 - canvasRect.sizeDelta.x / 10;
        float ylimit = canvasRect.sizeDelta.y / 2 - canvasRect.sizeDelta.y / 10;

        if (screenPoint.x < -xlimit && screenPoint.y > 0 || screenPoint.x > xlimit && screenPoint.y < 0)
        {
            arrowPos = new Vector3(-xlimit, 0, 0);
            arrowRect.localEulerAngles = new Vector3(0, 0, 90);
        }
        else if (screenPoint.x > xlimit && screenPoint.y > 0 || screenPoint.x < -xlimit && screenPoint.y > 0)
        {
            arrowPos = new Vector3(xlimit, 0, 0);
            arrowRect.localEulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            arrowRect.localEulerAngles = new Vector3(0, 0, 180);
            if (screenPoint.z < 0)
                arrowPos = new Vector3(0, -ylimit, 0);
            else
            {
                if (radar.GetDist() < 50)
                {
                    SetVisible(false);
                    return;
                }
                arrowPos = screenPoint;
            }
        }

        SetVisible(true);
        Vector3 arrowWorldPosition = cam.ScreenToWorldPoint(arrowPos);
        arrowRect.localPosition = new Vector3(arrowPos.x, arrowPos.y, -1f);
    }

    private Vector3 GetColossusScreenPoint()
    {
        radar.GetColossusPos();

        Vector3 viewportPos = cam.WorldToViewportPoint(radar.GetColossusPos());

        Debug.Log(viewportPos);

        return new Vector3(
            ((viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)),
            viewportPos.z
            );
    }

    private Vector3 Capping(Vector3 toCap)
    {
        Vector3 cappedScreenPoint = toCap;
        float xlimit = canvasRect.sizeDelta.x / 2 - canvasRect.sizeDelta.x / 10;
        float ylimit = canvasRect.sizeDelta.y / 2 - canvasRect.sizeDelta.y / 10;

        if (cappedScreenPoint.x < -xlimit) cappedScreenPoint.x = -xlimit;
        if (cappedScreenPoint.x > xlimit) cappedScreenPoint.x = xlimit;
        if (cappedScreenPoint.y < -ylimit) cappedScreenPoint.y = -ylimit;
        if (cappedScreenPoint.y > ylimit) cappedScreenPoint.y = ylimit;

        if (cappedScreenPoint.z < 0 && Mathf.Abs(cappedScreenPoint.x) != xlimit && Mathf.Abs(cappedScreenPoint.y) != ylimit)
        {
            cappedScreenPoint.y = -ylimit;
        }

        return cappedScreenPoint;
    }

    private void SetVisible(bool b)
    {
        Color color = radarImg.color;
        if (b)
            color.a = 100;
        else
            color.a = 0;
        radarImg.color = color;
    }

    private void UpdateDir()
    {
        dirArrow = radar.GetDirVector();
    }

    private void UpdateArrow()
    {

    }
}
