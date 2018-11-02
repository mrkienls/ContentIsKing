namespace ContentIsKing.MasterK
{
    public  class Main_MasterK
    {
        static string html = GetDataFromUrl.getHTML("https://www.facebook.com/truyencuoihay");
        static  string[] posts = GetDataFromUrl.getPostFromHtml(html);
        static string p = posts[0];
    }


   
}
