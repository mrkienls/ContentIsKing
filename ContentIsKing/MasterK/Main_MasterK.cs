using ContentIsKing.MasterK;
using System.Collections.Generic;

namespace ContentIsKing.MasterK
{
    public   class Main_MasterK
    {
          public Main_MasterK()
        {
            string html = GetDataFromUrl.getHTML("https://www.facebook.com/pg/truyencuoihay/posts/");
            List<PostContent> postContents = GetDataFromUrl.getPostFromHtml(html);
        }
         
       
    }

    public class PostContent
    {
        public string content { get; set; }
        public string image { get; set; }
        public string video { get; set; }
    }

}
