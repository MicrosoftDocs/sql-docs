---
title: "Define Logical Primary Keys in a Data Source View (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Define Logical Primary Keys in a Data Source View (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The Data Source View Wizard and Data Source View Designer automatically define a primary key for a table that is added to a data source view based on underlying database table.  
  
 Occasionally, you may need to manually define a primary key in the data source view. For example, for performance or design reasons, tables in a data source may not have explicitly defined primary key columns. Named queries and views may also omit the primary key column for a table. If a table, view, or named query does not have a physical primary key defined, you can manually define a logical primary key on the table, view or named query in Data Source View Designer.  
  
## Set a Logical Primary Key  
 Primary keys are required in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to uniquely identify records in a table, identify key columns in dimension tables and to support relationships between tables, views and named queries. These relationships are used to construct queries for retrieving data and metadata from underlying data sources, and to take advantage of advanced business intelligence features.  
  
 Any column can be used for the logical primary key, including a named calculation. When you create a logical primary key, a unique constraint is created in the data source view and marked as a primary key constraint. Any other existing logical primary key specified in the selected table is deleted.  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project or connect to the database that contains the data source view in which you wish to set a logical primary key.  
  
2.  In Solution Explorer, expand the **Data Source Views** folder, then double-click the data source view.  
  
     To locate a table or view, you can use the **Find Table** option by either clicking the **Data Source View**  menu or right-clicking in an open area of the **Tables** or **Diagram** panes.  
  
3.  In either the **Tables** or the **Diagram** pane, right-click the column or columns that you want to use to define a logical primary key, and then click **Set Logical Primary Key**.  
  
     The option to set a logical primary key is available only for tables that do not have a primary key.  
  
     Notice that after you set the key, a key icon now identifies the primary key columns.  
  
## See Also  
 [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)   
 [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md)  
  
  
