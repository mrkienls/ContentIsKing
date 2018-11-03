using ContentIsKing.MasterK;
using System.Collections.Generic;

namespace ContentIsKing.MasterK
{
    public   class Main_MasterK
    {
          public Main_MasterK()
        {
            string html = GetDataFromUrl.getHTML("https://www.facebook.com/truyencuoihay/posts/");
            List<PostContent> postContents = GetDataFromUrl.getPostFromHtml(html);
        }
         
       
    }

   

}
