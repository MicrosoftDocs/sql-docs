---
description: "MSSQLSERVER_5180"
title: MSSQLSERVER_5180
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "5180 (Database Engine error)"
ms.assetid: 
author: rgward
ms.author: ramakoni
---
# MSSQLSERVER_5180
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|5180|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|DSK_BAD_FCB_FILEID|
|Message Text|Could not open File Control Bank (FCB) for invalid file ID %d in database '%.*ls'. Verify the file location. Execute DBCC CHECKDB.|

## Explanation

A query or operation may fail with an error 5180 when an invalid file ID is referenced by the [!INCLUDE[ssDENoVersion](../../includes/ssdenoversion-md.md)]. This is an example:

> Msg 5180, Level 22, State 1, Line 1  
Could not open File Control Bank (FCB) for invalid file ID %d in database '%.*ls'. Verify the file location. Execute DBCC CHECKDB.

Since the error is raised with severity 22, the user's session will be disconnected. This error message is written into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Error Log and the Windows Application Event Log with EventID = 5180.

## Possible causes

The [!INCLUDE[ssDENoVersion](../../includes/ssdenoversion-md.md)] references a file ID in many different situations mostly when referencing a page ID (since the file ID is the first portion of the page ID). If for any reason, the file ID being referenced is < 0 or is not a valid file ID in a database (per the valid file IDs listed in system catalog views such as sys.database_files), then a 5180 error can be encountered.

One possible cause is that a stored file ID is not valid. Since the forwarded record in a row references another page, when that page is accessed and the file ID is invalid, a 5180 error could be encountered. This condition would be a database corruption error on the page with the forwarded record. (In this example, `DBCC CHECKDB` would report Msg 8993).

Another example is an invalid file ID as part of a page ID as stored in the page header for a next or prev page ID. This field is used to link a series of pages such as in a clustered index. If the file ID is invalid for the prev or next page, when the engine must reference this to traverse to the next or previous page, a 5180 error can be encountered. (In this example, `DBCC CHECKDB` reports Msg 8981).

If the problem is not an invalid file ID due to a corrupted stored page ID, then the problem may be within the [!INCLUDE[ssDENoVersion](../../includes/ssdenoversion-md.md)].

## User action

If you encounter this error, you should run `DBCC CHECKDB` against the database as listed in the error message. If you find errors, you should restore from a known good Backup. If you cannot restore from a Backup, then your other option is to use the repair option of `DBCC CHECKDB` as recommended by its output. In most cases, a repair of this type of error will result in a data loss. For more information about using  and causes of database corruption problems, see the article: [How to troubleshoot database consistency errors reported by DBCC CHECKDB](https://support.microsoft.com/kb/2015748).

If `DBCC CHECKDB` does not report any error and the problem continues to occur, you should contact Microsoft Technical Support for assistance. Be prepared to provide the query that is encountering the 5180 error. See the [More information](#more-information) section on how to determine what query encountered the error.

## More information

The File Control Block (FCB) is an internal memory structure that keeps track of important information about the file associated with the database. A file ID is used to uniquely identify an FCB for a database. If the server engine tries to reference a file ID that is invalid, the FCB structure cannot be located which triggers the 5180 error condition.

To find out what query encountered this error, you can use the following techniques:

- For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008 and later versions, see if the system_health session has a record of the error, which should include the query text. See resource for more information about the system_health session: [Supporting SQL Server 2008: The system_health session](https://techcommunity.microsoft.com/t5/sql-server-support/supporting-sql-server-2008-the-system-health-session/ba-p/315509).
- Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler and capture the `SQL:BatchStarting`, `RPC:Starting`, and Exception Events. Find the query that precedes the Exception Event for 5180 for the session associated with the Exception Event.
