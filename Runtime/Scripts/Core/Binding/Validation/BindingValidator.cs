using System;

namespace SBaier.DI
{
    public class BindingValidator : Injectable
    {
        private FromBindingValidator _fromValidator;

		void Injectable.Inject(Resolver resolver)
		{
            _fromValidator = resolver.Resolve<FromBindingValidator>();
        }

		public void Validate(Binding binding)
		{
            ValidateIsNotNull(binding);
            _fromValidator.Validate(binding);
        }

		private void ValidateIsNotNull(Binding binding)
		{
			if (binding == null)
				throw new ArgumentNullException();
		}
	}
}