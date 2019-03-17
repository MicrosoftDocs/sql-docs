---
title: "Define a State Variable | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 45d66152-883a-49a7-a877-2e8ab45f8f79
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Define a State Variable
  This procedure describes how to define a package variable where the CDC state is stored.  
  
 The CDC state variable is loaded, initialized, and updated by the CDC Control task and is used by the CDC Source data flow component to determine the current processing range for change records. The CDC state variable can be defined on any container common to the CDC Control task and the CDC source. This may be at the package level but may also be on other containers such as a loop container.  
  
 Manually modifying the CDC state variable value is not recommended, however it can be useful to understand its contents.  
  
 The following table provides a high-level description of the components of the CDC state variable value.  
  
|Component|Description|  
|---------------|-----------------|  
|`<state-name>`|This is the name of the current CDC state.|  
|`CS`|This marks the current processing range start point (Current Start).|  
|`<cs-lsn>`|This is the last (Log Sequence Number) LSN processed in the previous CDC run.|  
|`CE`|This marks the current processing range end point (Current End). The presence of the CE component in the CDC state is an indication that either a CDC package is currently processing or that a CDC package failed before fully processing its CDC processing range.|  
|`<ce-lsn>`|This is the last LSN to be processed in the current CDC Run. It is always assumed that the last sequence number to be processed is the maximum (0xFFF...).|  
|`IR`|This marks the initial processing range.|  
|`<ir-start>`|This is an LSN of a change just before the initial load began.|  
|`<ir-end>`|This is an LSN of a change just after the initial load ended.|  
|`TS`|This marks the timestamp for the last CDC state update.|  
|**\<timestamp>**|This is a decimal representation of the 64-bit, System.DateTime.UtcNow property.|  
|`ER`|This appears when the last operation failed and includes a short description of the cause of the error. If this component is present, it will always appear last.|  
|`<short-error-text>`|This is the short error description.|  
  
 The LSNs and sequence numbers are each encoded as a hexadecimal string of up to 20 digits representing the LSN value of Binary(10).  
  
 The following table describes the possible CDC state values.  
  
|State|Description|  
|-----------|-----------------|  
|(INITIAL)|This is the initial state before the any package was run on the current CDC group. This is also the state when the CDC state is empty.|  
|ILSTART (Initial Load Started)|This is the state when the initial load package starts, after the `MarkInitialLoadStart` operation call to the CDC Control task.|  
|ILEND (Initial Load Ended)|This is the state when the initial load package ends successfully, after the `MarkInitialLoadEnd` operation call to the CDC Control task.|  
|ILUPDATE (Initial Load Update)|This is the state on the runs of the trickle feed update package following the initial load, while still processing the initial processing range. This is after the `GetProcessingRange` operation call to the CDC Control task.<br /><br /> If using the __$reprocessing column, it is set to 1 to indicate that the package may be re-processing rows already at the target.|  
|TFEND (Trickle-Feed Update Ended)|This is the state expected for regular CDC runs. It indicates that the previous run completed successfully and that a new run with a new processing range can be started.|  
|TFSTART|This is the state on a non-initial run of the trickle feed update package, after the `GetProcessingRange` operation call to the CDC Control task.<br /><br /> This indicates that a regular CDC run is started but has not finished or has not yet finished, cleanly (`MarkProcessedRange`).|  
|TFREDO (Reprocessing Trickle-Feed Updates)|This is the state on a `GetProcessingRange` that occurs after TFSTART. This indicates that the previous run did not complete successfully.<br /><br /> If using the __$reprocessing column, it is set to 1 to indicate that the package may be re-processing rows already at the target.|  
|ERROR|The CDC group is in an ERROR state.|  
  
 The following are examples of CDC state variable values.  
  
-   ILSTART/IR/0x0000162B158700000000//TS/2011-08-07T17:10:43.0031645/  
  
-   ILSTART/IR/0x0000162B158700000000//TS/2011-08-07T17:10:43.0031645/  
  
-   TFEND/CS/0x0000025B000001BC0003/TS/2011-07-17T12:05:58.1001145/  
  
-   TFSTART/CS/0x0000030D000000AE0003/CE/0x0000159D1E0F01000000/TS/2011-08-09T05:30:43.9344900/  
  
-   TFREDO/CS/0x0000030D000000AE0003/CE/0x0000159D1E0F01000000/TS/2011-08-09T05:30:59.5544900/  
  
### To define a CDC state variable  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the CDC flow where you need to define the variable.  
  
2.  Click the **Package Explorer** tab, and add a new variable.  
  
3.  Give the variable a name that you can recognize as your state variable.  
  
4.  Give the variable a **String** data type.  
  
 Do not give the variable a value as part of its definition. The value must be set by the CDC Control task.  
  
 If you plan to use the CDC Control task with **Automatic State Persistence**, the CDC State variable will be read from the database state table you specify and will be updated back to that same table when its value changes. For more information about the State table, see [CDC Control Task](../control-flow/cdc-control-task.md)and [CDC Control Task Editor](../cdc-control-task-editor.md).  
  
 If you are not using the CDC Control task with Automatic State Persistence then you must load the variable value from persistent storage where its value was saved the last time the package ran and to write it back to the persistent storage when the processing of the current processing range was completed.  
  
## See Also  
 [CDC Control Task](../control-flow/cdc-control-task.md)   
 [CDC Control Task Editor](../cdc-control-task-editor.md)  
  
  
