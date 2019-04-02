---
title: "WMI Event Watcher Task Editor (WMI Options Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.wmieventwatcher.wmiquery.f1"
helpviewer_keywords: 
  - "WMI Event Watcher Task Editor"
ms.assetid: 525f3de7-a021-4e52-9939-3a83c88f131a
author: janinezhang
ms.author: janinez
manager: craigg
---
# WMI Event Watcher Task Editor (WMI Options Page)
  Use the **WMI Options** page of the **WMI Event Watcher Task Editor** dialog box to specify the source of the Windows Management Instrumentation Query Language (WQL) query and how the WMI Event Watcher task responds to Microsoft Windows Instrumentation (WMI) events.  
  
 To learn about this task, see [WMI Event Watcher Task](control-flow/wmi-event-watcher-task.md). For more information about WMI Query Language (WQL), see the Windows Management Instrumentation topic, [Querying with WQL](https://go.microsoft.com/fwlink/?LinkId=79045), in the MSDN Library.  
  
## Static Options  
 **WMIConnectionName**  
 Select a WMI connection manager in the list, or click \<**New WMI Connection...**> to create a new connection manager.  
  
 **Related Topics:** [WMI Connection Manager](connection-manager/wmi-connection-manager.md), [WMI Connection Manager Editor](../../2014/integration-services/wmi-connection-manager-editor.md)  
  
 **WQLQuerySourceType**  
 Select the source type of the WQL query that the task runs. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to a WQL query. Selecting this value displays the dynamic option, **WQLQuerySource**.|  
|**File connection**|Select a file that contains the WQL query. Selecting this value displays the dynamic option, **WQLQuerySource**.|  
|**Variable**|Set the source to a variable that defines WQL query. Selecting this value displays the dynamic option, **WQLQuerySource**.|  
  
 **ActionAtEvent**  
 Specify whether the WMI event logs the event and initiates an [!INCLUDE[ssIS](../includes/ssis-md.md)] action, or only logs the event.  
  
 **AfterEvent**  
 Specify whether the task succeeds or fails after it receives the WMI event, or if the task continues watching for the event to occur again.  
  
 **ActionAtTimeout**  
 Specify whether the task logs a WMI query time-out and initiates an [!INCLUDE[ssIS](../includes/ssis-md.md)] event in response, or only logs the time-out.  
  
 **AfterTimeout**  
 Specify whether the task succeeds or fails in response to a time-out, or if the task continues watching for another time-out to recur.  
  
 **NumberOfEvents**  
 Specify the number of events to watch for.  
  
 **Timeout**  
 Specify the number of seconds to wait for the event to occur. A value of 0 means that no time-out is in effect.  
  
## WQLQuerySource Dynamic Options  
  
### WQLQuerySource = Direct input  
 **WQLQuerySource**  
 Provide a query, or click the ellipsis button (...) and enter a query using the **WQL Query** dialog box.  
  
### WQLQuerySource = File connection  
 **WQLQuerySource**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
### WQLQuerySource = Variable  
 **WQLQuerySource**  
 Select a variable in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [WMI Event Watcher Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [WMI Data Reader Task](control-flow/wmi-data-reader-task.md)  
  
  
