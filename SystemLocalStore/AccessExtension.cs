namespace SystemLocalStore
{
    public static class AccessExtension
    {
        public static T Load<T>(this T abs)
        {
            return (T)abs;
        }
    }
}
