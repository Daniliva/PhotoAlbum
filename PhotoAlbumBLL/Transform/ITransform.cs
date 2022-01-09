namespace PhotoAlbumBLL.Transform
{
    public interface ITransform<T1, T2>
        where T1 : class
        where T2 : class
    {
        T1 Transform(T2 item);

        T2 Transform(T1 item);
    }
}