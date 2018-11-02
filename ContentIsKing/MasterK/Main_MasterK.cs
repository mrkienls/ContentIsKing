namespace ContentIsKing.MasterK
{
    public  static class Main_MasterK
    {
      
        static string html = GetDataFromUrl.getHTML("https://www.facebook.com/pg/truyencuoihay/posts/");
        static  string[] posts = GetDataFromUrl.getPostFromHtml(html);
        //static string p = posts[0];
        public static string hello="hello";

    }


   
}
