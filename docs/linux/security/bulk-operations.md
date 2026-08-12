---
title: Configure Bulk Import Operations for SQL Server on Linux
description: Learn how to configure and use the bulkadmin server role or the ADMINISTER BULK OPERATIONS permission for bulk data import in SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: matripathy, amitkh, atsingh
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
ai-usage: ai-assisted
# customer intent: As a database administrator, I want to configure bulk import operations on SQL Server on Linux so that non-sysadmin users can perform BULK INSERT and OPENROWSET(BULK...) operations securely.
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16"
---

# Configure bulk import operations for SQL Server on Linux (preview)

[!INCLUDE [sqlserver2022-and-later-linux](../../includes/applies-to-version/sqlserver2022-and-later-linux.md)]

> [!IMPORTANT]  
> This feature is currently in preview.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Cumulative Update 24 (CU24) and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update 3 (CU3), you can use the **bulkadmin** fixed server role or the `ADMINISTER BULK OPERATIONS` permission to perform bulk data import operations on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] running on Linux. Previously, only members of the **sysadmin** fixed server role could run [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET(BULK...)](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md) on Linux.

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux enforces additional file system and path validation checks for bulk operations, beyond what Windows requires. An administrator must:

- Grant appropriate [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] permissions to the user
- Grant Linux file system permissions on the data files
- Explicitly approve directory paths with **`mssql-conf`**

## Prerequisites

- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU24 or later version on Linux, or [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU3 or later version on Linux
- Administrative access to the Linux host
- Administrative access to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance

## Configure the Linux file system

Before you run bulk import operations, the `mssql` service account must have read access to the data files on the Linux file system.

1. Create a directory for your bulk data files:

   ```bash
   mkdir -p /tmp/bulkload/sales/
   ```

1. Create a sample data file:

   ```bash
   cat > /tmp/bulkload/sales/loadsalesdata.csv << EOF
   Id,CustomerName,OrderDate,Amount
   1,John Doe,2026-02-01,500.75
   2,Jane Smith,2026-02-05,1500.20
   3,Mark Lee,2026-02-10,320.50
   4,Alice Johnson,2026-02-15,785.00
   5,Bob Brown,2026-02-20,930.40
   EOF
   ```

1. Grant read permission to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] process account (`mssql`) on the data file:

   ```bash
   setfacl -m u:mssql:r /tmp/bulkload/sales/loadsalesdata.csv
   ```

## Configure allowed paths with mssql-conf

An administrator must approve the directories from which bulk operations can read, using the `bulkadmin.allowedpathslist` setting in **`mssql-conf`**. This change takes effect immediately and doesn't require a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service restart.

```bash
sudo /opt/mssql/bin/mssql-conf set bulkadmin.allowedpathslist "/tmp/bulkload/sales"
```

To specify multiple directories, separate each path with a colon (`:`):

```bash
sudo /opt/mssql/bin/mssql-conf set bulkadmin.allowedpathslist "/tmp/bulkload/sales:/tmp/bulkload/marketing"
```

Alternatively, you can specify a parent directory to allow all subdirectories under it.

### Path restrictions

The following restrictions apply to paths configured for bulk operations:

- The path must be an absolute path. Relative paths containing `.` or `..` aren't allowed.
- The root path (`/`) isn't allowed.
- The path must not exceed 4,096 characters.
- The path must not contain invalid characters (null, newline, carriage return, or tab).
- Symbolic links aren't allowed.
- The path must refer to a directory.

### Forbidden paths

The system blocks some critical paths by design. You can't use these locations as source data file paths for bulk operations, even if you add them to the allowed paths list:

- `/var/opt/mssql` (or the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] installation directory)
- `/var/opt/azcmagent/certs`
- `/var/opt/azcmagent/tokens`

Bulk operations that read source data from these paths fail for security reasons. This restriction doesn't apply to the `ERRORFILE` output path, which uses the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data directory by default.

## Set up SQL Server permissions

After you configure the Linux file system and allowed paths, set up the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] permissions for the user who performs the bulk import.

1. Connect to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance and create a sample database and table:

   ```sql
   CREATE DATABASE demodbforbulkinsert;
   GO

   USE demodbforbulkinsert;
   GO

   CREATE TABLE dbo.Sales
   (
       Id INT NOT NULL PRIMARY KEY,
       CustomerName NVARCHAR (200) NOT NULL,
       OrderDate DATE NOT NULL,
       Amount DECIMAL (18, 2) NOT NULL
   );
   GO
   ```

1. Create a login for the bulk import user:

   ```sql
   USE master;
   GO

   CREATE LOGIN BulkLoadUser
   WITH PASSWORD = '<strong_password>';
   GO
   ```

1. Grant the `ADMINISTER BULK OPERATIONS` permission or add the login to the **bulkadmin** fixed server role. Use one of the following options:

   ```sql
   -- Option 1: Add to the bulkadmin server role
   ALTER SERVER ROLE bulkadmin ADD MEMBER BulkLoadUser;
   GO
   ```

   ```sql
   -- Option 2: Grant the permission directly
   GRANT ADMINISTER BULK OPERATIONS TO BulkLoadUser;
   GO
   ```

1. Create a database user and grant the necessary table permissions:

   ```sql
   USE demodbforbulkinsert;
   GO

   CREATE USER BulkLoadUser FOR LOGIN BulkLoadUser;
   GO

   GRANT INSERT, SELECT
       ON dbo.Sales TO BulkLoadUser;
   GO
   ```

## Run a bulk import

Connect to the database as the `BulkLoadUser` login and run the bulk import:

```sql
USE demodbforbulkinsert;
GO

BULK INSERT dbo.Sales
FROM '/tmp/bulkload/sales/loadsalesdata.csv'
WITH (
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ERRORFILE = '/var/opt/mssql/data/bulk_errors'
);
GO

-- Verify the imported data
SELECT *
FROM dbo.Sales;
GO
```

The same permissions, Linux file system configuration, and path approval steps apply to `INSERT ... SELECT * FROM OPENROWSET(BULK...)` statements. For more information, see [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

## Upgrade and downgrade behavior

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU24 and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU3, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux supports bulk operations using the **bulkadmin** fixed server role or `ADMINISTER BULK OPERATIONS` permission.

If you downgrade to a cumulative update earlier than [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU24 or [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU3:

- Previously granted permissions remain assigned but aren't functional.
- Bulk operations require **sysadmin** permissions, as in versions before these cumulative updates.

## Related content

- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)
- [Security considerations for SQL Server on Linux](overview.md)
- [Security and permissions guide for SQL Server on Linux](permissions-guide.md)
- [Configure SQL Server on Linux with the mssql-conf tool](../configure/mssql-conf.md)
