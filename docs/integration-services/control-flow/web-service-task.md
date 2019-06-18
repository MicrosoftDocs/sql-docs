---
title: "Web Service Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.webservicetask.f1"
  - "sql13.dts.designer.webservicestask.general.f1"
  - "sql13.dts.designer.webservicestask.input.f1"
  - "sql13.dts.designer.webservicestask.output.f1"
helpviewer_keywords: 
  - "Web Service task [Integration Services]"
ms.assetid: 5c7206f1-7d6a-4923-8dff-3c4912da4157
author: janinezhang
ms.author: janinez
manager: craigg
---
# Web Service Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Web Service task executes a Web service method. You can use the Web Service task for the following purposes:  
  
-   Writing to a variable the values that a Web service method returns. For example, you could obtain the highest temperature of the day from a Web service method, and then use that value to update a variable that is used in an expression that sets a column value.  
  
-   Writing to a file the values that a Web service method returns. For example, a list of potential customers can be written to a file and the file then used as a data source in a package that cleans the data before it is written to a database.  
  
## WSDL File  
 The Web Service task uses an HTTP connection manager to connect to the Web service. The HTTP connection manager is configured separately from the Web Service task, and is referenced in the task. The HTTP connection manager specifies the server proxy settings such as the server URL, credentials for accessing the Web services server, and time-out length. For more information, see [HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md).  
  
> [!IMPORTANT]  
>  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 The HTTP connection manager can point to a Web site or to a Web Service Description Language (WSDL) file. The URL of the HTTP connection manager that points to a WSDL file includes the `?WSDL` parameter: for example, `https://MyServer/MyWebService/MyPage.asmx?WSDL`.  
  
 The WSDL file must be available locally to configure the Web Service task using the **Web Service Task Editor** dialog box that [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides.  
  
-   If the HTTP connection manager points to a Web site, the WSDL file must be copied manually to a local computer.  
  
-   If the HTTP connection manager points to a WSDL file, the file can be downloaded from the Web site to a local file by the Web Service task.  
  
 The WSDL file lists the methods that the Web service offers, the input parameters that the methods require, the responses that the methods return, and how to communicate with the Web service.  
  
 If the method uses input parameters, the Web Service task requires parameter values. For example, a Web service method that recommends the length of skis you should purchase based on your height requires that your height be submitted in an input parameter. The parameter values can be provided either by strings that are defined in the task, or by variables defined in the scope of the task or a parent container. The advantage of using variables is that they let you dynamically update the parameter values by using package configurations or scripts. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Package Configurations](../../integration-services/packages/package-configurations.md).  
  
 Many Web service methods do not use input parameters. For example, a Web service method that gets the names of presidents who were born in the current month would not require an input parameter because the Web service can determine the current month locally.  
  
 The results of the Web service method can be written to a variable or to a file. You use the File connection manager either to specify the file or to provide the name of the variable to write the results to. For more information, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md) and [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md).  
  
## Custom Logging Messages Available on the Web Service Task  
 The following table lists the custom log entries that you can enable for the Web Service task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**WSTaskBegin**|The task began to access a Web service.|  
|**WSTaskEnd**|The task completed a Web service method.|  
|**WSTaskInfo**|Descriptive information about the task.|  
  
## Configuration of the Web Service Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Programmatic Configuration of the Web Service Task  
 For more information about programmatically setting these properties, click one of the following topics:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.WebServiceTask.WebServiceTask>  
  
## Web Service Task Editor (General Page)
  Use the **General** page of the **Web Services Task Editor** dialog box to specify an HTTP connection manager, specify the location of the Web Services Description Language (WSDL) file the Web Service task uses, describe the Web Services task, and download the WSDL file.  
  
### Options  
 **HTTPConnection**  
 Select a connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
> [!IMPORTANT]  
>  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 **Related Topics:**  [HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md), [HTTP Connection Manager Editor &#40;Server Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-server-page.md)  
  
 **WSDLFile**  
 Type the fully qualified path of a WSDL file that is local to the computer, or click the browse button **(...)** and locate this file.  
  
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
  
## Web Service Task Editor (Input Page)
  Use the **Input** page of the **Web Service Task Editor** dialog box to specify the Web Service, the Web method, and the values to provide to the Web method as input. The values can be provided either by typing strings directly in the Value column, or by selecting variables in the Value column.  
  
### Options  
 **Service**  
 Select a Web service from the list to use to execute the Web method.  
  
 **Method**  
 Select a Web method from the list for the task to execute.  
  
 **WebMethodDocumentation**  
 Type a description of Web method, or the click the browse button **(...)** and then type the description in the **Web Method Documentation** dialog box.  
  
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
  
## Web Service Task Editor (Output Page)
  Use the **Output** page of the **Web Service Task Editor** dialog box to specify where to store the result returned by the Web method.  
  
### Static Options  
 **OutputType**  
 Select the storage type to use when storing the results. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File Connection**|Store the results in a file. Selecting this value displays the dynamic option, **File**.|  
|**Variable**|Store the results in a variable. Selecting this value displays the dynamic option, **Variable**.|  
  
### OutputType Dynamic Options  
  
#### OutputType = File Connection  
 **File**  
 Select a File connection manager in the list or click \<**New Connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
#### OutputType = Variable  
 **Variable**  
 Select a variable in the list or click \<**New Variable...**> to create a new variable.  
  
 **Related Topics:**  [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
## Related Content  
 Video, [How to: Call a Web Service by Using the Web Service Task (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=259642), on technet.microsoft.com.  
