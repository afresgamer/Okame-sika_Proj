using UnityEngine;

/*
人形を３回タッチ・スワイプする。受け取った指令で内容は変わる
３回タップし終わると、 3-呪い人形の数 だけの人形を入手する
*/
public class Swipe_Dolls : MonoBehaviour
{
    private bool Enabled;
    private bool _rotating;
    private float _rot;

    void Start()
    {
        _rotating = false;
        Enabled = true;
    }

    void Update()
    {
        if (GameController.instance.isCameraMove)
        {
            GameController.instance.info = AppUtil.GetTouch();

            if (Enabled == false)
            {
                return;
            }

            if (GameController.instance.info == TouchInfo.Began)
            {
                _rot = transform.eulerAngles.y + GetAngle(AppUtil.GetTouchPosition());
                _rotating = true;
            }
            else if (GameController.instance.info == TouchInfo.Ended)
            {
                _rotating = false;
            }
            else
            {
                // nothing to do
            }

            if (!_rotating)
            {
                return;
            }

            transform.rotation = Quaternion.Euler(0f, _rot - GetAngle(AppUtil.GetTouchPosition()), 0f);
        }
    }

	private float GetAngle (Vector3 pos)
	{
		var camera = FindObjectOfType<Camera>();
		var origin = camera.WorldToScreenPoint (transform.position);

		Vector3 diff = pos - origin;

		var angle = diff.magnitude < Vector3.kEpsilon
			? 0.0f
			: Mathf.Atan2 (diff.y, diff.x);

		return angle * Mathf.Rad2Deg;
	}
}
