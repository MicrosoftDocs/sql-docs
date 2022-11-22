---
description: "catalog.check_schema_version"
title: "catalog.check_schema_version | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: e0d5e9f5-59c6-4118-87b5-4aa5c37a7df6
author: chugugrace
ms.author: chugu
---
# catalog.check_schema_version 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Determines whether the SSISDB catalog schema and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] binaries (ISServerExec and SQLCLR assembly) are compatible.  
  
 The ISServerExec.exc logs an error message when the schema and the binaries are incompatible.  
  
 The SSISDB schema version is incremented when the schema changes during the application of patches and during upgrades. It is recommended that you run this stored procedure after an SSISDB backup has been restored to ensure that the schema and binaries are compatible.  
  
## Syntax  
  
```sql  
catalog.check_schema_version [ @use32bitruntime = ] use32bitruntime  
```  
  
## Arguments  
 [ @use32bitruntime= ] *use32bitruntime*  
 When the parameter is set to **1**, the 32-bit version of dtexec is called. The *use32bitruntime* is an **int**.  
  
 
## Return Code Value 
Returns 0 for success. 

## Result Set  

Returns a table that has the following format:

| Column name | Data type | Description |
|---|---|---|
| SERVER_BUILD | **decimal** | SQL Server version. For example, a server running SQL Server 2014 is `14.0.3335.7`. |
| SCHEMA_VERSION | **tinyint** | SQL Server version number. For example, SQL Server 2017 and 2019 are `6` and `7` respectively.|
| SCHEMA_BUILD | **string** | Schema build. |
| ASSEMBLY_BUILD | **string** | Assembly build. |
| SHARED_COMPONENT_VERSION | **string** | Shared component version. | 

## Permissions  
 This stored procedure requires the following permission:  
  
-   Membership to the **ssis_admin** database role.  
  
  
