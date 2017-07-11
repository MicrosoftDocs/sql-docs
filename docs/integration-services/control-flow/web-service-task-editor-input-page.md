---
title: "Web Service Task Editor (Input Page) | Microsoft Docs"
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
  - "sql13.dts.designer.webservicestask.input.f1"
helpviewer_keywords: 
  - "Web Service Task Editor"
ms.assetid: 93529c88-f540-47f2-a10a-12c87318ed6f
caps.latest.revision: 30
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Web Service Task Editor (Input Page)
  Use the **Input** page of the **Web Service Task Editor** dialog box to specify the Web Service, the Web method, and the values to provide to the Web method as input. The values can be provided either by typing strings directly in the Value column, or by selecting variables in the Value column.  
  
 To learn about this task, see [Web Service Task](../../integration-services/control-flow/web-service-task.md).  
  
## Options  
 **Service**  
 Select a Web service from the list to use to execute the Web method.  
  
 **Method**  
 Select a Web method from the list for the task to execute.  
  
 **WebMethodDocumentation**  
 Type a description of Web method, or the click the browse button **(…)** and then type the description in the **Web Method Documentation** dialog box.  
  
 **Name**  
 Lists the names of the inputs to the Web method.  
  
 **Type**  
 Lists the data type of the inputs.  
  
> [!NOTE]  
>  The Web Service task supports parameters of the following data types only: primitive types such as integers and strings; arrays and sequences of primitive types; and enumerations.  
  
 **Variable**  
 Select the check boxes to use variables to provide inputs.  
  
 **Value**  
 If the Variable check-boxes are selected, select the variables in the list to provide the inputs; otherwise, type the values to use in the inputs.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Web Service Task Editor &#40;General Page&#41;](../../integration-services/control-flow/web-service-task-editor-general-page.md)   
 [Web Service Task Editor &#40;Output Page&#41;](../../integration-services/control-flow/web-service-task-editor-output-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
  