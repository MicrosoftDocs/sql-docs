---
title: "MSSQLSERVER_3624"
description: "MSSQLSERVER_3624"
author: pijocoder
ms.author: mathoma
ms.reviewer: pijocoder
ms.date: 01/26/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3624 (Database Engine error)"
---
# MSSQLSERVER_3624

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 3624 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | SYS_ASSERTFAIL |
| Message Text | A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support. |

## Explanation

This error is most commonly raised together with [MSSQLSERVER_17066](mssqlserver-17066-database-engine-error.md)

 Error: 17066, Severity: 16, State: 1.
SQL Server Assertion: File: <"e:\\b\\s3\\sources\\sql\\ntdbms\\hekaton\\engine\\hadr\\physical\\ckptctrlprocesslogrecord.cpp">, line=1634 Failed Assertion = 'existingState == DB_Unencrypted'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.
Error: 3624, Severity: 20, State: 1.
A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support.


Error: 17066, Severity: 16, State: 1.
SQL Server Assertion: File: <"xdes.cpp">, line=4919 Failed Assertion = 'lck_sufficient (lckMode, LCK_M_IX) || lck_sufficient (lckMode, LCK_M_BU)'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.
Error: 3624, Severity: 20, State: 1.
A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support.





Error: 17066, Severity: 16, State: 1.
SQL Server Assertion: File: <diskio.cpp>, line=2902 Failed Assertion = 'filepos + cBytes <= GetMaxOffsetForIO ()'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.

Error: 3624, Severity: 20, State: 1.
A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support.

## Cause

## User Action

1. Check your build of SQL Server , see [Determine which version and edition of SQL Server Database Engine is running](/troubleshoot/sql/releases/find-my-sql-version)
1. Find the [Latest updates available for your SQL Server version ](/troubleshoot/sql/releases/download-and-install-latest-updates#latest-updates-available-for-currently-supported-versions-of-sql-server) or best open the [Excel file](https://download.microsoft.com/download/d/3/e/d3e28f3d-6a4f-47ce-aaa5-9d74c5590ed6/SQLServerBuilds.xlsx) that lists all fixes for all builds
1. Review if the articles or Excel file for any assert fixes your build and if you find that a later build exists, please install that build
1. In some cases you can search for the specific assert condition in Failed Assertion part of the 17066 error. For example, in the above message search for "lck_sufficient (lckMode, LCK_M_IX)"

