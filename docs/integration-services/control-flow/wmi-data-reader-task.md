---
title: "WMI Data Reader Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.wmidatareadertask.f1"
  - "sql13.dts.designer.wmidatareadertask.general.f1"
  - "sql13.dts.designer.wmidatareadertask.wmiquery.f1"
helpviewer_keywords: 
  - "WQL [Integration Services]"
  - "WMI Data Reader task [Integration Services]"
ms.assetid: dae57067-0275-4ac3-8f34-1b9d169f1112
author: janinezhang
ms.author: janinez
manager: craigg
---
# WMI Data Reader Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The WMI Data Reader task runs queries using the Windows Management Instrumentation (WMI) Query Language that returns information from WMI about a computer system. You can use the WMI Data Reader task for the following purposes:  
  
-   Query the Windows event logs on a local or remote computer and write the information to a file or variable.  
  
-   Obtain information about the presence, state, or properties of hardware components, and then use this information to determine whether other tasks in the control flow should run.  
  
-   Get a list of applications and determine what version of each application is installed.  
  
 You can configure the WMI Data Reader task in the following ways:  
  
-   Specify the WMI connection manager to use.  
  
-   Specify the source of the WQL query. The query can be stored in a task property, or the query can be stored outside the task, in a variable or a file.  
  
-   Define the format of the WQL query results. The task supports a table, property name/value pair, or property value format.  
  
-   Specify the destination of the query. The destination can be a variable or a file.  
  
-   Indicate whether the query destination is overwritten, kept, or appended.  
  
 If the source or destination is a file, the WMI Data Reader task uses a File connection manager to connect to the file. For more information, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
 The WMI Data Reader task uses a WMI connection manager to connect to the server from which it reads WMI information. For more information, see [WMI Connection Manager](../../integration-services/connection-manager/wmi-connection-manager.md).  
  
## WQL Query  
 WQL is a dialect of SQL with extensions to support WMI event notification and other WMI-specific features. For more information about WQL, see the Windows Management Instrumentation documentation in the [MSDN Library](https://go.microsoft.com/fwlink/?linkid=7022).  
  
> [!NOTE]  
>  WMI classes vary between versions of Windows.  
  
 The following WQL query returns entries in the Application log event.  
  
```  
SELECT * FROM Win32_NTLogEvent WHERE LogFile = 'Application' AND (SourceName='SQLISService' OR SourceName='SQLISPackage') AND TimeGenerated > '20050117'  
```  
  
 The following WQL query returns logical disk information.  
  
```  
SELECT FreeSpace, DeviceId, Size, SystemName, Description FROM Win32_LlogicalDisk  
```  
  
 The following WQL query returns a list of the quick fix engineering (QFE) updates to the operating system.  
  
```  
Select * FROM Win32_QuickFixEngineering  
```  
  
## Custom Logging Messages Available on the WMI Data Reader Task  
 The following table lists the custom log entries for the WMI Data Reader task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**WMIDataReaderGettingWMIData**|Indicates that the task began to read WMI data.|  
|**WMIDataReaderOperation**|Reports the WQL query that the task ran.|  
  
## Configuration of the WMI Data Reader Task  
 You can set properties programmatically or through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.WmiDataReaderTask.WmiDataReaderTask>  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## WMI Data Reader Task Editor (General Page)
  Use the **General** page of the **WMI Data Reader Task Editor** dialog box to name and describe the WMI Data Reader task.  
  
  For more information about WMI Query Language (WQL), see the Windows Management Instrumentation topic, [Querying with WQL](https://go.microsoft.com/fwlink/?LinkId=79045), in the MSDN Library.  
  
### Options  
 **Name**  
 Provide a unique name for the WMI Data Reader task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the WMI Data Reader task.  
  
## WMI Data Reader Task Editor (WMI Options Page)
  Use the **WMI Options** page of the **WMI Data Reader Task Editor** dialog box to specify the source of the Windows Management Instrumentation Query Language (WQL) query and the destination of the query result.  
  
 For more information about WMI Query Language (WQL), see the Windows Management Instrumentation topic, [Querying with WQL](https://go.microsoft.com/fwlink/?LinkId=79045), in the MSDN Library.  
  
### Static Options  
 **WMIConnectionName**  
 Select a WMI connection manager in the list, or click \<**New WMI Connection...**> to create a new connection manager.  
  
 **Related Topics:** [WMI Connection Manager](../../integration-services/connection-manager/wmi-connection-manager.md), [WMI Connection Manager Editor](../../integration-services/connection-manager/wmi-connection-manager-editor.md)  
  
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
  
### WQLQuerySourceType Dynamic Options  
  
#### WQLQuerySourceType = Direct input  
 **WQLQuerySource**  
 Provide a query, or click the ellipsis (...) and enter a query using the **WQL Query** dialog box.  
  
#### WQLQuerySourceType = File connection  
 **WQLQuerySource**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
#### WQLQuerySourceType = Variable  
 **WQLQuerySource**  
 Select a variable in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
### DestinationType Dynamic Options  
  
#### DestinationType = File connection  
 **Destination**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
#### DestinationType = Variable  
 **Destination**  
 Select a variable in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
