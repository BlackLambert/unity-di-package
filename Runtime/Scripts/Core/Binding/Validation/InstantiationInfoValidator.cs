using System;

namespace SBaier.DI
{
    public class InstantiationInfoValidator : Injectable
    {
        private InstanceCreationModeValidator _fromValidator;

		void Injectable.Inject(Resolver resolver)
		{
            _fromValidator = resolver.Resolve<InstanceCreationModeValidator>();
        }

		public void Validate(InstantiationInfo info)
		{
            ValidateIsNotNull(info);
            _fromValidator.Validate(info);
        }

		private void ValidateIsNotNull(InstantiationInfo info)
		{
			if (info == null)
				throw new ArgumentNullException();
		}
	}
}