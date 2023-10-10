---
title: "Enable the prerequisites for FileTable"
description: To use FileTables, first turn on FILESTREAM, specify a directory, and set certain options and access levels. Learn how to meet all prerequisites.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords:
  - "FileTables [SQL Server], prerequisites"
---
# Enable the prerequisites for FileTable

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Describes how to enable the prerequisites for creating and using FileTables.

## <a id="EnablePrereq"></a> Enabling prerequisites for FileTable

To enable the prerequisites for creating and using FileTables, enable the following items:

- **At the instance level:**

  - [Enabling FILESTREAM at the instance level](#BasicsFilestream)

- **At the database level:**

  - [Provide a FILESTREAM filegroup at the database level](#filegroup)
  - [Enable nontransactional access at the database level](#BasicsNTAccess)
  - [Specify a directory for FileTables at the database level](#BasicsDirectory)

## <a id="BasicsFilestream"></a> Enabling FILESTREAM at the instance level

FileTables extend the capabilities of the FILESTREAM feature of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You have to enable FILESTREAM for file I/O access at the Windows level, and on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], before you can create and use FileTables.

### <a id="HowToFilestream"></a> Enable FILESTREAM at the instance level

For information about how to enable FILESTREAM, see [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md).

When you call `sp_configure` to enable FILESTREAM at the instance level, you have to set the `filestream_access_level` option to `2`. For more information, see [FILESTREAM access level (server configuration option)](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).

### <a id="firewall"></a> Allow FILESTREAM through the firewall

For information about how to allow FILESTREAM through the firewall, see [Configure a Firewall for FILESTREAM Access](configure-a-firewall-for-filestream-access.md).

## <a id="filegroup"></a> Provide a FILESTREAM filegroup at the database level

Before you can create FileTables in a database, the database must have a FILESTREAM filegroup. For more information about this prerequisite, see [Create a FILESTREAM-Enabled Database](create-a-filestream-enabled-database.md).

## <a id="BasicsNTAccess"></a> Enable nontransactional access at the database level

FileTables let Windows applications obtain a Windows file handle to FILESTREAM data without requiring a transaction. To allow this nontransactional access to files stored in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you have to specify the desired level of nontransactional access at the database level for each database that will contain FileTables.

### <a id="HowToCheckAccess"></a> Check whether nontransactional access is enabled on databases

Query the catalog view [sys.database_filestream_options (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-filestream-options-transact-sql.md) and check the `non_transacted_access` and `non_transacted_access_desc` columns.

```sql
SELECT DB_NAME(database_id), non_transacted_access, non_transacted_access_desc
    FROM sys.database_filestream_options;
GO
```

### <a id="HowToNTAccess"></a> Enable nontransactional access at the database level

The available levels of nontransactional access are FULL, READ_ONLY, and OFF.

#### Specify the level of nontransactional access with Transact-SQL

When you **create a new database**, call the [CREATE DATABASE (SQL Server Transact-SQL)](../../t-sql/statements/create-database-transact-sql.md) statement with the `NON_TRANSACTED_ACCESS` FILESTREAM option.

```sql
CREATE DATABASE database_name
  WITH FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' );
```

When you **alter an existing database**, call the [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md) statement with the `NON_TRANSACTED_ACCESS` FILESTREAM option.

```sql
ALTER DATABASE database_name
  SET FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' );
```

#### Specify the level of nontransactional access with SQL Server Management Studio

You can specify the level of nontransactional access in the **FILESTREAM Non-transacted Access** field of the **Options** page of the **Database Properties** dialog box. For more information about this dialog box, see [Database Properties (Options Page)](../../relational-databases/databases/database-properties-options-page.md).

## <a id="BasicsDirectory"></a> Specify a directory for FileTables at the database level

When you enable nontransactional access to files at the database level, you can optionally provide a directory name at the same time with the `DIRECTORY_NAME` option. If you don't provide a directory name when you enable nontransactional access, then you have to provide it later before you can create FileTables in the database.

In the FileTable folder hierarchy, this database-level directory becomes the child of the share name specified for FILESTREAM at the instance level, and the parent of the FileTables created in the database. For more information, see [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md).

### <a id="HowToDirectory"></a> Specify a directory for FileTables at the database Level

The name that you specify must be unique across the instance for database-level directories.

#### Specify a directory for FileTables with Transact-SQL

When you **create a new database**, call the [CREATE DATABASE (SQL Server Transact-SQL)](../../t-sql/statements/create-database-transact-sql.md) statement with the `DIRECTORY_NAME` FILESTREAM option.

```sql
CREATE DATABASE database_name
  WITH FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' );
GO
```

When you **alter an existing database**, call the [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md) statement with the `DIRECTORY_NAME` FILESTREAM option. When you use these options to change the directory name, the database must be exclusively locked, with no open file handles.

```sql
ALTER DATABASE database_name
    SET FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' );
GO
```

When you **attach a database**, call the [CREATE DATABASE (SQL Server Transact-SQL)](../../t-sql/statements/create-database-transact-sql.md) statement with the `FOR ATTACH` option and with the `DIRECTORY_NAME` FILESTREAM option.

```sql
CREATE DATABASE database_name
    FOR ATTACH WITH FILESTREAM ( DIRECTORY_NAME = N'directory_name' );
GO
```

When you **restore a database**, call the [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md) statement with the `DIRECTORY_NAME` FILESTREAM option.

```sql
RESTORE DATABASE database_name
    WITH FILESTREAM ( DIRECTORY_NAME = N'directory_name' );
GO
```

#### Specify a directory for FileTables with SQL Server Management Studio

You can specify a directory name in the **FILESTREAM Directory Name** field of the **Options** page of the **Database Properties** dialog box. For more information about this dialog box, see [Database Properties (Options Page)](../../relational-databases/databases/database-properties-options-page.md).

### <a id="viewnames"></a> View existing directory names for the instance

To view the list of existing directory names for the instance, query the catalog view [sys.database_filestream_options (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-filestream-options-transact-sql.md) and check the `filestream_database_directory_name` column.

```sql
SELECT DB_NAME ( database_id ), directory_name
    FROM sys.database_filestream_options;
GO
```

### <a id="ReqDirectory"></a> Requirements and restrictions for the database-level directory

- Setting the `DIRECTORY_NAME` is optional when you call `CREATE DATABASE` or `ALTER DATABASE`. If you don't specify a value for `DIRECTORY_NAME`, then the directory name remains null. However you can't create FileTables in the database until you specify a value for `DIRECTORY_NAME` at the database level.

- The directory name that you provide must comply with the requirements of the file system for a valid directory name.

- When the database contains FileTables, you can't set the `DIRECTORY_NAME` back to a null value.

- When you attach or restore a database, the operation fails if the new database has a value for `DIRECTORY_NAME` that already exists in the target instance. Specify a unique value for `DIRECTORY_NAME` when you call `CREATE DATABASE FOR ATTACH` or `RESTORE DATABASE`.

- When you upgrade an existing database, the value of `DIRECTORY_NAME` is null.

- When you enable or disable nontransactional access at the database level, the operation doesn't check whether the directory name has been specified, or whether it is unique.

- When you drop a database that was enabled for FileTables, the database-level directory and all the directory structures of all the FileTables under it are removed.
