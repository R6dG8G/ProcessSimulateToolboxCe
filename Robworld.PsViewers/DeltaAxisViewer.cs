using System;
using Tecnomatix.Engineering;

namespace Robworld.PsViewers
{
    /// <summary>
    /// Delta axis viewer class
    /// </summary>
    public class DeltaAxisViewer : TxWPFViewer
    {
        #region Properties
        /// <summary>
        /// Get the name of the viewer
        /// </summary>
        public override string ViewerName => "Delta axis";

        /// <summary>
        /// Get a short description of the viewer
        /// </summary>
        public override string Description => "Shows the difference angles of a robot on two locations";

        /// <summary>
        /// Get the context menu of the viewer
        /// </summary>
        public override ITxViewerMenusCollection Menus => null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the viewer
        /// </summary>
        public DeltaAxisViewer()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the viewer
        /// </summary>
        public override void Initialize()
        {
            try
            {
                Uri resourceLocator = new Uri("/Robworld.PsViewers;component/DeltaAxis/RwDeltaAxisView.xaml", UriKind.Relative);
                System.Windows.Application.LoadComponent(this, resourceLocator);
            }
            catch (TxException ex)
            {
                string caption = "An Exception occured!!";
                TxMessageBox.ShowModal(ex.Message, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        #endregion
    }
}
