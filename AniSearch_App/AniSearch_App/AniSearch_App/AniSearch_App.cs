using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace AniSearch_App
{
    public class ApiRead
    {
        public enum SearchKrit { anime, manga, movie, character }
        string[] anime = { "anime", "manga", "movie", "character" };
        const string searchrl = "http://www.anisearch.com/{0}/index/?char=all&text={1}&q=true";
        public async Task webClient(string srb, SearchKrit krt)
        {
            string url = String.Format(searchrl, anime[(int)krt], srb);
            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync(url);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlAgilityPack.HtmlDocument resultat = new HtmlAgilityPack.HtmlDocument();
            resultat.LoadHtml(source);

            List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
            (x => (x.Name == "tr" && x.Attributes["responsive-table mtC"] != null &&
            x.Attributes["class"].Value.Contains("block_content"))).ToList();
        }
    }
}