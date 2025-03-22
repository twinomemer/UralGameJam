using UnityEngine;
using UnityEngine.UI;

public class SliderFollowObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // Объект, за которым будет следовать слайдер
    [SerializeField] private Vector3 offset; // Смещение слайдера относительно объекта (в пикселях)
    [SerializeField] private Slider slider; // Ссылка на слайдер

    private RectTransform canvasRectTransform;
    private Camera mainCamera;

    void Start()
    {
        // Получаем компоненты
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        mainCamera = Camera.main;

        // Проверка, что все компоненты на месте
        if (canvasRectTransform == null)
        {
            Debug.LogError("Canvas RectTransform не найден!");
        }
        if (mainCamera == null)
        {
            Debug.LogError("Основная камера не найдена!");
        }
        if (slider == null)
        {
            Debug.LogError("Слайдер не назначен!");
        }
        if (targetObject == null)
        {
            Debug.LogError("Целевой объект не назначен!");
        }
    }

    void Update()
    {
        if (targetObject != null && slider != null && mainCamera != null && canvasRectTransform != null)
        {
            // Преобразуем мировые координаты объекта в экранные координаты
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetObject.position);

            // Проверка, находится ли объект перед камерой
            if (screenPosition.z >= 0)
            {
                // Добавляем смещение (например, 50 пикселей вверх)
                screenPosition += offset;

                // Преобразуем экранные координаты в локальные координаты канваса
                Vector2 localPoint;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, mainCamera, out localPoint))
                {
                    // Устанавливаем позицию слайдера
                    slider.GetComponent<RectTransform>().anchoredPosition = localPoint;
                }
                else
                {
                    Debug.LogWarning("Не удалось преобразовать экранные координаты в локальные.");
                }
            }
            else
            {
                Debug.LogWarning("Объект находится за камерой.");
            }
        }
    }
}