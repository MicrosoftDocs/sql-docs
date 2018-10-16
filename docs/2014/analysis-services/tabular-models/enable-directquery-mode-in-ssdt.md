---
title: "Enable DirectQuery Design Mode (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 71fc7ebd-2e86-4a76-994b-66d3a57bcc9b
author: minewiskan
ms.author: owend
manager: craigg
---
# Enable DirectQuery Design Mode (SSAS Tabular)
  To create a model in DirectQuery mode, you must first change the design-time environment so that it supports the user of DirectQuery mode. When you do so, the designer will also do the following:  
  
-   Enable the use of the DirectQuery deployment properties.  
  
-   Change the workspace database to run in a hybrid mode that uses the cache for designing. When you actually deploy the model, the mode will be changed back to whatever value you specified in the project deployment properties.  
  
-   Disable design features that are incompatible with DirectQuery mode.  
  
-   Validate the existing model.  
  
 This procedure describes how to enable DirectQuery mode in the designer.  
  
### To enable use of DirectQuery in a model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the solution file.  
  
2.  In Object Explorer, double-click the Model.bim file.  
  
3.  In the **Properties** pane, change the property, **DirectQueryMode**, to **On**.  
  
4.  If there are errors, in Visual Studio, open the **Error List** and resolve any problems that would prevent the model from being switched to DirectQuery mode.  
  
## See Also  
 [DirectQuery Mode &#40;SSAS Tabular&#41;](directquery-mode-ssas-tabular.md)  
  
  
