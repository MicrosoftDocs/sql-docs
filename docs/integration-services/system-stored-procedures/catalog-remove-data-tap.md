---
description: "catalog.remove_data_tap"
title: "catalog.remove_data_tap | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: b77db3e6-478c-441a-a838-82c4de750275
author: chugugrace
ms.author: chugu
---
# catalog.remove_data_tap 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes a data tap from a component output that is in an execution. The unique identifier for the data tap is associated with an instance of the execution.  
  
## Syntax  
  
```sql  
catalog.remove_data_tap [ @data_tap_id = ] data_tap_id  
```  
  
## Arguments  
 [ @data_tap_id = ] *data_tap_id*  
 The unique identifier for the data tap that is created by using the catalog.add_data_tap stored procedure. The *data_tap_id* is **bigint**.  
  
## Remarks  

- When a package contains more than one data flow tasks that have the same name, the data tap is added to the first data flow task with the given name.  
  
- To remove data taps, the instance of the execution must be in the created state (a value of 1 in the **status** column of the [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md)view) .  
  
## Return Codes  
 0 (success)  
  
 When the stored procedure fails, it throws an error.  
  
## Result Set  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   MODIFY permissions on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes conditions that cause the stored procedure to fail.  
  
-   The user does not have MODIFY permissions.  
  
## See Also  
 [catalog.add_data_tap](../../integration-services/system-stored-procedures/catalog-add-data-tap.md)   
 [catalog.add_data_tap_by_guid](../../integration-services/system-stored-procedures/catalog-add-data-tap-by-guid.md)  
  
  
