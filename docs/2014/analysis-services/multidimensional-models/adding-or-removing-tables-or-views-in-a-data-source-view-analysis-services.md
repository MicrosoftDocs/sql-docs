---
title: "Adding or Removing Tables or Views in a Data Source View (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dsvdesigner.tablespane.f1"
helpviewer_keywords: 
  - "deleting tables"
  - "tables [Analysis Services]"
  - "removing tables"
  - "adding tables"
  - "data source views [Analysis Services], tables"
  - "tables [Analysis Services], data source views"
ms.assetid: 98307d04-6548-4d7d-9244-2371dd165249
author: minewiskan
ms.author: owend
manager: craigg
---
# Adding or Removing Tables or Views in a Data Source View (Analysis Services)
  After you have created a data source view (DSV) in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can modify it in Data Source View Designer by adding or removing tables and columns, including tables and columns from another data source.  
  
 To open the DSV in Data Source View Designer, you double-click the DSV in Solution Explorer. Once you open the DSV, you can use the **Add/Remove Tables** command on the button bar or menu to modify or extend the DSV. You can also work with the objects in the diagram. For example, you can select an object and then use the Delete key on your keyboard to remove an object.  
  
> [!WARNING]  
>  Use caution when removing a table. Removing a table deletes all the associated columns and relationships from the DSV and invalidates all objects bound to that table.  
  
## Selecting Tables or Views to Add or Remove  
 Using the **Add/Remove Tables** dialog box, you can move tables or views between the **Available objects** and **Included objects** lists. The **Available objects** list initially includes any tables or views in the primary data source that are not already in the data source view. If the primary data source supports the `OPENROWSET` function, you can also add tables or views from other data sources in the project or database.  
  
 Adding or removing a table to the DSV also adds or removes the table to the currently selected diagram in the DSV. For more information on diagrams, see [Work with Diagrams in Data Source View Designer &#40;Analysis Services&#41;](work-with-diagrams-in-data-source-view-designer-analysis-services.md).  
  
 After you move a table to the **Included objects** list in the **Add/Remove Tables** dialog box, you can add all related tables. This operation adds tables according to foreign key constraints in the data source, if such constraints exist. If foreign key constraints do not exist, you can use the `NameMatchingCriteria` property of the data source view to determine relationships by specifying a criterion for matching column names in tables to generate likely relationships. If the `NameMatchingCriteria`property is specified for the data source view, click **Add Related Tables** to add tables from the data source that have matching column names. For more information about setting the `NameMatchingCriteria` property, see [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md).  
  
> [!NOTE]  
>  Adding or removing objects in a data source view does not affect the underlying data source.  
  
## See Also  
 [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md)   
 [Work with Diagrams in Data Source View Designer &#40;Analysis Services&#41;](work-with-diagrams-in-data-source-view-designer-analysis-services.md)  
  
  
