---
title: "DROP CONTRACT (Transact-SQL)"
description: DROP CONTRACT (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_CONTRACT_TSQL"
  - "DROP CONTRACT"
helpviewer_keywords:
  - "dropping contracts"
  - "removing contracts"
  - "deleting contracts"
  - "contracts [Service Broker], dropping"
  - "DROP CONTRACT statement"
dev_langs:
  - "TSQL"
---
# DROP CONTRACT (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops an existing contract from a database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP CONTRACT contract_name   
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *contract_name*  
 The name of the contract to drop. Server, database, and schema names cannot be specified.  
  
## Remarks  
 You cannot drop a contract if any services or conversation priorities refer to the contract.  
  
 When you drop a contract, [!INCLUDE[ssSB](../../includes/sssb-md.md)] ends any existing conversations that use the contract with an error.  
  
## Permissions  
 Permission for dropping a contract defaults to the owner of the contract, members of the db_ddladmin or db_owner fixed database roles, and members of the sysadmin fixed server role.  
  
## Examples  
 The following example removes the contract `//Adventure-Works.com/Expenses/ExpenseSubmission` from the database.  
  
```sql  
DROP CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission] ;  
```  
  
## See Also  
 [ALTER BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-broker-priority-transact-sql.md)   
 [ALTER SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-service-transact-sql.md)   
 [CREATE CONTRACT &#40;Transact-SQL&#41;](../../t-sql/statements/create-contract-transact-sql.md)   
 [DROP BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-broker-priority-transact-sql.md)   
 [DROP SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-service-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
