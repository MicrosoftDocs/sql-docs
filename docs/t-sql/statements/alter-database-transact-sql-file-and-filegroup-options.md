---
title: "ALTER DATABASE File and Filegroup Options (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/21/2019"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ADD FILE"
  - "ADD_FILE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "deleting files"
  - "removing files"
  - "deleting filegroups"
  - "file modifications [SQL Server]"
  - "databases [SQL Server], size"
  - "relocating files"
  - "databases [SQL Server], modifying"
  - "file additions [SQL Server], ALTER DATABASE"
  - "file moving [SQL Server]"
  - "default filegroups"
  - "ALTER DATABASE statement, files and filegroups"
  - "initializing files [SQL Server]"
  - "database files [SQL Server]"
  - "moving files"
  - "filegroups [SQL Server], deleting"
  - "adding filegroups"
  - "adding files"
  - "database filegroups [SQL Server]"
  - "adding log files"
  - "modifying files"
  - "filegroups [SQL Server], adding"
  - "file initialization [SQL Server]"
  - "files [SQL Server], deleting"
  - "files [SQL Server], adding"
  - "databases [SQL Server], moving"
ms.assetid: 1f635762-f7aa-4241-9b7a-b51b22292b07
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# ALTER DATABASE (Transact-SQL) File and Filegroup Options

Modifies the files and filegroups associated with the database. Adds or removes files and filegroups from a database, and changes the attributes of a database or its files and filegroups. For other ALTER DATABASE options, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Click a product!

In the following row, click whichever product name you are interested in. The click displays different content here on this webpage, appropriate for whichever product you click.

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"

> |||
> |-|-|-|
> |**_\* SQL Server \*_**<br />&nbsp;|[SQL Database<br />managed instance](alter-database-transact-sql-file-and-filegroup-options.md?view=azuresqldb-mi-current)|

&nbsp;

# SQL Server

## Syntax

```
ALTER DATABASE database_name
{
    <add_or_modify_files>
  | <add_or_modify_filegroups>
}
[;

<add_or_modify_files>::=
{
    ADD FILE <filespec> [ ,...n ]
        [ TO FILEGROUP { filegroup_name } ]
  | ADD LOG FILE <filespec> [ ,...n ]
  | REMOVE FILE logical_file_name
  | MODIFY FILE <filespec>
}

<filespec>::=
(
    NAME = logical_file_name
    [ , NEWNAME = new_logical_name ]
    [ , FILENAME = {'os_file_name' | 'filestream_path' | 'memory_optimized_data_path' } ]
    [ , SIZE = size [ KB | MB | GB | TB ] ]
    [ , MAXSIZE = { max_size [ KB | MB | GB | TB ] | UNLIMITED } ]
    [ , FILEGROWTH = growth_increment [ KB | MB | GB | TB| % ] ]
    [ , OFFLINE ]
)

<add_or_modify_filegroups>::=
{
    | ADD FILEGROUP filegroup_name
        [ CONTAINS FILESTREAM | CONTAINS MEMORY_OPTIMIZED_DATA ]
    | REMOVE FILEGROUP filegroup_name
    | MODIFY FILEGROUP filegroup_name
        { <filegroup_updatability_option>
        | DEFAULT
        | NAME = new_filegroup_name
        | { AUTOGROW_SINGLE_FILE | AUTOGROW_ALL_FILES }
        }
}
<filegroup_updatability_option>::=
{
    { READONLY | READWRITE }
    | { READ_ONLY | READ_WRITE }
}

```

## Arguments

**\<add_or_modify_files>::=**

Specifies the file to be added, removed, or modified.

*database_name*
Is the name of the database to be modified.

ADD FILE
Adds a file to the database.

TO FILEGROUP { *filegroup_name* }
Specifies the filegroup to which to add the specified file. To display the current filegroups and which filegroup is the current default, use the [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md) catalog view.

ADD LOG FILE
Adds a log file be added to the specified database.

REMOVE FILE *logical_file_name*
Removes the logical file description from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deletes the physical file. The file cannot be removed unless it is empty.

*logical_file_name*
Is the logical name used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when referencing the file.

> [!WARNING]
> Removing a database file that has `FILE_SNAPSHOT` backups associated with it will succeed, but any associated snapshots will not be deleted to avoid invalidating the backups referring to the database file. The file will be truncated, but will not be physically deleted in order to keep the FILE_SNAPSHOT backups intact. For more information, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md). **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).

MODIFY FILE
Specifies the file that should be modified. Only one \<filespec> property can be changed at a time. NAME must always be specified in the \<filespec> to identify the file to be modified. If SIZE is specified, the new size must be larger than the current file size.

To modify the logical name of a data file or log file, specify the logical file name to be renamed in the `NAME` clause, and specify the new logical name for the file in the `NEWNAME` clause. For example:

```sql
MODIFY FILE ( NAME = logical_file_name, NEWNAME = new_logical_name )
```

To move a data file or log file to a new location, specify the current logical file name in the `NAME` clause and specify the new path and operating system file name in the `FILENAME` clause. For example:

```sql
MODIFY FILE ( NAME = logical_file_name, FILENAME = ' new_path/os_file_name ' )
```

When you move a full-text catalog, specify only the new path in the FILENAME clause. Do not specify the operating-system file name.

For more information, see [Move Database Files](../../relational-databases/databases/move-database-files.md).

For a FILESTREAM filegroup, NAME can be modified online. FILENAME can be modified online; however, the change does not take effect until after the container is physically relocated and the server is shutdown and then restarted.

You can set a FILESTREAM file to OFFLINE. When a FILESTREAM file is offline, its parent filegroup will be internally marked as offline; therefore, all access to FILESTREAM data within that filegroup will fail.

> [!NOTE]
> \<add_or_modify_files> options are not available in a Contained Database.

**\<filespec>::=**

Controls the file properties.

NAME *logical_file_name*
Specifies the logical name of the file.

*logical_file_name*
Is the logical name used in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when referencing the file.

NEWNAME *new_logical_file_name*
Specifies a new logical name for the file.

*new_logical_file_name*
Is the name to replace the existing logical file name. The name must be unique within the database and comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). The name can be a character or Unicode constant, a regular identifier, or a delimited identifier.

FILENAME { **'**_os\_file\_name_**'** | **'**_filestream\_path_**'** | **'**_memory\_optimized\_data\_path_**'**}
Specifies the operating system (physical) file name.

' *os_file_name* '
For a standard (ROWS) filegroup, this is the path and file name that is used by the operating system when you create the file. The file must reside on the server on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed. The specified path must exist before executing the ALTER DATABASE statement.

> [!NOTE]
> `SIZE`, `MAXSIZE`, and `FILEGROWTH` parameters cannot be set when a UNC path is specified for the file.
>
> System databases cannot reside in UNC share directories.

Data files should not be put on compressed file systems unless the files are read-only secondary files, or if the database is read-only. Log files should never be put on compressed file systems.

If the file is on a raw partition, *os_file_name* must specify only the drive letter of an existing raw partition. Only one file can be put on each raw partition.

**'** *filestream_path* **'**
For a FILESTREAM filegroup, FILENAME refers to a path where FILESTREAM data will be stored. The path up to the last folder must exist, and the last folder must not exist. For example, if you specify the path `C:\MyFiles\MyFilestreamData`, then `C:\MyFiles` must exist before you run ALTER DATABASE, but the `MyFilestreamData` folder must not exist.

> [!NOTE]
> The SIZE and FILEGROWTH properties do not apply to a FILESTREAM filegroup.

**'** *memory_optimized_data_path* **'**
For a memory-optimized filegroup, FILENAME refers to a path where memory-optimized data will be stored. The path up to the last folder must exist, and the last folder must not exist. For example, if you specify the path `C:\MyFiles\MyData`, then `C:\MyFiles` must exist before you run ALTER DATABASE, but the `MyData` folder must not exist.

The filegroup and file (`<filespec>`) must be created in the same statement.

> [!NOTE]
> The SIZE and FILEGROWTH properties do not apply to a MEMORY_OPTIMIZED_DATA filegroup.

For more information on memory-optimized filegroups, see [The Memory Optimized Filegroup](../../relational-databases/in-memory-oltp/the-memory-optimized-filegroup.md).

SIZE *size*
Specifies the file size. SIZE does not apply to FILESTREAM filegroups.

*size*
Is the size of the file.

When specified with ADD FILE, *size* is the initial size for the file. When specified with MODIFY FILE, *size* is the new size for the file, and must be larger than the current file size.

When *size* is not supplied for the primary file, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the size of the primary file in the **model** database. When a secondary data file or log file is specified but *size* is not specified for the file, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] makes the file 1 MB.

The KB, MB, GB, and TB suffixes can be used to specify kilobytes, megabytes, gigabytes, or terabytes. The default is MB. Specify a whole number and do not include a decimal. To specify a fraction of a megabyte, convert the value to kilobytes by multiplying the number by 1024. For example, specify 1536 KB instead of 1.5 MB (1.5 x 1024 = 1536).

> [!NOTE]
> `SIZE` cannot be set:
>
> - When a UNC path is specified for the file
> - For `FILESTREAM` and `MEMORY_OPTIMIZED_DATA` filegroups

MAXSIZE { *max_size*| UNLIMITED }
Specifies the maximum file size to which the file can grow.

*max_size*
Is the maximum file size. The KB, MB, GB, and TB suffixes can be used to specify kilobytes, megabytes, gigabytes, or terabytes. The default is MB. Specify a whole number and do not include a decimal. If *max_size* is not specified, the file size will increase until the disk is full.

UNLIMITED
Specifies that the file grows until the disk is full. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a log file specified with unlimited growth has a maximum size of 2 TB, and a data file has a maximum size of 16 TB. There is no maximum size when this option is specified for a FILESTREAM container. It continues to grow until the disk is full.

> [!NOTE]
> `MAXSIZE` cannot be set when a UNC path is specified for the file.

FILEGROWTH *growth_increment*
Specifies the automatic growth increment of the file. The FILEGROWTH setting for a file cannot exceed the MAXSIZE setting. FILEGROWTH does not apply to FILESTREAM filegroups.

*growth_increment*
Is the amount of space added to the file every time new space is required.

The value can be specified in MB, KB, GB, TB, or percent (%). If a number is specified without an MB, KB, or % suffix, the default is MB. When % is specified, the growth increment size is the specified percentage of the size of the file at the time the increment occurs. The size specified is rounded to the nearest 64 KB.

A value of 0 indicates that automatic growth is set to off and no additional space is allowed.

If FILEGROWTH is not specified, the default values are:

|Version|Default values|
|-------------|--------------------|
|Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|Data 64 MB. Log files 64 MB.|
|Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Data 1 MB. Log files 10%.|
|Prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Data 10%. Log files 10%.|

> [!NOTE]
> `FILEGROWTH` cannot be set:
>
> - When a UNC path is specified for the file
> - For `FILESTREAM` and `MEMORY_OPTIMIZED_DATA` filegroups

OFFLINE
Sets the file offline and makes all objects in the filegroup inaccessible.

> [!CAUTION]
> Use this option only when the file is corrupted and can be restored. A file set to OFFLINE can only be set online by restoring the file from backup. For more information about restoring a single file, see [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md).
>
> \<filespec> options are not available in a Contained Database.

**\<add_or_modify_filegroups>::=**

Add, modify, or remove a filegroup from the database.

ADD FILEGROUP *filegroup_name*
Adds a filegroup to the database.

CONTAINS FILESTREAM
Specifies that the filegroup stores FILESTREAM binary large objects (BLOBs) in the file system.

CONTAINS MEMORY_OPTIMIZED_DATA

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)])

Specifies that the filegroup stores memory optimized data in the file system. For more information, see [In-Memory OLTP - In-Memory Optimization](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md). Only one `MEMORY_OPTIMIZED_DATA` filegroup is allowed per database. For creating memory optimized tables, the filegroup cannot be empty. There must be at least one file. *filegroup_name* refers to a path. The path up to the last folder must exist, and the last folder must not exist.

REMOVE FILEGROUP *filegroup_name*
Removes a filegroup from the database. The filegroup cannot be removed unless it is empty. Remove all files from the filegroup first. For more information, see "REMOVE FILE *logical_file_name*," earlier in this topic.

> [!NOTE]
> Unless the FILESTREAM Garbage Collector has removed all the files from a FILESTREAM container, the `ALTER DATABASE REMOVE FILE` operation to remove a FILESTREAM container will fail and return an error. See the [Removing a FILESTREAM Container](#removing-a-filestream-container) section later in this topic.

MODIFY FILEGROUP *filegroup_name* { \<filegroup_updatability_option> | DEFAULT | NAME **=**_new\_filegroup\_name_ }
Modifies the filegroup by setting the status to READ_ONLY or READ_WRITE, making the filegroup the default filegroup for the database, or changing the filegroup name.

\<filegroup_updatability_option>
Sets the read-only or read/write property to the filegroup.

DEFAULT
Changes the default database filegroup to *filegroup_name*. Only one filegroup in the database can be the default filegroup. For more information, see [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md).

NAME = *new_filegroup_name*
Changes the filegroup name to the *new_filegroup_name*.

AUTOGROW_SINGLE_FILE
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)])

When a file in the filegroup meets the autogrow threshold, only that file grows. This is the default.

AUTOGROW_ALL_FILES

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)])

When a file in the filegroup meets the autogrow threshold, all files in the filegroup grow.

> [!NOTE]
> This is the default value for TempDB.

**\<filegroup_updatability_option>::=**

Sets the read-only or read/write property to the filegroup.

READ_ONLY | READONLY
Specifies the filegroup is read-only. Updates to objects in it are not allowed. The primary filegroup cannot be made read-only. To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

Because a read-only database does not allow data modifications:

- Automatic recovery is skipped at system startup.
- Shrinking the database is not possible.
- No locking occurs in read-only databases. This can cause faster query performance.

> [!NOTE]
> The keyword `READONLY` will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using `READONLY` in new development work, and plan to modify applications that currently use `READONLY`. Use `READ_ONLY` instead.

READ_WRITE | READWRITE
Specifies the group is READ_WRITE. Updates are enabled for the objects in the filegroup. To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

> [!NOTE]
> The keyword `READWRITE` will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using `READWRITE` in new development work, and plan to modify applications that currently use `READWRITE` to use `READ_WRITE` instead.
> [!TIP]
> The status of these options can be determined by examining the **is_read_only** column in the **sys.databases** catalog view or the **Updateability** property of the `DATABASEPROPERTYEX` function.

## Remarks

To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).

You cannot add or remove a file while a `BACKUP` statement is running.

A maximum of 32,767 files and 32,767 filegroups can be specified for each database.

Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the state of a database file (for example, online or offline), is maintained independently from the state of the database. For more information, see [File States](../../relational-databases/databases/file-states.md).

- The state of the files within a filegroup determines the availability of the whole filegroup. For a filegroup to be available, all files within the filegroup must be online.
- If a filegroup is offline, any try to access the filegroup by an SQL statement will fail with an error. When you build query plans for `SELECT` statements, the query optimizer avoids nonclustered indexes and indexed views that reside in offline filegroups. This enables these statements to succeed. However, if the offline filegroup contains the heap or clustered index of the target table, the `SELECT` statements fail. Additionally, any `INSERT`, `UPDATE`, or `DELETE` statement that modifies a table with any index in an offline filegroup will fail.

SIZE, MAXSIZE, and FILEGROWTH parameters cannot be set when a UNC path is specified for the file.

SIZE and FILEGROWTH parameters cannot be set for memory optimized filegroups.

The keyword `READONLY` will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using `READONLY` in new development work, and plan to modify applications that currently use READONLY. Use `READ_ONLY` instead.

The keyword `READWRITE` will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using `READWRITE` in new development work, and plan to modify applications that currently use `READWRITE` to use `READ_WRITE` instead.

## Moving Files

You can move system or user-defined data and log files by specifying the new location in FILENAME. This may be useful in the following scenarios:

- Failure recovery. For example, the database is in suspect mode or shutdown caused by hardware failure.
- Planned relocation.
- Relocation for scheduled disk maintenance.

For more information, see [Move Database Files](../../relational-databases/databases/move-database-files.md).

## Initializing Files

By default, data and log files are initialized by filling the files with zeros when you perform one of the following operations:

- Create a database.
- Add files to an existing database.
- Increase the size of an existing file.
- Restore a database or filegroup.

Data files can be initialized instantaneously. This enables for fast execution of these file operations. For more information, see [Database File Initialization](../../relational-databases/databases/database-instant-file-initialization.md).

## <a name="removing-a-filestream-container"></a> Removing a FILESTREAM Container

Even though FILESTREAM container may have been emptied using the "DBCC SHRINKFILE" operation, the database may still need to maintain references to the deleted files for various system maintenance reasons. [sp_filestream_force_garbage_collection](../../relational-databases/system-stored-procedures/filestream-and-filetable-sp-filestream-force-garbage-collection.md) will run the FILESTREAM Garbage Collector to remove these files when it is safe to do so. Unless the FILESTREAM Garbage Collector has removed all the files from a FILESTREAM container, the ALTER DATABASE REMOVE FILE operation will fail to remove a FILESTREAM container and will return an error. The following process is recommended to remove a FILESTREAM container.

1. Run [DBCC SHRINKFILE](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md) with the EMPTYFILE option to move the active contents of this container to other containers.
2. Ensure that Log backups have been taken, in the FULL or BULK_LOGGED recovery model.
3. Ensure that the replication log reader job has been run, if relevant.
4. Run [sp_filestream_force_garbage_collection](../../relational-databases/system-stored-procedures/filestream-and-filetable-sp-filestream-force-garbage-collection.md) to force the garbage collector to delete any files that are no longer needed in this container.
5. Execute ALTER DATABASE with the REMOVE FILE option to remove this container.
6. Repeat steps 2 through 4 once more to complete the garbage collection.
7. Use ALTER Database...REMOVE FILE to remove this container.

## Examples

### A. Adding a file to a database

The following example adds a 5-MB data file to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
ADD FILE
(
    NAME = Test1dat2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\t1dat2.ndf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
);
GO
```

### B. Adding a filegroup with two files to a database

The following example creates the filegroup `Test1FG1` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database and adds two 5-MB files to the filegroup.

```sql
USE master
GO
ALTER DATABASE AdventureWorks2012
ADD FILEGROUP Test1FG1;
GO
ALTER DATABASE AdventureWorks2012
ADD FILE
(
    NAME = test1dat3,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\t1dat3.ndf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
),  
(  
    NAME = test1dat4,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\t1dat4.ndf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
)  
TO FILEGROUP Test1FG1;
GO
```

### C. Adding two log files to a database

The following example adds two 5-MB log files to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
ADD LOG FILE
(
    NAME = test1log2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\test2log.ldf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
),
(
    NAME = test1log3,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\test3log.ldf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
);
GO
```

### D. Removing a file from a database

The following example removes one of the files added in example B.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
REMOVE FILE test1dat4;
GO
```

### E. Modifying a file

The following example increases the size of one of the files added in example B.
The ALTER DATABASE with MODIFY FILE command can only make a file size bigger, so if you need to make the file size smaller you need to use DBCC SHRINKFILE.

```sql
USE master;
GO

ALTER DATABASE AdventureWorks2012
MODIFY FILE
(NAME = test1dat3,
SIZE = 200MB);
GO
```

This example shrinks the size of a data file to 100 MB, and then specifies the size at that amount.

```sql
USE AdventureWorks2012;
GO

DBCC SHRINKFILE (AdventureWorks2012_data, 100);
GO

USE master;
GO
ALTER DATABASE AdventureWorks2012
MODIFY FILE
(NAME = test1dat3,
SIZE = 200MB);
GO
```

### F. Moving a file to a new location

The following example moves the `Test1dat2` file created in example A to a new directory.

> [!NOTE]
> You must physically move the file to the new directory before running this example. Afterward, stop and start the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or take the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database OFFLINE and then ONLINE to implement the change.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
MODIFY FILE
(
    NAME = Test1dat2,
    FILENAME = N'c:\t1dat2.ndf'
);
GO
```

### G. Moving tempdb to a new location

The following example moves `tempdb` from its current location on the disk to another disk location. Because `tempdb` is re-created each time the MSSQLSERVER service is started, you do not have to physically move the data and log files. The files are created when the service is restarted in step 3. Until the service is restarted, `tempdb` continues to function in its existing location.

1. Determine the logical file names of the `tempdb` database and their current location on disk.

    ```sql
    SELECT name, physical_name
    FROM sys.master_files
    WHERE database_id = DB_ID('tempdb');
    GO
    ```

2. Change the location of each file by using `ALTER DATABASE`.

    ```sql
    USE maser;
    GO
    ALTER DATABASE tempdb
    MODIFY FILE (NAME = tempdev, FILENAME = 'E:\SQLData\tempdb.mdf');
    GO
    ALTER DATABASE tempdb
    MODIFY FILE (NAME = templog, FILENAME = 'E:\SQLData\templog.ldf');
    GO
    ```

3. Stop and restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

4. Verify the file change.

    ```sql
    SELECT name, physical_name
    FROM sys.master_files
    WHERE database_id = DB_ID('tempdb');
    ```

5. Delete the tempdb.mdf and templog.ldf files from their original location.

### H. Making a filegroup the default

The following example makes the `Test1FG1` filegroup created in example B the default filegroup. Then, the default filegroup is reset to the `PRIMARY` filegroup. Note that `PRIMARY` must be delimited by brackets or quotation marks.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
MODIFY FILEGROUP Test1FG1 DEFAULT;
GO
ALTER DATABASE AdventureWorks2012
MODIFY FILEGROUP [PRIMARY] DEFAULT;
GO
```

### I. Adding a Filegroup Using ALTER DATABASE

The following example adds a `FILEGROUP` that contains the `FILESTREAM` clause to the `FileStreamPhotoDB` database.

```sql
--Create and add a FILEGROUP that CONTAINS the FILESTREAM clause.
ALTER DATABASE FileStreamPhotoDB
ADD FILEGROUP TodaysPhotoShoot
CONTAINS FILESTREAM;
GO

--Add a file for storing database photos to FILEGROUP
ALTER DATABASE FileStreamPhotoDB
ADD FILE
(
  NAME= 'PhotoShoot1',
  FILENAME = 'C:\Users\Administrator\Pictures\TodaysPhotoShoot.ndf'
)
TO FILEGROUP TodaysPhotoShoot;
GO
```

The following example adds a `FILEGROUP` that contains the `MEMORY_OPTIMIZED_DATA` clause to the `xtp_db` database. The filegroup stores memory optimized data.

```sql
--Create and add a FILEGROUP that CONTAINS the MEMORY_OPTIMIZED_DATA clause.
ALTER DATABASE xtp_db
ADD FILEGROUP xtp_fg
CONTAINS MEMORY_OPTIMIZED_DATA;
GO

--Add a file for storing memory optimized data to FILEGROUP
ALTER DATABASE xtp_db
ADD FILE
(
  NAME='xtp_mod',
  FILENAME='d:\data\xtp_mod'
)
TO FILEGROUP xtp_fg;
GO
```

### J. Change filegroup so that when a file in the filegroup meets the autogrow threshold, all files in the filegroup grow

 The following example generates the required `ALTER DATABASE` statements to modify read-write filegroups with the `AUTOGROW_ALL_FILES` setting.

```sql
--Generate ALTER DATABASE ... MODIFY FILEGROUP statements
--so that all read-write filegroups grow at the same time.
SET NOCOUNT ON;

DROP TABLE IF EXISTS #tmpdbs
CREATE TABLE #tmpdbs (id int IDENTITY(1,1), [dbid] int, [dbname] sysname, isdone bit);

DROP TABLE IF EXISTS #tmpfgs
CREATE TABLE #tmpfgs (id int IDENTITY(1,1), [dbid] int, [dbname] sysname, fgname sysname, isdone bit);

INSERT INTO #tmpdbs ([dbid], [dbname], [isdone])
SELECT database_id, name, 0 FROM master.sys.databases (NOLOCK) WHERE is_read_only = 0 AND state = 0;

DECLARE @dbid int, @query VARCHAR(1000), @dbname sysname, @fgname sysname

WHILE (SELECT COUNT(id) FROM #tmpdbs WHERE isdone = 0) > 0
BEGIN
  SELECT TOP 1 @dbname = [dbname], @dbid = [dbid] FROM #tmpdbs WHERE isdone = 0

  SET @query = 'SELECT ' + CAST(@dbid AS NVARCHAR) + ', ''' + @dbname + ''', [name], 0 FROM [' + @dbname + '].sys.filegroups WHERE [type] = ''FG'' AND is_read_only = 0;'
  INSERT INTO #tmpfgs
  EXEC (@query)

  UPDATE #tmpdbs
  SET isdone = 1
  WHERE [dbid] = @dbid
END;

IF (SELECT COUNT(ID) FROM #tmpfgs) > 0
BEGIN
  WHILE (SELECT COUNT(id) FROM #tmpfgs WHERE isdone = 0) > 0
  BEGIN
    SELECT TOP 1 @dbname = [dbname], @dbid = [dbid], @fgname = fgname FROM #tmpfgs WHERE isdone = 0

    SET @query = 'ALTER DATABASE [' + @dbname + '] MODIFY FILEGROUP [' + @fgname + '] AUTOGROW_ALL_FILES;'

    PRINT @query

    UPDATE #tmpfgs
    SET isdone = 1
    WHERE [dbid] = @dbid AND fgname = @fgname
  END
END;
GO
```

## See Also

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=sql-server-2017)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)
- [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)
- [Binary Large Objects](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md)
- [DBCC SHRINKFIL](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)
- [sp_filestream_force_garbage_collection](../../relational-databases/system-stored-procedures/filestream-and-filetable-sp-filestream-force-garbage-collection.md)
- [Database File Initialization](../../relational-databases/databases/database-instant-file-initialization.md)

::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"

> |||
> |-|-|-|
> |[SQL Server](alter-database-transact-sql-file-and-filegroup-options.md?view=sql-server-2016)|**_\* SQL Database<br />managed instance \*_**<br />&nbsp;|

&nbsp;

## Azure SQL Database managed instance

Use this statement with a database in Azure SQL Database managed instance.

## Syntax for databases in a managed instance

```
ALTER DATABASE database_name
{
    <add_or_modify_files>
  | <add_or_modify_filegroups>
}
[;]

<add_or_modify_files>::=
{
    ADD FILE <filespec> [ ,...n ]
        [ TO FILEGROUP { filegroup_name } ]
  | REMOVE FILE logical_file_name
  | MODIFY FILE <filespec>
}
  
<filespec>::=
(
    NAME = logical_file_name
    [ , SIZE = size [ KB | MB | GB | TB ] ]
    [ , MAXSIZE = { max_size [ KB | MB | GB | TB ] | UNLIMITED } ]
    [ , FILEGROWTH = growth_increment [ KB | MB | GB | TB| % ] ]
)

<add_or_modify_filegroups>::=
{
    | ADD FILEGROUP filegroup_name
    | REMOVE FILEGROUP filegroup_name
    | MODIFY FILEGROUP filegroup_name
        { <filegroup_updatability_option>
        | DEFAULT
        | NAME = new_filegroup_name
        | { AUTOGROW_SINGLE_FILE | AUTOGROW_ALL_FILES }
        }
}  
<filegroup_updatability_option>::=
{
    { READONLY | READWRITE }
    | { READ_ONLY | READ_WRITE }
}

```

## Arguments

**\<add_or_modify_files>::=**

Specifies the file to be added, removed, or modified.

*database_name*
Is the name of the database to be modified.

ADD FILE
Adds a file to the database.

TO FILEGROUP { *filegroup_name* }
Specifies the filegroup to which to add the specified file. To display the current filegroups and which filegroup is the current default, use the [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md) catalog view.

REMOVE FILE *logical_file_name*
Removes the logical file description from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deletes the physical file. The file cannot be removed unless it is empty.

*logical_file_name*
Is the logical name used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when referencing the file.

MODIFY FILE
Specifies the file that should be modified. Only one \<filespec> property can be changed at a time. NAME must always be specified in the \<filespec> to identify the file to be modified. If SIZE is specified, the new size must be larger than the current file size.

**\<filespec>::=**

Controls the file properties.

NAME *logical_file_name*
Specifies the logical name of the file.

*logical_file_name*
Is the logical name used in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when referencing the file.

NEWNAME *new_logical_file_name*
Specifies a new logical name for the file.

*new_logical_file_name*
Is the name to replace the existing logical file name. The name must be unique within the database and comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). The name can be a character or Unicode constant, a regular identifier, or a delimited identifier.

SIZE *size*
Specifies the file size.

*size*
Is the size of the file.

When specified with ADD FILE, *size* is the initial size for the file. When specified with MODIFY FILE, *size* is the new size for the file, and must be larger than the current file size.

When *size* is not supplied for the primary file, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the size of the primary file in the **model** database. When a secondary data file or log file is specified but *size* is not specified for the file, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] makes the file 1 MB.

The KB, MB, GB, and TB suffixes can be used to specify kilobytes, megabytes, gigabytes, or terabytes. The default is MB. Specify a whole number and do not include a decimal. To specify a fraction of a megabyte, convert the value to kilobytes by multiplying the number by 1024. For example, specify 1536 KB instead of 1.5 MB (1.5 x 1024 = 1536).

MAXSIZE { *max_size*| UNLIMITED }
Specifies the maximum file size to which the file can grow.

*max_size*
Is the maximum file size. The KB, MB, GB, and TB suffixes can be used to specify kilobytes, megabytes, gigabytes, or terabytes. The default is MB. Specify a whole number and do not include a decimal. If *max_size* is not specified, the file size will increase until the disk is full.

UNLIMITED
Specifies that the file grows until the disk is full. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a log file specified with unlimited growth has a maximum size of 2 TB, and a data file has a maximum size of 16 TB.

FILEGROWTH *growth_increment*
Specifies the automatic growth increment of the file. The FILEGROWTH setting for a file cannot exceed the MAXSIZE setting.

*growth_increment*
Is the amount of space added to the file every time new space is required.

The value can be specified in MB, KB, GB, TB, or percent (%). If a number is specified without an MB, KB, or % suffix, the default is MB. When % is specified, the growth increment size is the specified percentage of the size of the file at the time the increment occurs. The size specified is rounded to the nearest 64 KB.

A value of 0 indicates that automatic growth is set to off and no additional space is allowed.

If FILEGROWTH is not specified, the default values are:

- Data 64 MB
- Log files 64 MB

**\<add_or_modify_filegroups>::=**

Add, modify, or remove a filegroup from the database.

ADD FILEGROUP *filegroup_name*
Adds a filegroup to the database.

The following example creates a filegroup that is added to a database named sql_db_mi, and adds a file to the filegroup.

```sql
ALTER DATABASE sql_db_mi ADD FILEGROUP sql_db_mi_fg;
GO
ALTER DATABASE sql_db_mi ADD FILE (NAME='sql_db_mi_mod') TO FILEGROUP sql_db_mi_fg;
```

REMOVE FILEGROUP *filegroup_name*
Removes a filegroup from the database. The filegroup cannot be removed unless it is empty. Remove all files from the filegroup first. For more information, see "REMOVE FILE *logical_file_name*," earlier in this topic.

MODIFY FILEGROUP _filegroup\_name_ { \<filegroup_updatability_option> | DEFAULT | NAME **=**_new\_filegroup\_name_ }
Modifies the filegroup by setting the status to READ_ONLY or READ_WRITE, making the filegroup the default filegroup for the database, or changing the filegroup name.

\<filegroup_updatability_option>
Sets the read-only or read/write property to the filegroup.

DEFAULT
Changes the default database filegroup to *filegroup_name*. Only one filegroup in the database can be the default filegroup. For more information, see [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md).

NAME = *new_filegroup_name*
Changes the filegroup name to the *new_filegroup_name*.

AUTOGROW_SINGLE_FILE

When a file in the filegroup meets the autogrow threshold, only that file grows. This is the default.

AUTOGROW_ALL_FILES

When a file in the filegroup meets the autogrow threshold, all files in the filegroup grow.

**\<filegroup_updatability_option>::=**

Sets the read-only or read/write property to the filegroup.

READ_ONLY | READONLY
Specifies the filegroup is read-only. Updates to objects in it are not allowed. The primary filegroup cannot be made read-only. To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

Because a read-only database does not allow data modifications:

- Automatic recovery is skipped at system startup.
- Shrinking the database is not possible.
- No locking occurs in read-only databases. This can cause faster query performance.

> [!NOTE]
> The keyword READONLY will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using READONLY in new development work, and plan to modify applications that currently use READONLY. Use READ_ONLY instead.

READ_WRITE | READWRITE
Specifies the group is READ_WRITE. Updates are enabled for the objects in the filegroup. To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

> [!NOTE]
> The keyword `READWRITE` will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using `READWRITE` in new development work, and plan to modify applications that currently use `READWRITE` to use `READ_WRITE` instead.

The status of these options can be determined by examining the **is_read_only** column in the **sys.databases** catalog view or the **Updateability** property of the `DATABASEPROPERTYEX` function.

## Remarks

To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).

You cannot add or remove a file while a `BACKUP` statement is running.

A maximum of 32,767 files and 32,767 filegroups can be specified for each database.

## Examples

### A. Adding a file to a database

The following example adds a 5-MB data file to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
ADD FILE
(
  NAME = Test1dat2,
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
);
GO
```

### B. Adding a filegroup with two files to a database

The following example creates the filegroup `Test1FG1` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database and adds two 5-MB files to the filegroup.

```sql
USE master
GO
ALTER DATABASE AdventureWorks2012
ADD FILEGROUP Test1FG1;
GO
ALTER DATABASE AdventureWorks2012
ADD FILE
(
    NAME = test1dat3,
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
),
(
    NAME = test1dat4,
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
)  
TO FILEGROUP Test1FG1;
GO

```

### C. Removing a file from a database

The following example removes one of the files added in example B.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
REMOVE FILE test1dat4;
GO
```

### D. Modifying a file

The following example increases the size of one of the files added in example B.
The ALTER DATABASE with MODIFY FILE command can only make a file size bigger, so if you need to make the file size smaller you need to use DBCC SHRINKFILE.

```sql
USE master;
GO

ALTER DATABASE AdventureWorks2012
MODIFY FILE
(NAME = test1dat3,
SIZE = 200MB);
GO
```

This example shrinks the size of a data file to 100 MB, and then specifies the size at that amount.

```sql
USE AdventureWorks2012;
GO

DBCC SHRINKFILE (AdventureWorks2012_data, 100);
GO

USE master;
GO

ALTER DATABASE AdventureWorks2012
MODIFY FILE
(NAME = test1dat3,
SIZE = 200MB);
GO
```

### E. Making a filegroup the default

The following example makes the `Test1FG1` filegroup created in example B the default filegroup. Then, the default filegroup is reset to the `PRIMARY` filegroup. Note that `PRIMARY` must be delimited by brackets or quotation marks.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
MODIFY FILEGROUP Test1FG1 DEFAULT;
GO
ALTER DATABASE AdventureWorks2012
MODIFY FILEGROUP [PRIMARY] DEFAULT;
GO
```

### F. Adding a Filegroup Using ALTER DATABASE

The following example adds a `FILEGROUP` to the `MyDB` database.

```sql
--Create and add a FILEGROUP
ALTER DATABASE MyDB
ADD FILEGROUP NewFG;
GO

--Add a file to FILEGROUP
ALTER DATABASE MyDB
ADD FILE
(
    NAME= 'MyFile',
)
TO FILEGROUP NewFG;
GO
```

### G. Change filegroup so that when a file in the filegroup meets the autogrow threshold, all files in the filegroup grow

The following example generates the required `ALTER DATABASE` statements to modify read-write filegroups with the `AUTOGROW_ALL_FILES` setting.

```sql
--Generate ALTER DATABASE ... MODIFY FILEGROUP statements
--so that all read-write filegroups grow at the same time.
SET NOCOUNT ON;

DROP TABLE IF EXISTS #tmpdbs
CREATE TABLE #tmpdbs (id int IDENTITY(1,1), [dbid] int, [dbname] sysname, isdone bit);

DROP TABLE IF EXISTS #tmpfgs
CREATE TABLE #tmpfgs (id int IDENTITY(1,1), [dbid] int, [dbname] sysname, fgname sysname, isdone bit);

INSERT INTO #tmpdbs ([dbid], [dbname], [isdone])
SELECT database_id, name, 0 FROM master.sys.databases (NOLOCK) WHERE is_read_only = 0 AND state = 0;

DECLARE @dbid int, @query VARCHAR(1000), @dbname sysname, @fgname sysname

WHILE (SELECT COUNT(id) FROM #tmpdbs WHERE isdone = 0) > 0
BEGIN
    SELECT TOP 1 @dbname = [dbname], @dbid = [dbid] FROM #tmpdbs WHERE isdone = 0

    SET @query = 'SELECT ' + CAST(@dbid AS NVARCHAR) + ', ''' + @dbname + ''', [name], 0 FROM [' + @dbname + '].sys.filegroups WHERE [type] = ''FG'' AND is_read_only = 0;'
    INSERT INTO #tmpfgs
    EXEC (@query)

    UPDATE #tmpdbs
    SET isdone = 1
    WHERE [dbid] = @dbid
END;

IF (SELECT COUNT(ID) FROM #tmpfgs) > 0
BEGIN
    WHILE (SELECT COUNT(id) FROM #tmpfgs WHERE isdone = 0) > 0
    BEGIN
        SELECT TOP 1 @dbname = [dbname], @dbid = [dbid], @fgname = fgname FROM #tmpfgs WHERE isdone = 0

        SET @query = 'ALTER DATABASE [' + @dbname + '] MODIFY FILEGROUP [' + @fgname + '] AUTOGROW_ALL_FILES;'

        PRINT @query

        UPDATE #tmpfgs
        SET isdone = 1
        WHERE [dbid] = @dbid AND fgname = @fgname
    END
END;
GO
```

## See Also

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-mi-current)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)
- [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)
- [DBCC SHRINKFILE](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)
- [The Memory-Optimized Filegroup](../../relational-databases/in-memory-oltp/the-memory-optimized-filegroup.md)

::: moniker-end
