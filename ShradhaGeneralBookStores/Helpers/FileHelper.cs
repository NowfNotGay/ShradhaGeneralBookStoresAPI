namespace ShradhaGeneralBookStores.Helpers;
public static class FileHelper
{
    public static string genderateFileName(this string filename)
    {
        var name = Guid.NewGuid().ToString().Replace("-", "");
        var lasstdot = filename.LastIndexOf('.');
        var ext = filename.Substring(lasstdot);
        return name + ext;
    }
}
