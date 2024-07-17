---
title: "sp_get_endpoint_certificate (Transact-SQL)"
description: Returns a public key of the certificate used for authentication on endpoint of specified type with certificate-based trust configured.
author: MladjoA
ms.author: mlandzic
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_get_endpoint_certificate"
  - "sp_get_endpoint_certificate_TSQL"
helpviewer_keywords:
  - "sp_get_endpoint_certificate"
dev_langs:
  - "TSQL"
---
# sp_get_endpoint_certificate (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a public key of the certificate used for authentication on endpoint of specified type with certificate-based trust configured. Supported types of endpoints are Database Mirroring endpoint (also used for Link feature of Azure SQL Managed Instance) and Service Broker endpoint.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_get_endpoint_certificate [ @endpoint_type = ] endpoint_type
[ ; ]
```

## Arguments

#### [ @endpoint_type = ] *endpoint_type*

Type of endpoint for which certificate's public key is required. *@endpoint_type* is **int**, and can be one of these values:

| Value | Endpoint |
| --- | --- |
| `3` | Service Broker |
| `4` | Database Mirroring |

Values correspond to the endpoint payload types in [sys.endpoints](../system-catalog-views/sys-endpoints-transact-sql.md) system catalog view.

If any other integer value is provided, stored procedure returns `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `EndpointCertificatePublicKey` | **varbinary** | Public key of the certificate, in binary format |

## Remarks

If authentication type configured on the endpoint isn't certificate-based, stored procedure returns error.

## Permissions

User must have `VIEW` permission on the endpoint to execute `sp_get_endpoint_certificate`.
