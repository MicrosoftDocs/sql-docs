---
title: "Rename a Table or Column (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.renametableorcolumn.f1"
ms.assetid: 88061a39-c5aa-403d-a52b-7fdb365fc235
author: minewiskan
ms.author: owend
manager: craigg
---
# Rename a Table or Column (SSAS Tabular)
  You can change the name of a table during the import process by typing a **Friendly Name** in the **Select Tables and Views** page of the **Table Import Wizard**. You can also change table and column names if you import data by specifying a query on the **Specify a SQL Query** page of the **Table Import Wizard**.  
  
 After you have added the data to the model, the name (or title) of a table appears on the table tab, at the bottom of the model designer. You can change the name of your table to give it a more appropriate name. You can also rename a column after the data has been added to the model. This option is especially important when you have imported data from multiple sources, and want to ensure that columns in different tables have names that are easy to distinguish.  
  
### To rename a table  
  
1.  In the model designer, right-click the tab that contains the table that you want to rename, and then click **Rename**.  
  
2.  Type the new name.  
  
    > [!NOTE]  
    >  You can edit other properties of a table, including the connection information and column mappings, by using the **Edit Table Properties** dialog box. However, you cannot change the name in this dialog box.  
  
### To rename a column  
  
1.  In the model designer, double-click the header of the column that you want to rename, or right-click the header and select **Rename Column** from the context menu.  
  
2.  Type the new name.  
  
## Naming Requirements for Columns and Tables  
 The following words and characters cannot be used in the name of a table or column:  
  
-   Leading or trailing spaces  
  
-   Control characters  
  
-   The following characters (which are not valid in the names of Analysis Services objects): .,;':/\\*|?&%$!+=()[]{}<>  
  
-   Analysis Services reserved keywords, including Multidimensional Expressions (MDX) and Data Mining Extensions (DMX) function names and operators.  
  
## Effect of Renaming on Existing Tables, Columns, and Calculations  
 Whenever you change the name of a table, you change the name of the underlying table object, which may contain multiple columns or measures. Therefore, any columns that are in the table, and any relationships that use the table, must be updated to use the new name in their definitions. This update happens automatically in some cases. Measures are not updated automatically.  
  
 Moreover, any calculations that use the renamed table, or that use columns from the renamed table, must also be updated, and the data derived from those calculations must be refreshed and recalculated. Depending on how many tables and calculations are affected, this can take some time to complete. Therefore, the best time to rename tables is either during the import process, or before you start to build complex relationships and calculations.  
  
## See Also  
 [Tables and Columns &#40;SSAS Tabular&#41;](tables-and-columns-ssas-tabular.md)   
 [Import from PowerPivot &#40;SSAS Tabular&#41;](import-from-power-pivot-ssas-tabular.md)   
 [Import from Analysis Services &#40;SSAS Tabular&#41;](import-from-analysis-services-ssas-tabular.md)  
  
  
