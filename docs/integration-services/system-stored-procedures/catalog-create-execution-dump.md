---
title: "catalog.create_execution_dump"
description: "catalog.create_execution_dump"
author: chugugrace
ms.author: chugu
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: "language-reference"
---
# catalog.create_execution_dump 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Causes a running package to pause and create a dump file. The file is stored in the *\<drive>*:\Program Files\Microsoft SQL Server\130\Shared\ErrorDumps folder.  
  
## Syntax  
  
```sql  
catalog.create_execution_dump [ @execution_id = ] execution_id  
  
```  
  
## Arguments  
 [ @execution_id = ] *execution_id*  
 The execution ID for the running package. The *execution_id* is **bigint**.  
  
## Example  
 In the following example, the running package with an execution ID of 88 is prompted to create a dump file.  
  
```sql
EXEC create_execution_dump @execution_id = 88  
```  
  
## Return Codes  
 0 (success)  
  
 When the stored procedure fails, it throws an error.  
  
## Result Set  
 None  
  
## Permissions  
 This stored procedure requires users to be members of the **ssis_admin** database role.  
  
## Errors and Warnings  
 The following list describes conditions that cause the stored procedure to fail.  
  
-   An invalid execution ID is specified.  
  
-   The package has already completed.  
  
-   The package is currently creating a dump file.  
  
## See Also  
 [Generating Dump Files for Package Execution](../../integration-services/troubleshooting/generating-dump-files-for-package-execution.md)  
  
  
