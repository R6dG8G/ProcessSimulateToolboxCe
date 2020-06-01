using Robworld.PsPublicLibrary.CommandEnablers;
using Tecnomatix.Engineering;

namespace Robworld.PsCommands
{
    /// <summary>
    /// Mfg import, export and query command class
    /// </summary>
    public class ImportExportQueryMfgCommand : TxCompoundButtonCommand
    {
        #region Fields
        private ITxCompoundButtonElement defaultElement;
        private readonly RwEmsCommandEnabler enabler;
        #endregion

        #region Properties
        /// <summary>
        /// Get the Category under which the command appears
        /// </summary>
        public override string Category => "Robworld GmbH & Co. KG";

        /// <summary>
        /// Get a short description of the command
        /// </summary>
        public override string Description => "Tools for Mfg features";

        /// <summary>
        /// Shows the tooltip of the command
        /// </summary>
        public override string Tooltip => "Tools for Mfg features";

        /// <summary>
        /// Get the name of the command
        /// </summary>
        public override string Name => "Mfg import/export/query";

        /// <summary>
        /// Get the 16x16 bitmap of the command
        /// </summary>
        public override string Bitmap => "Images.Commands.MfgExport16x16.bmp";

        /// <summary>
        /// Get the 32x32 bitmap of the command
        /// </summary>
        public override string LargeBitmap => "Images.Commands.MfgExport32x32.png";

        /// <summary>
        /// Get the default button of the compound button command
        /// </summary>
        public override ITxCompoundButtonElement DefaultElement
        {
            get { return defaultElement; }
        }

        /// <summary>
        /// Get the collection of button commands
        /// </summary>
        public override ITxCompoundButtonRowCollection Rows
        {
            get
            {
                ITxCompoundButtonElement element1 = new TxCompoundButtonElement
                    (
                        "Mfg Export",
                        "Export Mfg data to the file system",
                        "Export Mfg data to the file system",
                        "Images.Commands.MfgExport16x16.bmp",
                        "Images.Commands.MfgExport32x32.png"
                    );
                ITxCompoundButtonElement element2 = new TxCompoundButtonElement
                    (
                        "Mfg Import",
                        "Import Mfgs from the file system to the study",
                        "Import Mfgs from the file system to the study",
                        "Images.Commands.MfgImport16x16.bmp",
                        "Images.Commands.MfgImport32x32.png"
                    );
                ITxCompoundButtonElement element3 = new TxCompoundButtonElement
                    (
                        "MfG Query",
                        "Shows the relationship between MfG Feature and MfG library",
                        "Shows the relationship between MfG Feature and MfG library",
                        "Images.Commands.MfgQuery16x16.bmp",
                        "Images.Commands.MfgQuery32x32.png"
                    );
                TxCompoundButtonElementCollection elements = new TxCompoundButtonElementCollection();
                elements.AddItem(element1);
                elements.AddItem(element2);
                elements.AddItem(element3);
                ITxCompoundButtonRow row = new TxCompoundButtonRow(elements);
                TxCompoundButtonRowCollection rows = new TxCompoundButtonRowCollection();
                rows.AddItem(row);
                defaultElement = element1;
                return rows;
            }
        }

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
        public ImportExportQueryMfgCommand()
        {
            enabler = new RwEmsCommandEnabler();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Execute the selected command
        /// </summary>
        /// <param name="cmdParams">The selected compound button element</param>
        public override void Execute(object cmdParams)
        {
            try
            {
                if (cmdParams is ITxCompoundButtonElement element)
                {
                    switch (element.Name.ToUpper())
                    {
                        case "MFG EXPORT":
                            MfgIEQ.RwExportMfgView exportView = new MfgIEQ.RwExportMfgView();
                            exportView.Show();
                            break;
                        case "MFG IMPORT":
                            MfgIEQ.RwImportMfgView importView = new MfgIEQ.RwImportMfgView();
                            importView.Show();
                            break;
                        case "MFG QUERY":
                            MfgIEQ.RwQueryMfgView queryView = new MfgIEQ.RwQueryMfgView();
                            queryView.Show();
                            break;
                        default:
                            TxMessageBox.ShowModal($"{element.Name} is a non supported command", "Mfg Import/Export/Query error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                            break;
                    }
                }
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
