---
title: "Data Mining Query Task Editor (Output Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.dmquerytask.output.f1"
helpviewer_keywords: 
  - "Data Mining Query Task Editor"
ms.assetid: 62f9e015-6fe0-4396-ad90-3ad51bf00025
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Data Mining Query Task Editor (Output Tab)
  Use the **Output** tab of the **Data Mining Query Task Editor** dialog box to specify the destination of the prediction query.  
  
 To learn about implementing data mining in packages, see [Data Mining Query Task](control-flow/data-mining-query-task.md) and [Data Mining Solutions](../analysis-services/data-mining/data-mining-solutions.md).  
  
## General Options  
 **Name**  
 Provide a unique name for the Data Mining Query task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Data Mining Query task.  
  
## Output Tab Options  
 **Connection**  
 Select a connection manager in the list or click **New** to create a new connection manager.  
  
 **New**  
 Create a new connection manager. Only the ADO.NET and OLE DB connection manager types can be used.  
  
 **Output table**  
 Specify the table to which the prediction query writes its results.  
  
 **Drop and re-create the output table**  
 Indicate whether the prediction query should overwrite content in the destination table by dropping and then re-creating the table.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Data Mining Query Task Editor &#40;Mining Model Tab&#41;](../../2014/integration-services/data-mining-query-task-editor-mining-model-tab.md)   
 [Data Mining Query Task Editor &#40;Query Tab&#41;](../../2014/integration-services/data-mining-query-task-editor-query-tab.md)   
 [Data Mining Designer](../analysis-services/data-mining/data-mining-designer.md)  
  
  
