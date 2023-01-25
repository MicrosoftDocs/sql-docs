---
description: "Define Filters"
title: "Define Filters | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.replconflictviewer.definefilters.f1"
helpviewer_keywords: 
  - "Define Filters dialog box"
ms.assetid: 1fa71d22-ce5a-4aae-ba05-4d755842aeac
author: "MashaMSFT"
ms.author: "mathoma"
---
# Define Filters
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Define Filters** dialog box allows you to define filters that you then apply to data conflicts to see a subset of the conflicts in the grid. To define a filter, choose an operator from the **Operator** drop-down list box and then enter a value. For example, to show only those conflicts in which the conflict loser is server **ReplTest1**, select **Equal to** from the **Operator** drop-down list box and enter **ReplTest1** in the first **Value** column.  
  
## Options  
 **Operator**  
 Select an operator for the filter, such as **Less than or Equal to**.
  
 **Value**  
 Enter a value for the filter. Most operators only require a value in the first **Value** column, but the **Between** and **Not Between** operators require a value in both of the **Value** columns.  
  
 **Clear**  
 Click this button to clear all filters that have been previously defined.  
  
## See Also  
 [Microsoft Replication Conflict Viewer &#40;Merge Replication&#41;](../../relational-databases/replication/microsoft-replication-conflict-viewer-merge-replication.md)   
 [Microsoft Replication Conflict Viewer &#40;Transactional Replication&#41;](../../relational-databases/replication/microsoft-replication-conflict-viewer-transactional-replication.md)  
  
  
