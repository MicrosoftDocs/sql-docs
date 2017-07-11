---
title: "catalog.catalog_properties (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
ms.assetid: e604a382-95c8-4764-b268-742eb5c6d4cf
caps.latest.revision: 10
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# catalog.catalog_properties (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the properties of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|property_name|**nvarchar(256)**|The name of the catalog property.|  
|property_value|**nvarchar(256)**|The value of the catalog property.|  
  
## Remarks  
 This view displays a row for each catalog property. The properties displayed by this view include the following:  
  
|Property Name|Description|  
|-------------------|-----------------|  
|**ENCRYPTION_ALGORITHM**|The type of encryption algorithm that is used to encrypt sensitive data. The supported values include: `DES`, `TRIPLE_DES`, `TRIPLE_DES_3KEY`, `DESX`, `AES_128`, `AES_192`, and `AES_256`. Note: The catalog database must be in single-user mode in order to change this property.|  
|**MAX_PROJECT_VERSIONS**|The number of new project versions that will be retained for a single project. When version cleanup is enabled, older versions beyond this count will be deleted.|  
|**OPERATION_CLEANUP_ENABLED**|When the value is `TRUE`, operation details and operation messages older than **RETENTION_WINDOW** (days) are deleted from the catalog. When the value is `FALSE`, all operation details and operation messages are stored in the catalog. Note: a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] job performs the operation cleanup.|  
|**RETENTION_WINDOW**|The number of days that operation details and operation messages are stored in the catalog. When the value is `-1`, the retention window is infinite. Note: If no cleanup is desired, set **OPERATION_CLEANUP_ENABLED** to **FALSE**.|  
|**VALIDATION_TIMEOUT**|Validations will be stopped if they do not complete in the number of seconds specified by this property.|  
|**VERSION_CLEANUP_ENABLED**|When the value is `TRUE`, only the **MAX_PROJECT_VERSIONS** number of project versions are stored in the catalog and all other project versions are deleted. When the value is **FALSE**, all project versions are stored in the catalog. Note: a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] job performs the operation cleanup.|  
|**SERVER_LOGGING_LEVEL**|The default logging level for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.|  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
  