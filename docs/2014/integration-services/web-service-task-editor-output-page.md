---
title: "Web Service Task Editor (Output Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.webservicestask.output.f1"
helpviewer_keywords: 
  - "Web Service Task Editor"
ms.assetid: 73c83969-7b0e-479d-a436-0a46b2068d01
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Web Service Task Editor (Output Page)
  Use the **Output** page of the **Web Service Task Editor** dialog box to specify where to store the result returned by the Web method.  
  
 To learn about this task, see [Web Service Task](control-flow/web-service-task.md).  
  
## Static Options  
 **OutputType**  
 Select the storage type to use when storing the results. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File Connection**|Store the results in a file. Selecting this value displays the dynamic option, **File**.|  
|**Variable**|Store the results in a variable. Selecting this value displays the dynamic option, **Variable**.|  
  
## OutputType Dynamic Options  
  
### OutputType = File Connection  
 **File**  
 Select a File connection manager in the list or click \<**New Connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
### OutputType = Variable  
 **Variable**  
 Select a variable in the list or click \<**New Variable...**> to create a new variable.  
  
 **Related Topics:**  [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Web Service Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Web Service Task Editor &#40;Input Page&#41;](../../2014/integration-services/web-service-task-editor-input-page.md)   
 [Expressions Page](expressions/expressions-page.md)  
  
  
