---
description: "System Variables"
title: "System Variables | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "containers [Integration Services], variables"
  - "tasks [Integration Services], variables"
  - "system variables [Integration Services]"
  - "event handlers [Integration Services], variables"
  - "variables [Integration Services], system"
ms.assetid: efecd0d4-1489-4eba-a8fe-275d647058b8
author: chugugrace
ms.author: chugu
---
# System Variables

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides a set of system variables that store information about the running package and its objects. These variables can be used in expressions and property expressions to customize packages, containers, tasks, and event handlers.  
  
 All variables-system and user-defined- can be used in the parameter bindings that the Execute SQL task uses to map variables to parameters.  
  
## System Variables for Packages  
 The following table describes the system variables that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides for packages.  
  
|System variable|Data type|Description|  
|---------------------|---------------|-----------------|  
|**CancelEvent**|Int32|The handle to a Windows Event object that the task can signal to indicate that the task should stop running.|  
|**ContainerStartTime**|DateTime|The start time of the container.|  
|**CreationDate**|DateTime|The date that the package was created.|  
|**CreatorComputerName**|String|The computer on which the package was created.|  
|**CreatorName**|String|The name of the person who built the package.|  
|**ExecutionInstanceGUID**|String|The unique identifier of the executing instance of a package.|  
|**FailedConfigurations**|String|The names of package configurations that have failed.|  
|**IgnoreConfigurationsOnLoad**|Boolean|Indicates whether package configurations are ignored when loading the package.|  
|**InteractiveMode**|Boolean|Indicates whether the package is run in interactive mode. If a package is running in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, this property is set to **True**. If a package is running using the **DTExec** command prompt utility, the property is set to **False**.|  
|**LocaleId**|Int32|The locale that the package uses.|  
|**MachineName**|String|The name of the computer on which the package is running.|  
|**OfflineMode**|Boolean|Indicates whether the package is in offline mode. Offline mode does not acquire connections to data sources.|  
|**PackageID**|String|The unique identifier of the package.|  
|**PackageName**|String|The name of the package.|  
|**StartTime**|DateTime|The time that the package started to run.|  
|**ServerExecutionID**|Int64|Execution ID for the package that is executed on the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server.<br /><br /> The default value is zero. The value is changed only if the package is executed by ISServerExec on the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Server. When there is a child package, the value is passed from the parent package to child package.|  
|**UserName**|String|The account of the user who started the package. The user name is qualified by the domain name.|  
|**VersionBuild**|Int32|The package version.|  
|**VersionComment**|String|Comments about the package version.|  
|**VersionGUID**|String|The unique identifier of the version.|  
|**VersionMajor**|Int32|The major version of the package.|  
|**VersionMinor**|Int32|The minor version of the package.|  
  
## System Variables for Containers  
 The following table describes the system variables that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides for the For Loop, Foreach Loop, and Sequence containers.  
  
|System variable|Data type|Description|Container|  
|---------------------|---------------|-----------------|---------------|  
|**LocaleId**|Int32|The locale that the container uses.|For Loop container<br /><br /> Foreach Loop container<br /><br /> Sequence container|  
  
## System Variables for Tasks  
 The following table describes the system variables that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides for tasks.  
  
|System variable|Data type|Description|  
|---------------------|---------------|-----------------|  
|**CreationName**|String|The name of the task.|  
|**LocaleId**|Int32|The locale that the task uses.|  
|**TaskID**|String|The unique identifier of a task instance.|  
|**TaskName**|String|The name of the task instance.|  
|**TaskTransactionOption**|Int32|The transaction option that the task uses.|  
  
## System Variables for Event Handlers  
 The following table describes the system variables that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides for event handlers. Not all variables are available to all event handlers.  
  
|System variable|Data type|Description|Event handler|  
|---------------------|---------------|-----------------|-------------------|  
|**Cancel**|Boolean|Indicates whether the event handler stops running when an error, warning, or query cancellation occurs.|OnError event handler<br /><br /> OnWarning event handler<br /><br /> OnQueryCancel event handler|  
|**ErrorCode**|Int32|The error identifier.|OnError event handler<br /><br /> OnInformation event handler<br /><br /> OnWarning event handler|  
|**ErrorDescription**|String|The description of the error.|OnError event handler<br /><br /> OnInformation event handler<br /><br /> OnWarning event handler|  
|**ExecutionStatus**|Boolean|The current execution status.|OnExecStatusChanged event handler|  
|**ExecutionValue**|DBNull|The execution value.|OnTaskFailed event handler|  
|**LocaleId**|Int32|The locale that the event handler uses.|All event handlers|  
|**PercentComplete**|Int32|The percentage of completed work.|OnProgress event handler|  
|**ProgressCountHigh**|Int32|The high part of a 64-bit value that indicates the total number of operations processed by the OnProgress event.|OnProgress event handler|  
|**ProgressCountLow**|Int32|The low part of a 64-bit value that indicates the total number of operations processed by the OnProgress event.|OnProgress event handler|  
|**ProgressDescription**|String|Description of the progress.|OnProgress event handler|  
|**Propagate**|Boolean|Indicates whether the event is propagated to a higher level event handler.<br /><br /> Note: The value of the **Propagate** variable is disregarded during the validation of the package. If you set **Propagate** to **False** in a child package, this does not prevent an event from propagating up to the parent package.|All event handlers|  
|**SourceDescription**|String|The description of the executable in the event handler that raised the event.|All event handlers|  
|**SourceID**|String|The unique identifier of the executable in the event handler that raised the event.|All event handlers|  
|**SourceName**|String|The name of the executable in the event handler that raised the event.|All event handlers|  
|**VariableDescription**|String|The variable description.|OnVariableValueChanged event handler|  
|**VariableID**|String|The unique identifier of the variable.|OnVariableValueChanged event handler|  
  
## System Variables in Parameter Bindings  
 It is frequently useful to save the values of system variables in tables when the package is run. For example, a package that dynamically creates a table and writes the GUID of the package execution instance that created the table in a table column.  
  
 If you use system variables to map to parameters in the SQL statement that an Execute SQL task uses, it is important that you set the data type of each parameter binding to the data type of the system variable. Otherwise, the values of system variables may be translated incorrectly. For example, if the **ExecutionInstanceGUID** system variable, which has the string data type and contains a string that represents the GUID of the executing instance of a package, is used in a parameter binding with the GUID data type, the GUID of the package instance will be translated incorrectly.  
  
 This rule applies to user-defined variables as well. But, whereas the data types of system variables cannot be changed and you have to tailor your use of these variables to fit the data types, user-defined are more flexible. The user-defined variables that are used in parameter bindings are usually defined with data types that are compatible with the data types of parameters to which they are mapped.  
  
## Related Tasks  
 [Map Query Parameters to Variables in an Execute SQL Task](./control-flow/execute-sql-task.md)  
  
