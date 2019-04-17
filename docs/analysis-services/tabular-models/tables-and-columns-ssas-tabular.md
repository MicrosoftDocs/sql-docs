---
title: "Analysis Services tabular model tables and columns | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Tables and Columns 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  After you have added tables and data into a model by using the Table Import Wizard, you can begin working with the tables by adding new columns of data, creating relationships between tables, defining calculations that extend the data, and filtering and sorting data in the tables for easier viewing.  
  
 Sections in this topic:  
  
-   [Benefits](#bkmk_benefits)  
  
-   [Working with tables and columns](#bkmk_working)  
  
-   [Related Tasks](#bkmk_related_tasks)  
  
##  <a name="bkmk_benefits"></a> Benefits  
 Tables, in tabular models, provide the framework in which columns and other metadata are defined. Tables include:  
  
 **Table Definition**  
 The table definition includes the set of columns. Columns can be imported from a data source or added manually, such as with calculated columns.  
  
 **Table Metadata**  
 Relationships, measures, roles, perspectives, and pasted data are all metadata the define objects within the context of a table.  
  
 **Data**  
 Data is populated in table columns when you first import tables by using the Table Import Wizard or by creating new data in calculated columns. When data changes at the source, or when a model is removed from memory, you must run a process operation to re-populate the data into the tables.  
  
##  <a name="bkmk_working"></a> Working with tables and columns  
 In the model designer, you do not create new model tables directly. A new tab is created automatically for you whenever data is imported or copied from another data source. Each tab (in the model designer) contains one table of data, which can include the following:  
  
-   A single table or view from a relational database, or from other non-relational sources, such as an Analysis Services cube.  
  
-   A tabular set of data imported from a feed or text file.  
  
-   A combination of both relational data and tabular (HTML) data copy and pasted into the table.  
  
 When you import data, each table or view, sheet, or file of data is added as a table in the model designer. You typically add data from various sources onto separate tabs, but you can combine data in a single table by using **Paste** and **Paste Append**. For more information, see [Copy and Paste Data](../../analysis-services/tabular-models/ssas-import-data-copy-and-paste-data.md).  
  
 After you have added the data that you need, you can create additional relationships between the tables, look up or reference related values in other tables, or create derived values by adding new calculated columns.  
  
 If you are working very large data sets, you may want to filter out certain data so it is not visible. You may also want to sort data in a different order. By using the model designer, you can use the filter, sort, and hide features to display, or not display, entire columns or certain data.  
  
##  <a name="bkmk_related_tasks"></a> Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Add columns to a table](../../analysis-services/tabular-models/add-columns-to-a-table-ssas-tabular.md)|Describes how to add a source column to a table definition.|  
|[Delete a Column](../../analysis-services/tabular-models/delete-a-column-ssas-tabular.md)|Describes how to delete a model table column by using the model designer or by using the Table Properties dialog box.|  
|[Change table, column, or row filter mappings](../../analysis-services/tabular-models/change-table-column-or-row-filter-mappings-ssas-tabular.md)|Describes how to change table, column, or row filter mappings by using the table preview or SQL query editor in the Edit Table Properties dialog box.|  
|[Specify Mark as Date Table for use with Time Intelligence](../../analysis-services/tabular-models/specify-mark-as-date-table-for-use-with-time-intelligence-ssas-tabular.md)|Describes how to use the Mark as Date Table dialog box to specify a date table and unique identifier column. Specifying a date table and unique identifier is necessary when using time intelligence functions in DAX formulas.|  
|[Add a table](../../analysis-services/tabular-models/add-a-table-ssas-tabular.md)|Describes how to add a table from a data source by using an existing data source connection.|  
|[Delete a Table](../../analysis-services/tabular-models/delete-a-table-ssas-tabular.md)|Describes how to delete tables in your model workspace database that you no longer need.|  
|[Rename a Table or Column](../../analysis-services/tabular-models/rename-a-table-or-column-ssas-tabular.md)|Describes how to rename a table or column to make it more identifiable in your model.|  
|[Set the Data Type of a Column](../../analysis-services/tabular-models/set-the-data-type-of-a-column-ssas-tabular.md)|Describes how to change the data type of a column. The data type defines how data in the column is stored and presented.|  
|[Hide or freeze columns](../../analysis-services/tabular-models/hide-or-freeze-columns-ssas-tabular.md)|Describes how to hide columns that you don't want to display and how to keep an area of a model visible while you scroll to another area of the model by freezing (locking) specific columns in one area.|  
|[Calculated Columns](../../analysis-services/tabular-models/ssas-calculated-columns.md)|Topics in this section describe how you can use calculated columns to add aggregated data to your model.|  
|[Filter and Sort Data](http://msdn.microsoft.com/library/55ebd7a6-2458-4398-911f-fcfeb2413f1b)|Topics in this section describe how you can filter or sort data by using controls in the model designer.|  
  
  
