---
title: "DROP ASSEMBLY (Transact-SQL)"
description: DROP ASSEMBLY (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP ASSEMBLY"
  - "DROP_ASSEMBLY_TSQL"
helpviewer_keywords:
  - "removing assemblies"
  - "DROP ASSEMBLY statement"
  - "deleting assemblies"
  - "assemblies [CLR integration], removing"
  - "dropping assemblies"
  - "WITH NO DEPENDENTS option"
dev_langs:
  - "TSQL"
---
# DROP ASSEMBLY (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes an assembly and all its associated files from the current database. Assemblies are created by using [CREATE ASSEMBLY](../../t-sql/statements/create-assembly-transact-sql.md) and modified by using [ALTER ASSEMBLY](../../t-sql/statements/alter-assembly-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP ASSEMBLY [ IF EXISTS ] assembly_name [ ,...n ]  
[ WITH NO DEPENDENTS ]  
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the assembly only if it already exists.  
  
 *assembly_name*  
 Is the name of the assembly you want to drop.  
  
 WITH NO DEPENDENTS  
 If specified, drops only *assembly_name* and none of the dependent assemblies that are referenced by the assembly. If not specified, DROP ASSEMBLY drops *assembly_name* and all dependent assemblies.  
  
## Remarks  
 Dropping an assembly removes an assembly and all its associated files, such as source code and debug files, from the database.  
  
 If WITH NO DEPENDENTS is not specified, DROP ASSEMBLY drops *assembly_name* and all dependent assemblies. If an attempt to drop any dependent assemblies fails, DROP ASSEMBLY returns an error.  
  
 DROP ASSEMBLY returns an error if the assembly is referenced by another assembly that exists in the database or if it is used by common language runtime (CLR) functions, procedures, triggers, user-defined types or aggregates in the current database.  
  
 DROP ASSEMBLY does not interfere with any code referencing the assembly that is currently running. However, after DROP ASSEMBLY executes, any attempts to invoke the assembly code will fail.  
  
## Permissions  
 Requires ownership of the assembly, or CONTROL permission on it.  
  
## Examples  
 The following example assumes the assembly `HelloWorld` is already created in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```sql  
DROP ASSEMBLY Helloworld ;  
```  
  
## See Also  
 [CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md)   
 [ALTER ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-assembly-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Getting Information About Assemblies](../../relational-databases/clr-integration/assemblies-getting-information.md)  
