using System;
using Tecnomatix.Engineering;

namespace Robworld.PsViewers
{
    /// <summary>
    /// Robotic Operation Viewer class
    /// </summary>
    public class RoboticOperationViewer : TxWPFViewer
    {
        #region Properties
        /// <summary>
        /// Get the name of the viewer
        /// </summary>
        public override string ViewerName => "Robotic Operations Viewer";

        /// <summary>
        /// Get a short description of the viewer
        /// </summary>
        public override string Description => "Performs several tests on Robotic operations with the assigned robot";

        /// <summary>
        /// Get the context menu of the viewer
        /// </summary>
        public override ITxViewerMenusCollection Menus => null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the viewer
        /// </summary>
        public RoboticOperationViewer()
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
                Uri resourceLocator = new Uri("/Robworld.PsViewers;component/RoboticOperations/RwRoboticOperationView.xaml", UriKind.Relative);
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
