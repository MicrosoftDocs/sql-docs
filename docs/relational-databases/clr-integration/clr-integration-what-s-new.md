---
title: "What's New in CLR Integration"
description: Microsoft SQL Server hosting CLR is called CLR integration. This article describes new features in CLR integration in SQL Server 2012.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: whats-new
ms.custom:
  - intro-whats-new
---
# What's new in CLR integration?

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The following are new features in [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] common language runtime (CLR) integration in [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions:

- In version 4 of the CLR, CLR database objects no longer catch corrupted state exceptions. These exceptions are now caught in the CLR integration hosting layer. CLR database components can still catch these exceptions by setting a code attribute ([\<legacyCorruptedStateExceptionsPolicy> Element](/dotnet/framework/configure-apps/file-schema/runtime/legacycorruptedstateexceptionspolicy-element)). However, this attribute isn't recommended, because results aren't reliable when a corrupted state exception occurs.

- Due to strict security requirements in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], CLR database components continue to use the Code Access Security model defined in CLR version 2.0.

- In CLR version 4, a format error in a `System.TimeSpan` value generates a `System.FormatException` error. Before version 4 of the CLR, a format error in a `System.TimeSpan` value was ignored. Database applications that rely on the behavior before version 4 of the CLR should run with a [database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) of 100 or lower. For more information, see [<TimeSpan_LegacyFormatMode> Element](/dotnet/framework/configure-apps/file-schema/runtime/timespan-legacyformatmode-element).

- CLR version 4 supports Unicode 5.1. Sort operations involving some accent marks and symbols are improved. Compatibility problems might occur if your application relies on legacy sorting behavior. To enable legacy sorting, the [database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) must be set to 100 or lower. To support this functionality, [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] installs `sort00001000.dll` in the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] 4 directory (`C:\Windows\Microsoft.NET\Framework\v4.0.30319`). For more information, see [\<CompatSortNLSVersion> Element](/dotnet/framework/configure-apps/file-schema/runtime/compatsortnlsversion-element).

- The following columns were added to [sys.dm_clr_appdomains](../system-dynamic-management-objects/sys-dm-clr-appdomains-transact-sql.md): `total_processor_time_ms`, `total_allocated_memory_kb`, and `survived_memory_kb`.
