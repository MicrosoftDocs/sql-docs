---
title: "sp_delete_firewall_rule (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2016"
ms.prod: ""
ms.prod_service: "sql-database, sql-data-warehouse"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_firewall_rule_TSQL"
  - "sp_delete_firewall_rule"
  - "sys.sp_delete_firewall_rule"
  - "sys.sp_delete_firewall_rule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_firewall_rule procedure"
ms.assetid: cf93eed1-ba97-4850-9fcc-b9c5a9317908
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sp_delete_firewall_rule (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md)]

  Removes server-level firewall settings from your [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server. This stored procedure is only available in the master database to the server-level principal login.  

  
## Syntax  
  
```  
sp_delete_firewall_rule [@name =] 'name' 
[ ; ] 
```  
  
## Arguments  
 The argument of the stored procedure is:  
  
 [@name =] '*name*'  
 The name of the server-level firewall setting that will be removed. *name* is **nvarchar (128)** with no default.  
  
## Remarks  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
## Permissions  
 Only the server-level principal login created by the provisioning process can delete server level firewall rules. The user must be connected to the master database to execute sp_delete_firewall_rule.  
  
## Example  
 The following example removes the server-level firewall setting named 'Example setting 1'. Execute the statement in the virtual master database.  
  
```   
EXEC sp_delete_firewall_rule N'Example setting 1';   
```  
  
## See Also  
 [Azure SQL Database Firewall](https://azure.microsoft.com/documentation/articles/sql-database-firewall-configure/)   
 [How to: Configure Firewall Settings (Azure SQL Database)](https://azure.microsoft.com/documentation/articles/sql-database-configure-firewall-settings/)   
 [sp_set_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)   
 [sys.firewall_rules &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-firewall-rules-azure-sql-database.md)  
  
  


