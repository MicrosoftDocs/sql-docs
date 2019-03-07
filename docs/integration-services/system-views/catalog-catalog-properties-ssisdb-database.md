---
title: "catalog.catalog_properties (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/11/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: e604a382-95c8-4764-b268-742eb5c6d4cf
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.catalog_properties (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the properties of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|property_name|**nvarchar(256)**|The name of the catalog property.|  
|property_value|**nvarchar(256)**|The value of the catalog property.|  
  
## Remarks  
 This view displays a row for each catalog property.
  
|Property Name|Description|  
|-------------------|-----------------|  
|**DEFAULT_EXECUTION_MODE**|The server-wide default execution mode for packages - `Server` (0) or `Scale Out` (1). |
|**ENCRYPTION_ALGORITHM**|The type of encryption algorithm that is used to encrypt sensitive data. The supported values include: `DES`, `TRIPLE_DES`, `TRIPLE_DES_3KEY`, `DESX`, `AES_128`, `AES_192`, and `AES_256`. Note: The catalog database must be in single-user mode in order to change this property.|
|**IS_SCALEOUT_ENABLED**|When the value is `True`, the SSIS Scale Out feature is enabled. If you have not enabled Scale Out, this property may not appear in the view.|
|**MAX_PROJECT_VERSIONS**|The number of new project versions that are retained for a single project. When version cleanup is enabled, older versions beyond this count are deleted.|  
|**OPERATION_CLEANUP_ENABLED**|When the value is `TRUE`, operation details and operation messages older than **RETENTION_WINDOW** (days) are deleted from the catalog. When the value is `FALSE`, all operation details and operation messages are stored in the catalog. Note: a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] job performs the operation cleanup.|  
|**RETENTION_WINDOW**|The number of days that operation details and operation messages are stored in the catalog. When the value is `-1`, the retention window is infinite. Note: If no cleanup is desired, set **OPERATION_CLEANUP_ENABLED** to **FALSE**.|
|**SCHEMA_BUILD**|The build number of the SSISDB catalog database schema. This number changes whenever the SSISDB catalog is created or upgraded.|
|**SCHEMA_VERSION**|The major version number of the SSISDB catalog database schema. This number changes whenever the SSISDB catalog is created or the major version is upgraded.|
|**VALIDATION_TIMEOUT**|Validations are stopped if they do not complete in the number of seconds specified by this property.|  
|**SERVER_CUSTOMIZED_LOGGING_LEVEL**|The default customized logging level for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. If you have not created any customized logging levels, this property may not appear in the view.|
|**SERVER_LOGGING_LEVEL**|The default logging level for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.|
|**SERVER_OPERATION_ENCRYPTION_LEVEL**|When the value is 1 (`PER_EXECUTION`), the certificate and symmetric key used for protecting sensitive execution parameters and execution logs are created for each *execution*. When the value is 2 (`PER_PROJECT`), the certificate and symmetric key are created one time for each *project*. For more info about this property, see the Remarks for the SSIS stored procedure [catalog.cleanup_server_log](../system-stored-procedures/catalog-cleanup-server-log.md#remarks).|
|**VERSION_CLEANUP_ENABLED**|When the value is `TRUE`, only the **MAX_PROJECT_VERSIONS** number of project versions are stored in the catalog and all other project versions are deleted. When the value is **FALSE**, all project versions are stored in the catalog. Note: a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] job performs the operation cleanup.|
|||
  
## Permissions  
 This view requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
  
