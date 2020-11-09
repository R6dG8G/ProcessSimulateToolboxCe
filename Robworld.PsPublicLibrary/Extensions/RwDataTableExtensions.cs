using System.Collections.Generic;
using System.Data;

namespace Robworld.PsPublicLibrary.Extensions
{
    /// <summary>
    /// DataTable extension for reordering the columns
    /// </summary>
    public static class RwDataTableExtensions
    {
        /// <summary>
        /// DataTable extension for reordering the columns on the basis of a column index
        /// </summary>
        /// <param name="table">The DataTable that has to be reordered</param>
        /// <param name="startColumnIndex">Zero based index of the start column</param>
        /// <returns></returns>
        public static DataTable Reorder(this DataTable table, int startColumnIndex)
        {
            if (table == null) return null;

            SortedSet<string> sorted = new SortedSet<string>();
            int columnIndex = startColumnIndex;
            for (int i = startColumnIndex; i < table.Columns.Count; i++)
            {
                sorted.Add(table.Columns[i].ColumnName);
            }

            for (int i = 0; i < startColumnIndex; i++)
            {
                table.Columns[i].SetOrdinal(i);
            }

            foreach (string columnName in sorted)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
            }
            return table;
        }

        /// <summary>
        /// DataTable extension for reordering the columns on the basis of a given prefix
        /// </summary>
        /// <param name="table">The DataTable that has to be reordered</param>
        /// <param name="prefix">Only the columns that starts with the defined prefix are reordered</param>
        /// <returns></returns>
        public static DataTable Reorder(this DataTable table, string prefix)
        {
            if (table == null) return null;

            SortedSet<string> sorted = new SortedSet<string>();
            int columnIndex = 0;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].ColumnName.StartsWith(prefix))
                {
                    sorted.Add(table.Columns[i].ColumnName);
                }
                else
                {
                    table.Columns[i].SetOrdinal(columnIndex);
                    columnIndex++;
                }
            }

            foreach (string columnName in sorted)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
            }
            return table;
        }
    }
}
