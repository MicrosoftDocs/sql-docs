---
title: "Place Data and Log Files on Separate Drives"
description: Place data and log files on separate logical drives. Separate locations allow activity for each to occur at the same time, improving SQL performance.
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Place data and log files on separate drives

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether data and log files are placed on separate logical drives. Placing both data AND log files on the same device can cause contention for that device, resulting in poor performance. Placing the files on separate drives allows the I/O activity to occur at the same time for both the data and log files.

## Recommendations

When you create a new database, specify separate drives for the data and logs. To move files after the database is created, the database must be taken offline. Move files by using one of the following methods:

> [!NOTE]  
> This policy cannot detect separate physical devices through mount points

- Restore the database from backup by using the RESTORE DATABASE statement with the WITH MOVE option.

- Detach and then attach the database specifying separate locations for the data and log devices.

- Specify a new location by running the ALTER DATABASE statement with the MODIFY FILE option, and then restarting the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## For more information

[Move Database Files](../databases/move-database-files.md)

[Move User Databases](../databases/move-user-databases.md)

[Database Detach and Attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
