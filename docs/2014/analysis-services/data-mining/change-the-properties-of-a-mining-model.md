---
title: "Change the Properties of a Mining Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining models [Analysis Services], properties"
  - "properties [data mining]"
ms.assetid: aefaeb7f-d174-48d1-a188-0987a3b1196b
author: minewiskan
ms.author: owend
manager: craigg
---
# Change the Properties of a Mining Model
  Some mining model properties apply to the model as a whole, and other model properties apply to individual columns. Examples of properties that apply to the entire model would be the `Drillthrough` property, which specifies whether the case data should be available for querying, and the `Description` property. Properties that apply to the column include `Usage` and `ModelingFlags`, which control how data in the column is used within the model.  
  
 The following model properties have advanced editors that you can use to create expressions or configure complex model properties. The following properties provide:  
  
-   `Filter` property: Opens the [Data Set Filter or Model Filter Dialog Box](../data-set-filter-or-model-filter-dialog-box.md).  
  
-   `AlgorithmParameters` property: Opens the [Algorithm Parameters Dialog Box &#40;Mining Models View&#41;](../algorithm-parameters-dialog-box-mining-models-view.md).  
  
 For information about how to set the properties in a mining model, see [Mining Model Columns](mining-model-columns.md).  
  
### To change the properties of a mining model  
  
1.  In the **Mining Models** tab in Data Mining Designer, right-click either the column header that contains the name of the mining model, or the row in the grid that contains the name of the mining algorithm, and then select **Properties**.  
  
2.  In the **Properties** window on the right side of the screen, highlight the value that corresponds to the property that you want to change, and enter the new value.  
  
     The new value will take effect when you select a different element in the designer.  
  
### To change the properties of a mining model column  
  
1.  In the **Mining Models** tab in Data Mining Designer, right-click the cell in the grid at the intersection of the mining structure column and the mining model, and then select **Properties**.  
  
2.  In the **Properties** window on the right side of the screen, highlight the value that corresponds to the property that you want to change, and enter the new value.  
  
    > [!NOTE]  
    >  If the column usage is set to `Ignore`, the **Properties** window for the column is blank.  
  
     The new value will take effect when you select a different element in the designer.  
  
## See Also  
 [Mining Model Tasks and How-tos](mining-model-tasks-and-how-tos.md)  
  
  
