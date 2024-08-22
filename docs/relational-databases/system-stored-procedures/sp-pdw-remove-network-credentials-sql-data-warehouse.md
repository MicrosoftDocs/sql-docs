---
title: "sp_pdw_remove_network_credentials"
titleSuffix: Azure Synapse Analytics
description: sp_pdw_remove_network_credentials removes network credentials stored in Azure Synapse Analytics.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest"
---
# sp_pdw_remove_network_credentials (Azure Synapse Analytics)

[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

`sp_pdw_remove_network_credentials` removes network credentials stored in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] to access a network file share. For example, use this stored procedure to remove permission for [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] to perform backup and restore operations on a server that resides within your own network.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
sp_pdw_remove_network_credentials 'target_server_name'
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### '*target_server_name*'

Specifies the target server host name or IP address. *target_server_name* is **nvarchar(337)** with no default. Credentials to access this server are removed from [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. This doesn't change or remove any permissions on the actual target server, which is managed by your own team.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires `ALTER SERVER STATE` permission.

## Error Handling

An error occurs if removing credentials doesn't succeed on the Control node and all Compute nodes.

## Remarks

This stored procedure removes network credentials from the NetworkService account for [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. The NetworkService account runs each instance of SMP [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the Control node and the Compute nodes. For example, when a backup operation runs, the Control node and each Compute node use the NetworkService account credentials to access the target server.

## Metadata

To list all credentials and to verify the credentials have been removed, use [sys.dm_pdw_network_credentials](../system-dynamic-management-views/sys-dm-pdw-network-credentials-transact-sql.md).

To add credentials, use [sp_pdw_add_network_credentials (Azure Synapse Analytics)](sp-pdw-add-network-credentials-sql-data-warehouse.md).

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### A. Remove credentials for performing a database backup

The following example removes user name and password credentials for accessing the target server, which has an IP address of `10.192.147.63`.

```sql
EXEC sp_pdw_remove_network_credentials '10.192.147.63';
```

## Related content

- [sp_pdw_remove_network_credentials (Azure Synapse Analytics)](sp-pdw-remove-network-credentials-sql-data-warehouse.md)
