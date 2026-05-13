using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Mozo.Comun.Helper.Global;

public static partial class Glo
{
    //  c.NoPersona.Mayuscula().Substring(1, 1).Dibuja(new Font("Arial", 24.0f), Color.Black, Color.White, 90, 90).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

    public static byte[] EmptyImage(this string Text)
    {
        var Ms = new MemoryStream();
        try
        {
            if (Text.EsNulo())
                Text = "NOT";
            else
                Text = Text.Mayuscula().Substring(0, 1);
            var Img = Dibuja(Text, new Font("Arial", 24.0f), Color.Black, Color.White, 90, 90);
            Img.Save(Ms, ImageFormat.Jpeg);
            return Ms.ToArray();
        }
        finally
        {
            Ms.Dispose();
            Ms = null;
        }
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    c.NoPersona.EmptyImage().Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    return File(ms.ToArray(), "image/jpg");
        //}
    }

    public static Image Dibuja(this string text, Font font, Color textColor, Color backColor, float? NuWidth = null,
        float? NuHeight = null)
    {
        //first, create a dummy bitmap just to get a graphics object
        Image img = new Bitmap(1, 1);
        var drawing = Graphics.FromImage(img);

        //measure the string to see how big the image needs to be
        var textSize = drawing.MeasureString(text, font);

        //free up the dummy image and old graphics object
        img.Dispose();
        drawing.Dispose();

        //create a new image of the right size
        if (NuWidth != null && NuHeight != null)
            img = new Bitmap((int)NuWidth, (int)NuHeight);
        else
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);


        drawing = Graphics.FromImage(img);

        //paint the background
        drawing.Clear(backColor);

        //create a brush for the text
        Brush textBrush = new SolidBrush(textColor);

        var sf = new StringFormat();
        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;

        drawing.DrawString(text, font, textBrush, new Rectangle(0, 0, img.Width, img.Height), sf);


        drawing.Save();

        textBrush.Dispose();
        drawing.Dispose();

        return img;
    }
}