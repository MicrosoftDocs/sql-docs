---
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.topic: include
---
> [!NOTE]  
> [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] support changing the default time zone from universal coordinated time (UTC). If you modify the time zone at the [database](../statements/alter-database-scoped-configuration-transact-sql.md#local-time-zone) or [session](../statements/set-time-zone-transact-sql.md) level, this function returns local time based on the configured time zone value. For more information, see [ALTER DATABASE SCOPED CONFIGURATION](../statements/alter-database-scoped-configuration-transact-sql.md) and [SET TIME ZONE](../statements/set-time-zone-transact-sql.md).
