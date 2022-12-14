---
title: "@@SERVERNAME (Transact-SQL)"
description: "@@SERVERNAME (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "09/07/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@SERVERNAME"
  - "@@SERVERNAME_TSQL"
helpviewer_keywords:
  - "@@SERVERNAME function"
  - "local servers [SQL Server]"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;SERVERNAME (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the name of the local server that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@SERVERNAME  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **nvarchar**  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets the server name to the computer name during installation. To change the name of the server, use **sp_addserver**, and then restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 With multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed, @@SERVERNAME returns the following local server name information if the local server name has not been changed since set up.  
  
|Instance|Server information|  
|--------------|------------------------|  
|Default instance|'*servername*'|  
|Named instance|'*servername*\\*instancename*'|  
|failover cluster instance - default instance|'*network_name_for_fci_in_wsfc*'|  
|failover cluster instance - named instance|'*network_name_for_fci_in_wsfc*\\*instancename*'|  
  
 Although the @@SERVERNAME function and the SERVERNAME property of SERVERPROPERTY function may return strings with similar formats, the information can be different. The SERVERNAME property automatically reports changes in the network name of the computer.  
  
 In contrast, @@SERVERNAME does not report such changes. @@SERVERNAME reports changes made to the local server name using the **sp_addserver** or **sp_dropserver** stored procedure.  
  
## Examples  
 The following example shows using `@@SERVERNAME`.  
  
```sql  
SELECT @@SERVERNAME AS 'Server Name'  
```  
  
 Here is a sample result set.  
  
```  
Server Name  
---------------------------------  
ACCTG  
  
```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)   
 [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)  
  
  
