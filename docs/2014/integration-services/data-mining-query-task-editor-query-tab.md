---
title: "Data Mining Query Task Editor (Query Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.dmquerytask.query.f1"
helpviewer_keywords: 
  - "Data Mining Query Task Editor"
ms.assetid: 72b1755d-d226-46c5-b862-0c9333196a10
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Data Mining Query Task Editor (Query Tab)
  Use the **Query** tab of the **Data Mining Query Task** dialog box to create prediction queries based on a mining model. In this dialog box you can also bind parameters and result sets to variables.  
  
 To learn about implementing data mining in packages, see [Data Mining Query Task](control-flow/data-mining-query-task.md) and [Data Mining Solutions](../analysis-services/data-mining/data-mining-solutions.md).  
  
## General Options  
 **Name**  
 Provide a unique name for the Data Mining Query task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Data Mining Query task.  
  
## Build Query Tab Options  
 **Data mining query**  
 Type a data mining query.  
  
 **Related Topics:**  [Data Mining Extensions &#40;DMX&#41; Reference](/sql/dmx/data-mining-extensions-dmx-reference)  
  
 **Build New Query**  
 Create the data mining query using a graphical tool.  
  
 **Related Topics:** [Data Mining Query](control-flow/data-mining-query.md)  
  
## Parameter Mapping Tab Options  
 **Parameter Name**  
 Optionally, update the parameter name. Map the parameter to a variable by selecting a variable in the **Variable Name** list.  
  
 **Variable Name**  
 Select a variable in the list to map it to the parameter.  
  
 **Add**  
 Add to a parameter to the list.  
  
 **Remove**  
 Select a parameter, and then click **Remove**.  
  
## Result Set Tab Options  
 **Result Name**  
 Optionally, update the result set name. Map the result to a variable by selecting a variable in the **Variable Name** list.  
  
 After you have added a result by clicking **Add**, provide a unique name for the result.  
  
 **Variable Name**  
 Select a variable in the list to map it to the result set.  
  
 **Result Type**  
 Indicate whether to return a single row or a full result set.  
  
 **Add**  
 Add a result set to the list.  
  
 **Remove**  
 Select a result, and then click **Remove**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Data Mining Query Task Editor &#40;Mining Model Tab&#41;](../../2014/integration-services/data-mining-query-task-editor-mining-model-tab.md)   
 [Data Mining Query Task Editor &#40;Output Tab&#41;](../../2014/integration-services/data-mining-query-task-editor-output-tab.md)   
 [Data Mining Designer](../analysis-services/data-mining/data-mining-designer.md)  
  
  
