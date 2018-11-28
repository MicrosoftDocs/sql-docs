---
title: "Refresh the Schema in a Data Source View (Analysis Services) | Microsoft Docs"
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
# Refresh the Schema in a Data Source View (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  After defining a data source view (DSV) in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or database, the schema in an underlying data source may change. These changes are not automatically detected or updated in a development project. Moreover, if you deployed the project to a server, you will now encounter processing errors if Analysis Services can no longer connect to the external data source.  
  
 To update the DSV so that it matches the external data source, you can refresh the DSV in Business Intelligence Development Studio (BIDS). Refreshing the DSV detects changes to the external data sources upon which the DSV is based, and builds a change list that enumerates the additions or deletions in the external data source. You can then apply the set of changes to the DSV that will realign it to the underlying data source. Note that additional work is often required to further update the cubes and dimensions in the project that use the DSV.  
  
 This topic includes the following sections:  
  
 [Changes Supported in Refresh](#bkmk_changlist)  
  
 [Refresh a DSV in SQL Server Data Tools](#bkmk_DSVrefresh)  
  
##  <a name="bkmk_changlist"></a> Changes Supported in Refresh  
 DSV Refresh can include any of the following the actions:  
  
-   Deletion of tables, columns, and relationships  
  
-   Addition of columns and relationships, as applied to tables that are already included in the DSV  
  
-   Addition of new unique constraints. If a logical primary key exists for a table in the DSV and a physical key is added to the table in the data source, the logical key is removed and replaced by the physical key.  
  
 Refresh never adds new tables to a DSV. If you want to add a new table, you must add it manually. For more information, see [Adding or Removing Tables or Views in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/adding-or-removing-tables-or-views-in-a-data-source-view-analysis-services.md).  
  
##  <a name="bkmk_DSVrefresh"></a> Refresh a DSV in SQL Server Data Tools  
 To refresh a DSV, double-click the DSV from Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  This launches the DSV designer.  Then click the Refresh Data Source View button in the designer or choose **Refresh** from the Data Source View menu.  
  
 During refresh, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] queries all underlying relational data sources to determine whether there have been changes in tables/views which are included in the DSV. If connections can be established to all underlying data sources and there have been any changes, you will see them in the **Refresh Data Source View** dialog box.  
  
 ![Refresh Data Source View dialog box](../../analysis-services/multidimensional-models/media/ssas-olapdsv-refresh.gif "Refresh Data Source View dialog box")  
  
 The dialog box lists tables, columns, constraints, and relationships that will be deleted or added in the DSV. The report also lists any named query or calculation that cannot be successfully prepared. The affected objects are listed in a tree view with columns and relationships nested under tables and the type of change (deletion or addition) indicated for each object. The standard data source view object icons indicate the type of object affected.  
  
 Refresh is based completely on the names of the underlying objects. Therefore, if an underlying object is renamed in the data source, Data Source View Designer treats the renamed object as two separate operations-a deletion and an addition. In this case, you may have to manually add the renamed object back to the data source view. You may also have to re-create relationships or logical primary keys.  
  
> [!IMPORTANT]  
>  If you are aware that a table has been renamed in a data source, you may want to use the **Replace Table** command to replace the table with the renamed table before you refresh the data source view. For more information, see [Replace a Table or a Named Query in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/replace-a-table-or-a-named-query-in-a-data-source-view-analysis-services.md).  
  
 After you examine the report, you can either accept the changes or cancel the update to reject any changes. All changes must be accepted or rejected together. You cannot choose individual items in the list. You can also save a report of the changes.  
  
## See Also  
 [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)  
  
  
