---
title: "DROP CERTIFICATE (Transact-SQL)"
description: DROP CERTIFICATE removes a certificate from the database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/24/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DROP CERTIFICATE"
  - "DROP_CERTIFICATE_TSQL"
helpviewer_keywords:
  - "certificates [SQL Server], removing"
  - "removing certificates"
  - "dropping certificates"
  - "DROP CERTIFICATE statement"
  - "deleting certificates"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azure-sqldw-latest || =fabric-sqldb"
---
# DROP CERTIFICATE (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Synapse Analytics FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Removes a certificate from the database.

> [!IMPORTANT]  
> A backup of the certificate used for database encryption should be retained even if the encryption is no longer enabled on a database. Even though the database isn't encrypted anymore, parts of the transaction log might still remain protected; the certificate might be needed for some operations until the full backup of the database is performed. The certificate is also needed to be able to restore from the backups created at the time the database was encrypted.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

[!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/\synapse-analytics-od-unsupported-syntax.md)]

## Syntax

```syntaxsql
DROP CERTIFICATE certificate_name
```

## Arguments

#### *certificate_name*

The unique name by which the certificate is known in the database.

## Remarks

Certificates can only be dropped if no entities are associated with them.

## Permissions

Requires `CONTROL` permission on the certificate.

## Examples

The following example drops the certificate `Shipping04` from the `AdventureWorks` database.

```sql
USE AdventureWorks2022;

DROP CERTIFICATE Shipping04;
```

## Examples: Analytics Platform System (PDW)

The following example drops the certificate `Shipping04`.

```sql
USE master;

DROP CERTIFICATE Shipping04;
```

## Related content

- [BACKUP CERTIFICATE (Transact-SQL)](backup-certificate-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](create-certificate-transact-sql.md)
- [ALTER CERTIFICATE (Transact-SQL)](alter-certificate-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
