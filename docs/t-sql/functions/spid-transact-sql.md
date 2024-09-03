---
title: "@@SPID (Transact-SQL)"
description: "@@SPID (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@SPID"
  - "@@SPID_TSQL"
helpviewer_keywords:
  - "@@SPID function"
  - "session_id"
  - "server process IDs [SQL Server]"
  - "IDs [SQL Server], user processes"
  - "SPID"
  - "session IDs [SQL Server]"
  - "process ID of current user process"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# &#x40;&#x40;SPID (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the session ID of the current user process.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@SPID  
```  
  
## Return Types
 **smallint**  
  
## Remarks  
 @@SPID can be used to identify the current user process in the output of **sp_who**.  
  
## Examples  
 This example returns the session ID, login name, and user name for the current user process.  
  
```sql  
SELECT @@SPID AS 'ID', SYSTEM_USER AS 'Login Name', USER AS 'User Name';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
ID     Login Name                     User Name                       
------ ------------------------------ ------------------------------  
54     SEATTLE\joanna                 dbo                             
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 This example returns the [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] session ID, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Control node session ID, login name, and user name for the current user process.  
  
```sql  
SELECT SESSION_ID() AS ID, @@SPID AS 'Control ID', SYSTEM_USER AS 'Login Name', USER AS 'User Name';  
```  
  
## See Also  
 [Configuration Functions](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [sp_lock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lock-transact-sql.md)   
 [sp_who](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)  
  
  

