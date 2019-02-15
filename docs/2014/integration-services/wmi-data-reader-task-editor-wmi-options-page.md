---
title: "WMI Data Reader Task Editor (WMI Options Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.wmidatareadertask.wmiquery.f1"
helpviewer_keywords: 
  - "WMI Data Reader Task Editor"
ms.assetid: 4b8d4716-882d-41b0-b77e-e0e2741a2cd5
author: douglaslms
ms.author: douglasl
manager: craigg
---
# WMI Data Reader Task Editor (WMI Options Page)
  Use the **WMI Options** page of the **WMI Data Reader Task Editor** dialog box to specify the source of the Windows Management Instrumentation Query Language (WQL) query and the destination of the query result.  
  
 To learn about this task, see [WMI Data Reader Task](control-flow/wmi-data-reader-task.md). For more information about WMI Query Language (WQL), see the Windows Management Instrumentation topic, [Querying with WQL](https://go.microsoft.com/fwlink/?LinkId=79045), in the MSDN Library.  
  
## Static Options  
 **WMIConnectionName**  
 Select a WMI connection manager in the list, or click \<**New WMI Connection...**> to create a new connection manager.  
  
 **Related Topics:** [WMI Connection Manager](connection-manager/wmi-connection-manager.md), [WMI Connection Manager Editor](../../2014/integration-services/wmi-connection-manager-editor.md)  
  
 **WQLQuerySourceType**  
 Select the source type of the WQL query that the task runs. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to a WQL query. Selecting this value displays the dynamic option **WQLQuerySourceType**.|  
|**File connection**|Select a file that contains the WQL query. Selecting this value displays the dynamic option **WQLQuerySourceType**.|  
|**Variable**|Set the source to a variable that defines the WQL query. Selecting this value displays the dynamic option **WQLQuerySourceType**.|  
  
 **OutputType**  
 Specify whether the output should be a data table, property value, or property name and value.  
  
 **OverwriteDestination**  
 Specifies whether to keep, overwrite, or append to the original data in the destination file or variable.  
  
 **DestinationType**  
 Select the destination type of the WQL query that the task runs. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File connection**|Select a file to save the results of the WQL query in. Selecting this value displays the dynamic option, **DestinationType**.|  
|**Variable**|Set the variable to store the results of the WQL query in. Selecting this value displays the dynamic option, **DestinationType**.|  
  
## WQLQuerySourceType Dynamic Options  
  
### WQLQuerySourceType = Direct input  
 **WQLQuerySource**  
 Provide a query, or click the ellipsis (...) and enter a query using the **WQL Query** dialog box.  
  
### WQLQuerySourceType = File connection  
 **WQLQuerySource**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
### WQLQuerySourceType = Variable  
 **WQLQuerySource**  
 Select a variable in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
## DestinationType Dynamic Options  
  
### DestinationType = File connection  
 **Destination**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
### DestinationType = Variable  
 **Destination**  
 Select a variable in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [WMI Data Reader Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [WMI Event Watcher Task](control-flow/wmi-event-watcher-task.md)  
  
  
