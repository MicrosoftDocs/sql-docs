---
title: "sp_pdw_add_network_credentials"
titleSuffix: Azure Synapse Analytics
description: sp_pdw_add_network_credentials stores network credentials in Azure Synapse Analytics and associates them with a server.
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
# sp_pdw_add_network_credentials (Azure Synapse Analytics)

[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

`sp_pdw_add_network_credentials` stores network credentials in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and associates them with a server. For example, use this stored procedure to give [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] appropriate read/write permissions to perform database backup and restore operations on a target server, or to create a backup of a certificate used for transparent data encryption (TDE).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
sp_pdw_add_network_credentials
    'target_server_name'
    , 'user_name'
    , 'password'
[ ; ]
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### '*target_server_name*'

Specifies the target server host name or IP address. *target_server_name* is **nvarchar(337)** with no default. [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] accesses this server with the username and password credentials passed to this stored procedure.

To connect through the InfiniBand network, use the InfiniBand IP address of the target server.

#### '*user_name*'

Specifies the user_name that's permissions to access the target server. *user_name* is **nvarchar(513)** with no default. If credentials already exist for the target server, they're updated to the new credentials.

#### '*password*ꞌ

Specifies the password for *user_name*.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires `ALTER SERVER STATE` permission.

## Error Handling

An error occurs if adding credentials doesn't succeed on the Control node and all Compute nodes.

## Remarks

This stored procedure adds network credentials to the `NetworkService` account for [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. The `NetworkService` account runs each instance of SMP [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the Control node and the Compute nodes. For example, when a backup operation runs, the Control node and each Compute node use the `NetworkService` account credentials to gain read and write permission to the target server.

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### A. Add credentials for performing a database backup

The following example associates the user name and password credentials for the domain user `seattle\david` with a target server that's an IP address of `10.172.63.255`. The user `seattle\david` has read/write permissions to the target server. [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] stores these credentials and uses them to read and write to and from the target server, as necessary for backup and restore operations.

```sql
EXEC sp_pdw_add_network_credentials
    '10.172.63.255',
    'seattle\david',
    '********';
```

The backup command requires that the server name is entered as an IP address.

> [!NOTE]  
> To perform the database backup over InfiniBand, be sure to use the InfiniBand IP address of the backup server.

## Related content

- [sp_pdw_remove_network_credentials (Azure Synapse Analytics)](sp-pdw-remove-network-credentials-sql-data-warehouse.md)
