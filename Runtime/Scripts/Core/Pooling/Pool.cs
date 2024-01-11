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

	public interface Pool<TItem, TArg1, TArg2>
	{
		TItem Request(TArg1 arg1, TArg2 arg2);
		void Return(TItem item);
	}
}
