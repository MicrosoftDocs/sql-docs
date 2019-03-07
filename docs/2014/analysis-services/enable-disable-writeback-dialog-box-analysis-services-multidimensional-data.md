---
title: "Enable-Disable Writeback Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.partitiondesigner.writebackenabledisable.f1"
ms.assetid: 2d254393-3f0d-4b70-8b98-87159f9f3639
author: minewiskan
ms.author: owend
manager: craigg
---
# Enable-Disable Writeback Dialog Box (Analysis Services - Multidimensional Data)
  The **Enable/Disable Writeback** dialog box enables or disables writeback for a measure group in a cube. Enabling writeback on a measure group defines a writeback partition and creates a writeback table for that measure group. Disabling writeback on a measure group removes the writeback partition but does not delete the writeback table, to avoid unanticipated data loss. The **Enable/Disable Writeback** dialog box is displayed by:  
  
-   Clicking **Writeback Settings** in the **Measure Groups** pane on the **Partitions** tab in Cube Designer.  
  
-   Right-clicking a partition in the **Partitions** grid in the **Measure Groups** pane on the **Partitions** tab in Cube Designer and selecting **Writeback Settings** from the context menu.  
  
## Options  
 **Table name**  
 Type the name of the writeback table to create for the selected partition. The writeback table stores the changes made to the measure group from a client application.  
  
> [!NOTE]  
>  This option is disabled if writeback is not enabled.  
  
 **Data source**  
 Select the data source to contain the writeback table.  
  
> [!NOTE]  
>  This option is disabled if writeback is not enabled.  
  
 **New**  
 Click to display the **Connection Manager** dialog box and define a new data source to contain the writeback table.  
  
> [!NOTE]  
>  This option is disabled if writeback is not enabled.  
  
  
