---
title: "Hierarchies in Analysis Services tabular models | Microsoft Docs"
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
# Hierarchies
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Hierarchies, in tabular models, are metadata that define relationships between two or more columns in a table. Hierarchies can appear separate from other columns in a reporting client field list, making them easier for client users to navigate and include in a report.  
  
##  <a name="bkmk_benefits"></a> Benefits  
 Tables can include dozens or even hundreds of columns with unusual column names in no apparent order. This can lead to an unordered appearance in reporting client field lists, making it difficult for users to find and include data in a report. Hierarchies can provide a simple, intuitive view of an otherwise complex data structure.  
  
 For example, in a Date table, you can create a Calendar hierarchy. Calendar Year is used as the top-most parent level, with Month, Week, and Day included as child levels (Calendar Year->Month->Week->Day). This hierarchy shows a logical relationship from Calendar Year to Day. A client user can then select Calendar Year from a Field List to include all levels in a PivotTable, or expand the hierarchy, and select only particular levels to be included in the PivotTable.  
  
 Because each level in a hierarchy is a representation of a column in a table, the level can be renamed. While not exclusive to hierarchies (any column can be renamed in a tabular model), renaming hierarchy levels can make it easier for users to find and include levels in a report. Renaming a level does not rename the column it references; it simply makes the level more identifiable. In our Calendar Year hierarchy example, in the Date table in Data View, the columns: CalendarYear, CalendarMonth, CalendarWeek, and CalendarDay were renamed to Calendar Year, Month, Week, and Day to make them more easily identifiable. Renaming levels has the additional benefit of providing consistency in reports, since users will less likely need to change column names to make them more readable in PivotTables, charts, etc.  
  
 Hierarchies can be included in perspectives. Perspectives define viewable subsets of a model that provide focused, business-specific, or application-specific viewpoints of the model. A perspective, for example, could provide users a viewable list (hierarchy) of only those data items necessary for their specific reporting requirements. For more information, see [Perspectives](../../analysis-services/tabular-models/perspectives-ssas-tabular.md).  
  
 Hierarchies are not meant to be used as a security mechanism, but as a tool for providing a better user experience. All security for a particular hierarchy is inherited from the underlying model. Hierarchies cannot provide access to model objects to which a user does not already have access. Security for the model database must be resolved before access to objects in the model can be provided through a hierarchy. Security roles can be used to secure model metadata and data. For more information, see [Roles](../../analysis-services/tabular-models/roles-ssas-tabular.md).  
  
##  <a name="bkmk_define"></a> Defining hierarchies  
 You create and manage hierarchies by using the model designer in Diagram View. Creating and managing hierarchies is not supported in the model designer in Data View. To view the model designer in Diagram View, click the **Model** menu, then point to **Model View**, and then click **Diagram View**.  
  
 To create a hierarchy, right-click a column you want to specify as the parent level, and then click **Create Hierarchy**. You can multi-select any number of columns (within a single table) to include, or you can later add columns as child levels by clicking and dragging columns to the parent level. When multiple columns are selected, columns are automatically placed in an order based on cardinality. You can change the order by clicking and dragging a column (level) to a different order or by using Up and Down navigation controls on the context menu. When adding a column as a child level, you can use auto-detect by dragging and dropping the column onto the parent level.  
  
 A column can appear in more than one hierarchy. Hierarchies cannot include non-column objects such as measures or KPIs. A hierarchy can be based on columns from within a single table only. If you multi-select a measure along with one or more columns, or if you select columns from multiple tables, the **Create Hierarchy** command is disabled in the context menu. To add a column from a different table, use the RELATED DAX function to add a calculated column that references the column from the related table. The function uses the following syntax: `=RELATED(TableName[ColumnName])`. For more information, see RELATED Function.  
  
 By default, new hierarchies are named hierarchy1, hierarchy 2, etc. You should change hierarchy names to reflect the nature of the parent-child relationship, making them more identifiable to users.  
  
 After you have created hierarchies, you can test their efficacy by using the Analyze in Excel feature. For more information, see [Analyze in Excel](../../analysis-services/tabular-models/analyze-in-excel-ssas-tabular.md).  
  
##  <a name="bkmk_related_tasks"></a> Related tasks  
  
|Task|Description|  
|----------|-----------------|  
|[Create and Manage Hierarchies](../../analysis-services/tabular-models/create-and-manage-hierarchies-ssas-tabular.md)|Describes how to create and manage hierarchies by using the model designer in Diagram View.|  
  
## See Also  
 [Tabular Model Designer](../../analysis-services/tabular-models/tabular-model-designer-ssas.md)   
 [Perspectives](../../analysis-services/tabular-models/perspectives-ssas-tabular.md)   
 [Roles](../../analysis-services/tabular-models/roles-ssas-tabular.md)  
  
  
