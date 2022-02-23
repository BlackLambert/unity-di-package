using UnityEngine;

namespace SBaier.DI.Examples
{
	[CreateAssetMenu(fileName = "PhiSettingsInstaller", menuName = "ScriptableObjects/PhiSettingsInstaller")]
	public class PhiSettingsInstaller : ScriptableObjectInstaller
	{
		[SerializeField]
		private PhiSettings _settings;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_settings);
		}
	}
}
