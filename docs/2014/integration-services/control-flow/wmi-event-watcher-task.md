---
title: "WMI Event Watcher Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.wmieventwatchertask.f1"
helpviewer_keywords: 
  - "WQL [Integration Services]"
  - "WMI Event Watcher task [Integration Services]"
ms.assetid: b5bb52e9-a77e-41e1-93f9-d4c3bc6b2c9a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# WMI Event Watcher Task
  The WMI Event Watcher task watches for a Windows Management Instrumentation (WMI) event using a Management Instrumentation Query Language (WQL) event query to specify events of interest. You can use the WMI Event Watcher task for the following purposes:  
  
-   Wait for notification that files have been added to a folder and then initiate the processing of the file.  
  
-   Run a package that deletes files when the available memory on a server drops lower than a specified percentage.  
  
-   Watch for installation of an application, and then run a package that uses the application.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a task that reads WMI information.  
  
 For more information about this task, click the following topic:  
  
-   [WMI Data Reader Task](wmi-data-reader-task.md)  
  
## WQL Queries  
 WQL is a dialect of SQL with extensions to support WMI event notification and other WMI-specific features. For more information about WQL, see the Windows Management Instrumentation documentation in the [MSDN Library](https://go.microsoft.com/fwlink/?linkid=62553).  
  
> [!NOTE]  
>  WMI classes vary between versions of Windows.  
  
 The following query watches for notification that the CPU use is more than 40 percent.  
  
```  
SELECT * from __InstanceModificationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_Processor' and TargetInstance.LoadPercentage > 40  
```  
  
 The following query watches for notification that a file has been added to a folder.  
  
```  
SELECT * FROM __InstanceCreationEvent WITHIN 10 WHERE TargetInstance ISA "CIM_DirectoryContainsFile" and TargetInstance.GroupComponent= "Win32_Directory.Name=\"c:\\\\WMIFileWatcher\""   
```  
  
## Custom Logging Messages Available on the WMI Event Watcher Task  
 The following table lists the custom log entries for the WMI Event Watcher task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`WMIEventWatcherEventOccurred`|Indicates that an event occurred that the task was monitoring.|  
|`WMIEventWatcherTimedout`|Indicates that the task timed out.|  
|`WMIEventWatcherWatchingForWMIEvents`|Indicates that the task began to execute the WQL query. The entry includes the query.|  
  
## Configuration of the WMI Event Watcher Task  
 You can configure the WMI Data Reader task in the following ways:  
  
-   Specify the WMI connection manager to use.  
  
-   Specify the source of the WQL query. The source can be external to the task, a variable or a file, or the query can be stored in a task property.  
  
-   Specify the action that the task takes when the WMI event occurs. You can log the event notification and the status after the event, or raise custom [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] events that provide information associated with the WMI event, the notification, and the status after the event.  
  
-   Define how the task responds to the event. The task can be configured to succeed or fail, depending on the event, or the task can just watch for the event again.  
  
-   Specify the action the task takes when the WMI query times out. You can log the time-out and the status after time-out, or raise a custom [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] event, indicating that the WMI event timed out and logging the time-out and time-out status.  
  
-   Define how the task responds to the time-out. The task can be configured to succeed or fail, or the task can just watch for the event again.  
  
-   Specify the number of times the task watches for the event.  
  
-   Specify the time-out.  
  
 If the source is a file, the WMI Event Watcher task uses a File connection manager to connect to the file. For more information, see [Flat File Connection Manager](../connection-manager/file-connection-manager.md).  
  
 The WMI Event Watcher task uses a WMI connection manager to connect to the server from which it reads WMI information. For more information, see [WMI Connection Manager](../connection-manager/wmi-connection-manager.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [WMI Event Watcher Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [WMI Event Watcher Task Editor &#40;WMI Options Page&#41;](../wmi-event-watcher-task-editor-wmi-options-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Programmatic Configuration of the WMI Event Watcher Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.WmiEventWatcherTask.WmiEventWatcherTask>  
  
  
