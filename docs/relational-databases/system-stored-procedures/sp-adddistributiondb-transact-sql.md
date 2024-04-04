---
title: "sp_adddistributiondb (Transact-SQL)"
description: Creates a new distribution database and installs the Distributor schema.
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/30/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_adddistributiondb_TSQL"
  - "sp_adddistributiondb"
helpviewer_keywords:
  - "sp_adddistributiondb"
dev_langs:
  - "TSQL"
---
# sp_adddistributiondb (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a new distribution database and installs the Distributor schema. The distribution database stores procedures, schema, and metadata used in replication. This stored procedure is executed at the Distributor on the `master` database in order to create the distribution database, and install the necessary tables and stored procedures required to enable the replication distribution.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_adddistributiondb
    [ @database = ] N'database'
    [ , [ @data_folder = ] N'data_folder' ]
    [ , [ @data_file = ] N'data_file' ]
    [ , [ @data_file_size = ] data_file_size ]
    [ , [ @log_folder = ] N'log_folder' ]
    [ , [ @log_file = ] N'log_file' ]
    [ , [ @log_file_size = ] log_file_size ]
    [ , [ @min_distretention = ] min_distretention ]
    [ , [ @max_distretention = ] max_distretention ]
    [ , [ @history_retention = ] history_retention ]
    [ , [ @security_mode = ] security_mode ]
    [ , [ @login = ] N'login' ]
    [ , [ @password = ] N'password' ]
    [ , [ @createmode = ] createmode ]
    [ , [ @from_scripting = ] from_scripting ]
    [ , [ @deletebatchsize_xact = ] deletebatchsize_xact ]
    [ , [ @deletebatchsize_cmd = ] deletebatchsize_cmd ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The name of the distribution database to be created. *@database* is **sysname**, with no default. If the specified database already exists and isn't already marked as a distribution database, then the objects needed to enable distribution are installed, and the database is marked as a distribution database. If the specified database is already enabled as a distribution database, an error is returned.

#### [ @data_folder = ] N'*data_folder*'

The name of the directory used to store the distribution database data file. *@data_folder* is **nvarchar(255)**, with a default of `NULL`. If `NULL`, the data directory for that instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is used, for example, `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Data`.

#### [ @data_file = ] N'*data_file*'

The name of the database file. *@data_file* is **nvarchar(255)**, with a default of `NULL`. If `NULL`, the stored procedure constructs a file name using the database name.

#### [ @data_file_size = ] *data_file_size*

The initial data file size in megabytes (MB). *@data_file_size* is **int**, with a default of `5`, which is 5 MB.

#### [ @log_folder = ] N'*log_folder*'

The name of the directory for the database log file. *@log_folder* is **nvarchar(255)**, with a default of `NULL`. If `NULL`, the data directory for that instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is used (for example, `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Data`).

#### [ @log_file = ] N'*log_file*'

The name of the log file. *@log_file* is **nvarchar(255)**, with a default of `NULL`. If `NULL`, the stored procedure constructs a file name using the database name.

#### [ @log_file_size = ] *log_file_size*

The initial log file size in megabytes (MB). *@log_file_size* is **int**, with a default of `0`, which creates the file using the smallest log file size allowed by the [!INCLUDE [ssde-md](../../includes/ssde-md.md)].

#### [ @min_distretention = ] *min_distretention*

The minimum retention period, in hours, before transactions are deleted from the distribution database. *@min_distretention* is **int**, with a default of `0`.

#### [ @max_distretention = ] *max_distretention*

The maximum retention period, in hours, before transactions are deleted. *@max_distretention* is **int**, with a default of `72`. Subscriptions that haven't received replicated commands, and that are older than the maximum distribution retention period, are marked as inactive and need to be reinitialized. Error number 21011 is issued for each inactive subscription. A value of `0` means that replicated transactions aren't stored in the distribution database.

#### [ @history_retention = ] *history_retention*

The number of hours to retain history. *@history_retention* is **int**, with a default of `48`, which means two days.

#### [ @security_mode = ] *security_mode*

The security mode to use when connecting to the Distributor. *@security_mode* is **int**, with a default of `1`.

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication
- `1` specifies Windows authentication

#### [ @login = ] N'*login*'

The login name used when connecting to the Distributor to create the distribution database. *@login* is **sysname**, with a default of `NULL`. *@login* is required if *@security_mode* is set to `0`.

#### [ @password = ] N'*password*'

The password used when connecting to the Distributor. *@password* is **sysname**, with a default of `NULL`. *@password* is required if *@security_mode* is set to `0`.

#### [ @createmode = ] *createmode*

*@createmode* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `0` | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `1` (default) | `CREATE DATABASE` or use existing database and then apply the `instdist.sql` file to create replication objects in the distribution database. |
| `2` | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |

#### [ @from_scripting = ] *from_scripting*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @deletebatchsize_xact = ] *deletebatchsize_xact*

Specifies the batch size to be used during cleanup of expired transactions from the `MSRepl_Transactions` tables. *@deletebatchsize_xact* is **int**, with a default of `5000`.

**Applies to:** [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] with Service Pack 4, [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] with Service Pack 2, [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and later versions.

#### [ @deletebatchsize_cmd = ] *deletebatchsize_cmd*

Specifies the batch size to be used during cleanup of expired commands from the `MSRepl_Commands` tables. *@deletebatchsize_cmd* is **int**, with a default of `2000`.

**Applies to:** [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] with Service Pack 4, [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] with Service Pack 2, [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and later versions.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_adddistributiondb` is used in all types of replication. However, this stored procedure only runs at a distributor.

You must configure the distributor by executing [sp_adddistributor](sp-adddistributor-transact-sql.md) before executing `sp_adddistributiondb`.

Run `sp_adddistributor` prior to running `sp_adddistributiondb`.

## Examples

This script uses SQLCMD scripting variables and must run in SQLCMD mode. The variables are in the form `$(MyVariable)`. For information about how to use scripting variables on the command line and in SQL Server Management Studio, see the [Executing Replication Scripts](../replication/concepts/replication-system-stored-procedures-concepts.md#executing-replication-scripts).

```sql
DECLARE @distributor AS SYSNAME;
DECLARE @distributionDB AS SYSNAME;
DECLARE @publisher AS SYSNAME;
DECLARE @directory AS NVARCHAR(500);
DECLARE @publicationDB AS SYSNAME;

-- Specify the Distributor name.
SET @distributor = $(DistPubServer);

-- Specify the distribution database.
SET @distributionDB = N'distribution';

-- Specify the Publisher name.
SET @publisher = $(DistPubServer);

-- Specify the replication working directory.
SET @directory = N'\\' + $(DistPubServer) + '\repldata';

-- Specify the publication database.
SET @publicationDB = N'AdventureWorks2022';

-- Install the server MYDISTPUB as a Distributor using the defaults,
-- including autogenerating the distributor password.
USE master

EXEC sp_adddistributor @distributor = @distributor;

-- Create a new distribution database using the defaults, including
-- using Windows Authentication.
USE master

EXEC sp_adddistributiondb @database = @distributionDB,
    @security_mode = 1;
GO

-- Create a Publisher and enable AdventureWorks2022 for replication.
-- Add MYDISTPUB as a publisher with MYDISTPUB as a local distributor
-- and use Windows Authentication.
DECLARE @distributionDB AS SYSNAME;
DECLARE @publisher AS SYSNAME;

-- Specify the distribution database.
SET @distributionDB = N'distribution';
-- Specify the Publisher name.
SET @publisher = $( DistPubServer );

USE [distribution]

EXEC sp_adddistpublisher @publisher = @publisher,
    @distribution_db = @distributionDB,
    @security_mode = 1;
GO
```

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_adddistributiondb`.

## Related content

- [Configure Publishing and Distribution](../replication/configure-publishing-and-distribution.md)
- [sp_changedistributiondb (Transact-SQL)](sp-changedistributiondb-transact-sql.md)
- [sp_dropdistributiondb (Transact-SQL)](sp-dropdistributiondb-transact-sql.md)
- [sp_helpdistributiondb (Transact-SQL)](sp-helpdistributiondb-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Configure Distribution](../replication/configure-distribution.md)
