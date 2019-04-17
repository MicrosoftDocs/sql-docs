---
title: "Configure Reporting Properties for Power View Reports | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Supplemental Lesson - Configure Reporting Properties for Power View Reports
[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

In this supplemental lesson, you will set reporting properties for the AW Internet Sales project. Reporting properties make it easier for end-users to select and display model data in Power View. You will also set properties to hide certain columns and tables, and create new data for use in charts.   
  
Estimated time to complete this lesson: **30 minutes**  
  
## Prerequisites  
This supplemental lesson is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this supplemental lesson, you should have completed all previous lessons.  
In order to complete this particular supplemental lesson, you must also have the following:  
  
-   The AW Internet Sales project (completed through this tutorial) ready to be deployed or already deployed to an Analysis Services server.  
  
  
## Model properties that affect reporting  
When authoring a tabular model, there are certain properties that you can set on individual columns and tables to enhance the end-user reporting experience in Power View. In addition, you can create additional model data to support data visualization and other features specific to the reporting client. For the sample Adventure Works Internet Sales Model, here are some of the changes you will make:  
  
-   **Add new data** - Adding new data in a calculated column by using a DAX formula creates date information in a format that is easier to display in charts.  
  
-   **Hide tables and columns that are not useful to the end user** - The **Hidden** property controls whether tables and table columns are displayed in the reporting client. Items with that are hidden are still part of the model and remain available for queries and calculations.  
  
-   **Enable one-click tables** - By default, no action occurs if an end-user clicks a table in the field list. To change this behavior so that a click on the table adds the table to the report, you will set Default Field Set on each column that you want to include in the table. This property is set on the table columns that end users will most likely want to use.  
  
-   **Set grouping where needed** - The **Keep Unique Rows** property determines if the values in the column should be grouped by values in a different field, such as an identifier field. For columns that contain duplicate values such as Customer Name (for example, multiple customers named John Smith), it is important to group (keep unique rows) on the **Row Identifier** field in order to provide your end users with the correct results.  
  
-   **Set data types and data formats** - By default, Power View applies rules based on column data type to determine whether the field can be used as a measure. Because each data visualization in Power View also has rules about where measures and non-measures can be placed, it is important to set the data type in the model, or override the default, to achieve the behavior you want for your end-user.  
  
-   **Set the Sort by Column** property - The **Sort By Column** property specifies if the values in the column should be sorted by values in a different field. For example, on the Month Calendar column that contains the month name, sort by the column Month Number.  
  
## Hide tables from client Tools  
Because there is already a Product Category calculated column and Product Subcategory calculated column in the Product table, it is not necessary to have the Product Category and Product Subcategory tables visible to client applications.  
  
#### To hide the Product Category and Product Subcategory tables  
  
1.  In the model designer, right-click on the **Product Category** table (tab), and then click **Hide from Client Tools**.  
  
2.  Right-click on the **Product Subcategory** table (tab), and then click **Hide from Client Tools**.  
  
## Create new data for charts  
Sometimes it may be necessary to create new data in your model by using DAX formulas. In this task, you will add two new calculated columns to the Date table. These new columns will provide date fields in a format convenient for use in charts.  
  
#### To create new data for charts  
  
1.  In the **Date** table, scroll to the far right, and then click on **Add Column**.  
  
2.  Add two new calculated columns using the following formulas in the formula bar:  
  
    |Column Name|Formula|  
    |---------------|-----------|  
    |Year Quarter|=[Calendar Year] & " Q" & [Calendar Quarter]|  
    |Year Month|=[Calendar Year] & FORMAT([Month],"#00")|  
  
## Default field set  
The Default Field Set is a predefined list of columns and measures for a table that are automatically added to a report canvas when the table is clicked on in the report field list. Essentially, you can specify the default columns, measures, and field ordering users will want to see when this table is visualized in Power View reports.  For the Internet Sales model, you will define a default field set and order for the Customer, Geography, and Product tables. Included are only those most common columns that users will want to see when analyzing Adventure Works Internet Sales data by using Power View reports.  
  
For detailed information about Default Field Set, see [Configure Default Field Set for Power View Reports](../analysis-services/tabular-models/power-view-configure-default-field-set-for-reports.md) in SQL Server Books Online.  
  
#### To set default field set for tables  
  
1.  In the model designer, click the **Customer** table (tab).  
  
2.  In the **Properties** window, under **Reporting Properties**, in the **Default Field Set** property, click **Click to edit** to open the **Default Field Set** dialog box.  
  
3.  In the **Default Field Set** dialog box, in the **Fields in the table** list box, press Ctrl, and select the following fields, and then click **Add**.  
  
    **Birth Date**, **Customer Alternate Id**, **First Name**, **Last Name**.  
  
4.  In the **Default fields, in order** window, use the Move Up and Move Down buttons to put the following order:  
  
    **Customer Alternate Id**  
  
    **First Name**  
  
    **Last Name**  
  
    **Birth Date**.  
  
5.  Click **Ok** to close the **Default Field Set** dialog box for the **Customer** table.  
  
6.  Perform these same steps for the **Geography** table, selecting the following fields and putting them in this order.  
  
    **City**, **State Province Code**, **Country Region Code**.  
  
7.  Finally, perform these same steps for the **Product** table, selecting the following fields and putting them in this order.  
  
    **Product Alternate Id**, **Product Name**.  
  
## Table behavior  
By using Table Behavior properties, you can change the default behavior for different visualization types and grouping behavior for tables used in Power View reports. This allows better default placement of identifying information such as names, images, or titles in tile, card, and chart layouts.  
  
For detailed information about Table Behavior properties, see [Configure Table Behavior Properties for Power View Reports](../analysis-services/tabular-models/power-view-configure-table-behavior-properties-for-reports.md) in SQL Server Books Online.  
  
#### To set table behavior 
  
1.  In the model designer, click the **Customer** table (tab).  
  
2.  In the **Properties** window, in the **Table Behavior** property, click **Click to edit**, to open the **Table Behavior** dialog box.  
  
3.  In the **Table Behavior** dialog box, in the **Row Identifier** dropdown list box, select the **Customer Id** column.  
  
4.  In the **Keep Unique Rows** list box, select **First Name** and **Last Name**.  
  
    This property setting specifies these columns provide values that should be treated as unique even if they are duplicates, for example, when two or more employees share the same name.  
  
5.  In the **Default Label** dropdown list box, select the **Last Name** column.  
  
    This property setting specifies this column provides a display name to represent row data.  
  
6.  Repeat these steps for the **Geography** table, selecting the **Geography Id** column as the Row Identifier, and the **City** column in the **Keep Unique Rows** list box. You do not need to set a Default Label for this table.  
  
7.  Repeat these steps, for the **Product** table, selecting the **Product Id** column as the Row Identifier, and the **Product Name** column in the **Keep Unique Rows** list box. For **Default Label**, select **Product Alternate Id**.  
  
## Reporting properties for columns  
There are a number of basic column properties and specific reporting properties on columns you can set to improve the model reporting experience. For example, it may not be necessary for users to see every column in every table. Just as you hid the Product Category and Product Subcategory tables earlier, by using a column's Hidden property, you can hide particular columns from a table that is otherwise shown. Other properties, such as Data Format and Sort by Column, can also affect how column data can appear in reports. You will set some of those on particular columns now. Other columns require no action, and are not shown below.  
  
You will only set a few different column properties here, but there are many others. For more detailed information about column reporting properties, see [Column Properties](../analysis-services/tabular-models/column-properties-ssas-tabular.md) in SQL Server Books Online.  
  
#### To set properties for columns  
  
1.  In the model designer, click the **Customer** table (tab).  
  
2.  Click on the **Customer Id** column to display the column properties in the **Properties** window.  
  
3.  In the **Properties** window, set the **Hidden** property to True. The **Customer Id** column then becomes greyed out in the model designer.  
  
4.  Repeat these steps, setting the following column and reporting properties for each table specified. Leave all other properties at their default settings.  
  
    Note: For all date columns, make sure **Data Type** is **Date**.  
  
    **Customer**  
  
    |Column|Property|Value|  
    |----------|------------|---------|  
    |Geography Id|Hidden|True|  
    |Birth Date|Data Format|Short Date|  
  
    **Date**  
  
    > [!NOTE]  
    > Because the Date table was selected as the models date table by using the Mark as Date Table setting, in Lesson 7: Mark as Date Table, and the Date column in the Date table as the column to be used as the unique identifier, the Row Identifier property for the Date column will automatically be set to True, and cannot be changed. When using time-intelligence functions in DAX formulas, you must specify a date table. In this model, you created a number of measures using time-intelligence functions to calculate sales data for various periods such as previous and current quarters, and also for use in KPIs. For more information about specifying a date table, see [Specify Mark as Date Table for use with Time Intelligence](../analysis-services/tabular-models/specify-mark-as-date-table-for-use-with-time-intelligence-ssas-tabular.md) in SQL Server Books Online.  
  
    |Column|Property|Value|  
    |----------|------------|---------|  
    |Date|Data Format|Short Date|  
    |Day Number of Week|Hidden|True|  
    |Day Name|Sort By Column|Day Number of Week|  
    |Day of Week|Hidden|True|  
    |Day of Month|Hidden|True|  
    |Day of Year|Hidden|True|  
    |Month Name|Sort By Column|Month|  
    |Month|Hidden|True|  
    |Month Calendar|Hidden|True|  
    |Fiscal Quarter|Hidden|True|  
    |Fiscal Year|Hidden|True|  
    |Fiscal Semester|Hidden|True|  
  
    **Geography**  
  
    |Column|Property|Value|  
    |----------|------------|---------|  
    |Geography Id|Hidden|True|  
    |Sales Territory Id|Hidden|True|  
  
    **Product**  
  
    |Column|Property|Value|  
    |----------|------------|---------|  
    |Product Id|Hidden|True|  
    |Product Alternate Id|Default Label|True|  
    |Product Subcategory Id|Hidden|True|  
    |Product Start Date|Data Format|Short Date|  
    |Product End Date|Data Format|Short Date|  
  
    **Internet Sales**  
  
    |Column|Property|Value|  
    |----------|------------|---------|  
    |Product Id|Hidden|True|  
    |Customer Id|Hidden|True|  
    |Promotion Id|Hidden|True|  
    |Currency Id|Hidden|True|  
    |Sales Territory Id|Hidden|True|  
    |Order Quantity|Data Type<br /><br />Data Format<br /><br />Decimal Places|Decimal Number<br /><br />Decimal Number<br /><br />0|  
    |Order Date|Data Format|Short Date|  
    |Due Date|Data Format|Short Date|  
    |Ship Date|Data Format|Short Date|  
  
## Redeploy the Adventure Works Internet Sales tabular model  
Because you have changed the model, you must re-deploy it.  
  
#### To redeploy the Adventure Works Internet Sales tabular model  
  
-   In SSDT, click the **Build** menu, and then click **Deploy Adventure Works Internet Sales Model**.  
  
    The **Deploy** dialog box appears and displays the deployment status of the metadata as well as each table included in the model.  
  
## Next steps  
You can now use Power View to visualize data from the model. Ensure the Analysis Services and Reporting Services accounts on the SharePoint site have read permissions to the Analysis Services instance where you deployed your model.  
  
To create a Reporting Services report data source that points to your model, see [Table Model Connection Type (SSRS)](http://msdn.microsoft.com/library/hh270317%28v=SQL.110%29.aspx).  
  
  
  
