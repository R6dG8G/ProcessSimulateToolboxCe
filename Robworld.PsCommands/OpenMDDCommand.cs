using Robworld.PsPublicLibrary.CommandEnablers;
using Tecnomatix.Engineering;

namespace Robworld.PsCommands
{
    /// <summary>
    /// OpenMDD command class
    /// </summary>
    public class OpenMDDCommand : TxButtonCommand
    {
        #region Fields
        private readonly RwSingleRobotCommandEnabler enabler;
        #endregion

        #region Properties
        /// <summary>
        /// Get the Category under which the command appears
        /// </summary>
        public override string Category => "Robworld GmbH & Co. KG";

        /// <summary>
        /// Get a short description of the command
        /// </summary>
        public override string Description => "Open the machine data directory of the selected robot";

        /// <summary>
        /// Shows the tooltip of the command
        /// </summary>
        public override string Tooltip => "Open the machine data directory of the selected robot";

        /// <summary>
        /// Get the name of the command
        /// </summary>
        public override string Name => "Open MDD";

        /// <summary>
        /// Get the 16x16 bitmap of the command
        /// </summary>
        public override string Bitmap => "Images.Commands.OpenMDD16x16.bmp";

        /// <summary>
        /// Get the 32x32 bitmap of the command
        /// </summary>
        public override string LargeBitmap => "Images.Commands.OpenMDD32x32.png";

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
        /// Creates a new instance of the OpenMDD command
        /// </summary>
        public OpenMDDCommand()
        {
            enabler = new RwSingleRobotCommandEnabler();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Executes the OpenMDD command
        /// </summary>
        /// <param name="cmdParams"></param>
        public override void Execute(object cmdParams)
        {
            try
            {
                OpenMDD.RwOpenMDD.OpenMachineDataDirectory();
            }
            catch (TxArgumentException exception)
            {
                string caption = "OpenMDD argument exception!";
                TxMessageBox.ShowModal(exception.Message, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            catch (System.Exception exception)
            {
                string caption = "OpenMDD exception!";
                TxMessageBox.ShowModal(exception.Message, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
