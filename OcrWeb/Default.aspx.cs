using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace OcrWeb
{
    public partial class Default : System.Web.UI.Page
    {
        //This is a quick app to show POC for using microsoft's ocr library in a web app. 
        //There needs to be better feedback for the user. This is an example only!
        #region Event Handlers

        private void OnSubmitFileClicked(object sender, EventArgs args)
        {
            if (imageFile.PostedFile != null && imageFile.PostedFile.ContentLength > 0)
            {
                // for now just fail hard if there's any error however in a propper app I would expect a full demo.
                FileInfo fi = new FileInfo(imageFile.PostedFile.FileName);
                //saveAsPath is set to the path represented by 
                //Windows.Storage.ApplicationData.Current.LocalFolder 
                //in the CommandLineOcr windows store app for simplicity
                string saveAsPath = @"C:\Users\Wesley\AppData\Local\Packages\2ca0072b-e230-42c2-a5f2-6ee47ccce84d_yekwsnrkhg0pr\LocalState\" + fi.Name;
                imageFile.PostedFile.SaveAs(saveAsPath);
                OcrCommandLineCaller.StartOcr(fi.Name);
                if (File.Exists(saveAsPath + ".txt"))
                {
                    resultText.InnerText = File.ReadAllText(saveAsPath + ".txt");

                }
                else
                {
                    resultText.InnerText = "Failed";
                }
                inputPanel.Visible = false;
                resultPanel.Visible = true;
            }
        }

        private void OnRestartClicked(object sender, EventArgs args)
        {
            resultPanel.Visible = false;
            inputPanel.Visible = true;
        }

        #endregion

        #region Page Setup
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        //----------------------------------------------------------------------
        private void InitializeComponent()
        {
            this.restartButton.ServerClick += OnRestartClicked;
            this.submitFile.ServerClick += OnSubmitFileClicked;
        }

        #endregion
    }
}