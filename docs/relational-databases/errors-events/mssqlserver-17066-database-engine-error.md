---
title: "MSSQLSERVER_17066"
description: "MSSQLSERVER_17066"
author: pijocoder
ms.author: jopilov
ms.reviewer: mathoma
ms.date: 02/01/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17066 (Database Engine error)"
---
# MSSQLSERVER_17066

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 17066 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | SQLASSERT_ONLY |
| Message Text | SQL Server Assertion: File: \<%s>, line=%d Failed Assertion = '%s'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted. |

## Explanation

Asserts are statements placed in the code of an application to ensure that certain conditions are satisfied. In that respect an assert behaves similar to an error. You can think of asserts as emphatic, firm errors. The condition specified must be met for the program to continue regular execution. If the condition isn't met, the assert is raised. For more information, see [C/C++ Assertions](/visualstudio/debugger/c-cpp-assertions).

SQL Server uses asserts in many places to ensure that conditions are true. For example, the condition 'existingState == DB_Unencrypted' asserts that a database state is unencrypted before the next command in the code is run. If that's not the case, the assert is raised. Error 3624 notifies you that such a condition wasn't met and an assert was raised.

Error 3624 is raised together with [MSSQLSERVER_3624](mssqlserver-3624-database-engine-error.md). Here are examples of how you can see these errors in the SQL Server error log. You'll also see the assert condition raised during run time and the error is sent from SQL Server to the client application.

 ```output
Error: 17066, Severity: 16, State: 1.
SQL Server Assertion: File: <"e:\\b\\s3\\sources\\sql\\ntdbms\\hekaton\\engine\\hadr\\physical\\ckptctrlprocesslogrecord.cpp">, line=1634 Failed Assertion = 'existingState == DB_Unencrypted'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.

Error: 3624, Severity: 20, State: 1.
A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support.
```

```output
Error: 17066, Severity: 16, State: 1.
SQL Server Assertion: File: <"xdes.cpp">, line=4919 Failed Assertion = 'lck_sufficient (lckMode, LCK_M_IX) || lck_sufficient (lckMode, LCK_M_BU)'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.

Error: 3624, Severity: 20, State: 1.
A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support.
```

```Error: 17066, Severity: 16, State: 1.
SQL Server Assertion: File: <diskio.cpp>, line=2902 Failed Assertion = 'filepos + cBytes <= GetMaxOffsetForIO ()'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.

Error: 3624, Severity: 20, State: 1.
A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support.
```

## Cause

Here are some possible reasons for an assert failure to occur:

**Product Bug**  
A common reason for assert failures are issues in the product that lead to the conditions not to be true. These issues need to be investigated by Microsoft and when reproduced and understood can be fixed.

**Database corruption**
Another common cause for assert failures is physical inconsistencies in databases. Damaged data structures, when read in memory, can cause the expected conditions to be false and thus raise an assert.

**External library or filter driver**
A somewhat common cause for assert messages has been found to be caused by external DLL inside SQL Server memory or a filter driver designed to monitor or intercept SQL Server activity. If such an external component modifies objects or structures that SQL Server uses, assert failures can occur. For more information, see [Performance and consistency issues when certain modules or filter drivers are loaded](/troubleshoot/sql/database-engine/performance/performance-consistency-issues-filter-drivers-modules).

**Hardware problem (memory, CPU)**
Faulty hardware can cause corruption of data structures in memory and therefore lead to assert failures. This issue is less common but it occurs.

## User Action

1. Check your build of SQL Server, see [Determine which version and edition of SQL Server Database Engine is running](/troubleshoot/sql/releases/find-my-sql-version)
1. Find the [Latest updates available for your SQL Server version](/troubleshoot/sql/releases/download-and-install-latest-updates#latest-updates-available-for-currently-supported-versions-of-sql-server) or best open the [Excel file](https://aka.ms/sqlserverbuilds) that lists all fixes for all builds
1. Review the articles or Excel file for any assert fixes released *after* your current SQL Server build. If you find a later build that fixes assert issues, consider upgrading to that build.
1. In some cases, you can search for the specific assert condition in Failed Assertion part of the 17066 error. For example, in the above message search for `lck_sufficient (lckMode, LCK_M_IX)`. This expression will help you with a more accurate search for a matching issue. This expression is the condition that triggers the assert in the first place and it's quite specific.
1. Run DBCC CHECKDB on your databases. If [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) reports database corruption errors, focus on resolving that issue before anything else. Restore a clean database backup and investigate root cause for the database inconsistencies. For more information, see [Troubleshoot database consistency errors reported by DBCC CHECKDB](/troubleshoot/sql/database-engine/database-file-operations/troubleshoot-dbcc-checkdb-errors).
1. Check if there are external modules loaded in SQL Server memory space and also check for filter drivers

   ```sql
   SELECT * FROM sys.dm_os_loaded_modules
   WHERE company != 'Microsoft Corporation'
   ```

   For filter drivers, run the following command from Command Prompt

   ```bash
   fltmc filters
   ```

   Follow the recommendations in [Performance and consistency issues when certain modules or filter drivers are loaded](/troubleshoot/sql/database-engine/performance/performance-consistency-issues-filter-drivers-modules).

1. If you've already upgraded your SQL Server to the latest Cumulative Update, and DBCC CHECKDB doesn't report any errors, then reach out to Microsoft technical support and be ready to provide the following information:
    1. SQL Server error logs from the \Log folder
    1. SQL Server memory dumps (SQLDump00xx.mdmp) generated in the \Log folder
    1. Steps to reproduce the assert when available. What query or action leads to the assert to be raised?
    1. Output from `fltmc filters` and from the `sys.dm_os_loaded_modules DMV.
