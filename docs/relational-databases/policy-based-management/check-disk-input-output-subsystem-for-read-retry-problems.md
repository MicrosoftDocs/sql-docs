---
title: "Check disk IO Subsystem for read retry problems - Policy-Based Management"
description: This rule checks the event log for SQL Server error message 825, which indicates SQL Server was unable to read data from the disk on the first try.
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Check disk input-output subsystem for read retry problems

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks the event log for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error message 825. This message indicates that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] was unable to read data from the disk on the first try. This message indicates a major problem with the disk I/O subsystem. This message doesn't currently indicate a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] problem. However, the disk problem could cause data loss or database corruption if it isn't resolved.

## Best practices recommendations

The following actions might help you discover and resolve the underlying hardware problem:

- Review the error log and the variable text in this message for clues that explain the problem.

- Check the disk system. The problem could be related to the disks, the disk controllers, array cards, or disk drivers.

- Contact the disk manufacturer for the latest utilities for checking the status of the disk system.

- Contact the disk manufacturer for the latest driver updates.

## Related content

[MSSQLSERVER_825](../errors-events/mssqlserver-825-database-engine-error.md)

[SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10))
