---
description: "MSSQLSERVER_854"
title: MSSQLSERVER_854
ms.custom: ""
ms.date: 08/20/2020
ms.prod: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "854 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni,TejasAks,VenCher,AjayJ,SureshKa
---
# MSSQLSERVER_854

 [!INCLUDE [SQL Server](../../includes/ssnoversion-md.md)]
 [!INCLUDE [SQL Server 2019](../../includes/sssqlv15-md.md)]
 [!INCLUDE [SQL Server 2017](../../includes/sssql17-md.md)]
 [!INCLUDE [SQL Server 2016](../../includes/sssql15-md.md)]
 [!INCLUDE [SQL Server 2014](../../includes/sssql14-md.md)]
 [!INCLUDE [SQL Server 2012](../../includes/sssql11-md.md)]
 [!INCLUDE [SQL Server 2008](../../includes/sskatmai-md.md)]
 [!INCLUDE [Azure SQL DB](../../includes/sssdsfull-md.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|854|
|Event Source|MSSQLSERVER|
|Component|SQL SQLEngine|
|Symbolic Name|HARDWARE_MEMORY_SCRUBBER|
|Message Text|Machine supports memory error recovery. SQL memory protection is enabled to recover from memory corruption|
||

## Explanation

This message indicates the the hardware in the operating system supports the ability to recover from memory errors. On computers that have newer hardware and are running Windows Server 2012 or a later version, the hardware can notify the operating system and applications that memory pages (operating system pages) are marked as bad or damaged. Applications such as SQL Server can register these bad memory page notifications by using the following API set:

- `GetMemoryErrorHandlingCapabilities`
- `RegisterBadMemoryNotification`
- `BadMemoryCallbackRoutine`

SQL Server adds support for these notifications in Microsoft SQL Server 2012 and later versions. During SQL Server startup, SQL Server checks whether the hardware supports this new feature. Additionally, you receive the following message in the error log:

> \<Datetime> Server Machine supports memory error recovery. SQL memory protection is enabled to recover from memory corruption.

## User action

Check if you are encountering other errors like 855 and 856 and take appropriate corrective action.

## More information

You can use SQL Server trace flag 849 to keep SQL Server from registering with the operating system for memory error notifications. However, be aware that trace flag 849 will disable SQL Server from receiving bad memory notifications from operating system. Therefore, we do not recommend that you use this trace flag under typical circumstances.
