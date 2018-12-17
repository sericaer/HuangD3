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
        public float Lock;

        public float Curr = 0f;

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

			Filler.color = Color;
			if (OnSliderChange == null) {
				OnSliderChange += delegate { };
			}
		}

        private void Start()
        {
            SliderOne.minValue = Min;
            SliderOne.maxValue = Max;
            SliderOne.value = Curr;

            SliderTwo.minValue = Min;
            SliderTwo.maxValue = Max;
            SliderTwo.value = Lock;
        }

        public void OnLockedChange(float value)
        {
            Lock = value;
            if(SliderOne.value > Lock)
            {
                SliderOne.value = Lock;
                OnCorrectSliderOne(Lock);
            }
            SliderTwo.value = Lock;
            OnCorrectSliderTwo(Lock);

            if(Lock.CompareTo(Max) == 0)
            {
                SliderTwo.transform.Find("Handle Slide Area").gameObject.SetActive(false);
            }
            else
            {
                SliderTwo.transform.Find("Handle Slide Area").gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Callback on first slider change
        /// </summary>
        public void OnCorrectSliderOne(float value) {
            DrawFiller(SliderOne.handleRect.localPosition, SliderTwo.handleRect.localPosition);
			if (value > SliderTwo.value) {
				SliderOne.value = SliderTwo.value;
			} else {
				OnSliderChange.Invoke (SliderOne.value, SliderTwo.value);
			}
		}

		/// <summary>
		/// Callback on second slider change
		/// </summary>
		public void OnCorrectSliderTwo (float value) {
			DrawFiller (SliderOne.handleRect.localPosition, SliderTwo.handleRect.localPosition);
			if (value < SliderOne.value) {
				SliderTwo.value = SliderOne.value;
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