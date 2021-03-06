using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;

namespace GridEXTutorial5CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            productsTableAdapter1.Fill(jsNorthWindDataSet1.Products);
            categoriesTableAdapter1.Fill(jsNorthWindDataSet1.Categories);
            suppliersTableAdapter1.Fill(jsNorthWindDataSet1.Suppliers);
            FillCategoriesValueList();
            GridEX1.DropDowns["Suppliers"].SetDataBinding(jsNorthWindDataSet1, "Suppliers");
        }
        private void FillCategoriesValueList()
        {
            //Get the CategoryID column
            GridEXColumn column = GridEX1.RootTable.Columns["CategoryID"];
            //Set HasValueList property equal to true in order to be able to use the ValueList property
            column.HasValueList = true;
            //Get the ValueList collection associated to this column
            GridEXValueListItemCollection valueList = column.ValueList;

            //Fill the ValueList
            valueList.PopulateValueList(jsNorthWindDataSet1.Categories.DefaultView, "CategoryID", "CategoryName");

            //An alternative way to fill the value list is using the Add method as follows:

            /*
            DataView view = jSNorthWindDataSet1.Categories.DefaultView;
            foreach(DataRowView row in view)
            {
                valueList.Add(row["CategoryID"],(string)row["CategoryName"]);
            }
            */


            //Setting other column related properties

            //When using a value list you could use DropDownList and Combo EditType 
            //in the column and the values for the dropdown list will be the values
            //in the ValueList collection
            column.EditType = EditType.DropDownList;
            //To be able to sort using the replaced value and not the value in the
            //CategoryID field change the CompareTarget property to Text instead of value
            column.CompareTarget = ColumnCompareTarget.Text;

            //Likewise, to group by the replaced text do:
            column.DefaultGroupInterval = GroupInterval.Text;

        }

        private void AddConditionalFormatting()
        {
            //Adding a condition using Discontinued field

            GridEXFormatCondition fc;
            fc = new GridEXFormatCondition(
                GridEX1.RootTable.Columns["Discontinued"],
                ConditionOperator.Equal, true);

            //setting Format style properties to be applied to all the rows
            //that met this condition
            fc.FormatStyle.FontStrikeout = TriState.True;
            fc.FormatStyle.ForeColor = SystemColors.GrayText;
            GridEX1.RootTable.FormatConditions.Add(fc);


            //adding a format condition to use font bold when OnSale field is true
            fc = new GridEXFormatCondition(
                GridEX1.RootTable.Columns["OnSale"],
                ConditionOperator.Equal, true);
            fc.FormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            fc.FormatStyle.ForeColor = Color.Red;
            GridEX1.RootTable.FormatConditions.Add(fc);

        }
    }
}