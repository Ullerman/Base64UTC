//using System.Data.Linq;
using System.Drawing;

static string Base64Encode(byte[] bytes)
{
    string base64 = Convert.ToBase64String(bytes);
    return base64;
}
static byte[] Base64Decode(string base64)
{
    byte[] bytes = Convert.FromBase64String(base64);
    return bytes;
}
static byte[] PNGToBytes(string path)
{
    List<byte> bytes = new List<byte>();
    Bitmap bitmap = new Bitmap(path);
    for (int y = 0; y < bitmap.Height; y++)
    {
        for (int x = 0; x < bitmap.Width; x++)
        {
            Color color = bitmap.GetPixel(x, y);
            bytes.Add(color.A);
            bytes.Add(color.R);
            bytes.Add(color.G);
            bytes.Add(color.B);
        }
    }
    return bytes.ToArray();
}
static string PNGtobase64(string path)
{
    byte[] bytes = PNGToBytes(path);
    string base64 = Base64Encode(bytes);
    return base64;
}
static Bitmap BytestoPNG(byte[] bytes, int width, int height)
{
    Bitmap bitmap = new Bitmap(width, height);
    int index = 0;
    for(int i = 0; i < bytes.Length; i += 4)
    {
        Color pixel = Color.FromArgb(bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3]);
        int x = index % width;
        int y = index / width;
        bitmap.SetPixel(x, y, pixel);
        index++;
    }

    return bitmap;
}


static Bitmap Base64toPNG(string base64)
{
    byte[] bytes = Base64Decode(base64);
    int width = (int)MathF.Sqrt((float)(base64.Length));
    Bitmap image = BytestoPNG(bytes, width, width);
    return image;
}
static string LoadFile(string path)
{
    string text = "";
    try
    {
        text = File.ReadAllText(path);
    }
    catch (System.IO.FileNotFoundException)
    {
        Console.WriteLine($"File{path} not found");
    }
    return text;
}
static void SaveFile(string path, string text)
{
    File.WriteAllText(path, text);
}
string base64 = PNGtobase64(
    "C:\\Users\\Leon\\OneDrive - Rodillian Multi Academy Trust\\Pictures\\image.png"
);
SaveFile("C:\\Users\\Leon\\OneDrive - Rodillian Multi Academy Trust\\Pictures\\image.txt", base64);
string image = LoadFile(
    "C:\\Users\\Leon\\OneDrive - Rodillian Multi Academy Trust\\Pictures\\image.txt"
);
Bitmap bitmap = Base64toPNG(image);
bitmap.Save("C:\\Users\\Leon\\OneDrive - Rodillian Multi Academy Trust\\Pictures\\image2.png");
