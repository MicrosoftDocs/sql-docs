---
title: "Data Viewer"
description: "Data Viewer"
author: chugugrace
ms.author: chugu
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: ui-reference
f1_keywords:
  - "sql13.dts.designer.dataviewer.f1"
helpviewer_keywords:
  - "Data Viewer dialog box"
---
# Data Viewer

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  If a path is configured to use a data viewer, the Data Viewer displays the data buffer by buffer as data moves between two data flow components.  
  
## Options  
 **Green arrow**  
 Click to display the data in the next buffer. If the data can be moved in a single buffer, this option is not available.  
  
 **Detach**  
 Detach the data viewer.  
  
 **Note** Detaching a data viewer does not delete the data viewer. If the data viewer has been detached, data continues to flow through the path, but the data in the viewer is not updated to match the data in each buffer.  
  
 **Attach**  
 Attach a data viewer.  
  
 **Note** When the data viewer is attached, it displays information from each buffer in the data flow and then pauses. You can advance through the buffers by using the green arrow.  
  
 **Copy Data**  
 Copy data in the current buffer to the Clipboard.  
  
## See Also  
 [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md)  
  
  
