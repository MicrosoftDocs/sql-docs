---
title: "sys.firewall_rules (Azure SQL Database)"
description: Returns information about the server-level firewall settings associated with your Azure SQL Database.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/09/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.firewall_rules"
  - "firewall_rules"
  - "sys.firewall_rules_TSQL"
  - "firewall_rules_TSQL"
helpviewer_keywords:
  - "firewall_rules"
  - "sys.firewall_rules"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.firewall_rules (Azure SQL Database)

[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Returns information about the server-level firewall settings associated with your [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

The `sys.firewall_rules` view contains the following columns:

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|`id`|**int**|The identifier of the server-level firewall setting.|
|`name`|**nvarchar(128)**|The name you chose to describe and distinguish the server-level firewall setting.|
|`start_ip_address`|**varchar(45)**|The lowest IP address in the range of the server-level firewall setting. IP addresses equal to or greater than this can attempt to connect to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server. The lowest possible IP address is `0.0.0.0`.|
|`end_ip_address`|**varchar(45)**|The highest IP address in the range of the server-level firewall setting. IP addresses equal to or less than this can attempt to connect to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server. The highest possible IP address is `255.255.255.255`.<br /><br />**Note:** Azure connection attempts are allowed when both this field and the **start_ip_address** field equals `0.0.0.0`.|
|`create_date`|**datetime**|UTC date and time when the server-level firewall setting was created.<br /><br />**Note:** UTC is an acronym for Coordinated Universal Time.|
|`modify_date`|**datetime**|UTC date and time when the server-level firewall setting was last modified.|

## Remarks

To return information about the database-level firewall settings associated with your [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], use [sys.database_firewall_rules (Azure SQL Database)](sys-database-firewall-rules-azure-sql-database.md).

## Permissions

Read-only access to this view is available to all users with permission to connect to the `master` database.

## See also

- [sp_set_firewall_rule (Azure SQL Database)](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)
- [sp_delete_firewall_rule (Azure SQL Database)](../../relational-databases/system-stored-procedures/sp-delete-firewall-rule-azure-sql-database.md)
- [sp_set_database_firewall_rule (Azure SQL Database)](../../relational-databases/system-stored-procedures/sp-set-database-firewall-rule-azure-sql-database.md)
- [sp_delete_database_firewall_rule (Azure SQL Database)](../../relational-databases/system-stored-procedures/sp-delete-database-firewall-rule-azure-sql-database.md)
- [sys.database_firewall_rules (Azure SQL Database)](../../relational-databases/system-catalog-views/sys-database-firewall-rules-azure-sql-database.md)
- [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)
- [Configure a Firewall for FILESTREAM Access](../../relational-databases/blob/configure-a-firewall-for-filestream-access.md)
- [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md)
