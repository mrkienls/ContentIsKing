using ContentIsKing.MasterK;
using System.Collections.Generic;

namespace ContentIsKing.MasterK
{
    public   class Main_MasterK
    {
          public Main_MasterK()
        {
            string html = GetDataFromUrl.getHTML("https://www.facebook.com/pg/truyencuoingancucvui/posts/");
            List<PostContent> postContents = GetDataFromUrl.getPostFromHtml(html);



            string noidung = "";
            foreach (PostContent c in postContents)
            {
                noidung = c.content;
                string urlImage = c.image;
                string pathImageSaved = GetDataFromUrl.DownloadImage(urlImage);
                Post_To_Xenzu.post_xenzu(noidung, pathImageSaved,"nguyentrungkienctn","cstd1234");
            }
        

         
        }
         
       
    }

   

}
