---
title: Create a Database Master Key
description: Learn how to create a database master key in SQL Server by using Transact-SQL. This essential key encrypts other keys and certificates.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 07/22/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
helpviewer_keywords:
  - "database master key [SQL Server], creating"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Create a database master key

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to create a *database master key* in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [tsql](../../../includes/tsql-md.md)]. The database master key encrypts other keys and certificates inside a database. Create the database master key once per database, and back it up to a secure off-site location so you can restore it if it's deleted or corrupted.

## Permissions

Requires `CONTROL` permission on the database.

## Create the database master key

[!INCLUDE [article-uses-adventureworks](../../../includes/article-uses-adventureworks.md)]

1. Connect to the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance where you want to create the database master key. [!INCLUDE [connect-instance-client](../../../includes/connect-instance-client.md)]

1. Choose a strong password for encrypting the database master key. [!INCLUDE [password-complexity](../../../linux/includes/password-complexity.md)]

1. Review and run the following Transact-SQL script in the context of the database where you want to create the database master key. Change the password to match your environment.

   > [!CAUTION]  
   > You need the password to open the database master key. Make sure you store this password safely and securely.

   ```sql
   USE AdventureWorks2025;
   GO

   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
   ```

1. Back up the newly created database master key. For more information, see [Back up a database master key](back-up-a-database-master-key.md).

## Related content

- [Back up a database master key](back-up-a-database-master-key.md)
- [Restore a database master key](restore-a-database-master-key.md)
- [CREATE MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-master-key-transact-sql.md)
- [OPEN MASTER KEY (Transact-SQL)](../../../t-sql/statements/open-master-key-transact-sql.md)
