---
description: "MSSQLSERVER_854"
title: MSSQLSERVER_854
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "854 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_854
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|854|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|HARDWARE_MEMORY_SCRUBBER|
|Message Text|Machine supports memory error recovery. SQL memory protection is enabled to recover from memory corruption|

## Explanation

This message indicates the the hardware in the operating system supports the ability to recover from memory errors. On computers that have newer hardware and are running Windows Server 2012 or a later version, the hardware can notify the operating system and applications that memory pages (operating system pages) are marked as bad or damaged. Applications such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can register these bad memory page notifications by using the following API set:

- `GetMemoryErrorHandlingCapabilities`
- `RegisterBadMemoryNotification`
- `BadMemoryCallbackRoutine`

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adds support for these notifications in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012 and later versions. During [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] checks whether the hardware supports this new feature. Additionally, you receive the following message in the error log:

> \<Datetime> Server Machine supports memory error recovery. SQL memory protection is enabled to recover from memory corruption.

## User action

Check if you are encountering other errors like 855 and 856 and take appropriate corrective action.

## More information

You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] trace flag 849 to keep [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from registering with the operating system for memory error notifications. However, be aware that enabling trace flag 849 will prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from receiving bad memory notifications from operating system. Therefore, we do not recommend that you use this trace flag under typical circumstances.
