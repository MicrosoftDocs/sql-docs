---
title: "SQL Server Properties (FILESTREAM Tab)"
description: Find out how to use the FILESTREAM tab of the SQL Server Properties dialog box to enable FILESTREAM for an installation of SQL Server 2019.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---
# SQL Server Properties (FILESTREAM tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use this page to enable [FILESTREAM](../../relational-databases/blob/filestream-sql-server.md) for this installation of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

## Options

#### Enable FILESTREAM for Transact-SQL access

Select to enable FILESTREAM for [!INCLUDE [tsql](../../includes/tsql-md.md)] access. This control must be checked before the other control options are available.

#### Enable FILESTREAM for file I/O streaming access

Select to enable Win32 streaming access for FILESTREAM.

#### Windows share name

The name of the Windows share for storing FILESTREAM data.

#### Allow remote clients to have streaming access to FILESTREAM data

Select this control to allow remote clients to access this FILESTREAM data on this server.
