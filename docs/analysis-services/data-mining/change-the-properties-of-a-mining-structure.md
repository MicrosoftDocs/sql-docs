---
title: "Change the Properties of a Mining Structure | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Change the Properties of a Mining Structure
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  There are two kinds of properties on a mining structure, both of which can be modified:  
  
-   Properties of the mining structure that affect the entire structure  
  
-   Properties on individual columns in the structure  
  
 Note that some properties are dependent on other property settings. For example, you cannot set properties that control binning behavior (such as <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn.DiscretizationMethod%2A> or <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn.DiscretizationBucketCount%2A>) until you have set the data type of the column to **Discretized**.  
  
 For more information about mining structure properties, see [Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md).  
  
### To change the properties of a mining structure  
  
1.  On the **Mining Structure** tab in Data Mining Designer, right-click either the mining structure or a column in the mining structure, and then select **Properties**.  
  
     The **Properties** window opens on the right side of the screen, if it was not already visible.  
  
2.  In the **Properties** window, select the value that corresponds to the property that you want to change, and then enter the new value.  
  
     The new value will take effect when you select a different element in the designer.  
  
## See Also  
 [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md)  
  
  
