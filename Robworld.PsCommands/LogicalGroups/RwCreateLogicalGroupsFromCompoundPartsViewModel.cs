using Engineering.ModelObjects;
using EngineeringInternalExtension.ModelObjects;
using Robworld.PsPublicLibrary.Mvvm;
using Robworld.PsPublicLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Tecnomatix.Engineering;
using Tecnomatix.Engineering.PrivateImplementationDetails;
using Tecnomatix.Engineering.Ui.WPF;
using Tecnomatix.Planning;

namespace Robworld.PsCommands.LogicalGroups
{
    /// <summary>
    /// Create logical Groups from a selected compound part
    /// </summary>
    public class RwCreateLogicalGroupsFromCompoundPartsViewModel : RwViewModelBase
    {
        #region Fields
        private string logicalGroupsGroupBoxHeader;
        private Dictionary<string, TxObjectList> logicalGroups;
        private bool canEnableGroupingRulesInput;
        private string groupingRules;
        private string[] rules;
        #endregion

        #region Properties
        /// <summary>
        /// Get the title of the window
        /// </summary>
        public string Title { get { return "Create logical groups for compound parts"; } }

        /// <summary>
        /// Get the height of the window
        /// </summary>
        public int WindowHeight { get { return 350; } }

        /// <summary>
        /// Get the width of the window
        /// </summary>
        public int WindowWidth { get { return 260; } }

        /// <summary>
        /// Get the caption of the logical groups data groupbox
        /// </summary>
        public string LogicalGroupsGroupBoxHeader
        {
            get { return logicalGroupsGroupBoxHeader; }
            private set
            {
                if (logicalGroupsGroupBoxHeader != value)
                {
                    logicalGroupsGroupBoxHeader = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the logical groups
        /// </summary>
        public ObservableCollection<RwLogicalGroupViewModel> LogicalGroupsData { get; set; }

        /// <summary>
        /// Get or set the selected grouping mode
        /// </summary>
        public RwLogicalGroupCreationModes SelectedGrouping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CanEnableGroupingRulesInput
        {
            get { return canEnableGroupingRulesInput; }
            set
            {
                if (canEnableGroupingRulesInput != value)
                {
                    canEnableGroupingRulesInput = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the rules for grouping by the part prototype name
        /// </summary>
        public string GroupingRules
        {
            get { return groupingRules; }
            set
            {
                if (groupingRules != value)
                {
                    groupingRules = value;
                    CreateGroupingRules();
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get the tooltip for the grouping rules input
        /// </summary>
        public string GroupingRulesTooltip { get; private set; }

        /// <summary>
        /// Represents the command for selecting the grouping mode
        /// </summary>
        public ICommand GroupingRuleCommand { get; private set; }

        /// <summary>
        /// Represents the command for executing the logical groups creation process
        /// </summary>
        public ICommand ExecuteLogicalGroupsCreationCommand { get; private set; }

        /// <summary>
        /// Represents the command for choosing the compund part
        /// </summary>
        public ICommand CompoundPartPickedEvent { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create an instance of the create logical groups command
        /// </summary>
        public RwCreateLogicalGroupsFromCompoundPartsViewModel()
        {
            LogicalGroupsGroupBoxHeader = "Logical Groups";
            GroupingRulesTooltip = "Separator is |";
            SelectedGrouping = RwLogicalGroupCreationModes.VARIANTNAME;
            CanEnableGroupingRulesInput = false;
            LogicalGroupsData = new ObservableCollection<RwLogicalGroupViewModel>();
            GroupingRuleCommand = new RwActionCommand(SetGroupingRuleExecuted, SetGroupingRuleCanExecute);
            ExecuteLogicalGroupsCreationCommand = new RwActionCommand(LogicalGroupsCreationExecuted, LogicalGroupsCreationCanExecute);
            CompoundPartPickedEvent = new RwActionCommand(CompoundPartPickedExecuted, CompoundPartPickedCanExecute);
            LogicalGroupsData.CollectionChanged += OnLogicalGroupsDataCollectionChanged;
        }
        #endregion

        #region Command enablers
        /// <summary>
        /// Decide if the command for picking a compound part can be enabled
        /// </summary>
        /// <param name="arg">Not in use</param>
        /// <returns>Returns always true</returns>
        private bool CompoundPartPickedCanExecute(object arg)
        {
            return true;
        }

        /// <summary>
        /// Decide if the radio buttons for the grouping rules can be enabled
        /// </summary>
        /// <param name="arg">Not in use</param>
        /// <returns>Returns always true</returns>
        private bool SetGroupingRuleCanExecute(object arg)
        {
            return true;
        }

        /// <summary>
        /// Decide if the command for executing the logical groups creation can be enabled
        /// </summary>
        /// <param name="arg">Not in use</param>
        /// <returns>Returns true if a compund part is selected</returns>
        private bool LogicalGroupsCreationCanExecute(object arg)
        {
            return LogicalGroupsData != null && LogicalGroupsData.Count > 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set the compound part for the logical groups creation process
        /// </summary>
        /// <param name="obj">The TxObjEditBoxControl of the view</param>
        private void CompoundPartPickedExecuted(object obj)
        {
            if (obj is TxObjEditBoxControl wpfControl)
            {
                TxCompoundPart compoundPart = wpfControl.Object as TxCompoundPart;
                if (compoundPart != null)
                {
                    logicalGroups = new Dictionary<string, TxObjectList>();
                    TraverseCompoundPart(compoundPart);
                    ConvertDictionaryToObservableCollection();
                }
                else
                {
                    LogicalGroupsData.Clear();
                }
            }
        }

        /// <summary>
        /// Convert the dictionary of parts to an Observalble collection
        /// </summary>
        private void ConvertDictionaryToObservableCollection()
        {
            LogicalGroupsData.Clear();
            foreach (KeyValuePair<string, TxObjectList> pair in logicalGroups)
            {
                LogicalGroupsData.Add(new RwLogicalGroupViewModel { GroupName = pair.Key, GroupTypeName = string.Empty, Items = pair.Value });
            }
        }

        /// <summary>
        /// Traverse the Compound part and add the components to the dictionary.
        /// Empty omponents are skipped!
        /// </summary>
        /// <param name="compoundPart">The selected compund part</param>
        private void TraverseCompoundPart(TxCompoundPart compoundPart)
        {
            if (compoundPart.Count > 0)
            {
                foreach (var item in compoundPart)
                {
                    if (item is TxCompoundPart subCompoundPart)
                    {
                        //Recursive call of TraverseCompoundPart
                        TraverseCompoundPart(subCompoundPart);
                    }
                    else if (item is TxComponent component)
                    {
                        GroupComponent(component);
                    }
                }
            }
        }

        /// <summary>
        /// Group the component to a logical group
        /// </summary>
        /// <param name="component">The current component</param>
        private void GroupComponent(TxComponent component)
        {
            if (SelectedGrouping == RwLogicalGroupCreationModes.VARIANTNAME)
            {
                GroupComponentByVariantName(component);
            }
            else if (SelectedGrouping == RwLogicalGroupCreationModes.PROTOTYPENAME)
            {
                GroupComponentByPartName(component);
            }
        }

        /// <summary>
        /// Group the components by the filename of the prototype
        /// </summary>
        /// <param name="component">The current component</param>
        private void GroupComponentByPartName(TxComponent component)
        {
            TxStorage storage = component.StorageObject as TxStorage;
            if (storage is TxLibraryStorage && component.Visibility != TxDisplayableObjectVisibility.CannotBeDisplayed && rules != null && rules.Length > 0)
            {
                string filename = RwFileAndDirectoryUtilities.GetFilenameWithoutExtension((storage as TxLibraryStorage).FullPath).ToUpper();
                foreach (string rule in rules)
                {
                    if (filename.Contains(rule))
                    {
                        AddToDictionary(rule, component);
                        return;
                    }
                }
                AddToDictionary("EMPTY", component);
            }
        }

        /// <summary>
        /// Group the components by the variant name column of the resource tree
        /// </summary>
        /// <param name="component">The current component</param>
        private void GroupComponentByVariantName(TxComponent component)
        {
            /*
             * Thanks to Siemens API Team for support
             * https://community.plm.automation.siemens.com/t5/Tecnomatix-Developer-Forum/Variant-name/m-p/550867#M1499
            */
            ITxPlanningObject planningObject = component.PlanningRepresentation;
            if (planningObject is ITxPlanningVariantSetAssignable variantSetAssignable && component.Visibility != TxDisplayableObjectVisibility.CannotBeDisplayed)
            {
                //Needs a using Engineering.ModelObjects; beginning with V15.1
                //Needs a using EngineeringInternalExtension.ModelObjects; for V15.0 and lower
                ITxPlanningVariantSet variantSet = variantSetAssignable.GetVariantSet();
                if (variantSet != null)
                {
                    AddToDictionary(variantSet.Name, component);
                }
                else
                {
                    AddToDictionary("EMPTY", component);
                }
            }
        }

        /// <summary>
        /// Create the rules for grouping by part name
        /// </summary>
        private void CreateGroupingRules()
        {
            rules = null;
            rules = GroupingRules.Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Add a part to the dictionary
        /// </summary>
        /// <param name="key">The variant name of the part</param>
        /// <param name="component">The part as a component</param>
        private void AddToDictionary(string key, TxComponent component)
        {
            if (!logicalGroups.ContainsKey(key))
            {
                logicalGroups.Add(key, new TxObjectList { component });
            }
            else
            {
                logicalGroups[key].Add(component);
            }
        }

        /// <summary>
        /// Set the grouping type
        /// </summary>
        /// <param name="obj">The grouping parameter</param>
        private void SetGroupingRuleExecuted(object obj)
        {
            string mode = obj.ToString();
            if (mode.Equals("V"))
            {
                CanEnableGroupingRulesInput = false;
                SelectedGrouping = RwLogicalGroupCreationModes.VARIANTNAME;
            }
            else
            {
                CanEnableGroupingRulesInput = true;
                SelectedGrouping = RwLogicalGroupCreationModes.PROTOTYPENAME;
            }
        }

        /// <summary>
        /// Execute the logical groups creation process
        /// </summary>
        /// <param name="obj">Not in use</param>
        private void LogicalGroupsCreationExecuted(object obj)
        {
            foreach (RwLogicalGroupViewModel data in LogicalGroupsData)
            {
                TxLogicalGroup group = TxApplication.ActiveDocument.LogicalRoot.CreateLogicalGroup(new TxLogicalGroupCreationData { Name = data.GroupName, TypeName = data.GroupTypeName });
                group.AddObjects(data.Items);
            }
        }

        /// <summary>
        /// Update the groupbox header of the logical groups collection
        /// </summary>
        /// <param name="sender">The logical groups collection</param>
        /// <param name="e">The collection changed event arguments</param>
        private void OnLogicalGroupsDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (LogicalGroupsData != null)
            {
                LogicalGroupsGroupBoxHeader = $"({LogicalGroupsData.Count}) logical group(s) to create";
            }
        }
        #endregion
    }
}
