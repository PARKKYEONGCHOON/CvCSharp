    public static Mat BitmapToMat(Bitmap bitmap)
    {
        
        if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
        {
            Bitmap converted = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(converted))
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            }

            bitmap = converted;
        }

        
        Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

        try
        {
            int stride = bmpData.Stride;
            int bytes = stride * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

           
            Mat mat = new Mat(bitmap.Height, bitmap.Width, MatType.CV_8UC3);
            Marshal.Copy(rgbValues, 0, mat.Data, bytes);

            return mat;
        }
        finally
        {
            bitmap.UnlockBits(bmpData);
        }
    }