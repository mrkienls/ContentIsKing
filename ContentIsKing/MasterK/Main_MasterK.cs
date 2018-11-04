using ContentIsKing.MasterK;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ContentIsKing.MasterK
{
    public   class Main_MasterK
    {
          public Main_MasterK()
        {
            //  string html = GetDataFromUrl.getHTML("https://www.facebook.com/pg/nhunghinhanhhaihuocvavuinhon/posts/");
            string html = GetDataFromUrl.getHTML("https://www.facebook.com/motminhlatotnhat/posts");
            List<PostContent> postContents = GetDataFromUrl.getPostFromHtml(html);



            string noidung = "";
            string pathImageSaved = "";
            foreach (PostContent c in postContents)
            {
                noidung = c.content;
                noidung = Regex.Replace(noidung, "<.*?>|&.*?;", "");


                string urlImage = c.image;
                if (urlImage != "")
                { pathImageSaved = GetDataFromUrl.DownloadImage(urlImage); }
                else
                { pathImageSaved = ""; }


                // save to db xml
                DatabaseXML.MainDatabase.saveXML("db.xml",noidung,pathImageSaved);

                //   Post_To_Xenzu.post_xenzu(noidung, pathImageSaved,"nguyentrungkienctn","cstd1234");
            }
        

         
        }
         
       
    }

   

}
