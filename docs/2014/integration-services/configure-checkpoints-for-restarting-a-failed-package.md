---
title: "Configure Checkpoints for Restarting a Failed Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "checkpoints [Integration Services]"
  - "restarting packages"
  - "starting packages"
ms.assetid: 9afffa5a-d803-4653-8afc-386453fc163f
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Configure Checkpoints for Restarting a Failed Package
  You configure [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages to restart from a point of failure, instead of rerunning the entire package, by setting the properties that apply to checkpoints.  
  
### To configure a package to restart  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want to configure.  
  
2.  In **Solution Explorer**, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  Right-click anywhere in the background of the control flow design surface, and then click **Properties**.  
  
5.  Set the SaveCheckpoints property to `True`.  
  
6.  Type the name of the checkpoint file in the CheckpointFileName property.  
  
7.  Set the CheckpointUsage property to one of two values:  
  
    -   Select `Always` to always restart the package from the checkpoint.  
  
        > [!IMPORTANT]  
        >  An error occurs if the checkpoint file is not available.  
  
    -   Select `IfExists` to restart the package only if the checkpoint file is available.  
  
8.  Configure the tasks and containers from which the package can restart.  
  
    -   Right-click a task or container and click **Properties**.  
  
    -   Set the FailPackageOnFailure property to `True` for each selected task and container.  
  
## See Also  
 [Restart Packages by Using Checkpoints](packages/restart-packages-by-using-checkpoints.md)  
  
  
