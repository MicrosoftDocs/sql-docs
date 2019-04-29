---
title: "Create an Alias for a Model Column | Microsoft Docs"
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
# Create an Alias for a Model Column
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can create an alias for a model column. This might be useful when the mining structure name is too long to easily work with, or when you want to rename the column to be more descriptive of its contents or usage in the model. For example, if you make a copy of a structure column and then discretize the column differently for a particular model, you can rename the column to reflect the content more accurately.  
  
 To create an alias for a model column, you use the **Properties** pane and set the [Name](https://docs.microsoft.com/bi-reference/assl/properties/name-element-assl) property of the column.  
  
 On the **Mining Models** tab of Data Mining Designer, the alias appears enclosed in parentheses next to the column usage label.  
  
 For information about how to set the properties of a mining model, see [Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md).  
  
### To add an alias to a mining model column  
  
1.  In the **Mining Models** tab in Data Mining Designer, right-click the cell in the mining model for the mining column that you want to change, and then select **Properties**.  
  
2.  In the **Properties** window on the right side of the screen, click the cell next to the Name property and delete the current value. Type a new name for the column.  
  
## See Also  
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)   
 [Mining Model Properties](../../analysis-services/data-mining/mining-model-properties.md)  
  
  
