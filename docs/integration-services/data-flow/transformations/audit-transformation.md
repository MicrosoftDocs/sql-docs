---
description: "Audit Transformation"
title: "Audit Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.audittrans.f1"
  - "sql13.dts.designer.audittransformation.f1"
helpviewer_keywords: 
  - "environment data in packages [Integration Services]"
  - "Audit transformation"
ms.assetid: 8c143682-9c81-4150-83d6-1d9678151d37
author: chugugrace
ms.author: chugu
---
# Audit Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  The Audit transformation enables the data flow in a package to include data about the environment in which the package runs. For example, the name of the package, computer, and operator can be added to the data flow. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes system variables that provide this information.  
  
## System Variables  
 The following table describes the system variables that the Audit transformation can use.  
  
|System variable|Index|Description|  
|---------------------|-----------|-----------------|  
|**ExecutionInstanceGUID**|0|The GUID that identifies the execution instance of the package.|  
|**PackageID**|1|The unique identifier of the package.|  
|**PackageName**|2|The package name.|  
|**VersionID**|3|The version of the package.|  
|**ExecutionStartTime**|4|The time the package started to run.|  
|**MachineName**|5|The computer name.|  
|**UserName**|6|The login name of the person who started the package.|  
|**TaskName**|7|The name of the Data Flow task with which the Audit transformation is associated.|  
|**TaskId**|8|The unique identifier of the Data Flow task.|  
  
## Configuration of the Audit Transformation  
 You configure the Audit transformation by providing the name of a new output column to add to the transformation output, and then mapping the system variable to the output column. You can map a single system variable to multiple columns.  
  
 This transformation has one input and one output. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Audit Transformation Editor
  The Audit transformation enables the data flow in a package to include data about the environment in which the package runs. For example, the name of the package, computer, and operator can be added to the data flow. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes system variables that provide this information.  
  
### Options  
 **Output column name**  
 Provide the name for a new output column that will contain the audit information.  
  
 **Audit type**  
 Select an available system variable to supply the audit information.  
  
|Value|Description|  
|-----------|-----------------|  
|**Execution instance GUID**|Insert the GUID that uniquely identifies the execution instance of the package.|  
|**Package ID**|Insert the GUID that uniquely identifies the package.|  
|**Package name**|Insert the package name.|  
|**Version ID**|Insert the GUID that uniquely identifies the version of the package.|  
|**Execution start time**|Insert the time at which package execution started.|  
|**Machine name**|Insert the name of the computer on which the package was launched.|  
|**User name**|Insert the login name of the user who launched the package.|  
|**Task name**|Insert the name of the Data Flow task with which the Audit transformation is associated.|  
|**Task ID**|Insert the GUID that uniquely identifies the Data Flow task with which the Audit transformation is associated.|  
  
