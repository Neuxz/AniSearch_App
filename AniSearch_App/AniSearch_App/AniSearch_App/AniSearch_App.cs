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
        public enum SearchKit { anime, manga, movie, character }
        string[] anime = { "anime", "manga", "movie", "character" };
        const string searchrl = "http://www.anisearch.com/{0}/index/?char=all&text={1}&q=true";
        public async Task<string> Search(string srb, SearchKit krt)
        {
            string url = String.Format(searchrl, anime[(int)krt], srb);
            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync(url);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlAgilityPack.HtmlDocument resultat = new HtmlAgilityPack.HtmlDocument();
            resultat.LoadHtml(source);
            String st = "";
            foreach(HtmlNode ht in resultat.DocumentNode.Descendants())
            {
                st+=("-----\n");
                st += ("ID:" + ht.Id + "\n");
                st += ("Attributes:" + ht.Attributes + "\n");
                st += ("Name:" + ht.Name + "\n");
                if (ht.ChildNodes.Count > 0)
                    st += "Childs" + readHtmlNodes(ht, 1);
                st += ("-----\n");
                st += ("\n");
            }
            List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
            (x => (x.Name == "tr" && x.Attributes["responsive-table mtC"] != null &&
            x.Attributes["class"].Value.Contains("block_content"))).ToList();
            return st;
        }

        private string readHtmlNodes(HtmlNode html, int tabCount)
        {
            string tab = "";
            string st = "";
            for ( int i = 0; i < tabCount; i++) tab += "\t";
            foreach (HtmlNode ht in html.ChildNodes)
            {
                st += (tab + "-----\n");
                st += (tab + "ID:" + ht.Id + "\n");
                st += (tab + "Attributes:" + ht.Attributes + "\n");
                st += (tab + "Name:" + ht.Name + "\n");
                if (ht.ChildNodes.Count > 0)
                    st += "tab + Childs" + readHtmlNodes(ht, tabCount + 1);
                st += (tab + "-----\n");
                st += ("\n");
            }
            return st;
        }
    }
}