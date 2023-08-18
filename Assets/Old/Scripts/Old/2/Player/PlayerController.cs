using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;

    [Header("LimitRigion")]
    [SerializeField] Vector2 limit;

	//-------------------------------------------------------------------
	private void OnDestroy()
	{
        Destroy(gameObject);
	}

	void FixedUpdate()
    {
        Move();
        MoveLimit();
    }

//-------------------------------------------------------------------
    // à⁄ìÆ
    void Move()
	{
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var dir = new Vector2(x, y);

        transform.Translate(Time.deltaTime * dir.normalized * speed);
    }

    // à⁄ìÆêßå¿
    void MoveLimit()
	{
        var pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -limit.x, limit.x);
        pos.y = Mathf.Clamp(pos.y, -limit.y, limit.y);

        transform.position = pos;
	}
}
