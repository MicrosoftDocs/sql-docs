---
title: "Delete a Mining Model from a Mining Structure | Microsoft Docs"
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
# Delete a Mining Model from a Mining Structure
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
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
  
-   [DROP MINING MODEL &#40;DMX&#41;](../../dmx/drop-mining-model-dmx.md)  
  
## See Also  
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)  
  
  
