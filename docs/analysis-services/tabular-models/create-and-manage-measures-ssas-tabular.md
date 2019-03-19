---
title: "Create and manage measures in Analysis Services tabular models | Microsoft Docs"
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
# Create and manage measures 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  A measure is a formula that is created for use in a report or Excel PivotTable (or PivotChart). Measures can be based on standard aggregation functions, such as COUNT or SUM, or you can define your own formula by using DAX. The tasks in this topic describe how to create and manage measures by using a table's measure grid.  
  
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
 [Measures](../../analysis-services/tabular-models/measures-ssas-tabular.md)   
 [KPIs](../../analysis-services/tabular-models/kpis-ssas-tabular.md)   
 [Calculated columns](../../analysis-services/tabular-models/ssas-calculated-columns.md)  
  
  
