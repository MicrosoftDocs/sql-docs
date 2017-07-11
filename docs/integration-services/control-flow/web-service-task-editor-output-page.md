---
title: "Web Service Task Editor (Output Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.webservicestask.output.f1"
helpviewer_keywords: 
  - "Web Service Task Editor"
ms.assetid: 73c83969-7b0e-479d-a436-0a46b2068d01
caps.latest.revision: 27
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Web Service Task Editor (Output Page)
  Use the **Output** page of the **Web Service Task Editor** dialog box to specify where to store the result returned by the Web method.  
  
 To learn about this task, see [Web Service Task](../../integration-services/control-flow/web-service-task.md).  
  
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
 Select a File connection manager in the list or click \<**New Connection…**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
### OutputType = Variable  
 **Variable**  
 Select a variable in the list or click \<**New Variable…**> to create a new variable.  
  
 **Related Topics:**  [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](http://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Web Service Task Editor &#40;General Page&#41;](../../integration-services/control-flow/web-service-task-editor-general-page.md)   
 [Web Service Task Editor &#40;Input Page&#41;](../../integration-services/control-flow/web-service-task-editor-input-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
  