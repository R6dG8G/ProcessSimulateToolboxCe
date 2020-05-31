using Robworld.PsPublicLibrary.CommandEnablers;
using Tecnomatix.Engineering;

namespace Robworld.PsCommands
{
    /// <summary>
    /// Logical groups from compound parts command class
    /// </summary>
    public class CreateLogicalGroupsFromPartsCommand : TxButtonCommand
    {
        #region Fields
        private readonly RwAlwaysTrueCommandEnabler enabler;
        #endregion

        #region Properties
        /// <summary>
        /// Get the Category under which the command appears
        /// </summary>
        public override string Category => "Robworld GmbH & Co. KG";

        /// <summary>
        /// Get the description of the command
        /// </summary>
        public override string Description => "Create logical groups from compound parts based on variant names or prototype names";

        /// <summary>
        /// Shows the tooltip of the command
        /// </summary>
        public override string Tooltip => "Create logical groups from compound parts based on variant names or prototype names";

        /// <summary>
        /// Get the name of the command
        /// </summary>
        public override string Name => "Create logical part groups";

        /// <summary>
        /// Get the 16x16 bitmap of the command
        /// </summary>
        public override string Bitmap => "Images.Commands.CreateLogicalGroups16x16.bmp";

        /// <summary>
        /// Get the 32x32 bitmap of the command
        /// </summary>
        public override string LargeBitmap => "Images.Commands.CreateLogicalGroups32x32.png";

        /// <summary>
        /// Get the command enabler
        /// </summary>
        public override ITxCommandEnabler CommandEnabler
        {
            get { return enabler; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the command
        /// </summary>
        public CreateLogicalGroupsFromPartsCommand()
        {
            enabler = new RwAlwaysTrueCommandEnabler();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Execute the selected command
        /// </summary>
        /// <param name="cmdParams"></param>
        public override void Execute(object cmdParams)
        {
            try
            {
                LogicalGroups.RwCreateLogicalGroupsFromCompoundPartsView view = new LogicalGroups.RwCreateLogicalGroupsFromCompoundPartsView();
                view.Show();
            }
            catch (TxException ex)
            {
                string caption = "An Exception occured!!";
                TxMessageBox.ShowModal(ex.Message, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Activate the command if conditions are okay
        /// </summary>
        /// <returns>The boolean value of the conditions check</returns>
        public override bool Connect()
        {
            return true;
        }
        #endregion
    }
}
