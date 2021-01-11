using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using TitlesSimilarity.Wikipedia.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace TitlesSimilarity.Wikipedia
{
    class WikiRequest
    {
        private static string apiUrlBegin = "https://en.wikipedia.org/w/api.php?action=query&format=json";
        private static string[] wikiCommonImagesIgnoreList =
        {
            "File:Commons-logo.svg",
            "File:BSicon ",
            "File:Flag of ",
            "File:Question book-new.svg",
            "File:Red pog.svg"
        };

        /// <summary>
        /// Obtains articles from Wiki by specified location.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Article> GetArticles(Location location, uint count = 50)
        {
            string queryUrl = apiUrlBegin + $"&list=geosearch&gsradius={location.Radius}&"
            + $"gscoord={location.WikiQueryCoordinates}&gslimit={count}";
            Article[] articles = new Article[count];

            string json = PerformRequest(queryUrl);
            ArticlesGeo response = JsonDeserialize<ArticlesGeo>(json);

            foreach (Article article in response.query.geosearch)
                yield return article;
        }

        /// <summary>
        /// Enumerates through wiki images.
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        public IEnumerable<Image> GetImageTitles(IEnumerable<Article> articles)
        {
            string pageIds = string.Join("|", (from a in articles select a.pageid));
            string queryUrl = apiUrlBegin + $"&prop=images&imlimit=500&pageids={pageIds}";
            string json, urlImContinueSuffix = "";
            ImagesResponse response;

            do
            {
                json = PerformRequest(queryUrl + urlImContinueSuffix);
                json = json.Replace("{\"continue\":", "{\"can_continue\":");
                response = JsonDeserialize<ImagesResponse>(json);

                urlImContinueSuffix = "&imcontinue=" + response?.can_continue?.imcontinue;

                if (response.query == null || response.query.pages == null)
                    yield break;

                foreach(var page in response.query.pages)
                {
                    if(page.Value.images != null)
                        foreach (var image in page.Value.images)
                            yield return image;
                }
            } while (response?.can_continue != null);
        }

        /// <summary>
        /// Performs a request and read response.
        /// </summary>
        /// <param name="queryUrl">URL to request.</param>
        /// <returns>String representation of the response.</returns>
        private string PerformRequest(string queryUrl)
        {
            WebRequest request = WebRequest.Create(queryUrl);
            request.Proxy = WebRequest.GetSystemWebProxy();
            request.Timeout = 3000;

            string responseText;
            using (StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                responseText = responseReader.ReadToEnd();
            }
            return responseText;
        }

        /// <summary>
        /// Deserializes JSON text.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="json">Text in JSON format.</param>
        /// <returns>Deserialized object of T type.</returns>
        private T JsonDeserialize<T>(string json)
        {
            var jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Checks for some wiki common image titles.
        /// </summary>
        /// <param name="imgTitle">Image title.</param>
        /// <returns>True if it is wiki common image, false - otherwise.</returns>
        public static bool IsWikiCommonsImage(string imgTitle)
        {
            foreach (string commonImgTitle in wikiCommonImagesIgnoreList)
                if (imgTitle.StartsWith(commonImgTitle))
                    return true;

            return false;
        }
    }
}
