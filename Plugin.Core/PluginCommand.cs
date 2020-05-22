namespace Plugin.Core
{
    public abstract class BasePluginCommand
    {
    }

    public abstract class PluginCommand<T> : BasePluginCommand
    {
        public T Result { get; set; }
    }
}