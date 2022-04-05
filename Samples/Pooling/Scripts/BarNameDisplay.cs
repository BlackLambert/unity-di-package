using UnityEngine;
using UnityEngine.UI;

namespace SBaier.DI.Examples.Pooling
{
    public class BarNameDisplay : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Text _text;

		private Bar _bar;

		public void Inject(Resolver resolver)
		{
			_bar = resolver.Resolve<Bar>();
		}

		private void OnEnable()
		{
			UpdateText();
		}

		private void Start()
		{
			UpdateText();
		}

		private void UpdateText()
		{
			_text.text = _bar.ToString();
		}
	}
}
