---
title: "What's New in CLR Integration"
description: Microsoft SQL Server hosting CLR is called CLR integration. This article describes new features in CLR integration in SQL Server 2012.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: clr
ms.topic: conceptual
ms.custom: intro-whats-new
ms.assetid: 871fcccd-b726-4b13-9f95-d02b4b39d8ab
---
# CLR Integration - What's New
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The following are new features in CLR integration in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]:  
  
-   In version 4 of the CLR, CLR database objects no longer catch corrupted state exceptions. These exceptions are now caught in the CLR integration hosting layer. These exceptions can still be caught by the CLR database components by setting a code attribute ([\<legacyCorruptedStateExceptionsPolicy> Element](/dotnet/framework/configure-apps/file-schema/runtime/legacycorruptedstateexceptionspolicy-element)). However, this is not recommended because results are not reliable when a corrupted state exception occurs.  
  
-   Due to the strict security requirements of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], CLR database components will continue to use the Code Access Security model defined in CLR version 2.0.  
  
-   In CLR version 4, a format error in a **System.TimeSpan** value will generate a **System.FormatExceptions**. Prior to version 4 of the CLR, a format error in a **System.TimeSpan** value was ignored. Database applications that rely on the behavior prior to version 4 of the CLR should run with a database compatibility level (**ALTER DATABASE Compatibility Level**) of 100 or lower. For more information, see [<TimeSpan_LegacyFormatMode> Element](/dotnet/framework/configure-apps/file-schema/runtime/timespan-legacyformatmode-element).  
  
-   Version 4 of the CLR supports Unicode 5.1. Sort operations involving some accent marks and symbols will be improved. Compatibility problems may occur if your application relies on legacy sorting behavior. To enable legacy sorting, the database compatibility level (**ALTER DATABASE Compatibility Level**) must be set to 100 or lower. To support this, [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] will install sort00001000.dll in the .NET Framework 4 directory (C:\Windows\Microsoft.NET\Framework\v4.0.30319). For more information, see [\<CompatSortNLSVersion> Element](/dotnet/framework/configure-apps/file-schema/runtime/compatsortnlsversion-element).  
  
-   The following columns have been added to [sys.dm_clr_appdomains](../../relational-databases/system-dynamic-management-views/sys-dm-clr-appdomains-transact-sql.md): **total_processor_time_ms**, **total_allocated_memory_kb**, and **survived_memory_kb**.  
  
