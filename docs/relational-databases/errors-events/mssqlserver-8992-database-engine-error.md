---
title: "MSSQLSERVER_8992"
description: MSSQLSERVER_8992 describes when DBCC CHECKDB finds an inconsistency in the system metadata tables for the specified object.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 06/09/2025
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "8992 (Database Engine error)"
---
# MSSQLSERVER_8992

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Item | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 8992 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | DBCC3_CHECK_CATALOG |
| Message Text | Check Catalog Msg ERROR Level LEVEL State STATE: MESSAGE. |

> [!NOTE]  
> 8992 Error message references another specific message (ranging from 3851 to 3858) about the actual inconsistency.

## Explanation

`DBCC CHECKCATALOG` or `DBCC CHECKDB` found an inconsistency in the system metadata tables for the specified object. That is, there's an inconsistency between the recorded object ID and the object specified in error message.

This error can occur when one or more system tables were manually updated in a way that creates an inconsistency in the system metadata. For example, you might manually delete an object from the `sysobjects` table without removing associated rows in other tables such as `sysindexes` and `syscolumns`.

This error can occur when running `DBCC CHECKDB` against a database that was upgraded from [!INCLUDE [ssversion2000-md](../../includes/ssversion2000-md.md)] to a later version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. In [!INCLUDE [ssversion2000-md](../../includes/ssversion2000-md.md)], `DBCC CHECKDB` didn't include `DBCC CHECKCATALOG` functionality, so the error isn't caught before upgrade unless `DBCC CHECKCATALOG` is specifically executed against the database in [!INCLUDE [ssversion2000-md](../../includes/ssversion2000-md.md)].

You might see any of the following errors together with error 8992:

| Message ID | Message text |
| --- | --- |
| `3851` | `An invalid row (%ls) was found in the system table sys.%ls%ls.` |
| `3852` | `Row (%ls) in sys.%ls%ls does not have a matching row (%ls) in sys.%ls%ls.` |
| `3853` | `Attribute (%ls) of row (%ls) in sys.%ls%ls does not have a matching row (%ls) in sys.%ls%ls.` |
| `3854` | `Attribute (%ls) of row (%ls) in sys.%ls%ls has a matching row (%ls) in sys.%ls%ls that is invalid.` |
| `3855` | `Attribute (%ls) exists without a row (%ls) in sys.%ls%ls.` |
| `3856` | `Attribute (%ls) exists but should not for row (%ls) in sys.%ls%ls.` |
| `3857` | `The attribute (%ls) is required but is missing for row (%ls) in sys.%ls%ls.` |
| `3858` | `The attribute (%ls) of row (%ls) in sys.%ls%ls has an invalid value.` |

## User action

### Drop and recreate the specified object

If possible, drop and recreate the specified object. For example, if the object is a stored procedure or user-defined type, recreating the object might resolve the problem.

### Restore from backup

If the problem isn't hardware related and a known clean backup is available, restore the database from the backup. This action is only applicable if the backup doesn't contain the metadata error.

### Export the data to a new database

If the backup also contains the metadata inconsistency, you need to create a new database and export the contents of the existing database to the new database.

### DBCC CHECKDB can't repair this error

This error can't be repaired. If you can't restore the database from a backup, contact Microsoft Support.

### Don't manually update system tables

Don't make manual updates to system tables. SQL Server doesn't support any manual changes to system databases. If you update a system table in a SQL Server database, the following events are logged:

#### When a system table is manually updated

```output
Msg 17659: Warning: System table ID <id> has been updated directly in database ID <id> and cache coherence may not have been maintained. SQL Server should be restarted.
```

#### Start a database with a system table that was manually updated

```output
Msg 3859: Warning: The system catalog was updated directly in database ID <id>, most recently at date_time.
```

#### You execute the DBCC CHECKDB command after a system table is manually updated

```output
Msg 3859: Warning: The system catalog was updated directly in database ID <id>, most recently at date_time.
```

## Related content

- [System base tables](../system-tables/system-base-tables.md)
