using TMPro;
using UnityEngine;

public class DeckDragRotate : MonoBehaviour
{
    private Vector3 dragStartPos;
    private bool isDragging = false;
    public RectTransform deck;

    public float maxAngleL = -88f;   // 왼쪽으로 최대 회전
    public float maxAngleR = 67f;    // 오른쪽으로 최대 회전
    public float sensitivity = 1.5f;   // 드래그 감도

    private float baseAngle = -10f;  // 초기 각도
    private float currentAngle;      // 현재 각도 (누적)

    private int curCardIdx = 0;
    public TextMeshProUGUI IdxTextTemp;

    void Start()
    {
        currentAngle = baseAngle;
        deck.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentPos = Input.mousePosition;
            Vector3 delta = currentPos - dragStartPos;

            // 현재 저장된 각도를 기준으로 드래그 값 추가
            float angle = currentAngle + delta.x * sensitivity;
            angle = Mathf.Clamp(angle, -88, 67);

            deck.rotation = Quaternion.Euler(0, 0, angle);

            // 각도 → 카드 인덱스 매핑
            curCardIdx = (int)((-angle + 67 - 5.85) / 1.9);
            if ((-angle + 67) < 5.85) curCardIdx = 0;
            // 예: 범위를 26칸으로 나눈 경우 (필요에 맞게 수정)
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // 드래그 끝난 시점의 각도를 저장 → 다음 드래그 시작 기준
            currentAngle = deck.eulerAngles.z;
            if (currentAngle > 180f) currentAngle -= 360f; // -180~180 보정

            isDragging = false;
        }

        IdxTextTemp.text = curCardIdx.ToString();
    }

    private System.Collections.IEnumerator RotateBack()
    {
        Quaternion startRot = deck.rotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, baseAngle);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 3f;
            deck.rotation = Quaternion.Lerp(startRot, targetRot, t);
            yield return null;
        }
    }
}
