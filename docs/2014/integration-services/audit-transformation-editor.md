---
title: "Audit Transformation Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.audittransformation.f1"
helpviewer_keywords: 
  - "Audit Transformation Editor"
ms.assetid: 32786a34-5870-4fde-83c7-ec74d62404b8
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Audit Transformation Editor
  The Audit transformation enables the data flow in a package to include data about the environment in which the package runs. For example, the name of the package, computer, and operator can be added to the data flow. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes system variables that provide this information.  
  
 To learn more about the Audit transformation, see [Audit Transformation](data-flow/transformations/audit-transformation.md).  
  
## Options  
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
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)  
  
  
