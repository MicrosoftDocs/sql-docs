---
title: "Sqlmaint Utility"
description: In SQL Server, use sqlmaint to run DBCC checks, back up a database and its transaction log, update statistics, and rebuild indexes.
author: cwebb-msft
ms.author: chweb
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
helpviewer_keywords:
  - "database maintenance plans [SQL Server]"
  - "sqlmaint utility"
  - "maintaining databases [SQL Server]"
  - "backups [SQL Server], sqlmaint utility"
  - "command prompt utilities [SQL Server], sqlmaint"
  - "maintenance plans [SQL Server], command prompt"
  - "backing up [SQL Server], sqlmaint utility"
---
# sqlmaint utility

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

The **sqlmaint** utility performs a specified set of maintenance operations on one or more databases. Use **sqlmaint** to run `DBCC` checks, back up a database and its transaction log, update statistics, and rebuild indexes. All database maintenance activities generate a report that you can send to a designated text file, HTML file, or e-mail account. **sqlmaint** executes database maintenance plans created in previous versions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. To run [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] maintenance plans from the command prompt, use the [dtexec Utility](../integration-services/packages/dtexec-utility.md).

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../includes/ssnotedepfutureavoid-md.md)] Use the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] maintenance plan feature instead. For more information on maintenance plans, see [Maintenance plans](../relational-databases/maintenance-plans/maintenance-plans.md).

## Syntax

```console
sqlmaint
[ -? ] |
[
     [ -S server_name [ \instance_name ] ]
     [ -U login_ID [ -P password ] ]
     {
          [ -D database_name | -PlanName name | -PlanID guid ]
          [ -Rpt text_file ]
          [ -To operator_name ]
          [ -HtmlRpt html_file [ -DelHtmlRpt <time_period> ] ]
          [ -RmUnusedSpace threshold_percentfree_percent ]
          [ -CkDB | -CkDBNoIdx ]
          [ -CkAl | -CkAlNoIdx ]
          [ -CkCat ]
          [ -UpdOptiStats sample_percent ]
          [ -RebldIdx free_space ]
          [ -SupportComputedColumn ]
          [ -WriteHistory ]
          [
               { -BkUpDB [ backup_path ] | -BkUpLog [ backup_path ] }
               { -BkUpMedia
                    { DISK [
                           [ -DelBkUps <time_period> ]
                           [ -CrBkSubDir ]
                           [ -UseDefDir ]
                          ]
                     | TAPE
                    }
               }
               [ -BkUpOnlyIfClean ]
               [ -VrfyBackup ]
          ]
     }
]
<time_period> ::=
number [ minutes | hours | days | weeks | months ]
```

## Arguments

Separate the parameters and their values with a space. For example, include a space between `-S` and *server_name*.

#### -?

Returns the syntax diagram for **sqlmaint**. Use this parameter on its own.

#### -S *server_name*[\\*instance_name*]

Specifies the target instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. Specify `<server_name>` to connect to the default instance of [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] on that server. Specify `<server_name>\<instance_name>` to connect to a named instance of [!INCLUDE [ssDE](../includes/ssde-md.md)] on that server. If you don't specify a server, **sqlmaint** connects to the default instance of [!INCLUDE [ssDE](../includes/ssde-md.md)] on the local computer.

#### -U *login_ID*

Specifies the account to use when connecting to the server. If you don't supply this parameter, **sqlmaint** attempts to use Windows authentication. If *login_ID* contains special characters, enclose it in double quotation marks (`"`); otherwise, the double quotation marks are optional.

> [!IMPORTANT]  
> When possible, use Windows authentication.

#### -P *password*

Specifies the password for the *login_ID*. This parameter is only valid if you also supply the `-U` parameter. If *password* contains special characters, enclose it in double quotation marks; otherwise, the double quotation marks are optional.

> [!IMPORTANT]  
> The password isn't masked. When possible, use Windows authentication.

#### -D *database_name*

Specifies the name of the database in which to perform the maintenance operation. If *database_name* contains special characters, enclose it in double quotation marks; otherwise, the double quotation marks are optional.

#### -PlanName *name*

Specifies the name of a database maintenance plan defined using the Database Maintenance Plan Wizard. The only information **sqlmaint** uses from the plan is the list of databases in the plan. Any maintenance activities you specify in the other **sqlmaint** parameters are applied to this list of databases.

#### -PlanID *guid*

Specifies the globally unique identifier (GUID) of a database maintenance plan defined using the Database Maintenance Plan Wizard. The only information **sqlmaint** uses is the list of databases in the plan. Any maintenance activities you specify in the other **sqlmaint** parameters are applied to this list of databases. This value must match a `plan_id` value in `msdb.dbo.sysdbmaintplans`.

#### -Rpt *text_file*

Specifies the full path and file name for the generated report. The report is also generated on the screen. The report maintains version information by adding a date to the file name. The date is generated as follows, at the end of the file name but before the period, in the form `_<yyyyMMddhhmm>`. `<yyyy>` = year, `<MM>` = month, `<dd>` = day, `<hh>` = hour, `<mm>` = minute.

If you run the utility at 10:23 A.M. on December 1, 1996, and this is the *text_file* value:

```output
C:\Program Files\Microsoft SQL Server\MSSQL\Backup\AdventureWorks2022_maint.rpt
```

The generated file name is:

```output
C:\Program Files\Microsoft SQL Server\MSSQL\Backup\AdventureWorks2022_maint_199612011023.rpt
```

The full Universal Naming Convention (UNC) file name is required for *text_file* when **sqlmaint** accesses a remote server.

#### -To *operator_name*

Specifies the operator to whom the generated report is sent through SQL Mail.

#### -HtmlRpt *html_file*

Specifies the full path and name of the file into which an HTML report is generated. **sqlmaint** generates the file name by appending a string of the format `_<yyyyMMddhhmm>` to the file name, just as it does for the `-Rpt` parameter.

The full UNC file name is required for *html_file* when **sqlmaint** accesses a remote server.

#### -DelHtmlRpt <*time_period*>

Deletes any HTML report in the report directory if the time interval after the creation of the report file exceeds *time_period*. `-DelHtmlRpt` looks for files whose name fits the pattern generated from the *html_file* parameter. If *html_file* is `C:\Program Files\Microsoft SQL Server\MSSQL\Backup\AdventureWorks2022_maint.htm`, then `-DelHtmlRpt` causes **sqlmaint** to delete any files whose names match the pattern `C:\Program Files\Microsoft SQL Server\MSSQL\Backup\AdventureWorks2022_maint*.htm` and that are older than the specified *time_period*.

#### -RmUnusedSpace *threshold_percent free_percent*

Specifies that unused space is removed from the database specified in `-D`. This option is only useful for databases that are defined to grow automatically. *Threshold_percent* specifies in megabytes the size that the database must reach before **sqlmaint** attempts to remove unused data space. If the database is smaller than the *threshold_percent*, no action is taken. *Free_percent* specifies how much unused space must remain in the database, specified as a percentage of the final size of the database.

For example, if a 200-MB database contains 100 MB of data, specifying 10 for *free_percent* results in the final database size being 110 MB. A database isn't expanded if it's smaller than *free_percent* plus the amount of data in the database. For example, if a 108-MB database has 100 MB of data, specifying 10 for *free_percent* doesn't expand the database to 110 MB; it remains at 108 MB.

#### -CkDB | -CkDBNoIdx

Specifies that a [DBCC CHECKDB](../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) statement, or a `DBCC CHECKDB` statement with the `NOINDEX` option, runs in the database specified in `-D`.

**sqlmaint** writes a warning to *text_file* if the database is in use when it runs.

#### -CkAl | -CkAlNoIdx

Specifies that a [DBCC CHECKALLOC](../t-sql/database-console-commands/dbcc-checkalloc-transact-sql.md) statement with the `NOINDEX` option runs in the database specified in `-D`.

#### -CkCat

Specifies that a [DBCC CHECKCATALOG](../t-sql/database-console-commands/dbcc-checkcatalog-transact-sql.md) statement runs in the database specified in `-D`.

#### -UpdOptiStats *sample_percent*

Specifies that the following statement runs on each table in the database:

```sql
UPDATE STATISTICS table WITH SAMPLE sample_percent PERCENT;
```

If the tables contain computed columns, you must also specify the `-SupportedComputedColumn` argument when you use `-UpdOptiStats`.

For more information, see [UPDATE STATISTICS](../t-sql/statements/update-statistics-transact-sql.md).

#### -RebldIdx *free_space*

Specifies that indexes on tables in the target database should be rebuilt by using the *free_space* percent value as the inverse of the fill factor. For example, if *free_space* percentage is 30, then the fill factor used is 70. If a *free_space* percentage value of 100 is specified, the indexes are rebuilt with the original fill factor value.

If the indexes are on computed columns, you must also specify the `-SupportComputedColumn` argument when you use `-RebldIdx`.

#### -SupportComputedColumn

Must be specified to run `DBCC` maintenance commands with **sqlmaint** on computed columns.

#### -WriteHistory

Specifies that an entry is made in `msdb.dbo.sysdbmaintplan_history` for each maintenance action performed by **sqlmaint**. If `-PlanName` or `-PlanID` is specified, the entries in `sysdbmaintplan_history` use the ID of the specified plan. If `-D` is specified, the entries in `sysdbmaintplan_history` are made with zeroes for the plan ID.

#### -BkUpDB [ *backup_path* ] | -BkUpLog [ *backup_path* ]

Specifies a backup action. `-BkUpDb` backs up the entire database. `-BkUpLog` backs up only the transaction log.

*backup_path* specifies the directory for the backup. *backup_path* isn't needed if `-UseDefDir` is also specified, and `-UseDefDir` overrides *backup_path* if both are specified. The backup can be placed in a directory or a tape device address (for example, `\\.\TAPE0`). The file name for a database backup is generated automatically as follows:

```output
dbname_db_yyyyMMddhhmm.BAK
```

Where:

- `<dbname>` is the name of the database being backed up.
- `<yyyyMMddhhmm>` is the time of the backup operation with `<yyyy>` = year, `<MM>` = month, `<dd>` = day, `<hh>` = hour, and `<mm>` = minute.

The file name for a transaction backup is generated automatically with a similar format:

```output
dbname_log_yyyymmddhhmm.BAK
```

If you use the `-BkUpDB` parameter, you must also specify the media by using the `-BkUpMedia` parameter.

#### -BkUpMedia

Specifies the media type of the backup, either `DISK` or `TAPE`.

#### DISK

Specifies that the backup medium is disk.

#### -DelBkUps <*time_period*>

For disk backups, specifies that any backup file in the backup directory be deleted if the time interval after the creation of the backup exceeds the *time_period*.

#### -CrBkSubDir

For disk backups, specifies that a subdirectory be created in the *backup_path* directory or in the default backup directory if `-UseDefDir` is also specified. The name of the subdirectory is generated from the database name specified in `-D`. `-CrBkSubDir` offers an easy way to put all the backups for different databases into separate subdirectories without having to change the *backup_path* parameter.

#### -UseDefDir

For disk backups, specifies that the backup file be created in the default backup directory. `UseDefDir` overrides *backup_path* if both are specified. With a default [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] setup, the default backup directory is `C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup`.

#### TAPE

Specifies that the backup medium is tape.

#### -BkUpOnlyIfClean

Specifies that the backup occurs only if the specified `-Ck` checks don't find problems with the data. Maintenance actions run in the same sequence as they appear in the command prompt. Specify the parameters `-CkDB`, `-CkDBNoIdx`, `-CkAl`, `-CkAlNoIdx`, `-CkTxtAl`, or `-CkCat` before the `-BkUpDB` or `-BkUpLog` parameters if you're also going to specify `-BkUpOnlyIfClean`. If you don't specify these parameters, the backup occurs whether or not the check reports problems.

#### -VrfyBackup

Specifies that `RESTORE VERIFYONLY` is run on the backup when it completes.

#### *number* [ minutes\| hours\| day\| weeks\| months ]

Specifies the time interval used to determine if a report or backup file is old enough to be deleted. *number* is an integer followed (without a space) by a unit of time. Valid examples:

- `12weeks`
- `3months`
- `15days`

If you specify only *number*, the default date part is `weeks`.

## Remarks

The **sqlmaint** utility performs maintenance operations on one or more databases. If you specify `-D`, the utility performs the operations specified in the remaining switches only on the specified database. If you specify `-PlanName` or `-PlanID`, the only information **sqlmaint** retrieves from the specified maintenance plan is the list of databases in the plan. All operations specified in the remaining **sqlmaint** parameters are applied against each database in the list obtained from the plan. The **sqlmaint** utility doesn't apply any of the maintenance activities defined in the plan itself.

The **sqlmaint** utility returns 0 if it runs successfully or 1 if it fails. Failure is reported if:

- Any of the maintenance actions fail.

- The `-CkDB`, `-CkDBNoIdx`, `-CkAl`, `-CkAlNoIdx`, `-CkTxtAl`, or `-CkCat` checks find problems with the data.

- A general failure is encountered.

## Permissions

Any Windows user with **Read and Execute** permission on `sqlmaint.exe` can execute the **sqlmaint** utility. By default, `sqlmaint.exe` is stored in the `<X>:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER1\MSSQL\Binn` folder. Additionally, the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] login you specify with `-login_ID` must have the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] permissions required to perform the specified action. If you use Windows authentication to connect to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] login mapped to the authenticated Windows user must have the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] permissions required to perform the specified action.

For example, using the `-BkUpDB` requires permission to execute the `BACKUP` statement. And using the `-UpdOptiStats` argument requires permission to execute the `UPDATE STATISTICS` statement. For more information, see [Permissions (Database Engine)](../relational-databases/security/permissions-database-engine.md).

## Examples

### A. Perform DBCC checks on a database

This example runs `DBCC` checks against a database.

```console
sqlmaint -S MyServer -D AdventureWorks2022 -CkDB -CkAl -CkCat -Rpt C:\MyReports\AdvWks_chk.rpt
```

### B. Update statistics

This example updates statistics using a 15% sample in all databases in a plan. Any database that reaches 110 MB is shrunk to have only 10% free space.

```console
sqlmaint -S MyServer -PlanName MyUserDBPlan -UpdOptiStats 15 -RmUnusedSpace 110 10
```

### C. Back up all databases

This example backs up all databases in a plan to their individual subdirectories, using the default `<X>:\Program Files\Microsoft SQL Server\MSSQLl13.MSSQLSERVER\MSSQL\Backup` directory. It also deletes any backups older than two weeks.

```console
sqlmaint -S MyServer -PlanName MyUserDBPlan -BkUpDB -BkUpMedia DISK -UseDefDir -CrBkSubDir -DelBkUps 2weeks
```

### D. Back up a database

This example backs up a single database to the default `<X>:\Program Files\Microsoft SQL Server\MSSQLl13.MSSQLSERVER\MSSQL\Backup` directory.

```console
sqlmaint -S MyServer -BkUpDB -BkUpMedia DISK -UseDefDir
```

## Related content

- [BACKUP (Transact-SQL)](../t-sql/statements/backup-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../t-sql/statements/update-statistics-transact-sql.md)
