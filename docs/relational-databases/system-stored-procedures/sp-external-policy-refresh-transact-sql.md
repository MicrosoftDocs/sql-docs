---
title: "sp_external_policy_refresh (Transact-SQL)"
description: Reference documentation to explain sp_external_policy_refresh (Transact-SQL) system stored procedure.
author: srdanbozovic
ms.author: srbozovi
ms.date: "11/07/2022"
ms.prod: sql
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_external_policy_refresh_TSQL"
  - "sp_external_policy_refresh"
helpviewer_keywords:
  - "sp_external_policy_refresh system stored procedure"
dev_langs:
  - "TSQL"
---

# sp_external_policy_refresh  (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Force immediate download of latest published policies.

## Syntax  
  
```  
  
sp_external_policy_refresh  [ @type = ] 'type'   
```  
  
## Arguments  
#### [ @type = ] '*type*'
 Type can be: *reload* (complete policy download) or *update* (incremental policy download). Default type is *update*.
  
## Return Code Values  
 0 (success) or a nonzero number (failure)  
    
## Permissions  
 Requires ALTER SERVER STATE (covered by CONTROL SERVER) permission.
  
## Examples  
  
### A. Complete policy refresh 
 The following example downloads complete set of policies.  
  
```  
EXEC sp_external_policy_refresh reload
```  
  
### B. Incremental policy refresh
 The following example downloads incremental policy.  
  
```  
EXEC sp_external_policy_refresh
```  
