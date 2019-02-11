---
title: "sys.database_firewall_rules (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql-database
ms.reviewer: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.database_firewall_rules_TSQL"
  - "database_firewall_rules_TSQL"
  - "sys.database_firewall_rules"
  - "database_firewall_rules"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database_firewall_rules"
  - "sys.database_firewall_rules"
ms.assetid: 2e821593-3b9f-43d6-a99b-1ceffe177faf
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.database_firewall_rules (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns information about the database-level firewall settings associated with your [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Database-level firewall settings are particularly useful when using contained database users. For more information, see [Contained Database Users - Making Your Database Portable](../../relational-databases/security/contained-database-users-making-your-database-portable.md).  
  
 The `sys.database_firewall_rules` view contains the following columns:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|id|**INTEGER**|The identifier of the database-level firewall setting.|  
|name|**NVARCHAR(128)**|The name you chose to describe and distinguish the database-level firewall setting.|  
|start_ip_address|**VARCHAR(50)**|The lowest IP address in the range of the database-level firewall setting. IP addresses equal to or greater than this can attempt to connect to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] instance. The lowest possible IP address is `0.0.0.0`.|  
|end_ip_address|**VARCHAR(50)**|The highest IP address in the range of the firewall setting. IP addresses equal to or less than this can attempt to connect to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] instance. The highest possible IP address is `255.255.255.255`.<br /><br /> Note: Windows Azure connection attempts are allowed when both this field and the **start_ip_address** field equals `0.0.0.0`.|  
|create_date|**DATETIME**|UTC date and time when the database-level firewall setting was created.|  
|modify_date|**DATETIME**|UTC date and time when the database-level firewall setting was last modified.|  
  
## Remarks  
 To remove a database firewall rule, use [sp_delete_database_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-delete-database-firewall-rule-azure-sql-database.md). To set a firewall rule for all of [!INCLUDE[ssSDS](../../includes/sssds-md.md)], see [sp_set_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md). To return information about existing database firewall rules, query [sys.database_firewall_rules (Azure SQL Database)](../../relational-databases/system-catalog-views/sys-database-firewall-rules-azure-sql-database.md).  
  
## Permissions  
 This view is available in the **master** database and in each user database. Read-only access to this view is available to all users with permission to connect to the database.  
  
## See Also  
 [sp_set_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)   
 [sys.database_firewall_rules (Azure SQL Database)](../../relational-databases/system-catalog-views/sys-database-firewall-rules-azure-sql-database.md)   
 [sp_delete_database_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-delete-database-firewall-rule-azure-sql-database.md)   
 [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)   
 [Configure a Firewall for FILESTREAM Access](../../relational-databases/blob/configure-a-firewall-for-filestream-access.md)   
 [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md)  
  
  
