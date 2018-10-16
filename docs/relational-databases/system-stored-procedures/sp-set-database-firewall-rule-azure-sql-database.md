---
title: "sp_set_database_firewall_rule (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "08/04/2017"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_set_database_firewall_rule"
  - "sp_set_database_firewall_rule_TSQL"
  - "sys.sp_set_database_firewall_rule"
  - "sys.sp_set_database_firewall_rule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_set_database_firewall_rule"
  - "firewall_rules, setting database rules"
ms.assetid: 8f0506b6-a4ac-4e4d-91db-8077c40cb17a
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sp_set_database_firewall_rule (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Creates or updates the database-level firewall rules for your [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Database firewall rules can be configured for the **master** database, and for user databases on [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. Database firewall rules are particularly useful when using contained database users. For more information, see [Contained Database Users - Making Your Database Portable](../../relational-databases/security/contained-database-users-making-your-database-portable.md).  
  
## Syntax  
  
```  
  
sp_set_database_firewall_rule [@name = ] [N]'name'  
, [@start_ip_address =] 'start_ip_address'  
, [@end_ip_address =] 'end_ip_address'
[ ; ]  
```  
  
## Arguments  
 **[@name** = ] [N]'*name*'  
 The name used to describe and distinguish the database-level firewall setting. *name* is **nvarchar(128)** with no default value. The Unicode identifier `N` is optional for [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]. 
  
 **[@start_ip_address** =] '*start_ip_address*'  
 The lowest IP address in the range of the database-level firewall setting. IP addresses equal to or greater than this can attempt to connect to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] instance. The lowest possible IP address is `0.0.0.0`. *start_ip_address* is **varchar(50)** with no default value.  
  
 [**@end_ip_address** =] '*end_ip_address*'  
 The highest IP address in the range of the database-level firewall setting. IP addresses equal to or less than this can attempt to connect to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] instance. The highest possible IP address is `255.255.255.255`. *end_ip_address* is **varchar(50)** with no default value.  
  
 The following table demonstrates the supported arguments and options in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
> [!NOTE]  
>  Azure connection attempts are allowed when both this field and the *start_ip_address* field equals `0.0.0.0`.  
  
## Remarks  
 The names of database-level firewall settings for a database must be unique. If the name of the database-level firewall setting provided for the stored procedure already exists in the database-level firewall settings table, the starting and ending IP addresses will be updated. Otherwise, a new database-level firewall setting will be created.  
  
 When you add a database-level firewall setting where the beginning and ending IP addresses are equal to `0.0.0.0`, you enable access to your database in the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server from any Azure resource. Provide a value to the *name* parameter that will help you remember what the firewall setting is for.  
  
## Permissions  
 Requires **CONTROL** permission on the database.  
  
## Examples  
 The following code creates a database-level firewall setting called `Allow Azure` that enables access to your database from Azure.  
  
```  
-- Enable Azure connections.  
EXECUTE sp_set_database_firewall_rule N'Allow Azure', '0.0.0.0', '0.0.0.0';  
  
```  
  
 The following code creates a database-level firewall setting called `Example DB Setting 1` for only the IP address `0.0.0.4`. Then, the `sp_set_database firewall_rule` stored procedure is called again to update the end IP address to `0.0.0.6`, in that firewall setting. This creates a range which allows IP addresses `0.0.0.4`, `0.0.0.5`, and `0.0.0.6` to access the database.
  
```  
-- Create database-level firewall setting for only IP 0.0.0.4  
EXECUTE sp_set_database_firewall_rule N'Example DB Setting 1', '0.0.0.4', '0.0.0.4';  
  
-- Update database-level firewall setting to create a range of allowed IP addresses
EXECUTE sp_set_database_firewall_rule N'Example DB Setting 1', '0.0.0.4', '0.0.0.6';  
  
```  
  
## See Also  
 [Azure SQL Database Firewall](https://azure.microsoft.com/documentation/articles/sql-database-firewall-configure/)   
 [How to: Configure Firewall Settings (Azure SQL Database)](https://azure.microsoft.com/documentation/articles/sql-database-configure-firewall-settings/)   
 [sp_set_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)   
 [sp_delete_database_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-delete-database-firewall-rule-azure-sql-database.md)   
 [sys.database_firewall_rules &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-database-firewall-rules-azure-sql-database.md)  
  
  
