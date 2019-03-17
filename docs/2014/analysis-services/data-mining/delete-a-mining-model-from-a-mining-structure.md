---
title: "Delete a Mining Model from a Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining structures [Analysis Services], mining models"
  - "deleting mining models"
  - "removing mining models"
  - "mining models [Analysis Services], deleting"
ms.assetid: 9ab1506b-856e-4762-a663-5adf15ac71e3
author: minewiskan
ms.author: owend
manager: craigg
---
# Delete a Mining Model from a Mining Structure
  You can delete mining models by using Data Mining Designer, by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or by using DMX statements.  
  
### Delete a mining model using SQL Server Data Tools  
  
1.  Select the **Mining Models** tab in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the model that you want to delete, and select **Delete**.  
  
     The **Delete Objects** dialog box opens.  
  
3.  Click **OK**.  
  
### Delete a mining model using SQL Server Management Studio  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that contains the model.  
  
2.  Expand **Mining Structures**, and then expand **Mining Models**.  
  
3.  Right-click the model you want to delete, and select **Delete**.  
  
     Deleting the model does not delete the training data, only the metadata and any patterns created when you trained the model.  
  
### Delete a mining model using DMX  
  
-   [DROP MINING MODEL &#40;DMX&#41;](/sql/dmx/drop-mining-model-dmx)  
  
## See Also  
 [Mining Model Tasks and How-tos](mining-model-tasks-and-how-tos.md)  
  
  
