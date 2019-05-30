---
title: "Enumerated Constants in Property Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "enumerators [Integration Services]"
  - "packages [Integration Services], expressions"
  - "dynamic properties"
  - "updating package properties"
  - "enumerated constants [Integration Services]"
  - "property expressions [Integration Services]"
ms.assetid: a4418315-38e2-4ad3-8784-576163b25d6f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Enumerated Constants in Property Expressions

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  If property expressions include values from an enumerator member list, the expression must use the numeric value of the enumerator member instead of the friendly name of the member. For example, if an expression sets the **LoggingMode** property then you must use the numeric value 2 instead of the friendly name Disabled.  
  
 This topic lists only the numeric values equivalent to friendly names of enumerators whose members are commonly used in property expressions. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model includes many additional enumerators that you use when you program the object model to build packages programmatically or code custom package elements such as tasks and data flow components.  
  
 In addition to the custom properties for packages and package objects, the Properties window in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] includes a set of properties that are available to packages, tasks, and the Foreach Loop, For Loop, and Sequence containers. The common properties that are set by values from enumerators-**ForceExecutionResult**, **LoggingMode**, **IsolationLevel**, and **Transaction Option**-are listed in Common Properties section.  
  
 The following sections provide information about enumerated constants:  
  
 [Package](#Package)  
  
 [Foreach Loop Enumerators](#Foreach)  
  
 [Tasks](#Tasks)  
  
 [Maintenance Plan Tasks](#MaintenancePlanTasks)  
  
 [Common Properties](#CommonProperties)  
  
##  <a name="Package"></a> Package  
 The following tables lists the friendly names and the numeric value equivalents for properties of packages that you set by using values from an enumerator.  
  
 **PackageType** property-Set by using values from the **DTSPackageType** enumeration.  
  
|Friendly name in DTSPackageType|Numeric value|  
|-------------------------------------|-------------------|  
|Default|0|  
|DTSWizard|1|  
|DTSDesigner|2|  
|SQLReplication|3|  
|DTSDesigner100|5|  
|SQLDBMaint|6|  
  
 **CheckpointUsage** property-Set by using values from the **DTSCheckpointUsage** enumeration.  
  
|Friendly name in DTSCheckpointUsage|Numeric value|  
|-----------------------------------------|-------------------|  
|Never|0|  
|IfExists|1|  
|Always|2|  
  
 **PackagePriorityClass** property-Set by using values from the **DTSPriorityClass** enumeration.  
  
|Friendly name in DTSPriorityClass|Numeric value|  
|---------------------------------------|-------------------|  
|Default|0|  
|AboveNormal|1|  
|Normal|2|  
|BelowNormal|3|  
|Idle|4|  
  
 **ProtectionLevel** property-Set by using values from the **DTSProtectionLevel** enumeration.  
  
|Friendly name in DTSProtectionLevel|Numeric value|  
|-----------------------------------------|-------------------|  
|DontSaveSensitive|0|  
|EncryptSensitiveWithUserKey|1|  
|EncryptSensitiveWithPassword|2|  
|EncryptAllWithPassword|3|  
|EncryptAllWithUserKey|4|  
|ServerStorage|5|  
  
##  <a name="PrecedenceConstraints"></a> Precedence Constraints  
 **EvalOp** property-Set by using values from the **DTSPrecedenceEvalOp** enumeration.  
  
|Friendly name in DTSPrecedenceEvalOp|Numeric value|  
|------------------------------------------|-------------------|  
|Expression|1|  
|Constraint|2|  
|ExpressionAndConstraint|3|  
|ExpressionOrConstraint|4|  
  
 **Value** property-Set by using values from the **DTSExecResult** enumeration.  
  
|Friendly Name|Numeric Value|  
|-------------------|-------------------|  
|Success|0|  
|Failure|1|  
|Completion|2|  
|Canceled|3|  
  
##  <a name="Foreach"></a> Foreach Loop Enumerators  
 The Foreach Loop includes a set of enumerators with properties that can be set by property expressions.  
  
### Foreach ADO Enumerator  
 **Type** property-Set by using values from the **ADOEnumerationType** enumeration.  
  
|Friendly name in ADOEnumerationType|Numeric value|  
|-----------------------------------------|-------------------|  
|EnumerateTables|0|  
|EnumerateAllRows|1|  
|EnumerateRowsInFirstTable|2|  
  
### Foreach Nodelist Enumerator  
 **SourceDocumentType**, **InnerXPathStringSourceType**, and **OuterXPathStringSourceType** properties-Set by using values from the **SourceType** enumeration.  
  
|Friendly name in SourceType|Numeric value|  
|---------------------------------|-------------------|  
|FileConnection|0|  
|Variable|1|  
|DirectInput|2|  
  
 **EnumerationType** property-Set by using values from the **EnumerationType** enumeration.  
  
|Friendly name in EnumerationType|Numeric value|  
|--------------------------------------|-------------------|  
|Navigator|0|  
|Node|1|  
|NodeText|2|  
|ElementCollection|3|  
  
 **InnerElementType** property-Set by using values from the **InnerElementType** enumeration.  
  
|Friendly name in InnerElementType|Numeric value|  
|---------------------------------------|-------------------|  
|Navigator|0|  
|Node|1|  
|NodeText|2|  
  
##  <a name="Tasks"></a> Tasks  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes numerous tasks with properties that can be set by property expressions.  
  
### Analysis Services Execute DDL Task  
 **SourceType** property-Set by using values from the **DDLSourceType** enumeration.  
  
|Friendly name in DDLSourceType|Numeric value|  
|------------------------------------|-------------------|  
|DirectInput|0|  
|FileConnection|1|  
|Variable|2|  
  
### Bulk Insert Task  
 **DataFileType** property-Set by using values from the **DTSBulkInsert_DataFileType** enumeration.  
  
|Friendly name in DTSBulkInsert_DataFileType|Numeric value|  
|--------------------------------------------------|-------------------|  
|DTSBulkInsert_DataFileType_Char|0|  
|DTSBulkInsert_DataFileType_Native|1|  
|DTSBulkInsert_DataFileType_WideChar|2|  
|DTSBulkInsert_DataFileType_WideNative|3|  
  
### Execute SQL Task  
 **ResultSetType** property-Set by using values from the **ResultSetType** enumeration.  
  
|Friendly name in ResultSetType|Numeric Value|  
|------------------------------------|-------------------|  
|ResultSetType_None|1|  
|ResultSetType_SingleRow|2|  
|ResultSetType_Rowset|3|  
|ResultSetType_XML|4|  
  
 **SqlStatementSourceType** property-Set by using values from the **SqlStatementSourceType** enumeration.  
  
|Friendly name in SqlStatementSourceType|Numeric Value|  
|---------------------------------------------|-------------------|  
|DirectInput|1|  
|FileConnection|2|  
|Variable|3|  
  
### File System Task  
 **Operation** property-Set by using values from the **DTSFileSystemOperation** enumeration.  
  
|Friendly name in DTSFileSystemOperation|Numeric value|  
|---------------------------------------------|-------------------|  
|CopyFile|0|  
|MoveFile|1|  
|DeleteFile|2|  
|RenameFile|3|  
|SetAttributes|4|  
|CreateDirectory|5|  
|CopyDirectory|6|  
|MoveDirectory|7|  
|DeleteDirectory|8|  
|DeleteDirectoryContent|9|  
  
 **Attributes** property-Set by using values from the **DTSFileSystemAttributes** enumeration.  
  
|Friendly name in DTSFileSystemAttributes|Numeric value|  
|----------------------------------------------|-------------------|  
|Normal|0|  
|Archive|1|  
|Hidden|2|  
|ReadOnly|4|  
|System|8|  
  
### FTP Task  
 **Operation** property-Set by using values from the **DTSFTPOp** enumeration.  
  
|Friendly name in DTSFTPOp|Numeric value|  
|-------------------------------|-------------------|  
|Send|0|  
|Receive|1|  
|DeleteLocal|2|  
|DeleteRemote|3|  
|MakeDirLocal|4|  
|MakeDirRemote|5|  
|RemoveDirLocal|6|  
|RemoveDirRemote|7|  
  
### Message Queue Task  
 **MessageType** property-Set by using values from the **MQMessageType** enumeration.  
  
|Friendly name in MQMessageType|Numeric value|  
|------------------------------------|-------------------|  
|DTSMQMessageType_String|0|  
|DTSMQMessageType_DataFile|1|  
|DTSMQMessageType_Variables|2|  
|DTSMQMessagType_StringMessageToVariable|3|  
  
 **StringCompareType** property-Set by using values from the **MQStringMessageCompare** enumeration.  
  
|Friendly name in MQStringMessageCompare|Numeric value|  
|---------------------------------------------|-------------------|  
|DTSMQStringMessageCompare_None|0|  
|DTSMQStringMessageCompare_Exact|1|  
|DTSMQStringMessageCompare_IgnoreCase|2|  
|DTSMQStringMessageCompare_Contains|3|  
  
 **TaskType** property-Set by using values from the **MQType** enumeration.  
  
|Friendly name in MQType|Numeric value|  
|-----------------------------|-------------------|  
|DTSMQType_Sender|0|  
|DTSMQType_Receiver|1|  
  
### Send Mail Task  
 **MessageSourceType** property-Set by using values from the **SendMailMessageSourceType** enumeration.  
  
|Friendly Name in SendMailMessageSourceType|Numeric Value|  
|------------------------------------------------|-------------------|  
|DirectInput|0|  
|FileConnection|1|  
|Variable|2|  
  
 **Priority** property-Set by using values from the **MailPriority** enumeration.  
  
|Friendly Name in MailPriority|Numeric Value|  
|-----------------------------------|-------------------|  
|High|1|  
|Normal|3|  
|Low|5|  
  
### Transfer Database Task  
 **Action** property-Set by using values from the **TransferAction** enumeration.  
  
|Friendly name in TransferAction|Numeric value|  
|-------------------------------------|-------------------|  
|Copy|0|  
|Move|1|  
  
 **Method** property-Set by using values from the **TransferMethod** enumeration.  
  
|Friendly name in TransferMethod|Numeric value|  
|-------------------------------------|-------------------|  
|DatabaseOffline|0|  
|DatabaseOnline|1|  
  
### Transfer Error Messages Task  
 **IfObjectExists** property-Set by using values from the **IfObjectExists** enumeration.  
  
|Friendly Name in IfObjectExists|Numeric value|  
|-------------------------------------|-------------------|  
|FailTask|0|  
|Overwrite|1|  
|Skip|2|  
  
### Transfer Jobs Task  
 **IfObjectExists** property-Set by using values from the **IfObjectExists** enumeration.  
  
|Friendly Name in IfObjectExists|Numeric value|  
|-------------------------------------|-------------------|  
|FailTask|0|  
|Overwrite|1|  
|Skip|2|  
  
### Transfer Logins Task  
 **IfObjectExists** property-Set by using values from the **IfObjectExists** enumeration.  
  
|Friendly name in IfObjectExists|Numeric value|  
|-------------------------------------|-------------------|  
|FailTask|0|  
|Overwrite|1|  
|Skip|2|  
  
 **LoginsToTransfer** property-Set by using values from the **LoginsToTransfer** enumeration.  
  
|Friendly name in LoginsToTransfer|Numeric value|  
|---------------------------------------|-------------------|  
|AllLogins|0|  
|SelectedLogins|1|  
|AllLoginsFromSelectedDatabases|2|  
  
### Transfer Master Stored Procedures Task  
 **IfObjectExists** property-Set by using values from the **IfObjectExists** enumeration.  
  
|Friendly name in IfObjectExists|Numeric value|  
|-------------------------------------|-------------------|  
|FailTask|0|  
|Overwrite|1|  
|Skip|2|  
  
### Transfer SQL Server Objects Task  
 **ExistingData** property-Set by using values from the **ExistingData** enumeration.  
  
|Friendly name in ExistingData|Numeric Value|  
|-----------------------------------|-------------------|  
|Replace|0|  
|Append|1|  
  
### Web Service Task  
 **OutputType** property-Set by using values from the **DTSOutputType** enumeration.  
  
|Friendly name in DTSOutputType|Numeric value|  
|------------------------------------|-------------------|  
|File|0|  
|Variable|1|  
  
### WMI Data Reader Task  
 **OverwriteDestination** property-Set by using values from the **OverwriteDestination** enumeration.  
  
|Friendly name in OverwriteDestination|Numeric value|  
|-------------------------------------------|-------------------|  
|OverwriteDestination|0|  
|AppendToDestination|1|  
|KeepOriginal|2|  
  
 **OutputType** property-Set by using values from the **OutputType** enumeration.  
  
|Friendly name in OutputType|Numeric value|  
|---------------------------------|-------------------|  
|DataTable|0|  
|PropertyValue|1|  
|PropertyNameAndValue|2|  
  
 **DestinationType** property-Set by using values from the **DestinationType** enumeration.  
  
|Friendly name in DestinationType|Numeric value|  
|--------------------------------------|-------------------|  
|FileConnection|0|  
|Variable|1|  
  
 **WqlQuerySourceType** property-Set by using values from the **QuerySourceType** enumeration.  
  
|Friendly Name in QuerySourceType|Numeric Value|  
|--------------------------------------|-------------------|  
|FileConnection|0|  
|DirectInput|1|  
|Variable|2|  
  
 WMI Event Watcher **ActionAtEvent** property-Set by using values from the **ActionAtEvent** enumeration.  
  
|Friendly Name in ActionAtEvent|Numeric Value|  
|------------------------------------|-------------------|  
|LogTheEventAndFireDTSEvent|0|  
|LogTheEvent|1|  
  
 **ActionAtTimeout** property-Set by using values from the **ActionAtTimeout** enumeration.  
  
|Friendly name in ActionAtTimeout|Numeric value|  
|--------------------------------------|-------------------|  
|LogTimeoutAndFireDTSEvent|0|  
|LogTimeout|1|  
  
 **AfterEvent** property-Set by using values from the **AfterEvent** enumeration.  
  
|Friendly name in AfterEvent|Numeric value|  
|---------------------------------|-------------------|  
|ReturnWithSuccess|0|  
|ReturnWithFailure|1|  
|WatchfortheEventAgain|2|  
  
 **AfterTimeout** property-Set by using values from the **AfterTimeout** enumeration.  
  
|Friendly name in AfterTimeout|Numeric value|  
|-----------------------------------|-------------------|  
|ReturnWithSuccess|0|  
|ReturnWithFailure|1|  
|WatchfortheEventAgain|2|  
  
 **WqlQuerySourceType** property-Set by using values from the **QuerySourceType** enumeration.  
  
|Friendly name in QuerySourceType|Numeric value|  
|--------------------------------------|-------------------|  
|FileConnection|0|  
|DirectInput|1|  
|Variable|2|  
  
### XML Task  
 **OperationType** property-Set by using values from the **DTSXMLOperation** enumeration.  
  
|Friendly name in DTSXMLOperation|Numeric value|  
|--------------------------------------|-------------------|  
|Validate|0|  
|XSLT|1|  
|XPATH|2|  
|Merge|3|  
|Diff|4|  
|Patch|5|  
  
 **SourceType**, **SecondOperandType**, and **XPathSourceType** properties-Set by using values from the **DTSXMLSourceType** enumeration.  
  
|Friendly name in DTSXMLSourceType|Numeric value|  
|---------------------------------------|-------------------|  
|FileConnection|0|  
|Variable|1|  
|DirectInput|2|  
  
 **DestinationType** and **DiffGramDestinationType** properties-Set by using values from the **DTSXMLSaveResultTo** enumeration.  
  
|Friendly name in DTSXMLSaveResultTo|Numeric value|  
|-----------------------------------------|-------------------|  
|FileConnection|0|  
|Variable|1|  
  
 **ValidationType** property-Set by using values from the **DTSXMLValidationType** enumeration.  
  
|Friendly name in DTSXMLValidationType|Numeric value|  
|-------------------------------------------|-------------------|  
|DTD|0|  
|XSD|1|  
  
 **XPathOperation** property-Set by using values from the **DTSXMLXPathOperation** enumeration.  
  
|Friendly name in DTSXMLXPathOperation|Numeric Value|  
|-------------------------------------------|-------------------|  
|Evaluation|0|  
|Values|1|  
|NodeList|2|  
  
 **DiffOptions** property-Set by using values from the **DTSXMLDiffOptions** enumeration. The options in this enumerator are not mutually exclusive. To use multiple options, provide a comma-separated list of the options to apply.  
  
|Friendly name in DTSXMLDiffOptions|Numeric Value|  
|----------------------------------------|-------------------|  
|None|0|  
|IgnoreChildOrder|1|  
|IgnoreComments|2|  
|IgnorePI|4|  
|IgnoreWhitespace|8|  
|IgnoreNamespaces|16|  
|IgnorePrefixes|32|  
|IgnoreXmlDecl|64|  
|IgnoreDtd|128|  
  
 **DiffAlgorithm** property-Set by using values from the **DTSXMLDiffAlgorithm** enumeration.  
  
|Friendly name in DTSXMLDiffAlgorithm|Numeric value|  
|------------------------------------------|-------------------|  
|Auto|0|  
|Fast|1|  
|Precise|2|  
  
##  <a name="MaintenancePlanTasks"></a> Maintenance Plan Tasks  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a set of tasks that perform SQL Server tasks for use in maintenance plans and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support working with these tasks programmatically and programming reference documentation does not include API documentation of these tasks and their enumerators.  
  
### All Maintenance Tasks  
 All maintenance tasks use the following enumerations to set the specified properties.  
  
 **DatabaseSelectionType** property-Set by using values from the **DatabaseSelection** enumeration.  
  
|Friendly name in DatabaseSelection|Numeric value|  
|----------------------------------------|-------------------|  
|None|0|  
|All|1|  
|System|2|  
|User|3|  
|Specific|4|  
  
 **TableSelectionType** property-Set by using values from the **TableSelection** enumeration.  
  
|Friendly name in TableSelection|Numeric value|  
|-------------------------------------|-------------------|  
|None|0|  
|All|1|  
|Specific|2|  
  
 **ObjectTypeSelection** property-Set by using values from the **ObjectType** enumeration.  
  
|Friendly name in ObjectType|Numeric value|  
|---------------------------------|-------------------|  
|Table|0|  
|View|1|  
|TableView|2|  
  
### Back Up Database Task  
 **DestinationCreationType** property-Set by using values from the **DestinationType** enumeration.  
  
|Friendly name in DestinationType|Numeric value|  
|--------------------------------------|-------------------|  
|Auto|0|  
|Manual|1|  
  
 **ExistingBackupsAction** property-Set by using values from the **ActionForExistingBackups** enumeration.  
  
|Friendly name in ActionForExistingBackups|Numeric value|  
|-----------------------------------------------|-------------------|  
|Append|0|  
|Overwrite|1|  
  
 **BackupAction** property-Set by using values from the **BackupTaskType** enumeration. This property works with the **BackupIsIncremental** property to define the type of backup that the task performs.  
  
|Friendly name in BackupTaskType|Numeric value|  
|-------------------------------------|-------------------|  
|Database|0|  
|Files|1|  
|Log|2|  
  
 **BackupDevice** property-Set by using values from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) **DeviceType** enumeration.  
  
|Friendly name in DeviceType|Numeric value|  
|---------------------------------|-------------------|  
|LogicalDevice|0|  
|Tape|1|  
|File|2|  
|Pipe|3|  
|VirtualDevice|4|  
  
### Maintenance Cleanup Task  
 **FileTypeSelected** property-Set by using values from the **FileType** enumeration.  
  
|Friendly name in FileType|Numeric value|  
|-------------------------------|-------------------|  
|FileBackup|0|  
|FileReport|1|  
  
 **OlderThanTimeUnitType** property-Set by using values from the **TimeUnitType** enumeration.  
  
|Friendly Name in TimeUnitType|Numeric Value|  
|-----------------------------------|-------------------|  
|Day|0|  
|Week|1|  
|Month|2|  
|Year|3|  
  
### Update Statistics Task  
 **UpdateType** property-Set by using values from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) **StatisticsTarget** enumeration.  
  
|Friendly name in StatisticsTarget|Numeric value|  
|---------------------------------------|-------------------|  
|Column|1|  
|Index|2|  
|All|3|  
  
##  <a name="CommonProperties"></a> Common Properties  
 Packages, tasks, and the Foreach Loop, For Loop, and Sequence containers can use the following enumerations to set the specified properties.  
  
 **ForceExecutionResult** property-Set by using values from the **DTSForcedExecResult** enumeration.  
  
|Friendly name in DTSForcedExecResult|Numeric value|  
|------------------------------------------|-------------------|  
|None|-1|  
|Success|0|  
|Failure|1|  
|Completion|2|  
  
 **IsolationLevel** property-Set by the .NET Framework **IsolationLevel** enumeration. For more information, see the .NET Framework Class Library in the [MSDN Library](https://go.microsoft.com/fwlink?LinkId=17313).  
  
 **LoggingMode** property-Set by using values from the **DTSLoggingMode** enumeration.  
  
|Friendly name in DTSLoggingMode|Numeric value|  
|-------------------------------------|-------------------|  
|UseParentSetting|0|  
|Enabled|1|  
|Disabled|2|  
  
 **TransactionOption** property-Set by using values from the **DTSTransactionOption** enumeration.  
  
|Friendly name in DTSTransactionOption|Numeric value|  
|-------------------------------------------|-------------------|  
|NotSupported|0|  
|Supported|1|  
|Required|2|  
  
## Related Tasks  
 [Add or Change a Property Expression](../../integration-services/expressions/add-or-change-a-property-expression.md)  
  
## See Also  
 [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md)   
 [Integration Services &#40;SSIS&#41; Packages](../../integration-services/integration-services-ssis-packages.md)   
 [Integration Services Containers](../../integration-services/control-flow/integration-services-containers.md)   
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Precedence Constraints](../../integration-services/control-flow/precedence-constraints.md)  
  
  
