using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TarotCardSelect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int curCardIdx = 0;
    private Vector3 startPos;
    private Vector3 dragStartPos;
    private bool isDragging = false;
    public RectTransform deck;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Input.mousePosition;
            startPos = transform.position;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // 드래그 중
            Vector3 currentPos = Input.mousePosition;
            Vector3 delta = currentPos - dragStartPos;

            // 화면 좌표 → 월드 좌표 변환 (UI면 RectTransform.anchoredPosition 사용)
            transform.position = startPos + delta * 0.01f; // 감도 조절
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // 터치 종료
            Vector3 endPos = Input.mousePosition;
            Vector3 swipeDelta = endPos - dragStartPos;

            if (swipeDelta.magnitude > 200f)  // 일정 거리 이상
            {
                // 밀어내기
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(swipeDelta.normalized * 500f); // 힘 줘서 날리기
                }
            }
            else
            {
                // 제자리로 되돌리기
                transform.position = startPos;
            }

            isDragging = false;
        }
    }   
}
