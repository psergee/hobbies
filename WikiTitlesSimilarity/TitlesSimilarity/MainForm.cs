using System;
using System.ComponentModel;
using System.Windows.Forms;
using TitlesSimilarity.Wikipedia;
using System.Collections.Generic;
using System.Linq;
using TitlesSimilarity.ML.Structures;
using TitlesSimilarity.ML.Metrics;
using TitlesSimilarity.ML.Algorithms;

namespace TitlesSimilarity
{
    public partial class MainForm : Form
    {
        private IEnumerable<Cluster> imgTitleClusters;
        private string[] imageTitles;

        private Location[] someInterestingPlaces =
        {
            new Wikipedia.Location { Name = "Bermuda", Latitude = 32.307800, Longitude = -64.750500, Radius = 10000 },
            new Wikipedia.Location { Name = "Everest", Latitude = 27.987850, Longitude = 86.925026, Radius = 10000 },
            new Wikipedia.Location { Name = "Hong Kong", Latitude = 22.396428, Longitude = 114.109497, Radius = 10000 },
            new Wikipedia.Location { Name = "Helsinki", Latitude = 60.169856, Longitude = 24.938379, Radius = 10000 },
            new Wikipedia.Location { Name = "Tokyo", Latitude = 35.689487, Longitude = 139.691706, Radius = 10000 }
        };

        public MainForm()
        {
            InitializeComponent();
            wikiRequester.DoWork += FindSimilarImageTitles;
            wikiRequester.RunWorkerCompleted += WikiRequester_RunWorkerCompleted;
            wikiRequester.ProgressChanged += WikiRequester_ProgressChanged;

            locationsCmbBx.DataSource = someInterestingPlaces;
            locationsCmbBx.DisplayMember = "Name";
        }

        private void WikiRequester_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.UserState is string)
                opStatusLbl.Text = (string)e.UserState;
        }

        /// <summary>
        /// Fills the text box with grouped image titles.
        /// </summary>
        private void WikiRequester_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || imgTitleClusters == null || imageTitles == null)
            {
                goBtn.Enabled = true;
                return;
            }

            string groupsDelimiter = Environment.NewLine + Environment.NewLine;
            foreach(var cluster in imgTitleClusters.OrderBy(cluster => cluster.items.Count).Reverse().Take(10))
            {
                imageTitlesTxtBox.AppendText(string.Join(Environment.NewLine, (from i in cluster.items select imageTitles[i])));
                imageTitlesTxtBox.AppendText(groupsDelimiter);
            }

            opStatusLbl.Text = "";
            goBtn.Enabled = true;
        }

        /// <summary>
        /// Start background work to find similar image titles.
        /// </summary>
        private void goBtn_Click(object sender, System.EventArgs e)
        {
            goBtn.Enabled = false;
            imageTitlesTxtBox.Clear();
            wikiRequester.RunWorkerAsync(locationsCmbBx.SelectedValue);
        }

        /// <summary>
        /// Obtains and groups wiki image titles by similarity.
        /// </summary>
        private void FindSimilarImageTitles(object sender, DoWorkEventArgs e)
        {
            WikiRequest wiki = new WikiRequest();
            Location interstingPlace = (Location)e.Argument;
            BackgroundWorker bgWorker = (BackgroundWorker)sender;

            try
            {
                bgWorker.ReportProgress(20, "Requesting wiki articles...");
                var wikiArticles = wiki.GetArticles(interstingPlace, 50);

                bgWorker.ReportProgress(40, "Requesting image titles...");
                imageTitles = (from img in wiki.GetImageTitles(wikiArticles)
                               where !WikiRequest.IsWikiCommonsImage(img.title)
                               select img.title.Remove(0, 5)).ToArray();
            }
            catch(Exception err)
            {
                bgWorker.ReportProgress(0, err.Message);
                e.Cancel = true;
                return;
            }

            bgWorker.ReportProgress(60, "Calculating moon phases...");
            double[][] similarityMatrix = JaggedDistanceMatrix.ComputeSimilarity(imageTitles, new TextCosine());

            bgWorker.ReportProgress(80, "Almost done...");
            var clustering = new AgglomerativeClustering();
            imgTitleClusters = clustering.Clusterize(similarityMatrix);
        }
    }
}
