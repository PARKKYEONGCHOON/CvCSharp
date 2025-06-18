public static Mat BitmapToMat(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            try
            {
                int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
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