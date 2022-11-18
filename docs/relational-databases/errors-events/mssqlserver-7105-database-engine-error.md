---
description: "MSSQLSERVER_7105"
title: MSSQLSERVER_7105
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "7105 (Database Engine error)"
ms.assetid: 
author: rgward
ms.author: ramakoni
---
# MSSQLSERVER_7105
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|7105|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|TXT_PGNOTEXIST|
|Message Text|The Database ID %d, Page %S_PGID, slot %d for LOB data type node does not exist. This is usually caused by transactions that can read uncommitted data on a data page. Run DBCC CHECKTABLE|

## Explanation

A query may encounter Msg 7105 when Large Object (LOB) data referenced by a database page row cannot be accessed.

Because this error is Severity Level 22, the connection is terminated by the server. This error message is also written into the SQL ERRORLOG file and Windows Application Event Log with EventID=7105.

## Possible causes

This error can occur due to one of the following reasons:

- A database corruption problem exists within a database page or within the LOB page structures the database page references.
- The query that is encountering the failure is using the `READ UNCOMMITTED ISOLATION LEVEL` or the `NOLOCK` query hint.
- A problem exists within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Engine causing the query to fail with this error.

See the Resolution and [More information](#more-information) sections to determine what is the cause for your specific problem and the appropriate solution.

## User action

1. As the message indicates, the first step you should take is to run `DBCC CHECKDB` against the database or `DBCC CHECKTABLE` against the table where the problem was encountered.

    - The database ID is provided in the message.
    - To find out the exact affected table without running `DBCC CHECKDB`, you will need to find out what tables were accessed by the query that encountered the error. One method is to use SQL Profiler to trace the query. However, in [!INCLUDE[sskatmai](../../includes/sskatmai-md.md)] and [!INCLUDE[sskatmai](../../includes/sskatmai-md.md)] R2 you may be able to find the query using the system_health Extended Events session. See this link for more information on how to use the system_health session: [Use the system_health Session](../extended-events/use-the-system-health-session.md).

    - As with any database consistency problem, you can resolve these errors by restoring from a known good Backup that does not contain this problem.

    - However, if you cannot restore from a Backup, follow the recommendations for `DBCC CHECKDB` or `DBCC CHECKTABLE` to repair these errors. It is possible that this will result in loss of data. For more information about using CHECKDB and causes of database corruption problems, see the article: [How to troubleshoot database consistency errors reported by DBCC CHECKDB](https://support.microsoft.com/kb/2015748).
  
1. It is possible this error was encountered because the query accessing the table was using an isolation level of `READ UNCOMMITTED` or the `NOLOCK` query hint (also known as a dirty read).

   - If `DBCC CHECKDB` or `DBCC CHECKTABLE` do not show any errors associated with this table and LOB data, then the most likely cause is the use of a dedirty read. If this is the case for your application, you will need to either avoid using a dirty read or retry the query.
  
   - If you find this is the cause of the error, no actual database consistency problem exists.

## More information

If database corruption is the cause for this problem, then `DBCC CHECKDB` and/or `DBCC CHECKTABLE` should report errors. However, these commands will not report Msg 7105. The errors you encounter from CHECKDB will depend on what is damaged within the reference to LOB structures or the LOB structures themselves.

- If the database page row does not correctly reference a valid LOB page, you may see errors like these:

    > Msg 8929, Level 16, State 1, Line 1  
    Object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039828480 (type In-row data): Errors found in off-row data with ID 131203072 owned by data record identified by RID = (1:179:1)  
    Msg 8964, Level 16, State 1, Line 1  
    Table error: Object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039894016 (type LOB data). The off-row data node at page (1:177), slot 1, text ID 131203072 is not referenced.  
    Msg 8965, Level 16, State 1, Line 1  
    Table error: Object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039894016 (type LOB data). The off-row data node at page (255:177), slot 1, text ID 131203072 is referenced by page (1:179), slot 1, but was not seen in the scan  

- Different scenarios for the problem can result in different combination of errors. In this example:  

    > Database Page 1:179, Slot 1 is referencing a LOB page that is not a valid page in the database (page 255:177). Page (1:177) is a valid LOB page but was never referenced by any database page. So in this situation, the problem is that the row in Slot 1 of Page 1:179 references page 255:177 instead of 1:177.

- The key to determining whether `DBCC CHECKDB` errors are related to LOB page problems by looking for the phrases 'off-row data' and 'type LOB data'.

    > Msg 8929 is an error related to the database page referencing the LOB pages.  
Msg 8964 is an error indicating a LOB page was not referenced by any database pages.  
Msg 8965 is an error indicating a LOB pages was referenced by a database page but doesn't exist as a valid page

    In many situations involving these types of errors, repair will result in the deletion of the rows pointing to LOB data and the LOB data itself. The repair algorithm will attempt to only remove LOB fragments that affect the database rows in question but that cannot be guaranteed in all situations depending on what is damaged within the LOB 'tree structure'.

- In the example shown here, the messages returned by a CHECKTABLE using `REPAIR_ALLOW_DATA_LOSS` look like:

    > Repair: Deleted record for object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039828480 (type In-row data), on page (1:179),  slot 1. Indexes will be rebuilt.  
    Repair: Deleted off-row data column with ID 131203072, for object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039894016 (type LOB data) on page (1:177), slot 1.  
    Msg 8929, Level 16, State 1, Line 1  
    Object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039828480 (type In-row data): Errors found in off-row data with ID 131203072 owned by data record identified by RID = (1:179:1)  
            The error has been repaired.  
    Msg 8964, Level 16, State 1, Line 1  
    Table error: Object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039894016 (type LOB data). The off-row data node at page (1:177), slot 1, text ID 131203072 is not referenced.  
            The error has been repaired.  
    Msg 8965, Level 16, State 1, Line 1  
    Table error: Object ID 2137058649, index ID 0, partition ID 72057594038910976, alloc unit ID 72057594039894016 (type LOB data). The off-row data node at page (255:177), slot 1, text ID 131203072 is referenced by page (1:179), slot 1, but was not seen in the scan.  
            Could not repair this error

    The last message that says `Could not repair this error` is misleading. The error was repaired because the database page row that pointed to the invalid page (255:177) was deleted.
