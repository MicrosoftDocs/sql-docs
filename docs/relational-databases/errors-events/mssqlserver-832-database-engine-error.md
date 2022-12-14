---
description: "MSSQLSERVER_832"
title: MSSQLSERVER_832
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "832 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_832
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|832|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|B_CONSTPAGECHANGED|
|Message Text|A page that should have been constant has changed (expected checksum: \<expected value>, actual checksum: \<actual value>, database \<dbid>, file \'\<filename\>', page \<pageno>). This usually indicates a memory failure or other hardware or OS corruption.|

## Explanation

An external factor has caused a database page to be modified outside the normal [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine code used to change database pages.  The conditions could be:  

- A thread running in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process that incorrect writes on a database page. This is often referred to as a "scribbler"
- A hardware or operating system problem were the memory backing the database page is incorrect modified or damaged  

When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] detects this behavior error 832 is raised.

## User action

To find the cause of the error, consider these options:

- You should run any normal hardware or system checks to determine if a memory, CPU, or other hardware-related problem exists. Ensure all system drivers, Operating system updates, and hardware updates have been applied to our system. Consider running any hardware manufacture diagnostics including memory-related tests.
- Evaluate what "external" DLLs may be loaded in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that could cause this problem including extended stored procedures, COM objects, or other DLLs that may be incorrectly modifying [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory reserved for database pages.  

Anytime you see this error, you should immediately consider running `DBCC CHECKDB` against the database referenced by the \<dbid> in the error message.

## More information

This error is detected by the background task often referred to as the LazyWriter. (The "command" for this task is seen as LAZY WRITER). Therefore, this error is not returned to a client application. The error will be written to the Windows Application Event Log as EventID=832.  

Only pages that are not currently modified in cache (or "dirty") are checked. It is why the message uses the terms "constant" because the page has never been changed since it was read in from disk. Furthermore, the page was read in "clean" from disk because it has a checksum value on the page and has not encountered a checksum failure (Msg 824). However, the page could be modified at some point after this error, and then written to disk with the incorrect modification. If this error occurs, a new checksum is calculated based on all modifications before it is written to disk. Therefore, the page could be damaged on disk but a subsequent read from disk may not trigger a checksum failure. It is important to run `DBCC CHECKDB` on any database that is referenced by this error.  

It is possible that even `DBCC CHECKDB` will not report an error for a page in this state after being written to disk. It is because the incorrect modification could be at locations on the page that don't hold any data, nor contain any important page or row structure information, or could be modifications to data that CHECKDB cannot detect.  

More details and information about Msg 832 can also be read in the whitepaper [SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)).
