---
title: "CREATE DATABASE (Transact-SQL)"
description: Create database syntax for SQL Server, Azure SQL Database, Azure Synapse Analytics, and Analytics Platform System
author: markingmyname
ms.author: maghan
ms.date: 06/01/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - "references_regions"
  - "event-tier1-build-2022"
f1_keywords:
  - "DATABASE_TSQL"
  - "DATABASE"
  - "CONTAINMENT_TSQL"
  - "CREATE DATABASE"
  - "CREATE_DATABASE_TSQL"
  - "CONTAINS_FILESTREAM_TSQL"
  - "CONTAINS FILESTREAM"
  - "CONTAINMENT"
helpviewer_keywords:
  - "snapshots [SQL Server database snapshots], creating"
  - "databases [SQL Server], creating"
  - "model database [SQL Server], database creation"
  - "mounted drives [SQL Server]"
  - "CREATE DATABASE"
  - "CREATE DATABASE statement"
  - "file creation [SQL Server]"
  - "creating databases"
  - "containment"
  - "filegroups [SQL Server], database creation"
  - "database creation [SQL Server], CREATE DATABASE statement"
  - "moving databases"
  - "attaching databases [SQL Server], CREATE DATABASE...FOR ATTACH"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-current||=azuresqldb-mi-current||=azure-sqldw-latest||>=aps-pdw-2016"
---
# CREATE DATABASE

Creates a new database.

Select one of the following tabs for the syntax, arguments, remarks, permissions, and examples for a particular SQL version with which you are working.

[!INCLUDE [select-product](../includes/select-product.md)]

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Database](create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](create-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-database-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server

## Overview

In SQL Server, this statement creates a new database and the files used and their filegroups. It can also be used to create a database snapshot, or attach database files to create a database from the detached files of another database.

## Syntax

Create a database.

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE DATABASE database_name
[ CONTAINMENT = { NONE | PARTIAL } ]
[ ON
      [ PRIMARY ] <filespec> [ ,...n ]
      [ , <filegroup> [ ,...n ] ]
      [ LOG ON <filespec> [ ,...n ] ]
]
[ COLLATE collation_name ]
[ WITH <option> [,...n ] ]
[;]

<option> ::=
{
      FILESTREAM ( <filestream_option> [,...n ] )
    | DEFAULT_FULLTEXT_LANGUAGE = { lcid | language_name | language_alias }
    | DEFAULT_LANGUAGE = { lcid | language_name | language_alias }
    | NESTED_TRIGGERS = { OFF | ON }
    | TRANSFORM_NOISE_WORDS = { OFF | ON}
    | TWO_DIGIT_YEAR_CUTOFF = <two_digit_year_cutoff>
    | DB_CHAINING { OFF | ON }
    | TRUSTWORTHY { OFF | ON }
    | PERSISTENT_LOG_BUFFER=ON ( DIRECTORY_NAME='<Filepath to folder on DAX formatted volume>' )
    | LEDGER = {ON | OFF}
}

<filestream_option> ::=
{
      NON_TRANSACTED_ACCESS = { OFF | READ_ONLY | FULL }
    | DIRECTORY_NAME = 'directory_name'
}

<filespec> ::=
{
(
    NAME = logical_file_name ,
    FILENAME = { 'os_file_name' | 'filestream_path' }
    [ , SIZE = size [ KB | MB | GB | TB ] ]
    [ , MAXSIZE = { max_size [ KB | MB | GB | TB ] | UNLIMITED } ]
    [ , FILEGROWTH = growth_increment [ KB | MB | GB | TB | % ] ]
)
}

<filegroup> ::=
{
FILEGROUP filegroup name [ [ CONTAINS FILESTREAM ] [ DEFAULT ] | CONTAINS MEMORY_OPTIMIZED_DATA ]
    <filespec> [ ,...n ]
}
```

Attach a database

```syntaxsql
CREATE DATABASE database_name
    ON <filespec> [ ,...n ]
    FOR { { ATTACH [ WITH <attach_database_option> [ , ...n ] ] }
        | ATTACH_REBUILD_LOG }
[;]

<attach_database_option> ::=
{
      <service_broker_option>
    | RESTRICTED_USER
    | FILESTREAM ( DIRECTORY_NAME = { 'directory_name' | NULL } )
}

<service_broker_option> ::=
{
    ENABLE_BROKER
  | NEW_BROKER
  | ERROR_BROKER_CONVERSATIONS
}
```

Create a database snapshot

```syntaxsql
CREATE DATABASE database_snapshot_name
    ON
    (
        NAME = logical_file_name,
        FILENAME = 'os_file_name'
    ) [ ,...n ]
    AS SNAPSHOT OF
[;]
```

## Arguments

#### *database_name*
This is the name of the new database. Database names must be unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

*database_name* can be a maximum of 128 characters, unless a logical name is not specified for the log file. If a logical log file name is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates the *logical_file_name* and the *os_file_name* for the log by appending a suffix to *database_name*. This limits *database_name* to 123 characters so that the generated logical file name is no more than 128 characters.

If data file name is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses *database_name* as both the *logical_file_name* and as the *os_file_name*. The default path is obtained from the registry. The default path can be changed in the **Server Properties (Database Settings Page)** in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. Changing the default path requires restarting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

#### CONTAINMENT = { NONE | PARTIAL }

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later

Specifies the containment status of the database. NONE = non-contained database. PARTIAL = partially contained database.

#### ON
Specifies that the disk files used to store the data sections of the database, data files, are explicitly defined. ON is required when followed by a comma-separated list of \<filespec> items that define the data files for the primary filegroup. The list of files in the primary filegroup can be followed by an optional, comma-separated list of \<filegroup> items that define user filegroups and their files.

#### PRIMARY
Specifies that the associated \<filespec> list defines the primary file. The first file specified in the \<filespec> entry in the primary filegroup becomes the primary file. A database can have only one primary file. For more information, see [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md).

If PRIMARY is not specified, the first file listed in the CREATE DATABASE statement becomes the primary file.

#### LOG ON
Specifies that the disk files used to store the database log, log files, are explicitly defined. LOG ON is followed by a comma-separated list of \<filespec> items that define the log files. If LOG ON is not specified, one log file is automatically created, which has a size that is 25 percent of the sum of the sizes of all the data files for the database, or 512 KB, whichever is larger. This file is placed in the default log-file location. For information about this location, see [View or Change the Default Locations for Data and Log Files in SSMS](../../database-engine/configure-windows/view-or-change-the-default-locations-for-data-and-log-files.md).

LOG ON cannot be specified on a database snapshot.

#### COLLATE *collation_name*
Specifies the default collation for the database. Collation name can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the default collation of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A collation name cannot be specified on a database snapshot.

A collation name cannot be specified with the FOR ATTACH or FOR ATTACH_REBUILD_LOG clauses. For information about how to change the collation of an attached database, visit this [Microsoft Web site](https://go.microsoft.com/fwlink/?linkid=16419&kbid=325335).

For more information about the Windows and SQL collation names, see [COLLATE](~/t-sql/statements/collations.md).

> [!NOTE]
> Contained databases are collated differently than non-contained databases. For more information, see [Contained Database Collations](../../relational-databases/databases/contained-database-collations.md).

#### WITH \<option>

#### **\<filestream_option>**

#### NON_TRANSACTED_ACCESS = { **OFF** | READ_ONLY | FULL }
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.

Specifies the level of non-transactional FILESTREAM access to the database.

|Value|Description|
|-----------|-----------------|
|OFF|Non-transactional access is disabled.|
|READONLY|FILESTREAM data in this database can be read by non-transactional processes.|
|FULL|Full non-transactional access to FILESTREAM FileTables is enabled.|

#### DIRECTORY_NAME = \<directory_name>
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later

A windows-compatible directory name. This name should be unique among all the Database_Directory names in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Uniqueness comparison is case-insensitive, regardless of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation settings. This option should be set before you create a FileTable in this database.

<BR><BR>

The following options are allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

#### **DEFAULT_FULLTEXT_LANGUAGE = \<lcid> | \<language name> | \<language alias>**

  **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later

  See [Configure the default full-text language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-full-text-language-server-configuration-option.md) for a full description of this option.

#### **DEFAULT_LANGUAGE = \<lcid> | \<language name> | \<language alias>**

  **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later

  See [Configure the default language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md) for a full description of this option.

#### **NESTED_TRIGGERS = { OFF | ON}**

  **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later

  See [Configure the nested triggers Server Configuration Option](../../database-engine/configure-windows/configure-the-nested-triggers-server-configuration-option.md) for a full description of this option.

#### **TRANSFORM_NOISE_WORDS = { OFF | ON}**

  **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later

  See [transform noise words Server Configuration Option](../../database-engine/configure-windows/transform-noise-words-server-configuration-option.md)for a full description of this option.

#### **TWO_DIGIT_YEAR_CUTOFF = { 2049 | \<any year between 1753 and 9999> }**

  Four digits representing a year. 2049 is the default value. See [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) for a full description of this option.

#### **DB_CHAINING { OFF | ON }**

  When ON is specified, the database can be the source or target of a cross-database ownership chain.

  When OFF, the database cannot participate in cross-database ownership chaining. The default is OFF.

  > [!IMPORTANT]
  > The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will recognize this setting when the cross db ownership chaining server option is 0 (OFF). When cross db ownership chaining is 1 (ON), all user databases can participate in cross-database ownership chains, regardless of the value of this option. This option is set by using [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

  To set this option, requires membership in the sysadmin fixed server role. The DB_CHAINING option cannot be set on these system databases: `master`, `model`, `tempdb`.

#### **TRUSTWORTHY { OFF | ON }**

  When ON is specified, database modules (for example, views, user-defined functions, or stored procedures) that use an impersonation context can access resources outside the database.

  When OFF, database modules in an impersonation context cannot access resources outside the database. The default is OFF.

  TRUSTWORTHY is set to OFF whenever the database is attached.

  By default, all system databases except the `msdb` database have TRUSTWORTHY set to OFF. The value cannot be changed for the `model` and `tempdb` databases. We recommend that you never set the TRUSTWORTHY option to ON for the `master` database.

#### **PERSISTENT_LOG_BUFFER=ON ( DIRECTORY_NAME='' )**

  When this option is specified, the transaction log buffer is created on a volume that is located on a disk device backed by Storage Class Memory (NVDIMM-N nonvolatile storage), also known as a persistent log buffer. For more information, see [Transaction Commit latency acceleration using Storage Class Memory](/archive/blogs/sqlserverstorageengine/transaction-commit-latency-acceleration-using-storage-class-memory-in-windows-server-2016sql-server-2016-sp1). **Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and newer.

#### **LEDGER = {ON | OFF}**

When set to `ON`, it creates a ledger database, in which the integrity of all user data is protected. Only ledger tables can be created in a ledger database. The default is `OFF`. The value of the `LEDGER` option cannot be changed once the database is created. For more information, see [Configure a ledger database](../../relational-databases/security/ledger/ledger-how-to-configure-ledger-database.md).

#### CREATE DATABASE ... FOR ATTACH [ WITH \< attach_database_option > ]

Specifies that the database is created by [attaching](../../relational-databases/databases/database-detach-and-attach-sql-server.md) an existing set of operating system files. There must be a \<filespec> entry that specifies the primary file. The only other \<filespec> entries required are those for any files that have a different path from when the database was first created or last attached. A \<filespec> entry must be specified for these files.

FOR ATTACH requires the following:

- All data files (MDF and NDF) must be available.
- If multiple log files exist, they must all be available.

If a read/write database has a single log file that is currently unavailable, and if the database was shut down with no users or open transactions before the attach operation, FOR ATTACH automatically rebuilds the log file and updates the primary file. In contrast, for a read-only database, the log cannot be rebuilt because the primary file cannot be updated. Therefore, when you attach a read-only database with a log that is unavailable, you must provide the log files, or the files in the FOR ATTACH clause.

> [!NOTE]
> A database created by a more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be attached in earlier versions.

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], any full-text files that are part of the database that is being attached will be attached with the database. To specify a new path of the full-text catalog, specify the new location without the full-text operating system file name. For more information, see the Examples section.

Attaching a database that contains a FILESTREAM option of "Directory name", into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance will prompt [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to verify that the Database_Directory name is unique. If it is not, the attach operation fails with the error, `FILESTREAM Database_Directory name is not unique in this SQL Server instance`. To avoid this error, the optional parameter, *directory_name*, should be passed in to this operation.

FOR ATTACH cannot be specified on a database snapshot.

FOR ATTACH can specify the RESTRICTED_USER option. RESTRICTED_USER allows for only members of the db_owner fixed database role and dbcreator and sysadmin fixed server roles to connect to the database, but does not limit their number. Attempts by unqualified users are refused.

#### \<service_broker_option>
If the database uses [!INCLUDE[ssSB](../../includes/sssb-md.md)], use the WITH \<service_broker_option> in your FOR ATTACH clause:

Controls [!INCLUDE[ssSB](../../includes/sssb-md.md)] message delivery and the [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier for the database. [!INCLUDE[ssSB](../../includes/sssb-md.md)] options can only be specified when the FOR ATTACH clause is used.

#### ENABLE_BROKER
Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] is enabled for the specified database. That is, message delivery is started, and `is_broker_enabled` is set to true in the `sys.databases` catalog view. The database retains the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier.

#### NEW_BROKER
Creates a new `service_broker_guid` value in both `sys.databases` and the restored database. Ends all conversation endpoints with cleanup. The broker is enabled, but no message is sent to the remote conversation endpoints. Any route that references the old [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier must be re-created with the new identifier.

#### ERROR_BROKER_CONVERSATIONS
Ends all conversations with an error stating that the database is attached or restored. The broker is disabled until this operation is completed and then enabled. The database retains the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier.

When you attach a replicated database that was copied instead of being detached, consider the following:

- If you attach the database to the same server instance and version as the original database, no additional steps are required.
- If you attach the database to the same server instance but with an upgraded version, you must execute [sp_vupgrade_replication](../../relational-databases/system-stored-procedures/sp-vupgrade-replication-transact-sql.md) to upgrade replication after the attach operation is complete.
- If you attach the database to a different server instance, regardless of version, you must execute [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to remove replication after the attach operation is complete.

> [!NOTE]
> Attach works with the **vardecimal** storage format, but the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] must be upgraded to at least [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP2. You cannot attach a database using vardecimal storage format to an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about the **vardecimal** storage format, see [Data Compression](../../relational-databases/data-compression/data-compression.md).

When a database is first attached or restored to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a copy of the database master key (encrypted by the service master key) is not yet stored in the server. You must use the **OPEN MASTER KEY** statement to decrypt the database master key (DMK). Once the DMK has been decrypted, you have the option of enabling automatic decryption in the future by using the **ALTER MASTER KEY REGENERATE** statement to provision the server with a copy of the DMK, encrypted with the service master key (SMK). When a database has been upgraded from an earlier version, the DMK should be regenerated to use the newer AES algorithm. For more information about regenerating the DMK, see [ALTER MASTER KEY](../../t-sql/statements/alter-master-key-transact-sql.md). The time required to regenerate the DMK key to upgrade to AES depends upon the number of objects protected by the DMK. Regenerating the DMK key to upgrade to AES is only necessary once, and has no impact on future regenerations as part of a key rotation strategy. For information about how to upgrade a database by using attach, see [Upgrade a Database Using Detach and Attach](../../relational-databases/databases/upgrade-a-database-using-detach-and-attach-transact-sql.md).

> [!IMPORTANT]
> We recommend that you do not attach databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server, and also examine the code, such as stored procedures or other user-defined code, in the database.

> [!NOTE]
> The **TRUSTWORTHY** and **DB_CHAINING** options have no effect when attaching a database.

#### FOR ATTACH_REBUILD_LOG
Specifies that the database is created by attaching an existing set of operating system files. This option is limited to read/write databases. There must be a *\<filespec>* entry specifying the primary file. If one or more transaction log files are missing, the log file is rebuilt. The ATTACH_REBUILD_LOG automatically creates a new, 1-MB log file. This file is placed in the default log-file location. For information about this location, see [View or Change the Default Locations for Data and Log Files in SSMS](../../database-engine/configure-windows/view-or-change-the-default-locations-for-data-and-log-files.md).

> [!NOTE]
> If the log files are available, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses those files instead of rebuilding the log files.

FOR ATTACH_REBUILD_LOG requires the following conditions:

- A clean shutdown of the database.
- All data files (MDF and NDF) must be available.

> [!IMPORTANT]
> This operation breaks the log backup chain. We recommend that a full database backup be performed after the operation is completed. For more information, see [BACKUP](../../t-sql/statements/backup-transact-sql.md).

Typically, FOR ATTACH_REBUILD_LOG is used when you copy a read/write database with a large log to another server where the copy will be used mostly, or only, for read operations, and therefore requires less log space than the original database.

FOR ATTACH_REBUILD_LOG cannot be specified on a database snapshot.

For more information about attaching and detaching databases, see [Database Detach and Attach](../../relational-databases/databases/database-detach-and-attach-sql-server.md).

#### \<filespec>
Controls the file properties.

#### NAME *logical_file_name*
Specifies the logical name for the file. NAME is required when FILENAME is specified, except when specifying one of the FOR ATTACH clauses. A FILESTREAM filegroup cannot be named PRIMARY.

*logical_file_name*
Is the logical name used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when referencing the file. *Logical_file_name* must be unique in the database and comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). The name can be a character or Unicode constant, or a regular or delimited identifier.

#### FILENAME { **'**_os\_file\_name_**'** | **'**_filestream\_path_**'** }
Specifies the operating system (physical) file name.

**'** *os_file_name* **'**
Is the path and file name used by the operating system when you create the file. The file must reside on one of the following devices: the local server on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, a Storage Area Network [SAN], or an iSCSI-based network. The specified path must exist before executing the CREATE DATABASE statement. For more information, see [Database Files and Filegroups](#database-files-and-filegroups) later in this article.

SIZE, MAXSIZE, and FILEGROWTH parameters can be set when a UNC path is specified for the file.

If the file is on a raw partition, *os_file_name* must specify only the drive letter of an existing raw partition. Only one data file can be created on each raw partition.

> [!NOTE]  
> Raw partitions are not supported in SQL Server 2014 and later versions.

Data files should not be put on compressed file systems unless the files are read-only secondary files, or the database is read-only. Log files should never be put on compressed file systems.

**'** *filestream_path* **'**
For a FILESTREAM filegroup, FILENAME refers to a path where FILESTREAM data will be stored. The path up to the last folder must exist, and the last folder must not exist. For example, if you specify the path `C:\MyFiles\MyFilestreamData`, `C:\MyFiles` must exist before you run ALTER DATABASE, but the `MyFilestreamData` folder must not exist.

The filegroup and file (`<filespec>`) must be created in the same statement.

The SIZE and FILEGROWTH properties do not apply to a FILESTREAM filegroup.

#### SIZE *size*
Specifies the size of the file.

SIZE cannot be specified when the *os_file_name* is specified as a UNC path. SIZE does not apply to a FILESTREAM filegroup.

*size*
Is the initial size of the file.

When *size* is not supplied for the primary file, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses the size of the primary file in the `model` database. The default size of the `model` database is 8 MB (beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) or 1 MB (for earlier versions). When a secondary data file or log file is specified, but *size* is not specified for the file, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] makes the file 8 MB (beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) or 1 MB (for earlier versions). The size specified for the primary file must be at least as large as the primary file of the `model` database.

The kilobyte (KB), megabyte (MB), gigabyte (GB), or terabyte (TB) suffixes can be used. The default is MB. Specify a whole number. Do not include a decimal. *Size* is an integer value. For values greater than 2147483647, use larger units.

#### MAXSIZE *max_size*
Specifies the maximum size to which the file can grow. MAXSIZE cannot be specified when the *os_file_name* is specified as a UNC path.

*max_size*
Is the maximum file size. The KB, MB, GB, and TB suffixes can be used. The default is MB. Specify a whole number. Do not include a decimal. If *max_size* is not specified, the file grows until the disk is full. *Max_size* is an integer value. For values greater than 2147483647, use larger units.

UNLIMITED
Specifies that the file grows until the disk is full. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a log file specified with unlimited growth has a maximum size of 2 TB, and a data file has a maximum size of 16 TB.

> [!NOTE]
> There is no maximum size when this option is specified for a FILESTREAM container. It continues to grow until the disk is full.

#### FILEGROWTH *growth_increment*
Specifies the automatic growth increment of the file. The FILEGROWTH setting for a file cannot exceed the MAXSIZE setting. FILEGROWTH cannot be specified when the *os_file_name* is specified as a UNC path. FILEGROWTH does not apply to a FILESTREAM filegroup.

*growth_increment*
Is the amount of space added to the file every time that new space is required.

The value can be specified in MB, KB, GB, TB, or percent (%). If a number is specified without an MB, KB, or % suffix, the default is MB. When % is specified, the growth increment size is the specified percentage of the size of the file at the time the increment occurs. The size specified is rounded to the nearest 64 KB, and the minimum value is 64 KB.

A value of 0 indicates that automatic growth is off and no additional space is allowed.

If FILEGROWTH is not specified, the default values are:

|Version|Default values|
|-------------|--------------------|
|Beginning [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]|Data 64 MB. Log files 64 MB.|
|Beginning [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Data 1 MB. Log files 10%.|
|Before [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Data 10%. Log files 10%.|

#### \<filegroup>
Controls the filegroup properties. Filegroup cannot be specified on a database snapshot.

#### FILEGROUP *filegroup_name*
Is the logical name of the filegroup.

*filegroup_name*
*filegroup_name* must be unique in the database and cannot be the system-provided names PRIMARY and PRIMARY_LOG. The name can be a character or Unicode constant, or a regular or delimited identifier. The name must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

CONTAINS FILESTREAM
Specifies that the filegroup stores FILESTREAM binary large objects (BLOBs) in the file system.

DEFAULT
Specifies the named filegroup is the default filegroup in the database.

CONTAINS MEMORY_OPTIMIZED_DATA
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later

Specifies that the filegroup stores memory_optimized data in the file system. For more information, see [In-Memory Optimization Overview and Usage Scenarios](../../relational-databases/in-memory-oltp/overview-and-usage-scenarios.md). Only one MEMORY_OPTIMIZED_DATA filegroup is allowed per database. For code samples that create a filegroup to store memory-optimized data, see [Creating a Memory-Optimized Table and a Natively Compiled Stored Procedure](../../relational-databases/in-memory-oltp/creating-a-memory-optimized-table-and-a-natively-compiled-stored-procedure.md).

#### *database_snapshot_name*
Is the name of the new database snapshot. Database snapshot names must be unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and comply with the rules for identifiers. *database_snapshot_name* can be a maximum of 128 characters.

#### ON **(** NAME **=**_logical\_file\_name_**,** FILENAME **='**_os\_file\_name_**')** [ **,**... *n* ]
For creating a database snapshot, specifies a list of files in the source database. For the snapshot to work, all the data files must be specified individually. However, log files are not allowed for database snapshots. FILESTREAM filegroups are not supported by database snapshots. If a FILESTREAM data file is included in a CREATE DATABASE ON clause, the statement will fail and an error will be raised.

For descriptions of NAME and FILENAME and their values, see the descriptions of the equivalent \<filespec> values.

> [!NOTE]
> When you create a database snapshot, the other \<filespec> options and the keyword PRIMARY are disallowed.

#### AS SNAPSHOT OF *source_database_name*
Specifies that the database being created is a database snapshot of the source database specified by *source_database_name*. The snapshot and source database must be on the same instance.

For more information, see [Database Snapshots](#database-snapshots) in the Remarks section.

## Remarks

The [master database](../../relational-databases/databases/master-database.md) should be backed up whenever a user database is created, modified, or dropped.

The `CREATE DATABASE` statement must run in autocommit mode (the default transaction management mode) and is not allowed in an explicit or implicit transaction.

You can use one `CREATE DATABASE` statement to create a database and the files that store the database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implements the CREATE DATABASE statement by using the following steps:

1. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses a copy of the [model database](../../relational-databases/databases/model-database.md) to initialize the database and its metadata.
2. A service broker GUID is assigned to the database.
3. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] then fills the rest of the database with empty pages, except for pages that have internal data that records how the space is used in the database.

A maximum of 32,767 databases can be specified on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Each database has an owner that can perform special activities in the database. The owner is the user that creates the database. The database owner can be changed by using [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md).

Some database features depend on features or capabilities present in the file system for full functionality of a database. Some examples of features that depend on file system feature set include:

- DBCC CHECKDB
- FileStream
- Online backups using VSS and file snapshots
- Database snapshot creation
- Memory Optimized Data filegroup

## Database Files and Filegroups

Every database has at least two files, a *primary file* and a *transaction log file*, and at least one filegroup. A maximum of 32,767 files and 32,767 filegroups can be specified for each database.

When you create a database, make the data files as large as possible based on the maximum amount of data you expect in the database.

We recommend that you use a Storage Area Network (SAN), iSCSI-based network, or locally attached disk for the storage of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database files, because this configuration optimizes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance and reliability.

## Database Snapshots

You can use the `CREATE DATABASE` statement to create a read-only, static view, a *database snapshot* of the *source database*. A database snapshot is transactionally consistent with the source database as it existed at the time when the snapshot was created. A source database can have multiple snapshots.

> [!NOTE]
> When you create a database snapshot, the `CREATE DATABASE` statement cannot reference log files, offline files, restoring files, and defunct files.

If creating a database snapshot fails, the snapshot becomes suspect and must be deleted. For more information, see [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).

Each snapshot persists until it is deleted by using `DROP DATABASE`.

For more information, see [Database Snapshots](../../relational-databases/databases/database-snapshots-sql-server.md).

## Database options

Several database options are automatically set whenever you create a database. For a list of these options, see [ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md).

## The `model` database and creating new databases

All user-defined objects in the [model database](../../relational-databases/databases/model-database.md) are copied to all newly created databases. You can add any objects, such as tables, views, stored procedures, data types, and so on, to the `model` database to be included in all newly created databases.

When a `CREATE DATABASE <database_name>` statement is specified without additional size parameters, the primary data file is made the same size as the primary file in the `model` database.

Unless `FOR ATTACH` is specified, each new database inherits the database option settings from the `model` database. For example, the database option *auto shrink* is set to **true** in `model` and in any new databases you create. If you change the options in the `model` database, these new option settings are used in any new databases you create. Changing operations in the `model` database does not affect existing databases. If FOR ATTACH is specified on the CREATE DATABASE statement, the new database inherits the database option settings of the original database.

## View database information

You can use catalog views, system functions, and system stored procedures to return information about databases, files, and filegroups. For more information, see [System Views](../language-reference.md).

## Permissions

Requires `CREATE DATABASE`, `CREATE ANY DATABASE`, or `ALTER ANY DATABASE` permission.

To maintain control over disk use on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], permission to create databases is typically limited to a few login accounts.

The following example provides the permission to create a database to the database user Fay.

```sql
USE master;
GO
GRANT CREATE DATABASE TO [Fay];
GO
```

### Permissions on Data and Log Files

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], certain permissions are set on the data and log files of each database. The following permissions are set whenever the following operations are applied to a database:

- Attached
- Backed up
- Created
- Detached
- Modified to add a new file
- Restored

The permissions prevent the files from being accidentally tampered with if they reside in a directory that has open permissions.

> [!NOTE]
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssExpressEd2005](../../includes/ssexpressed2005-md.md)] does not set data and log file permissions.

## Examples

### A. Create a database without specifying files

The following example creates the database `mytest` and creates a corresponding primary and transaction log file. Because the statement has no \<filespec> items, the primary database file is the size of the `model` database primary file. The transaction log is set to the larger of these values: 512 KB or 25% the size of the primary data file. Because MAXSIZE is not specified, the files can grow to fill all available disk space. This example also demonstrates how to drop the database named `mytest` if it exists, before creating the `mytest` database.

```sql
USE master;
GO
IF DB_ID (N'mytest') IS NOT NULL
DROP DATABASE mytest;
GO
CREATE DATABASE mytest;
GO
-- Verify the database files and sizes
SELECT name, size, size*1.0/128 AS [Size in MBs]
FROM sys.master_files
WHERE name = N'mytest';
GO
```

### B. Create a database that specifies the data and transaction log files

The following example creates the database `Sales`. Because the keyword PRIMARY is not used, the first file (`Sales_dat`) becomes the primary file. Because neither MB nor KB is specified in the SIZE parameter for the `Sales_dat` file, it uses MB and is allocated in megabytes. The `Sales_log` file is allocated in megabytes because the `MB` suffix is explicitly stated in the `SIZE` parameter.

```sql
USE master;
GO
CREATE DATABASE Sales
ON
( NAME = Sales_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\saledat.mdf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 )
LOG ON
( NAME = Sales_log,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\salelog.ldf',
    SIZE = 5MB,
    MAXSIZE = 25MB,
    FILEGROWTH = 5MB ) ;
GO
```

### C. Create a database by specifying multiple data and transaction log files

The following example creates the database `Archive` that has three `100-MB` data files and two `100-MB` transaction log files. The primary file is the first file in the list and is explicitly specified with the `PRIMARY` keyword. The transaction log files are specified following the `LOG ON` keywords. Note the extensions used for the files in the `FILENAME` option: `.mdf` is used for primary data files, `.ndf` is used for the secondary data files, and `.ldf` is used for transaction log files. This example places the database on the `D:` drive instead of with the `master` database.

```sql
USE master;
GO
CREATE DATABASE Archive
ON
PRIMARY
    (NAME = Arch1,
    FILENAME = 'D:\SalesData\archdat1.mdf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20),
    ( NAME = Arch2,
    FILENAME = 'D:\SalesData\archdat2.ndf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20),
    ( NAME = Arch3,
    FILENAME = 'D:\SalesData\archdat3.ndf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20)
LOG ON
  (NAME = Archlog1,
    FILENAME = 'D:\SalesData\archlog1.ldf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20),
  (NAME = Archlog2,
    FILENAME = 'D:\SalesData\archlog2.ldf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20) ;
GO
```

### D. Create a database that has filegroups

The following example creates the database `Sales` that has the following filegroups:

- The primary filegroup with the files `Spri1_dat` and `Spri2_dat`. The FILEGROWTH increments for these files are specified as `15%`.
- A filegroup named `SalesGroup1` with the files `SGrp1Fi1` and `SGrp1Fi2`.
- A filegroup named `SalesGroup2` with the files `SGrp2Fi1` and `SGrp2Fi2`.

This example places the data and log files on different disks to improve performance.

```sql
USE master;
GO
CREATE DATABASE Sales
ON PRIMARY
( NAME = SPri1_dat,
    FILENAME = 'D:\SalesData\SPri1dat.mdf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 15% ),
( NAME = SPri2_dat,
    FILENAME = 'D:\SalesData\SPri2dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 15% ),
FILEGROUP SalesGroup1
( NAME = SGrp1Fi1_dat,
    FILENAME = 'D:\SalesData\SG1Fi1dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 ),
( NAME = SGrp1Fi2_dat,
    FILENAME = 'D:\SalesData\SG1Fi2dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 ),
FILEGROUP SalesGroup2
( NAME = SGrp2Fi1_dat,
    FILENAME = 'D:\SalesData\SG2Fi1dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 ),
( NAME = SGrp2Fi2_dat,
    FILENAME = 'D:\SalesData\SG2Fi2dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 )
LOG ON
( NAME = Sales_log,
    FILENAME = 'E:\SalesLog\salelog.ldf',
    SIZE = 5MB,
    MAXSIZE = 25MB,
    FILEGROWTH = 5MB ) ;
GO
```

### E. Attach a database

The following example detaches the database `Archive` created in example D, and then attaches it by using the `FOR ATTACH` clause. `Archive` was defined to have multiple data and log files. However, because the location of the files has not changed since they were created, only the primary file has to be specified in the `FOR ATTACH` clause. Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], any full-text files that are part of the database that is being attached will be attached with the database.

```sql
USE master;
GO
sp_detach_db Archive;
GO
CREATE DATABASE Archive
  ON (FILENAME = 'D:\SalesData\archdat1.mdf')
  FOR ATTACH ;
GO
```

### F. Create a database snapshot

The following example creates the database snapshot `sales_snapshot0600`. Because a database snapshot is read-only, a log file cannot be specified. In conformance with the syntax, every file in the source database is specified, and filegroups are not specified.

The source database for this example is the `Sales` database created in example D.

```sql
USE master;
GO
CREATE DATABASE sales_snapshot0600 ON
    ( NAME = SPri1_dat, FILENAME = 'D:\SalesData\SPri1dat_0600.ss'),
    ( NAME = SPri2_dat, FILENAME = 'D:\SalesData\SPri2dt_0600.ss'),
    ( NAME = SGrp1Fi1_dat, FILENAME = 'D:\SalesData\SG1Fi1dt_0600.ss'),
    ( NAME = SGrp1Fi2_dat, FILENAME = 'D:\SalesData\SG1Fi2dt_0600.ss'),
    ( NAME = SGrp2Fi1_dat, FILENAME = 'D:\SalesData\SG2Fi1dt_0600.ss'),
    ( NAME = SGrp2Fi2_dat, FILENAME = 'D:\SalesData\SG2Fi2dt_0600.ss')
AS SNAPSHOT OF Sales ;
GO
```

### G. Create a database and specify a collation name and options

The following example creates the database `MyOptionsTest`. A collation name is specified and the `TRUSTYWORTHY` and `DB_CHAINING` options are set to `ON`.

```sql
USE master;
GO
IF DB_ID (N'MyOptionsTest') IS NOT NULL
DROP DATABASE MyOptionsTest;
GO
CREATE DATABASE MyOptionsTest
COLLATE French_CI_AI
WITH TRUSTWORTHY ON, DB_CHAINING ON;
GO
--Verifying collation and option settings.
SELECT name, collation_name, is_trustworthy_on, is_db_chaining_on
FROM sys.databases
WHERE name = N'MyOptionsTest';
GO
```

### H. Attach a full-text catalog that has been moved

The following example attaches the full-text catalog `AdvWksFtCat` along with the `AdventureWorks2012` data and log files. In this example, the full-text catalog is moved from its default location to a new location `c:\myFTCatalogs`. The data and log files remain in their default locations.

```sql
USE master;
GO
--Detach the AdventureWorks2012 database
sp_detach_db AdventureWorks2012;
GO
-- Physically move the full text catalog to the new location.
--Attach the AdventureWorks2012 database and specify the new location of the full-text catalog.
CREATE DATABASE AdventureWorks2012 ON
    (FILENAME = 'c:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\AdventureWorks2012_data.mdf'),
    (FILENAME = 'c:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\AdventureWorks2012_log.ldf'),
    (FILENAME = 'c:\myFTCatalogs\AdvWksFtCat')
FOR ATTACH;
GO
```

### I. Create a database that specifies a row filegroup and two FILESTREAM filegroups

The following example creates the `FileStreamDB` database. The database is created with one row filegroup and two FILESTREAM filegroups. Each filegroup contains one file:

- `FileStreamDB_data` contains row data. It contains one file, `FileStreamDB_data.mdf` with the default path.
- `FileStreamPhotos` contains FILESTREAM data. It contains two FILESTREAM data containers, `FSPhotos`, located at `C:\MyFSfolder\Photos` and `FSPhotos2`, located at `D:\MyFSfolder\Photos`. It is marked as the default FILESTREAM filegroup.
- `FileStreamResumes` contains FILESTREAM data. It contains one FILESTREAM data container, `FSResumes`, located at `C:\MyFSfolder\Resumes`.

```sql
USE master;
GO
-- Get the SQL Server data path.
DECLARE @data_path nvarchar(256);
SET @data_path = (SELECT SUBSTRING(physical_name, 1, CHARINDEX(N'master.mdf', LOWER(physical_name)) - 1)
      FROM master.sys.master_files
      WHERE database_id = 1 AND file_id = 1);

 -- Execute the CREATE DATABASE statement.
EXECUTE ('CREATE DATABASE FileStreamDB
ON PRIMARY
    (
    NAME = FileStreamDB_data
    ,FILENAME = ''' + @data_path + 'FileStreamDB_data.mdf''
    ,SIZE = 10MB
    ,MAXSIZE = 50MB
    ,FILEGROWTH = 15%
    ),
FILEGROUP FileStreamPhotos CONTAINS FILESTREAM DEFAULT
    (
    NAME = FSPhotos
    ,FILENAME = ''C:\MyFSfolder\Photos''
-- SIZE and FILEGROWTH should not be specified here.
-- If they are specified an error will be raised.
, MAXSIZE = 5000 MB
    ),
    (
      NAME = FSPhotos2
      , FILENAME = ''D:\MyFSfolder\Photos''
      , MAXSIZE = 10000 MB
     ),
FILEGROUP FileStreamResumes CONTAINS FILESTREAM
    (
    NAME = FileStreamResumes
    ,FILENAME = ''C:\MyFSfolder\Resumes''
    )
LOG ON
    (
    NAME = FileStream_log
    ,FILENAME = ''' + @data_path + 'FileStreamDB_log.ldf''
    ,SIZE = 5MB
    ,MAXSIZE = 25MB
    ,FILEGROWTH = 5MB
    )'
);
GO
```

### J. Create a database that has a FILESTREAM filegroup with multiple files

The following example creates the `BlobStore1` database. The database is created with one row filegroup and one FILESTREAM filegroup, `FS`. The FILESTREAM filegroup contains two files, `FS1` and `FS2`. Then the database is altered by adding a third file, `FS3`, to the FILESTREAM filegroup.

```sql
USE master;
GO

CREATE DATABASE [BlobStore1]
CONTAINMENT = NONE
ON PRIMARY
(
    NAME = N'BlobStore1',
    FILENAME = N'C:\BlobStore\BlobStore1.mdf',
    SIZE = 100MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1MB
),
FILEGROUP [FS] CONTAINS FILESTREAM DEFAULT
(  
    NAME = N'FS1',
    FILENAME = N'C:\BlobStore\FS1',
    MAXSIZE = UNLIMITED
),
(
    NAME = N'FS2',
    FILENAME = N'C:\BlobStore\FS2',
    MAXSIZE = 100MB
)
LOG ON
(
    NAME = N'BlobStore1_log',
    FILENAME = N'C:\BlobStore\BlobStore1_log.ldf',
    SIZE = 100MB,
    MAXSIZE = 1GB,
    FILEGROWTH = 1MB
);
GO

ALTER DATABASE [BlobStore1]
ADD FILE
(
    NAME = N'FS3',
    FILENAME = N'C:\BlobStore\FS3',
    MAXSIZE = 100MB
)
TO FILEGROUP [FS];
GO
```

## See also

- [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md)
- [Database Detach and Attach](../../relational-databases/databases/database-detach-and-attach-sql-server.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md)
- [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md)
- [sp_detach_db](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md)
- [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md)
- [Database Snapshots](../../relational-databases/databases/database-snapshots-sql-server.md)
- [Move Database Files](../../relational-databases/databases/move-database-files.md)
- [Databases](../../relational-databases/databases/databases.md)
- [Binary Large Object (Blob) Data](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md)

::: moniker-end
::: moniker range="=azuresqldb-current"

:::row:::
    :::column:::
        [SQL Server](create-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Database \*_**
    :::column-end:::
    :::column:::
        [SQL Managed Instance](create-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-database-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Database

## Overview

In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], this statement can be used with an Azure SQL server to create a single database or a database in an elastic pool. With this statement, you specify the database name, collation, maximum size, edition, service objective, and, if applicable, the elastic pool for the new database. It can also be used to create the database in an elastic pool. Additionally, it can be used to create a copy of the database on another SQL Database server.

## Syntax

### Create a database

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE DATABASE database_name [ COLLATE collation_name ]
{
  (<edition_options> [, ...n])
}
[ WITH <with_options> [,..n]]
[;]

<with_options> ::=
{
  CATALOG_COLLATION = { DATABASE_DEFAULT | SQL_Latin1_General_CP1_CI_AS }
  | BACKUP_STORAGE_REDUNDANCY = { 'LOCAL' | 'ZONE' | 'GEO' | 'GEOZONE'}
  | LEDGER = {ON | OFF}
}

<edition_options> ::=
{

  MAXSIZE = { 100 MB | 500 MB | 1 ... 1024 ... 4096 GB }
  | ( EDITION = { 'Basic' | 'Standard' | 'Premium' | 'GeneralPurpose' | 'BusinessCritical' | 'Hyperscale' }
  | SERVICE_OBJECTIVE =
    { 'Basic' | 'S0' | 'S1' | 'S2' | 'S3' | 'S4'| 'S6'| 'S7'| 'S9'| 'S12'
      | 'P1' | 'P2' | 'P4'| 'P6' | 'P11' | 'P15'
      | 'GP_Gen4_1' | 'GP_Gen4_2' | 'GP_Gen4_3' | 'GP_Gen4_4' | 'GP_Gen4_5' | 'GP_Gen4_6'
      | 'GP_Gen4_7' | 'GP_Gen4_8' | 'GP_Gen4_9' | 'GP_Gen4_10' | 'GP_Gen4_16' | 'GP_Gen4_24'
      | 'GP_Gen5_2' | 'GP_Gen5_4' | 'GP_Gen5_6' | 'GP_Gen5_8' | 'GP_Gen5_10' | 'GP_Gen5_12' | 'GP_Gen5_14'
      | 'GP_Gen5_16' | 'GP_Gen5_18' | 'GP_Gen5_20' | 'GP_Gen5_24' | 'GP_Gen5_32' | 'GP_Gen5_40' | 'GP_Gen5_80'
      | 'GP_Fsv2_8' | 'GP_Fsv2_10' | 'GP_Fsv2_12' | 'GP_Fsv2_14' | 'GP_Fsv2_16' | 'GP_Fsv2_18'
      | 'GP_Fsv2_20' | 'GP_Fsv2_24' | 'GP_Fsv2_32' | 'GP_Fsv2_36' | 'GP_Fsv2_72'
      | 'GP_S_Gen5_1' | 'GP_S_Gen5_2' | 'GP_S_Gen5_4' | 'GP_S_Gen5_6' | 'GP_S_Gen5_8'
      | 'GP_S_Gen5_10' | 'GP_S_Gen5_12' | 'GP_S_Gen5_14' | 'GP_S_Gen5_16'
      | 'GP_S_Gen5_18' | 'GP_S_Gen5_20' | 'GP_S_Gen5_24' | 'GP_S_Gen5_32' | 'GP_S_Gen5_40'
      | 'BC_Gen4_1' | 'BC_Gen4_2' | 'BC_Gen4_3' | 'BC_Gen4_4' | 'BC_Gen4_5' | 'BC_Gen4_6'
      | 'BC_Gen4_7' | 'BC_Gen4_8' | 'BC_Gen4_9' | 'BC_Gen4_10' | 'BC_Gen4_16' | 'BC_Gen4_24'
      | 'BC_Gen5_2' | 'BC_Gen5_4' | 'BC_Gen5_6' | 'BC_Gen5_8' | 'BC_Gen5_10' | 'BC_Gen5_12' | 'BC_Gen5_14'
      | 'BC_Gen5_16' | 'BC_Gen5_18' | 'BC_Gen5_20' | 'BC_Gen5_24' | 'BC_Gen5_32' | 'BC_Gen5_40' | 'BC_Gen5_80'
      | 'BC_M_8' | 'BC_M_10' | 'BC_M_12' | 'BC_M_14' | 'BC_M_16' | 'BC_M_18'
      | 'BC_M_20' | 'BC_M_24' | 'BC_M_32' | 'BC_M_64' | 'BC_M_128'
      | 'HS_GEN4_1' | 'HS_GEN4_2' | 'HS_GEN4_4' | 'HS_GEN4_8' | 'HS_GEN4_16' | 'HS_GEN4_24'
      | 'HS_GEN5_2' | 'HS_GEN5_4' | 'HS_GEN5_8' | 'HS_GEN5_16' | 'HS_GEN5_24' | 'HS_GEN5_32' | 'HS_GEN5_48' | 'HS_GEN5_80'
      | { ELASTIC_POOL(name = <elastic_pool_name>) } })
}
```

### Copy a database

```syntaxsql
CREATE DATABASE database_name
    AS COPY OF [source_server_name.] source_database_name
    [ ( SERVICE_OBJECTIVE =
      { 'Basic' |'S0' | 'S1' | 'S2' | 'S3'| 'S4'| 'S6'| 'S7'| 'S9'| 'S12'
      | 'P1' | 'P2' | 'P4'| 'P6' | 'P11' | 'P15'
      | 'GP_Gen4_1' | 'GP_Gen4_2' | 'GP_Gen4_3' | 'GP_Gen4_4' | 'GP_Gen4_5' | 'GP_Gen4_6'
      | 'GP_Gen4_7' | 'GP_Gen4_8' | 'GP_Gen4_9' | 'GP_Gen4_10' | 'GP_Gen4_16' | 'GP_Gen4_24'
      | 'GP_Gen5_2' | 'GP_Gen5_4' | 'GP_Gen5_6' | 'GP_Gen5_8' | 'GP_Gen5_10' | 'GP_Gen5_12' | 'GP_Gen5_14'
      | 'GP_Gen5_16' | 'GP_Gen5_18' | 'GP_Gen5_20' | 'GP_Gen5_24' | 'GP_Gen5_32' | 'GP_Gen5_40' | 'GP_Gen5_80'
      | 'GP_Fsv2_8' | 'GP_Fsv2_10' | 'GP_Fsv2_12' | 'GP_Fsv2_14' | 'GP_Fsv2_16' | 'GP_Fsv2_18'
      | 'GP_Fsv2_20' | 'GP_Fsv2_24' | 'GP_Fsv2_32' | 'GP_Fsv2_36' | 'GP_Fsv2_72'
      | 'GP_S_Gen5_1' | 'GP_S_Gen5_2' | 'GP_S_Gen5_4' | 'GP_S_Gen5_6' | 'GP_S_Gen5_8'
      | 'GP_S_Gen5_10' | 'GP_S_Gen5_12' | 'GP_S_Gen5_14' | 'GP_S_Gen5_16'
      | 'GP_S_Gen5_18' | 'GP_S_Gen5_20' | 'GP_S_Gen5_24' | 'GP_S_Gen5_32' | 'GP_S_Gen5_40'
      | 'BC_Gen4_1' | 'BC_Gen4_2' | 'BC_Gen4_3' | 'BC_Gen4_4' | 'BC_Gen4_5' | 'BC_Gen4_6'
      | 'BC_Gen4_7' | 'BC_Gen4_8' | 'BC_Gen4_9' | 'BC_Gen4_10' | 'BC_Gen4_16' | 'BC_Gen4_24'
      | 'BC_Gen5_2' | 'BC_Gen5_4' | 'BC_Gen5_6' | 'BC_Gen5_8' | 'BC_Gen5_10' | 'BC_Gen5_12' | 'BC_Gen5_14'
      | 'BC_Gen5_16' | 'BC_Gen5_18' | 'BC_Gen5_20' | 'BC_Gen5_24' | 'BC_Gen5_32' | 'BC_Gen5_40' | 'BC_Gen5_80'
      | 'BC_M_8' | 'BC_M_10' | 'BC_M_12' | 'BC_M_14' | 'BC_M_16' | 'BC_M_18'
      | 'BC_M_20' | 'BC_M_24' | 'BC_M_32' | 'BC_M_64' | 'BC_M_128'
      | 'HS_GEN4_1' | 'HS_GEN4_2' | 'HS_GEN4_4' | 'HS_GEN4_8' | 'HS_GEN4_16' | 'HS_GEN4_24'
      | 'HS_GEN5_2' | 'HS_GEN5_4' | 'HS_GEN5_8' | 'HS_GEN5_16' | 'HS_GEN5_24' | 'HS_GEN5_32' | 'HS_GEN5_48' | 'HS_GEN5_80'
      | { ELASTIC_POOL(name = <elastic_pool_name>) } })
   ]
[;]
```

## Arguments

#### *database_name*
The name of the new database. This name must be unique on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and comply with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules for identifiers. For more information, see [Identifiers](../../relational-databases/databases/database-identifiers.md).

#### *Collation_name*
Specifies the default collation for the database. Collation name can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the default collation, which is SQL_Latin1_General_CP1_CI_AS.

For more information about the Windows and SQL collation names, [COLLATE (Transact-SQL)](../../t-sql/statements/collations.md).

#### CATALOG_COLLATION
Specifies the default collation for the metadata catalog. *DATABASE_DEFAULT* specifies that the metadata catalog used for system views and system tables be collated to match the default collation for the database. This is the behavior found in SQL Server.

*SQL_Latin1_General_CP1_CI_AS* specifies that the metadata catalog used for system views and tables be collated to a fixed SQL_Latin1_General_CP1_CI_AS collation. This is the default setting on Azure SQL Database if unspecified.

#### BACKUP_STORAGE_REDUNDANCY
Specifies how the point-in-time restore and long-term retention backups for a database are replicated. Geo restore or ability to recover from regional outage is only available when database is created with 'GEO' backup storage redundancy. Unless explicitly specified, databases created with T-SQL use geo-redundant backup storage. 

> [!IMPORTANT]
> BACKUP_STORAGE_REDUNDANCY option for Azure SQL Database is available in public preview in Brazil South and generally available in Southeast Asia Azure region only.  

#### LEDGER = {ON | OFF}

When set to `ON`, it creates a ledger database, in which the integrity of all user data is protected. Only ledger tables can be created in a ledger database. The default is `OFF`. The value of the `LEDGER` option cannot be changed once the database is created. For more information, see [Configure a ledger database](../../relational-databases/security/ledger/ledger-how-to-configure-ledger-database.md).

#### MAXSIZE
Specifies the maximum size of the database. MAXSIZE must be valid for the specified EDITION (service tier) Following are the supported MAXSIZE values and defaults (D) for the service tiers.

> [!NOTE]
> The **MAXSIZE** argument does not apply to single databases in the Hyperscale service tier. Hyperscale tier databases grow as needed, up to 100 TB. The SQL Database service adds storage automatically - you do not need to set a maximum size.

**DTU model for single and pooled databases on a SQL Database server**

|**MAXSIZE**|**Basic**|**S0-S2**|**S3-S12**|**P1-P6**| **P11-P15** |
|:---|:---|:---|:---|:---|:---|
|100 MB|√|√|√|√|√|
|500 MB|√|√|√|√|√|
|1 GB|√|√|√|√|√|
|2 GB|√ (D)|√|√|√|√|
|5 GB|N/A|√|√|√|√|
|10 GB|N/A|√|√|√|√|
|20 GB|N/A|√|√|√|√|
|30 GB|N/A|√|√|√|√|
|40 GB|N/A|√|√|√|√|
|50 GB|N/A|√|√|√|√|
|100 GB|N/A|√|√|√|√|
|150 GB|N/A|√|√|√|√|
|200 GB|N/A|√|√|√|√|
|250 GB|N/A|√ (D)|√ (D)|√|√|
|300 GB|N/A|N/A|√|√|√|
|400 GB|N/A|N/A|√|√|√|
|500 GB|N/A|N/A|√|√ (D)|√|
|750 GB|N/A|N/A|√|√|√|
|1024 GB|N/A|N/A|√|√|√ (D)|
|From 1024 GB up to 4096 GB in increments of 256 GB* |N/A|N/A|N/A|N/A|√|

\* P11 and P15 allow MAXSIZE up to 4 TB with 1024 GB being the default size. P11 and P15 can use up to 4 TB of included storage at no additional charge. In the Premium tier, MAXSIZE greater than 1 TB is currently available in the following regions: US East2, West US, US Gov Virginia, West Europe, Germany Central, South East Asia, Japan East, Australia East, Canada Central, and Canada East. For additional details regarding resource limitations for the DTU model, see [DTU resource limits](/azure/sql-database/sql-database-dtu-resource-limits).

The MAXSIZE value for the DTU model, if specified, has to be a valid value shown in the previous table for the service tier specified.

**vCore model**

**General purpose - provisioned compute - Gen4 (part 1)**

|MAXSIZE|GP_Gen4_1|GP_Gen4_2|GP_Gen4_3|GP_Gen4_4|GP_Gen4_5|GP_Gen4_6|
|:----- | ------: |-------: |-------: |-------: |-------: |--------:|
|Max data size (GB)|1024|1024|1024|1536|1536|1536|

**General purpose - provisioned compute - Gen4 (part 2)**

|MAXSIZE|GP_Gen4_7|GP_Gen4_8|GP_Gen4_9|GP_Gen4_10|GP_Gen4_16|GP_Gen4_24
|:----- | ------: |-------: |-------: |-------: |-------: |--------:|
|Max data size (GB)|1536|3072|3072|3072|4096|4096|

**General purpose - provisioned compute - Gen5 (part 1)**

|MAXSIZE|GP_Gen5_2|GP_Gen5_4|GP_Gen5_6|GP_Gen5_8|GP_Gen5_10|GP_Gen5_12|GP_Gen5_14|
|:----- | ------: |-------: |-------: |-------: |--------: |---------:|--------: |
|Max data size (GB)|1024|1024|1024|1536|1536|1536|1536|

**General purpose - provisioned compute - Gen5 (part 2)**

|MAXSIZE|GP_Gen5_16|GP_Gen5_18|GP_Gen5_20|GP_Gen5_24|GP_Gen5_32|GP_Gen5_40|GP_Gen5_80|
|:----- | ------: |-------: |-------: |-------: |--------: |---------:|--------: |
|Max data size (GB)|3072|3072|3072|4096|4096|4096|4096|

**General purpose - provisioned compute - Fsv2-series (part 1)**

|MAXSIZE|GP_Fsv2_8|GP_Fsv2_10|GP_Fsv2_12|GP_Fsv2_14|GP_Fsv2_16|GP_Fsv2_18|
|:----- | ------: | ------: | ------: | ------: | ------: | ------: |
|Max data size (GB)|1024|1024|1024|1024|1536|1536|

**General purpose - provisioned compute - Fsv2-series (part 2)**

|MAXSIZE|GP_Fsv2_20|GP_Fsv2_24|GP_Fsv2_32|GP_Fsv2_36|GP_Fsv2_72|
|:----- | ------: | ------: | ------: | ------: | ------: |
|Max data size (GB)|1536|1536|3072|3072|4096|

**General purpose - serverless compute - Gen5 (part 1)**

|MAXSIZE|GP_S_Gen5_1|GP_S_Gen5_2|GP_S_Gen5_4|GP_S_Gen5_6|GP_S_Gen5_8|
|:----- | ------: |-------: |-------: |-------: |--------: |
|Max vCores|1|2|4|6|8|

**General purpose - serverless compute - Gen5 (part 2)**

|MAXSIZE|GP_S_Gen5_10|GP_S_Gen5_12|GP_S_Gen5_14|GP_S_Gen5_16|
|:----- | ------: |-------: |-------: |-------: |
|Max vCores|10|12|14|16|

**General purpose - serverless compute - Gen5 (part 3)**

|MAXSIZE|GP_S_Gen5_18|GP_S_Gen5_20|GP_S_Gen5_24|GP_S_Gen5_32|GP_S_Gen5_40|
|:----- | ------: |-------: |-------: |-------: |--------: |
|Max vCores|18|20|24|32|40|

**Business critical - provisioned compute - Gen4 (part 1)**

|Compute size (service objective)|BC_Gen4_1|BC_Gen4_2|BC_Gen4_3|BC_Gen4_4|BC_Gen4_5|BC_Gen4_6|
|:--------------- | ------: |-------: |-------: |-------: |-------: |-------: |
|Max data size (GB)|1024|1024|1024|1024|1024|1024|

**Business critical - provisioned compute - Gen4 (part 2)**

|Compute size (service objective)|BC_Gen4_7|BC_Gen4_8|BC_Gen4_9|BC_Gen4_10|BC_Gen4_16|BC_Gen4_24|
|:--------------- | ------: |-------: |-------: |--------: |--------: |--------: |
|Max data size (GB)|1024|1024|1024|1024|1024|1024|

**Business critical - provisioned compute - Gen5 (part 1)**

|MAXSIZE|BC_Gen5_2|BC_Gen5_4|BC_Gen5_6|BC_Gen5_8|BC_Gen5_10|BC_Gen5_12|BC_Gen5_14|
|:----- | ------: |-------: |-------: |-------: |---------: |--------:|--------: |
|Max data size (GB)|1024|1024|1024|1536|1536|1536|1536|

**Business critical - provisioned compute - Gen5 (part 2)**

|MAXSIZE|BC_Gen5_16|BC_Gen5_18|BC_Gen5_20|BC_Gen5_24|BC_Gen5_32|BC_Gen5_40|BC_Gen5_80|
|:----- | -------: |--------: |--------: |--------: |--------: |---------:|--------: |
|Max data size (GB)|3072|3072|3072|4096|4096|4096|4096|

**Business critical - provisioned compute - M-series (part 1)**

|MAXSIZE|BC_M_8|BC_M_10|BC_M_12|BC_M_14|BC_M_16|BC_M_18|
|:----- | -------: | -------: | -------: | -------: | -------: | -------: |
|Max data size (GB)|512|640|768|896|1024|1152|

**Business critical - provisioned compute - M-series (part 2)**

|MAXSIZE|BC_M_20|BC_M_24|BC_M_32|BC_M_64|BC_M_128|
|:----- | -------: | -------: | -------: | -------: | -------: |
|Max data size (GB)|1280|1536|2048|4096|4096|

If no `MAXSIZE` value is set when using the vCore model, the default is 32 GB. For additional details regarding resource limitations for vCore model, see [vCore resource limits](/azure/sql-database/sql-database-dtu-resource-limits).

#### EDITION
Specifies the service tier of the database.

Single and pooled databases. The available values are: 'Basic', 'Standard', 'Premium', 'GeneralPurpose', 'BusinessCritical', and 'Hyperscale'.

The following rules apply to MAXSIZE and EDITION arguments:

- If EDITION is specified but MAXSIZE is not specified, the default value for the edition is used. For example, if the EDITION is set to Standard, and the MAXSIZE is not specified, then the MAXSIZE is automatically set to 250 MB.
- If neither MAXSIZE nor EDITION is specified, the EDITION is set to `GeneralPurpose`, and MAXSIZE is set to 32 GB.

#### SERVICE_OBJECTIVE

- **For single and pooled databases**

  - Specifies the compute size (service objective). Available values for service objective are: `S0`, `S1`, `S2`, `S3`, `S4`, `S6`, `S7`, `S9`, `S12`, `P1`, `P2`, `P4`, `P6`, `P11`, `P15`, `GP_GEN4_1`, `GP_GEN4_2`, `GP_GEN4_3`, `GP_GEN4_4`, `GP_GEN4_5`, `GP_GEN4_6`, `GP_GEN4_7`, `GP_GEN4_8`, `GP_GEN4_7`, `GP_GEN4_8`, `GP_GEN4_9`, `GP_GEN4_10`, `GP_GEN4_16`, `GP_GEN4_24`, `BC_GEN4_1`, `BC_GEN4_2`, `BC_GEN4_3`, `BC_GEN4_4`, `BC_GEN4_5`, `BC_GEN4_6`, `BC_GEN4_7`, `BC_GEN4_8`, `BC_GEN4_9`, `BC_GEN4_10`, `BC_GEN4_16`, `BC_GEN4_24`, `GP_Gen5_2`, `GP_Gen5_4`, `GP_Gen5_6`, `GP_Gen5_8`, `GP_Gen5_10`, `GP_Gen5_12`, `GP_Gen5_14`, `GP_Gen5_16`, `GP_Gen5_18`, `GP_Gen5_20`, `GP_Gen5_24`, `GP_Gen5_32`, `GP_Gen5_40`, `GP_Gen5_80`, `GP_Fsv2_8`, `GP_Fsv2_10`, `GP_Fsv2_12`, `GP_Fsv2_14`, `GP_Fsv2_16`, `GP_Fsv2_18`, `GP_Fsv2_20`, `GP_Fsv2_24`, `GP_Fsv2_32`, `GP_Fsv2_36`, `GP_Fsv2_72`, `BC_Gen5_2`, `BC_Gen5_4`, `BC_Gen5_6`, `BC_Gen5_8`, `BC_Gen5_10`, `BC_Gen5_12`, `BC_Gen5_14`, `BC_Gen5_16`, `BC_Gen5_18`, `BC_Gen5_20`, `BC_Gen5_24`, `BC_Gen5_32`,`BC_Gen5_40`, `BC_Gen5_80`, `BC_M_8`, `BC_M_10`, `BC_M_12`, `BC_M_14`, `BC_M_16`, `BC_M_18`, `BC_M_20`, `BC_M_24`, `BC_M_32`, `BC_M_64`, `BC_M_128`.

- **For serverless databases**
- 
  - Specifies the compute size (service objective). Available values for service objective are: `GP_S_Gen5_1`, `GP_S_Gen5_2`, `GP_S_Gen5_4`, `GP_S_Gen5_6`, `GP_S_Gen5_8`, `GP_S_Gen5_10`, `GP_S_Gen5_12`, `GP_S_Gen5_14`, `GP_S_Gen5_16`, `GP_S_Gen5_18`, `GP_S_Gen5_20`, `GP_S_Gen5_24`, `GP_S_Gen5_32`, `GP_S_Gen5_40`.

- **For single databases in the Hyperscale service tier**

  - Specifies the compute size (service objective). Available values for service objective are: `HS_GEN4_1` `HS_GEN4_2` `HS_GEN4_4` `HS_GEN4_8` `HS_GEN4_16`, `HS_GEN4_24`, `HS_Gen5_2`, `HS_Gen5_4`, `HS_Gen5_8`, `HS_Gen5_16`, `HS_Gen5_24`, `HS_Gen5_32`, `HS_Gen5_48`, `HS_Gen5_80`.

For service objective descriptions and more information about the size, editions, and the service objectives combinations, see [Azure SQL Database Service Tiers](/azure/sql-database/sql-database-service-tiers). If the specified SERVICE_OBJECTIVE is not supported by the EDITION, you receive an error. To change the SERVICE_OBJECTIVE value from one tier to another (for example from S1 to P1), you must also change the EDITION value. For service objective descriptions and more information about the size, editions, and the service objectives combinations, see [Azure SQL Database Service Tiers and Performance Levels](/azure/azure-sql/database/purchasing-models), [DTU resource limits](/azure/sql-database/sql-database-dtu-resource-limits) and [vCore resource limits](/azure/sql-database/sql-database-dtu-resource-limits). Support for PRS service objectives has been removed. For questions, use this e-mail alias: premium-rs@microsoft.com.

#### ELASTIC_POOL (name = \<elastic_pool_name>)
**Applies to:** Single and pooled databases only. Does not apply to databases in the Hyperscale service tier.
To create a new database in an elastic database pool, set the SERVICE_OBJECTIVE of the database to ELASTIC_POOL and provide the name of the pool. For more information, see [Create and manage a SQL Database elastic pool](/azure/azure-sql/database/elastic-pool-overview).

#### AS COPY OF [source_server_name.]source_database_name
**Applies to:** Single and pooled databases only.
For copying a database to the same or a different [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server.

*source_server_name*
The name of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server where the source database is located. This parameter is optional when the source database and the destination database are to be located on the same [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server.

> [!NOTE]
> The `AS COPY OF` argument does not support the fully qualified unique domain names. In other words, if your server's fully qualified domain name is `serverName.database.windows.net`, use only `serverName` during database copy.

*source_database_name*

The name of the database that is to be copied.

## Remarks

Databases in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] have several default settings that are set when the database is created. For more information about these default settings, see the list of values in [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md).

`MAXSIZE` provides the ability to limit the size of the database. If the size of the database reaches its `MAXSIZE`, you receive error code 40544. When this occurs, you cannot insert or update data, or create new objects (such as tables, stored procedures, views, and functions). However, you can still read and delete data, truncate tables, drop tables and indexes, and rebuild indexes. You can then update `MAXSIZE` to a value larger than your current database size or delete some data to free storage space. There might be as much as a fifteen-minute delay before you can insert new data.

To change the size, edition, or service objective values later, use [ALTER DATABASE (Azure SQL Database)](../../t-sql/statements/alter-database-transact-sql.md?view=azuresqldb-current&preserve-view=true).

The `CATALOG_COLLATION` argument is only available during database creation.

## Database Copies

**Applies to:** Single and pooled databases only.

Copying a database using the `CREATE DATABASE` statement is an asynchronous operation. Therefore, a connection to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server is not needed for the full duration of the copy process. The `CREATE DATABASE` statement returns control to the user after the entry in `sys.databases` is created but before the database copy operation is complete. In other words, the `CREATE DATABASE` statement returns successfully when the database copy is still in progress.

- Monitoring the copy process on an [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] server: Query the `percentage_complete` or `replication_state_desc` columns in the [dm_database_copies](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md) or the `state` column in the **sys.databases** view. The [sys.dm_operation_status](../../relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md) view can be used as well as it returns the status of database operations including database copy.

At the time the copy process completes successfully, the destination database is transactionally consistent with the source database.

The following syntax and semantic rules apply to your use of the `AS COPY OF` argument:

- The source server name and the server name for the copy target might be the same or different. When they are the same, this parameter is optional and the server context of the current session is used by default.
- The source and destination database names must be specified, unique, and comply with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules for identifiers. For more information, see [Identifiers](../../relational-databases/databases/database-identifiers.md).
- The `CREATE DATABASE` statement must be executed within the context of the `master` database of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server where the new database will be created.
- After the copying completes, the destination database must be managed as an independent database. You can execute the `ALTER DATABASE` and `DROP DATABASE` statements against the new database independently of the source database. You can also copy the new database to another new database.
- The source database might continue to be accessed while the database copy is in progress.

For more information, see [Create a copy of an Azure SQL database using Transact-SQL](/azure/azure-sql/database/database-copy).

> [!IMPORTANT]
> By default, the database copy is created with the same backup storage redundancy as that of the source database. Changing the backup storage redundancy while creating a database copy is not supported via T-SQL. 


## Permissions

To create a database, the user login must be one of the following principals:

- The server-level principal login
- The Azure AD administrator for the local Azure SQL Server
- A login that is a member of the `dbmanager` database role

**Additional requirements for using `CREATE DATABASE ... AS COPY OF` syntax:** The login executing the statement on the local server must also be at least the `db_owner` on the source server. If the login is based on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication, the login executing the statement on the local server must have a matching login on the source [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server, with an identical name and password.

## Examples

### Simple example

A simple example for creating a database.

```sql
CREATE DATABASE TestDB1;
```

### Simple example with edition

A simple example for creating a general purpose database.

```sql
CREATE DATABASE TestDB2
( EDITION = 'GeneralPurpose' );
```

### Example with additional options

An example using multiple options.

```sql
CREATE DATABASE hito
COLLATE Japanese_Bushu_Kakusu_100_CS_AS_KS_WS
( MAXSIZE = 500 MB, EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_GEN4_8' ) ;
```

### Create a database copy

An example creating a copy of a database.

**Applies to:** Single and pooled databases only.

```sql
CREATE DATABASE escuela
AS COPY OF school;
```

### Create a database in an elastic pool

Creates new database in pool named S3M100:

**Applies to:** Single and pooled databases only.

```sql
CREATE DATABASE db1 ( SERVICE_OBJECTIVE = ELASTIC_POOL ( name = S3M100 ) ) ;
```

### Create a copy of a database on another logical server

The following example creates a copy of the `db_original` database named `db_copy` in the P2 compute size (service objective) for a single database. This is true regardless of whether `db_original` is in an elastic pool or a compute size (service objective) for a single database.

**Applies to:** Single and pooled databases only.

```sql
CREATE DATABASE db_copy
  AS COPY OF ozabzw7545.db_original ( SERVICE_OBJECTIVE = 'P2' );
```

The following example creates a copy of the `db_original` database, named `db_copy` in an elastic pool named `ep1`. This is true regardless of whether `db_original` is in an elastic pool or a compute size (service objective) for a single database. If `db_original` is in an elastic pool with a different name, then `db_copy` is still created in `ep1`.

**Applies to:** Single and pooled databases only.

```sql
CREATE DATABASE db_copy
  AS COPY OF ozabzw7545.db_original
  (SERVICE_OBJECTIVE = ELASTIC_POOL( name = ep1 ) ) ;
```

### Create database with specified catalog collation value

The following example sets the catalog collation to DATABASE_DEFAULT during database creation, which sets the catalog collation to be the same as the database collation.

```sql
CREATE DATABASE TestDB3 COLLATE Japanese_XJIS_140 (MAXSIZE = 100 MB, EDITION = 'Basic')
  WITH CATALOG_COLLATION = DATABASE_DEFAULT
```

### Create database using zone-redundancy for backups

The following example sets zone-redundancy for database backups. Both point-in-time restore backups and long-term retention backups (if configured) will use the same backup storage redundancy.

```sql
CREATE DATABASE test_zone_redundancy 
  WITH BACKUP_STORAGE_REDUNDANCY = 'ZONE';
```

### Create a ledger database

```sql
CREATE DATABASE MyLedgerDB ( EDITION = 'GeneralPurpose' ) WITH LEDGER = ON;
```

## See also

- [sys.dm_database_copies - Azure SQL Database](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md)
- [ALTER DATABASE (Azure SQL Database)](alter-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)

::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](create-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Managed Instance \*_**
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-database-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure SQL Managed Instance

## Overview

In [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], this statement is used to create a database. When creating a database on a managed instance, you specify the database name and collation.

## Syntax

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE DATABASE database_name [ COLLATE collation_name ]
[;]
```

> [!IMPORTANT]
> To add files or set containment for a database in a managed instance, use the [ALTER DATABASE](alter-database-transact-sql.md?tabs=sqldbmi) statement.

## Arguments

#### *database_name*
The name of the new database. This name must be unique on the SQL server and comply with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules for identifiers. For more information, see [Identifiers](../../relational-databases/databases/database-identifiers.md).

#### *Collation_name*
Specifies the default collation for the database. Collation name can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the default collation, which is SQL_Latin1_General_CP1_CI_AS.

For more information about the Windows and SQL collation names, [COLLATE (Transact-SQL)](../../t-sql/statements/collations.md).

## Remarks

Databases in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] have several default settings that are set when the database is created. For more information about these default settings, see the list of values in [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md).

> [!IMPORTANT]
> The `CREATE DATABASE` statement must be the only statement in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch.

The following are `CREATE DATABASE` limitations:

- Files and filegroups cannot be defined.
- `WITH`options are not supported.

  > [!TIP]
  > As workaround, use [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true). after `CREATE DATABASE` to set database options and to add files.

## Permissions

To create a database, a login must be one of the following:

- The server-level principal login
- The Azure AD administrator for the local Azure SQL Server
- A login that is a member of the `dbcreator` database role

## Examples

### Simple Example

A simple example for creating a database.

```sql
CREATE DATABASE TestDB1;
```

## See also

See [ALTER DATABASE](alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](create-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](create-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_**
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-database-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

## Overview

In Azure Synapse, this statement can be used with an Azure SQL Database server to create a dedicated SQL pool. With this statement, you specify the database name, collation, maximum size, edition, and service objective.

 - CREATE DATABASE is supported for standalone dedicated SQL pools (formerly SQL DW) using Gen2 service levels.
 - CREATE DATABASE is not supported for dedicated SQL pools in an Azure Synapse Analytics workspace. Instead, [use the Azure portal](../../azure-data-studio/quickstart-sql-dw.md). 
 - CREATE DATABASE is supported for serverless SQL pools in Azure Synapse Analytics.

## Syntax

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

### [Dedicated SQL pool](#tab/sqlpool)

```syntaxsql
CREATE DATABASE database_name [ COLLATE collation_name ]
(
    [ MAXSIZE = {
          250 | 500 | 750 | 1024 | 5120 | 10240 | 20480 | 30720
        | 40960 | 51200 | 61440 | 71680 | 81920 | 92160 | 102400
        | 153600 | 204800 | 245760
      } GB ,
    ]
    EDITION = 'datawarehouse',
    SERVICE_OBJECTIVE = {
          'DW100c' | 'DW200c' | 'DW300c' | 'DW400c' | 'DW500c'
        | 'DW1000c' | 'DW1500c' | 'DW2000c' | 'DW2500c' | 'DW3000c' | 'DW5000c'
        | 'DW6000c' | 'DW7500c' | 'DW10000c' | 'DW15000c' | 'DW30000c'
    }
)
[;]
```

### [Serverless SQL pool](#tab/sqlod)
```syntaxsql
CREATE DATABASE database_name [ COLLATE collation_name ]
[;] 
```
---

## Arguments

#### *database_name*
The name of the new database. This name must be unique on the SQL server, which can host both [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] databases and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] databases, and comply with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules for identifiers. For more information, see [Identifiers](../../relational-databases/databases/database-identifiers.md).

#### *collation_name*
Specifies the default collation for the database. Collation name can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the default collation, which is SQL_Latin1_General_CP1_CI_AS.

For more information about the Windows and SQL collation names, see [COLLATE (Transact-SQL)](./collations.md).

#### *MAXSIZE*
The default is 245,760 GB (240 TB).

**Applies to:** Optimized for Compute Gen1

The maximum allowable size for the database. The database cannot grow beyond MAXSIZE.

**Applies to:** Optimized for Compute Gen2

The maximum allowable size for rowstore data in the database. Data stored in rowstore tables, a columnstore index's deltastore, or a nonclustered index on a clustered columnstore index cannot grow beyond MAXSIZE. Data compressed into columnstore format does not have a size limit and is not constrained by MAXSIZE.

#### *EDITION*
Specifies the service tier of the database. For [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] use `datawarehouse`.

#### SERVICE_OBJECTIVE
Specifies the compute size (service objective). The service levels for Gen2 are measured in compute data warehouse units (cDWU), for example `DW2000c`. Gen1 service levels are measured in DWUs, for example `DW2000`. For more information about service objectives for Azure Synapse, see [Data Warehouse Units (DWUs)](/azure/sql-data-warehouse/what-is-a-data-warehouse-unit-dwu-cdwu#service-level-objective). Gen1 service objectives (no longer listed) are no longer supported, you may receive an error: `Azure SQL Data Warehouse Gen1 has been deprecated in this region. Please use SQL Analytics in Azure Synapse.`

## General Remarks

Use [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) to see the database properties.

Use [ALTER DATABASE - Azure Synapse Analytics](../../t-sql/statements/alter-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true) to change the max size, or service objective values later.

Azure Synapse is set to COMPATIBILITY_LEVEL 130 and cannot be changed. For more information, see [Improved Query Performance with Compatibility Level 130 in Azure SQL Database](./alter-database-transact-sql-compatibility-level.md).

## Permissions

Required permissions:

- Server level principal login, created by the provisioning process, or
- Member of the `dbmanager` database role.

## Error handling

If the size of the database reaches MAXSIZE you will receive error code 40544. When this occurs, you cannot insert and update data, or create new objects (such as tables, stored procedures, views, and functions). You can still read and delete data, truncate tables, drop tables and indexes, and rebuild indexes. You can then update MAXSIZE to a value larger than your current database size or delete some data to free storage space. There might be as much as a fifteen-minute delay before you can insert new data.

## Limitations and restrictions

You must be connected to the `master` database to create a new database.

The `CREATE DATABASE` statement must be the only statement in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch.

You cannot change the database collation after the database is created.

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)]

### A. Simple example
A simple example for creating a standalone dedicated SQL pool (formerly SQL DW). This creates the database with the smallest max size (10,240 GB), the default collation (SQL_Latin1_General_CP1_CI_AS), and the smallest Gen2 service objective (DW100c).

```sql
CREATE DATABASE TestDW
(EDITION = 'datawarehouse', SERVICE_OBJECTIVE='DW100c');
```

### B. Create a data warehouse database with all the options

An example of creating a 10-terabyte standalone dedicated SQL pool (formerly SQL DW).

```sql
CREATE DATABASE TestDW COLLATE Latin1_General_100_CI_AS_KS_WS
(MAXSIZE = 10240 GB, EDITION = 'datawarehouse', SERVICE_OBJECTIVE = 'DW1000c');
```

### C. Simple example in a Synapse Analytics serverless SQL pool

This creates the database in the serverless pool, specifying a collation (Latin1_General_100_CI_AS_KS_WS).

```sql
CREATE DATABASE TestDW COLLATE Latin1_General_100_CI_AS_KS_WS
```

## See also

- [ALTER DATABASE (Azure Synapse Analytics)](../../t-sql/statements/alter-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
- [CREATE TABLE (Azure Synapse Analytics)](../../t-sql/statements/create-table-azure-sql-data-warehouse.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)

::: moniker-end
::: moniker range=">=aps-pdw-2016"

:::row:::
    :::column:::
        [SQL Server](create-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](create-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Analytics Platform<br />System (PDW) \*_**
    :::column-end:::
:::row-end:::

&nbsp;

## Analytics Platform System

## Overview

In Analytics Platform System, this statement is used to create a new database on an Analytics Platform System appliance. Use this statement to create all files associated with an appliance database and to set maximum size and auto-growth options for the database tables and transaction log.

## Syntax

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE DATABASE database_name
WITH (
    [ AUTOGROW = ON | OFF , ]
    REPLICATED_SIZE = replicated_size [ GB ] ,
    DISTRIBUTED_SIZE = distributed_size [ GB ] ,
    LOG_SIZE = log_size [ GB ] )
[;]
```

## Arguments

#### *database_name*
The name of the new database. For more information on permitted database names, see "Object Naming Rules" and "Reserved Database Names" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].

#### AUTOGROW = ON | **OFF**
Specifies whether the *replicated_size*, *distributed_size*, and *log_size* parameters for this database will automatically grow as needed beyond their specified sizes. Default value is **OFF**.

If AUTOGROW is ON, *replicated_size*, *distributed_size*, and *log_size* will grow as required (not in blocks of the initial specified size) with each data insert, update, or other action that requires more storage than has already been allocated.

If AUTOGROW is OFF, the sizes will not grow automatically. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will return an error when attempting an action that requires *replicated_size*, *distributed_size*, or *log_size* to grow beyond their specified value.

AUTOGROW is either ON for all sizes or OFF for all sizes. For example, it is not possible to set AUTOGROW ON for *log_size*, but not set it for *replicated_size*.

#### *replicated_size* [ GB ]
A positive number. Sets the size (in integer or decimal gigabytes) for the total space allocated to replicated tables and corresponding data *on each Compute node*. For minimum and maximum *replicated_size* requirements, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].

If AUTOGROW is ON, replicated tables will be permitted to grow beyond this limit.

If AUTOGROW is OFF, an error will be returned if a user attempts to create a new replicated table, insert data into an existing replicated table, or update an existing replicated table in a manner that would increase the size beyond *replicated_size*.

#### *distributed_size* [ GB ]
A positive number. The size, in integer or decimal gigabytes, for the total space allocated to distributed tables (and corresponding data) *across the appliance*. For minimum and maximum *distributed_size* requirements, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].

If AUTOGROW is ON, distributed tables will be permitted to grow beyond this limit.

If AUTOGROW is OFF, an error will be returned if a user attempts to create a new distributed table, insert data into an existing distributed table, or update an existing distributed table in a manner that would increase the size beyond *distributed_size*.

#### *log_size* [ GB ]
A positive number. The size (in integer or decimal gigabytes) for the transaction log *across the appliance*.

For minimum and maximum *log_size* requirements, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].

If AUTOGROW is ON, the log file is permitted to grow beyond this limit. Use the [DBCC SHRINKLOG (Azure Synapse Analytics)](../../t-sql/database-console-commands/dbcc-shrinklog-azure-sql-data-warehouse.md) statement to reduce the size of the log files to their original size.

If AUTOGROW is OFF, an error will be returned to the user for any action that would increase the log size on an individual Compute node beyond *log_size*.

## Permissions

Requires the `CREATE ANY DATABASE` permission in the `master` database, or membership in the **sysadmin** fixed server role.

The following example provides the permission to create a database to the database user Fay.

```sql
USE master;
GO
GRANT CREATE ANY DATABASE TO [Fay];
GO
```

## General Remarks

Databases are created with database compatibility level 120, which is the compatibility level for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. This ensures that the database will be able to use all of the [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] functionality that PDW uses.

## Limitations and Restrictions

The CREATE DATABASE statement is not allowed in an explicit transaction. For more information, see [Statements](../../t-sql/statements/statements.md).

For information on minimum and maximum constraints on databases, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].

At the time a database is created, there must be enough available free space *on each Compute node* to allocate the combined total of the following sizes:

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database with tables the size of *replicated_table_size*.
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database with tables the size of (*distributed_table_size* / number of Compute nodes).
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logs the size of (*log_size* / number of Compute nodes).

## Locking

Takes a shared lock on the DATABASE object.

## Metadata

After this operation succeeds, an entry for this database will appear in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) and [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)metadata views.

## Examples: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

### A. Basic database creation examples

The following example creates the database `mytest` with a storage allocation of 100 GB per Compute node for replicated tables, 500 GB per appliance for distributed tables, and 100 GB per appliance for the transaction log. In this example, AUTOGROW is off by default.

```sql
CREATE DATABASE mytest
  WITH
    (REPLICATED_SIZE = 100 GB,
    DISTRIBUTED_SIZE = 500 GB,
    LOG_SIZE = 100 GB );
```

The following example creates the database `mytest` with the same parameters as above, except that AUTOGROW is turned on. This allows the database to grow outside the specified size parameters.

```sql
CREATE DATABASE mytest
  WITH
    (AUTOGROW = ON,
    REPLICATED_SIZE = 100 GB,
    DISTRIBUTED_SIZE = 500 GB,
    LOG_SIZE = 100 GB);
```

### B. Create a database with partial gigabyte sizes

The following example creates the database `mytest`, with AUTOGROW off, a storage allocation of 1.5 GB per Compute node for replicated tables, 5.25 GB per appliance for distributed tables, and 10 GB per appliance for the transaction log.

```sql
CREATE DATABASE mytest
  WITH
    (REPLICATED_SIZE = 1.5 GB,
    DISTRIBUTED_SIZE = 5.25 GB,
    LOG_SIZE = 10 GB);
```

## See also

- [ALTER DATABASE (Analytics Platform System)](../../t-sql/statements/alter-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)

::: moniker-end
