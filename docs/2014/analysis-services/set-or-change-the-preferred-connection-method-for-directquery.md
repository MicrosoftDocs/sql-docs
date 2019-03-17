---
title: "Set or Change the Preferred Connection Method for DirectQuery | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: f10d5678-d678-4251-8cce-4e30cfe15751
author: minewiskan
ms.author: owend
manager: craigg
---
# Set or Change the Preferred Connection Method for DirectQuery
  When you create a model for use in DirectQuery mode, you must first configure the design environment to support use of DirectQuery. To do this, see [Enable DirectQuery Design Mode &#40;SSAS Tabular&#41;](tabular-models/enable-directquery-mode-in-ssdt.md).  
  
 When you are ready to deploy the model, you must set some additional properties to enable users to access your model using one of the DirectQuery modes:  
  
-   You must indicate whether queries against the model should use cached data or the relational data source. You can use a hybrid mode or DirectQuery only.  
  
-   If you are using partitions, you must indicate which partition to use as the DirectQuery data source.  
  
-   You must set impersonation options for users who will be accessing the SQL Server data source.  
  
 This procedure describes how to set the preferred connection method for a DirectQuery model in the designer. It also describes how you can change this property in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] after the model has been deployed.  
  
### To set the preferred connection method for a DirectQuery model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the solution file for the DirectQuery model.  
  
2.  In Visual Studio, from the **Project** menu, select **Properties**.  
  
3.  In the **Properties** pane, change the property, **DirectQueryMode**, to one of the values that support DirectQuery usage:  
  
    -   **InMemory with DirectQuery**: If you use this option, the model is deployed but you must process the cache before you can run queries against the model.  
  
    -   **DirectQuery with InMemory**: If you use this option, the cache will be available for use by clients if it has already been processed. If you deploy the model with this setting and do not process the cache, some clients must get an error on trying to connect to the model.  
  
    -   **DirectQuery only**: If you use this option, the metadata is deployed but the model has no data in it. Clients that attempt to connect using In-Memory mode will get an error, indicating that the model does not exist or has not been processed.  
  
4.  If there are errors, in Visual Studio, open the **Error List** and resolve any problems that would prevent the model from being deployed in DirectQuery mode.  
  
### To verify or change the preferred connection method for a DirectQuery model  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], connect to the instance where you deployed the DirectQuery model.  
  
2.  Right-click the model database, and select **Properties**.  
  
3.  In the **Properties** pane, change the property, **DirectQueryMode**, to one of these values:  
  
    -   **DirectQuery Only**  
  
    -   **InMemory with DirectQuery**  
  
    -   **DirectQuery with InMemory**  
  
 Note that these properties are the same as the properties that you set on the project before deployment in Visual Studio. You can change the preferred connection mode for DirectQuery mode at any time, provided that you have configured the model to support DirectQuery usage.  
  
## See Also  
 [DirectQuery Mode &#40;SSAS Tabular&#41;](tabular-models/directquery-mode-ssas-tabular.md)   
 [Enable DirectQuery Design Mode &#40;SSAS Tabular&#41;](tabular-models/enable-directquery-mode-in-ssdt.md)  
  
  
