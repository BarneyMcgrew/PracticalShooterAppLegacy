﻿using Xamarin.Forms;

namespace PracticalShooterApp.Models
{
    public class BoardingContentModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        public View RotatorView { get; set; }

        #endregion
    }
}