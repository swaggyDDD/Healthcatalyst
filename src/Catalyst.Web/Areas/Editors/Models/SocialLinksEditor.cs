namespace Catalyst.Web.Areas.Editors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Model for the social links editor.
    /// </summary>
    public class SocialLinksEditor : EditorFormBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialLinksEditor"/> class.
        /// </summary>
        public SocialLinksEditor()
            : this ("Saving ...")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialLinksEditor"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public SocialLinksEditor(string title)
            : base(title)
        {
        }

        /// <summary>
        /// Gets or sets the property id.
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// Gets or sets the Facebook Url.
        /// </summary>
        [Display(Name = "Facebook")]
        public string Facebook { get; set; }

        /// <summary>
        /// Gets or sets the Twitter Url.
        /// </summary>
        [Display(Name = "Twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn Url.
        /// </summary>
        [Display(Name = "LinkedIn")]
        public string LinkedIn { get; set; }

        /// <summary>
        /// Gets or sets the Google+ Url.
        /// </summary>
        [Display(Name = "GooglePlus")]
        public string GooglePlus { get; set; }

        /// <summary>
        /// Gets or sets the YouTube Url.
        /// </summary>
        [Display(Name = "YouTube")]
        public string YouTube { get; set; }
    }
}