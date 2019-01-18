---
title: "Create and Manage Measures (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: edc1a4b2-96d3-4f34-bb70-6cacec79e819
author: minewiskan
ms.author: owend
manager: craigg
---
# Create and Manage Measures (SSAS Tabular)
  A measure is a formula that is created for use in a report or Excel PivotTable (or PivotChart). Measures can be based on standard aggregation functions, such as COUNT or SUM, or you can define your own formula by using DAX. The tasks in this topic describe how to create and manage measures by using a table's measure grid.  
  
 This topic includes the following tasks:  
  
-   [To create a measure using a standard aggregation formula](#bkmk_create_stand)  
  
-   [To create a measure using a custom formula](#bkmk_create_custom)  
  
-   [To edit measure properties](#bkmk_edit)  
  
-   [To rename a measure](#bkmk_rename)  
  
-   [To delete a measure](#bkmk_delete)  
  
## Tasks  
 To create and manage measures, you will use a table's measure grid. You can view the measure grid for a table in the model designer in Data View only. You cannot create measures or view the measure grid when in Diagram View; however, you can view existing measures in Diagram View. To show the measure grid for a table, click the **Table** menu, and then click **Show Measure Grid**.  
  
###  <a name="bkmk_create_stand"></a> To create a measure using a standard aggregation formula  
  
-   Click on the column for which you want to create the measure, then click the **Column** menu, then point to **AutoSum**, and then click an aggregation type.  
  
     The measure will be created automatically with a default name, followed by the formula in the first cell in the measure grid directly beneath the column.  
  
###  <a name="bkmk_create_custom"></a> To create a measure using a custom formula  
  
-   In the measure grid, beneath the column for which you want to create the measure, click a cell, then in the formula bar, type a name, followed by a colon (:), followed by an equals sign (=), followed by the formula. Press ENTER to accept the formula.  
  
###  <a name="bkmk_edit"></a> To edit measure properties  
  
-   In the measure grid, click a measure, and then In the **Properties** window, type or select a different property value.  
  
###  <a name="bkmk_rename"></a> To rename a measure  
  
-   In the measure grid, click on a measure, and then In the **Properties** window, in **Measure Name**, type a new name, and then click ENTER.  
  
     You can also rename a measure in the formula bar. The measure name precedes the formula, followed by a colon.  
  
###  <a name="bkmk_delete"></a> To delete a measure  
  
-   In the measure grid, right-click a measure, and then click **Delete**.  
  
## See Also  
 [Measures &#40;SSAS Tabular&#41;](measures-ssas-tabular.md)   
 [KPIs &#40;SSAS Tabular&#41;](kpis-ssas-tabular.md)   
 [Calculated Columns &#40;SSAS Tabular&#41;](ssas-calculated-columns.md)  
  
  
