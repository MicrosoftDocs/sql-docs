---
description: "MSSQLSERVER_856"
title: MSSQLSERVER_856
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "856 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_856
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|856|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|BAD_MEMORY_CLEAN_DATABASE_PAGE|
|Message Text|SQL Server has detected hardware memory corruption in database '%ls', file ID: %u, page ID; %u, memory address: 0x%I64x and has successfully recovered the page|

## Explanation

This message indicates [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] detected a bad memory page in a cached object outside of the buffer pool. This message is raised on systems that supports the ability to recover from memory errors. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] corrects these memory errors by discarding the corrupt memory pages that are not modified and logs this error message. If the corrupt memory page had been modified (dirty) then error 824 is raised ([MSSQLSERVER_824](mssqlserver-824-database-engine-error.md))

## User action

You should run hardware or system checks to determine if a memory or CPU problems exists. Ensure all system drivers, Operating system updates, and hardware updates have been applied to your system. Consider running any hardware manufacture diagnostics including memory related tests. Anytime you see this error, consider running `DBCC CHECKDB` against all databases in this instance.

## More information

On computers that have newer hardware and are running Windows Server 2012 or a later version, the hardware can notify the operating system and applications that memory pages (operating system pages) are marked as bad or damaged. Applications such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can register these bad memory page notifications by using the following API set:

- `GetMemoryErrorHandlingCapabilities`
- `RegisterBadMemoryNotification`
- `BadMemoryCallbackRoutine`

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adds support for these notifications in Microsoft [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions. During [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] checks whether the hardware supports this new feature. Additionally, you receive the following message in the error log:

> \<Datetime> Server Machine supports memory error recovery. SQL memory protection is enabled to recover from memory corruption.

Currently, only the buffer pool takes action when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives these notifications. When it receives a notification, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has to iterate through the whole buffer pool and discover the address for each allocated buffer. Then, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the `QueryWorkingSetEX` API to check whether any of the memory pages that back the data page is marked as bad. The `PSAPI_WORKING_SET_EX_BLOCK` output structure that corresponds to this memory page will have its member bad set to 1 if there is any damaged reported.

If that buffer pool or data page is currently not changed or not processing I/O, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can discard and de-commit the data page. Then, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logs the following message:

> SQL Server has detected hardware memory corruption in database '%ls', file ID: %u, page ID; %u, memory address: 0x%I64x and has successfully recovered the page.

When queries require that data page again, the buffer pool can read the data page back from disk and bring the contents back to the buffer pool. It is also possible for the on-disk version of the page to be in a damaged state. In that case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may log additional errors such as error 824.

If the bad page is used not by the buffer pool but by some other cached object or structure, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logs the following message:

> Uncorrectable hardware memory corruption detected. Your system may become unstable. Please check the Windows event log for more details.

If the server is reporting memory errors, you should contact the computer hardware vendor and perform appropriate actions such as performing memory diagnostics, updating BIOS and firmware, and replacing bad memory modules.

You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] trace flag 849 to keep [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from registering with the operating system for memory error notifications. However, be aware that enabling trace flag 849 will prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from receiving bad memory notifications from operating system. Therefore, we do not recommend that you use this trace flag under typical circumstances.

Also, be aware that, by default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will receive these notifications on supported hardware.

You should also be aware that when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] registers for these memory error notifications, the lazy writer system process does not perform constant page checks. For more information about constant page checks, see [How to troubleshoot Msg 832 (constant page has changed) in SQL Server](https://support.microsoft.com/help/2015759).
