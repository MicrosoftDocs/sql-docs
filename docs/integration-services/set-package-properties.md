---
description: "Set Package Properties"
title: "Set Package Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, properties"
  - "properties [Integration Services]"
  - "checkpoints [Integration Services]"
  - "execution properties [Integration Services]"
  - "packages [Integration Services], properties"
  - "identification properties [Integration Services]"
  - "passwords [Integration Services]"
  - "SSIS packages, properties"
  - "transaction properties [Integration Services]"
  - "updating package properties"
  - "forced execution value properties [Integration Services]"
  - "security properties [Integration Services]"
  - "version properties [Integration Services]"
  - "SQL Server Integration Services packages, properties"
ms.assetid: 13f81c3e-2b18-4f83-b445-a2f4a2c560aa
author: chugugrace
ms.author: chugu
---
# Set Package Properties

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


  When you create a package in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] by using the graphical interface that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides, you set the properties of the package object in the Properties window.  
  
 The **Properties** window provides a categorized and alphabetical list of properties. To arrange the **Properties** window by category, click the Categorized icon.  
  
 When arranged by category, the **Properties** window groups properties in the following categories:  
  
-   [Checkpoints](#Checkpoints)  
  
-   [Execution](#Execution)  
  
-   [Forced Execution Value](#ForcedExecutionValue)  
  
-   [Identification](#Identification)  
  
-   [Misc](#Misc)  
  
-   [Security](#Security)  
  
-   [Transactions](#Transactions)  
  
-   [Version](#Version)  
  
 For information about additional package properties that you cannot set in the **Properties** window, see <xref:Microsoft.SqlServer.Dts.Runtime.Package>.  
  
### To set package properties in the Properties window  
  
-   [Set the Properties of a Package]()  
  
## Properties by Category  
 The following tables list the package properties by category.  
  
###  <a name="Checkpoints"></a> Checkpoints  
 You can use the properties in this category to restart the package from a point of failure in the package control flow, instead of rerunning the package from the beginning of its control flow. For more information, see [Restart Packages by Using Checkpoints](../integration-services/packages/restart-packages-by-using-checkpoints.md).  
  
|Property|Description|  
|--------------|-----------------|  
|**CheckpointFileName**|The name of the file that captures the checkpoint information that enables a package to restart. When the package finishes successfully, this file is deleted.|  
|**CheckpointUsage**|Specifies when a package can be restarted. The values are **Never**, **IfExists**, and **Always**. The default value of this property is **Never**, which indicates that the package cannot be restarted. For more information, see <xref:Microsoft.SqlServer.Dts.Runtime.DTSCheckpointUsage>.|  
|**SaveCheckpoints**|Specifies whether the checkpoints are written to the checkpoint file when the package runs. The default value of this property is **False**.|  
  
> [!NOTE]  
>  The **/CheckPointing on** option of dtexec is equivalent to setting the **SaveCheckpoints** property of the package to True, and the **CheckpointUsage** property to Always. For more information, see [dtexec Utility](../integration-services/packages/dtexec-utility.md).  
  
###  <a name="Execution"></a> Execution  
 The properties in this category configure the run-time behavior of the package object.  
  
|Property|Description|  
|--------------|-----------------|  
|**DelayValidation**|Indicates whether package validation is delayed until the package runs. The default value for this property is **False**.|  
|**Disable**|Indicates whether the package is disabled. The default value of this property is **False**.|  
|**DisableEventHandlers**|Specifies whether the package event handlers run. The default value of this property is **False**.|  
|**FailPackageOnFailure**|Specifies whether the package fails if an error occurs in a package component. The only valid value of this property is **False**.|  
|**FailParentOnError**|Specifies whether the parent container fails if an error occurs in a child container. The default value is of this property is **False**.|  
|**MaxConcurrentExecutables**|The number of executable files that the package can run concurrently. The default value of this property is **-1**, which indicates that there is no limit.|  
|**MaximumErrorCount**|The maximum number of errors that can occur before a package stops running. The default value of this property is **1**.|  
|**PackagePriorityClass**|The Win32 thread priority class of the package thread. The values are **Default**, **AboveNormal**, **Normal**, **BelowNormal**, **Idle**. The default value of this property is **Default**. For more information, see <xref:Microsoft.SqlServer.Dts.Runtime.DTSPriorityClass>.|  
  
###  <a name="ForcedExecutionValue"></a> Forced Execution Value  
 The properties in this category configure an optional execution value for the package.  
  
|Property|Description|  
|--------------|-----------------|  
|**ForcedExecutionValue**|If ForceExecutionValue is set to **True**, a value that specifies the optional execution value that the package returns. The default value of this property is **0**.|  
|**ForcedExecutionValueType**|The data type of ForcedExecutionValue. The default value of this property is **Int32**.|  
|**ForceExecutionValue**|A Boolean value that specifies whether the optional execution value of the container should be forced to contain a particular value. The default value of this property is **False**.|  
  
###  <a name="Identification"></a> Identification  
 The properties in this category provide information such as the unique identifier and name of the package.  
  
|Property|Description|  
|--------------|-----------------|  
|**CreationDate**|The date that the package was created.|  
|**CreatorComputerName**|The name of the computer on which the package was created.|  
|**CreatorName**|The name of the person who created the package.|  
|**Description**|A description of package functionality.|  
|**ID**|The package GUID, which is assigned when the package is created. This property is read-only. To generate a new random value for the **ID** property, select **\<Generate New ID\>** in the drop-down list.|  
|**Name**|The name of the package.|  
|**PackageType**|The package type. The values are **Default**, **DTSDesigner**, **DTSDesigner100**, **DTSWizard**, **SQLDBMaint**, and **SQLReplication**. The default value of this property is **Default**. For more information, see <xref:Microsoft.SqlServer.Dts.Runtime.DTSPackageType>.|  
  
###  <a name="Misc"></a> Misc  
 The properties in this category are used to access the configurations and expressions that a package uses and to provide information about the locale and logging mode of the package. For more information, see [Use Property Expressions in Packages](../integration-services/expressions/use-property-expressions-in-packages.md).  
  
|Property|Description|  
|--------------|-----------------|  
|**Configurations**|The collection of configurations that the package uses. Click the browse button **(...)** to view and configure package configurations.|  
|**Expressions**|Click the browse button **(...)** to create expressions for package properties.<br /><br /> Note that you can create property expressions for all the package properties that object model includes, not just the properties listed in the Properties window.<br /><br /> For more information, see [Use Property Expressions in Packages](../integration-services/expressions/use-property-expressions-in-packages.md).<br /><br /> To view existing property expressions, expand **Expressions**. Click the browse button **(...)** in an expression text box to modify and evaluate an expression.|  
|**ForceExecutionResult**|The execution result of the package. The values are **None**, **Success**, **Failure**, and **Completion**. The default value of this property is **None**. For more information, see T:Microsoft.SqlServer.Dts.Runtime.DTSForcedExecResult.|  
|**LocaleId**|A Microsoft Win32 locale. The default value of this property is the locale of the operating system on the local computer.|  
|**LoggingMode**|A value that specifies the logging behavior of the package. The values are **Disabled**, **Enabled**, and **UseParentSetting**. The default value of this property is **UseParentSetting**. For more information, see <xref:Microsoft.SqlServer.Dts.Runtime.DTSLoggingMode>.|  
|**OfflineMode**|Indicates whether the package is in offline mode. This property is read-only. The property is set at the project level. Normally, [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer tries to connect to each data source used by your package to validate the metadata associated with sources and destinations. You can enable **Work Offline** from the **SSIS** menu, even before you open a package, to prevent these connection attempts and the resulting validation errors when the data sources are not available. You can also enable **Work Offline** to speed up operations in the designer, and disable it only when you want your package to be validated.|  
|**SuppressConfigurationWarnings**|Indicates whether the warnings generated by configurations are suppressed. The default value of this property is **False**.|  
|**UpdateObjects**|Indicates whether the package is updated to use newer versions of the objects it contains, if newer versions are available. For example, if this property is set to **True**, a package that includes a Bulk Insert task is updated to use the newer version of the Bulk Insert task that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides. The default value of this property is **False**.|  
  
###  <a name="Security"></a> Security  
 The properties in this category are used to set the protection level of the package. For more information, see [Access Control for Sensitive Data in Packages](../integration-services/security/access-control-for-sensitive-data-in-packages.md).  
  
|Property|Description|  
|--------------|-----------------|  
|**PackagePassword**|The password for package protection levels (**EncryptSensitiveWithPassword** and **EncryptAllWithPassword**) that require passwords.|  
|**ProtectionLevel**|The protection level of the package. The values are **DontSaveSensitive**, **EncryptSensitiveWithUserKey**, **EncryptSensitiveWithPassword**, **EncryptAllWithPassword**, and **ServerStorage**. The default value of this property is **EncryptSensitiveWithUserKey**. For more information, see <xref:Microsoft.SqlServer.Dts.Runtime.DTSProtectionLevel>.|  
  
###  <a name="Transactions"></a> Transactions  
 The properties in this category configure the isolation level and the transaction option of the package. For more information, see [Integration Services Transactions](../integration-services/integration-services-transactions.md).  
  
|Property|Description|  
|--------------|-----------------|  
|**IsolationLevel**|The isolation level of the package transaction. The values are **Unspecified**, **Chaos**, **ReadUncommitted**, **ReadCommitted**, **RepeatableRead**, **Serializable**, and **Snapshot**. The default value of this property is **Serializable**.<br /><br /> Note: The **Snapshot** value of the **IsolationLevel** property is incompatible with package transactions. Therefore, you cannot use the **IsolationLevel** property to set the isolation level of package transactions to **Shapshot**. Instead, use an SQL query to set package transactions to **Snapshot**. For more information, see [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../t-sql/statements/set-transaction-isolation-level-transact-sql.md).<br /><br /> The system applies the **IsolationLevel** property to package transactions only when the value of the **TransactionOption** property is **Required**.<br /><br /> The value of the **IsolationLevel** property requested by a child container is ignored when the following conditions are true:<br />The value of the child container's **TransactionOption** property is **Supported**.<br />The child container joins the transaction of a parent container.<br /><br /> The value of the **IsolationLevel** property requested by the container is respected only when the container initiates a new transaction. A container initiates a new transaction when the following conditions are true:<br />The value of the container's **TransactionOption** property is **Required**.<br />The parent has not already started a transaction.<br /><br /> <br /><br /> For more information, see <xref:Microsoft.SqlServer.Dts.Runtime.DtsContainer.IsolationLevel%2A>.|  
|**TransactionOption**|The transactional participation of the package. The values are **NotSupported**, **Supported**, **Required**. The default value of this property is **Supported**. For more information, see <xref:Microsoft.SqlServer.Dts.Runtime.DTSTransactionOption>.|  
  
###  <a name="Version"></a> Version  
 The properties in this category provide information about the version of the package object.  
  
|Property|Description|  
|--------------|-----------------|  
|**VersionBuild**|The version number of the build of the package.|  
|**VersionComments**|Comments about the version of the package.|  
|**VersionGUID**|The GUID of the version of the package. This property is read-only.|  
|**VersionMajor**|The latest major version of the package.|  
|**VersionMinor**|The latest minor version of the package.|  

## Set package properties in the Properties window 
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want to configure.  
  
2.  In **Solution Explorer**, double-click the package to open it in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, or right-click and select **View Designer**.  
  
3.  Click the **Control Flow** tab and then do one of the following:  
  
    -   Right-click anywhere in the background of the control flow design surface, and then click **Properties**.  
  
    -   On the **View** menu, click **Properties Window**.  
  
4.  Edit the package properties in the **Properties** window.  
  
5.  On the **File** menu, click **Save Selected Items** to save the updated package.  
