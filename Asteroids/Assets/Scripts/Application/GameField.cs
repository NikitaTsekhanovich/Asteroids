using UnityEngine;

namespace Application
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;
        
        private const float BoundRange = -0.3f;

        public void Init(
            RectTransform rectBackground,
            Canvas gameCanvas)
        {
            _boxCollider2D.offset = Vector2.zero;
            
            _boxCollider2D.size = new Vector2(
                rectBackground.rect.size.x * gameCanvas.transform.localScale.x,
                rectBackground.rect.size.y * gameCanvas.transform.localScale.y);
            
            BoundX = _boxCollider2D.size.x / 2;
            BoundY = _boxCollider2D.size.y / 2;
        }
        
        public float BoundX { get; private set; }
        public float BoundY { get; private set; }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var objectTransform = collision.transform;

            if (Mathf.Abs(objectTransform.position.y) - BoundY >= BoundRange &&
                Mathf.Abs(objectTransform.position.x) - BoundX >= BoundRange)
            {
                objectTransform.position *= -1;
            }
            else if (Mathf.Abs(objectTransform.position.y) >= BoundY)
            {
                objectTransform.position = 
                    new Vector3(objectTransform.position.x, -objectTransform.position.y, 0f);
            }
            else
            {
                objectTransform.position = 
                    new Vector3(-objectTransform.position.x, objectTransform.position.y, 0f);
            }
        }
    }
}
