---
title: "Task Host Container | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.taskhostcontainer.f1"
helpviewer_keywords: 
  - "containers [Integration Services], Task Host"
  - "Task Host container"
ms.assetid: 7394a2c2-1b07-427d-b94a-9792e7783d35
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Task Host Container
  The task host container encapsulates a single task. In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the task host is not configured separately; instead, it is configured when you set the properties of the task it encapsulates. For more information about the tasks that the task host containers encapsulate, see [Integration Services Tasks](integration-services-tasks.md).  
  
 This container extends the use of variables and event handlers to the task level. For more information, see [Integration Services &#40;SSIS&#41; Event Handlers](../integration-services-ssis-event-handlers.md) and [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md).  
  
## Configuration of the Task Host  
 You can set properties in the **Properties** window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or programmatically.  
  
 For information about setting these properties in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], see [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md).  
  
 For information about programmatically setting these properties, see <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>.  
  
## Related Tasks  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## See Also  
 [Integration Services Containers](integration-services-containers.md)  
  
  
