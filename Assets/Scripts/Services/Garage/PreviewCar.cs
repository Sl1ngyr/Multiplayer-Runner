using UnityEngine;

namespace Services.Garage
{
    public class PreviewCar : MonoBehaviour
    {
        [SerializeField] private float _speedRotation;

        private void Update()
        {
            RotatePreviewCar();
        }

        private void RotatePreviewCar()
        {
            transform.Rotate(
                transform.rotation.x,
                _speedRotation * Time.deltaTime,
                transform.rotation.z);
        }
    }
}