namespace SBaier.DI
{
    public interface Pool<TItem>
    {
		TItem Request();
		void Return(TItem item);
	}

	public interface Pool<TItem, TArg>
	{
		TItem Request(TArg arg);
		void Return(TItem item);
	}
}
