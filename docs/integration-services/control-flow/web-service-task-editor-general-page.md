---
title: "Web Service Task Editor (General Page) | Microsoft Docs"
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
  - "sql13.dts.designer.webservicestask.general.f1"
helpviewer_keywords: 
  - "Web Service Task Editor"
ms.assetid: 4d7df283-430d-4f0f-9dd4-5909554cd5eb
caps.latest.revision: 34
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Web Service Task Editor (General Page)
  Use the **General** page of the **Web Services Task Editor** dialog box to specify an HTTP connection manager, specify the location of the Web Services Description Language (WSDL) file the Web Service task uses, describe the Web Services task, and download the WSDL file.  
  
 For more information about this task, see [Web Service Task](../../integration-services/control-flow/web-service-task.md).  
  
## Options  
 **HTTPConnection**  
 Select a connection manager in the list, or click \<**New connection…**> to create a new connection manager.  
  
> [!IMPORTANT]  
>  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 **Related Topics:**  [HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md), [HTTP Connection Manager Editor &#40;Server Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-server-page.md)  
  
 **WSDLFile**  
 Type the fully qualified path of a WSDL file that is local to the computer, or click the browse button **(…)** and locate this file.  
  
 If you have already manually downloaded the WSDL file to the computer, select this file. However, if the WSDL file has not yet been downloaded, follow these steps:  
  
-   Create an empty file that has the ".wsdl" file name extension.  
  
-   Select this empty file for the **WSDLFile** option.  
  
-   Set the value of **OverwriteWSDLFile** to **True** to enable the empty file to be overwritten with the actual WSDL file.  
  
-   Click **Download WSDL** to download the actual WSDL file and overwrite the empty file.  
  
    > [!NOTE]  
    >  The **Download WSDL** option is not enabled until you provide the name of an existing local file in the **WSDLFile** box.  
  
 **OverwriteWSDLFile**  
 Indicate whether the WSDL file for the Web Service task can be overwritten.  
  
 If you intend to download the WSDL file by using the **Download WSDL** button, set this value to **True**.  
  
 **Name**  
 Provide a unique name for the Web Service task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Web Service task.  
  
 **Download WSDL**  
 Download the WSDL file.  
  
 This button is not enabled until you provide the name of an existing local file in the **WSDLFile** box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Web Service Task Editor &#40;Input Page&#41;](../../integration-services/control-flow/web-service-task-editor-input-page.md)   
 [Web Service Task Editor &#40;Output Page&#41;](../../integration-services/control-flow/web-service-task-editor-output-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
  