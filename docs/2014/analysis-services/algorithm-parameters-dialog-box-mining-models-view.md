---
title: "Algorithm Parameters Dialog Box (Mining Models View) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.models.algorithmparameters.f1"
helpviewer_keywords: 
  - "Algorithm Parameters dialog box"
ms.assetid: 57f9f6f8-8ca4-4a6e-8f18-85f0571b7060
author: minewiskan
ms.author: owend
manager: craigg
---
# Algorithm Parameters Dialog Box (Mining Models View)
  Use the **Algorithm Parameters** dialog box to adjust algorithm parameters that are specific to the selected model. When you change an algorithm parameter, you will usually change the results of the mining model. The way that each parameter affects the results depends on the algorithm you are using, and on your data. For more information, see [Customize Mining Models and Structure](data-mining/customize-mining-models-and-structure.md).  
  
## Options  
 **Parameters**  
 Lists the parameters that are available for the selected mining model.  
  
 The following list describes the available columns.  
  
|Column|Description|  
|------------|-----------------|  
|**Parameter**|List the name of the parameter.|  
|**Value**|Enter a value only if you want to change the default value of the parameter.|  
|**Default**|List the default value of the parameter that the algorithm will use if you do not supply a value in the **Value** column.|  
|**Range**|List the range of possible values that you can enter into the **Value** column. The ranges can be one of the following:<br /><br /> A discrete list, such as 1, 2, 3<br /><br /> An inclusive range, such as [0, 100]<br /><br /> An exclusive range, such as (0,...)<br /><br /> A combination, such as [0,...)|  
  
 **Description**  
 Describes the selected parameter in the **Parameters** list.  
  
 **Add**  
 Click to add additional, algorithm specific-parameters to the list. After adding the parameter, you can enter the correct name in the **Parameter** column and provide a value in the **Value** column.  
  
 **Remove**  
 Click to delete a custom parameter from the list.  
  
 If you delete one of the standard Analysis Services algorithm parameters from the list, the parameter will still be used in the model, but with the default values for that parameter. The parameter is not permanently deleted and will appear the next time that you open the dialog box.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Models View &#40;Data Mining Model Designer&#41;](mining-models-view-data-mining-model-designer.md)   
 [Moving Data Mining Objects](data-mining/moving-data-mining-objects.md)  
  
  
