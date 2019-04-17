---
title: "Web Service Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.webservicetask.f1"
helpviewer_keywords: 
  - "Web Service task [Integration Services]"
ms.assetid: 5c7206f1-7d6a-4923-8dff-3c4912da4157
author: janinezhang
ms.author: janinez
manager: craigg
---
# Web Service Task
  The Web Service task executes a Web service method. You can use the Web Service task for the following purposes:  
  
-   Writing to a variable the values that a Web service method returns. For example, you could obtain the highest temperature of the day from a Web service method, and then use that value to update a variable that is used in an expression that sets a column value.  
  
-   Writing to a file the values that a Web service method returns. For example, a list of potential customers can be written to a file and the file then used as a data source in a package that cleans the data before it is written to a database.  
  
## WSDL File  
 The Web Service task uses an HTTP connection manager to connect to the Web service. The HTTP connection manager is configured separately from the Web Service task, and is referenced in the task. The HTTP connection manager specifies the server proxy settings such as the server URL, credentials for accessing the Web services server, and time-out length. For more information, see [HTTP Connection Manager](../connection-manager/http-connection-manager.md).  
  
> [!IMPORTANT]  
>  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 The HTTP connection manager can point to a Web site or to a Web Service Description Language (WSDL) file. The URL of the HTTP connection manager that points to a WSDL file includes the `?WSDL` parameter: for example, `http://MyServer/MyWebService/MyPage.asmx?WSDL`.  
  
 The WSDL file must be available locally to configure the Web Service task using the **Web Service Task Editor** dialog box that [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides.  
  
-   If the HTTP connection manager points to a Web site, the WSDL file must be copied manually to a local computer.  
  
-   If the HTTP connection manager points to a WSDL file, the file can be downloaded from the Web site to a local file by the Web Service task.  
  
 The WSDL file lists the methods that the Web service offers, the input parameters that the methods require, the responses that the methods return, and how to communicate with the Web service.  
  
 If the method uses input parameters, the Web Service task requires parameter values. For example, a Web service method that recommends the length of skis you should purchase based on your height requires that your height be submitted in an input parameter. The parameter values can be provided either by strings that are defined in the task, or by variables defined in the scope of the task or a parent container. The advantage of using variables is that they let you dynamically update the parameter values by using package configurations or scripts. For more information, see [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md) and [Package Configurations](../package-configurations.md).  
  
 Many Web service methods do not use input parameters. For example, a Web service method that gets the names of presidents who were born in the current month would not require an input parameter because the Web service can determine the current month locally.  
  
 The results of the Web service method can be written to a variable or to a file. You use the File connection manager either to specify the file or to provide the name of the variable to write the results to. For more information, see [File Connection Manager](../connection-manager/file-connection-manager.md) and [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md).  
  
## Custom Logging Messages Available on the Web Service Task  
 The following table lists the custom log entries that you can enable for the Web Service task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`WSTaskBegin`|The task began to access a Web service.|  
|`WSTaskEnd`|The task completed a Web service method.|  
|`WSTaskInfo`|Descriptive information about the task.|  
  
## Configuration of the Web Service Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Web Service Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Web Service Task Editor &#40;Input Page&#41;](../web-service-task-editor-input-page.md)  
  
-   [Web Service Task Editor &#40;Output Page&#41;](../web-service-task-editor-output-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Programmatic Configuration of the Web Service Task  
 For more information about programmatically setting these properties, click one of the following topics:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.WebServiceTask.WebServiceTask>  
  
## Related Content  
 Video, [How to: Call a Web Service by Using the Web Service Task (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=259642), on technet.microsoft.com.  
  
 [How To Consume Web Service Through SSIS Package](https://www.c-sharpcorner.com/article/how-to-consume-web-service-through-ssis-package/).  
  
  
