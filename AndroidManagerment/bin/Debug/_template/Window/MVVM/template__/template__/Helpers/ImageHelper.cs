using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace template__.Helpers
{
    // sử dụng thư viện SautinSoft
    public static class ImageHelper
    {
        /// <summary>
        /// Chuyển đổi toàn bộ page của file pdf sang dạng ảnh .png
        /// </summary>
        /// <param name="numberThread">so luong thuc hien </param>
        /// <param name="rootFolder">folder chua file pdf</param>
        /// <param name="pdfs">file pdf </param>
        public static void ConvertPdfToPngInThread(int numberThread, string rootFolder, string pdfs)
        {
            string file = File.ReadAllText(pdfs);

            Queue<ThreadManager> q = new Queue<ThreadManager>();
            for (int i = 0; i < numberThread; i++)
            {
                var t = new ThreadManager();
                t.CreateThread(pdfs, i, numberThread, rootFolder).Start();
                q.Enqueue(t);
            }

            while (q.Count != 0)
            {
                var first = q.Peek();
                if (first.IsDone == true)
                    q.Dequeue();
            }
            Console.WriteLine("Done");
        }
    }

    public class ThreadManager
    {
        public bool IsDone { get; set; }

        public ThreadManager()
        {
            IsDone = false;
        }

        public Thread CreateThread(string pdfFile, int index, int numberThread, string rootFoler)
        {
            return new Thread(() =>
            {
                string pngFile = Path.ChangeExtension(pdfFile, ".png");

                //SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
                //f.ImageOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                //f.ImageOptions.Dpi = 300;
                //f.OpenPdf(pdfFile);

                //int range = f.PageCount / numberThread;
                //if (range == 0)
                //    range = f.PageCount;

                //int start = index * range;
                //int end = start + range;

                //if (end > f.PageCount)
                //    end = f.PageCount;

                //for (int i = start; i <= end; i++)
                //{
                //    if (f.ToImage(pngFile, i) == 0)
                //    {
                //        try
                //        {
                //            System.IO.File.Move(pngFile, rootFoler + @"/page" + i + ".png");
                //        }
                //        catch (Exception) { }
                //    }
                //}
                //f.ClosePdf();
                //IsDone = true;
            });
        }
    }
}
