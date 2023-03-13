---
title: "catalog.delete_environment_reference (SSISDB Database)"
description: "catalog.delete_environment_reference (SSISDB Database)"
author: chugugrace
ms.author: chugu
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: "language-reference"
---
# catalog.delete_environment_reference (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Deletes an environment reference from a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.delete_environment_reference [ @reference_id = ] reference_id  
```  
  
## Arguments  
 [ @reference_id = ] *reference_id*  
 The unique identifier of the environment reference. The *reference_id* is **bigint**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   MODIFY permission on the project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The environment reference identifier is not valid  
  
-   The user does not have the appropriate permissions  
  
  
