using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mopsicus.TwinSlider {

	/// <summary>
	/// Twin slider with border limit
	/// </summary>
	public class TwinSlider : MonoBehaviour {

		/// <summary>
		/// Callback on slider change
		/// </summary>
		public Action<float, float> OnSliderChange;

		/// <summary>
		/// First slider
		/// </summary>
		[SerializeField]
		private Slider SliderOne;

		/// <summary>
		/// Second slider
		/// </summary>
		[SerializeField]
		private Slider SliderTwo;

		/// <summary>
		/// Background image
		/// </summary>
		[SerializeField]
		private Image Background;

		/// <summary>
		/// Filler between sliders
		/// </summary>
		[SerializeField]
		private Image Filler;
        [SerializeField]
        private Image Filler1;
        /// <summary>
        /// Filler color
        /// </summary>
        [SerializeField]
		private Color Color;

		/// <summary>
		/// Min sliders value
		/// </summary>
		public float Min = 0f;

		/// <summary>
		/// Max sliders value
		/// </summary>
		public float Max = 1f;

		/// <summary>
		/// Border limit between sliders
		/// </summary>
		public float Border = 0f;

        public float Locked = 1f;

		/// <summary>
		/// Filler rect cache
		/// </summary>
		private RectTransform _fillerRect;

		/// <summary>
		/// Half of common slider width
		/// </summary>
		private float _width;

		/// <summary>
		/// Constructor
		/// </summary>
		private void Awake () {
            Filler = transform.Find("Filler").GetComponent<Image>();
            Filler1 = transform.Find("Filler1").GetComponent<Image>();

            _fillerRect = Filler.GetComponent<RectTransform> ();
			_width = GetComponent<RectTransform> ().sizeDelta.x / 2f;
			SliderOne.minValue = Min;
			SliderOne.maxValue = Max;
			SliderTwo.minValue = Min;
			SliderTwo.maxValue = Max;
			Filler.color = Color;
			if (OnSliderChange == null) {
				OnSliderChange += delegate { };
			}
		}


        public void OnLockedChange(float value)
        {
            SliderTwo.value = value;
        }

        /// <summary>
        /// Callback on first slider change
        /// </summary>
        public void OnCorrectSliderOne(float value) {
            DrawFiller(SliderOne.handleRect.localPosition, SliderTwo.handleRect.localPosition);
			if (value > SliderTwo.value - Border) {
				SliderOne.value = SliderTwo.value - Border;
			} else {
				OnSliderChange.Invoke (SliderOne.value, SliderTwo.value);
			}
		}

		/// <summary>
		/// Callback on second slider change
		/// </summary>
		public void OnCorrectSliderTwo (float value) {
			DrawFiller (SliderOne.handleRect.localPosition, SliderTwo.handleRect.localPosition);
			if (value < SliderOne.value + Border) {
				SliderTwo.value = SliderOne.value + Border;
			} else {
				OnSliderChange.Invoke (SliderOne.value, SliderTwo.value);
			}
		}



		/// <summary>
		/// Draw filler
		/// </summary>
		/// <param name="one">Coords of first slider handle</param>
		/// <param name="two">Coords of second slider handle</param>
		void DrawFiller (Vector3 one, Vector3 two) {
			float left = Mathf.Abs (_width + (_fillerRect.localPosition - new Vector3 { x = _width }).x);
            float right = Mathf.Abs(_width - two.x);

            Filler.GetComponent<RectTransform>().offsetMax = new Vector2 (-right, 0f);
            Filler.GetComponent<RectTransform>().offsetMin = new Vector2 (0, 0f);

            left = Mathf.Abs(_width + one.x);
            
            Filler1.GetComponent<RectTransform>().offsetMax = new Vector2(-right, 0f);
            Filler1.GetComponent<RectTransform>().offsetMin = new Vector2(left, 0f);
        }

	}

}