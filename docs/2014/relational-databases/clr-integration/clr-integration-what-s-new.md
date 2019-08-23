---
title: "What&#39;s New in CLR Integration | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
ms.assetid: 871fcccd-b726-4b13-9f95-d02b4b39d8ab
author: rothja
ms.author: jroth
manager: craigg
---
# What&#39;s New in CLR Integration
  The following are new features in CLR integration in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]:  
  
-   In version 4 of the CLR, CLR database objects no longer catch corrupted state exceptions. These exceptions are now caught in the CLR integration hosting layer. These exceptions can still be caught by the CLR database components by setting a code attribute ([\<legacyCorruptedStateExceptionsPolicy> Element](https://go.microsoft.com/fwlink/?LinkId=204954)). However, this is not recommended because results are not reliable when a corrupted state exception occurs.  
  
-   Due to the strict security requirements of [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], CLR database components will continue to use the Code Access Security model defined in CLR version 2.0.  
  
-   In CLR version 4, a format error in a `System.TimeSpan` value will generate a `System.FormatExceptions`. Prior to version 4 of the CLR, a format error in a `System.TimeSpan` value was ignored. Database applications that rely on the behavior prior to version 4 of the CLR should run with a database compatibility level (`ALTER DATABASE Compatibility Level`) of 100 or lower. For more information, see [<TimeSpan_LegacyFormatMode> Element](https://go.microsoft.com/fwlink/?LinkId=205109).  
  
-   Version 4 of the CLR supports Unicode 5.1. Sort operations involving some accent marks and symbols will be improved. Compatibility problems may occur if your application relies on legacy sorting behavior. To enable legacy sorting, the database compatibility level (`ALTER DATABASE Compatibility Level`) must be set to 100 or lower. To support this, [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] will install sort00001000.dll in the .NET Framework 4 directory (C:\Windows\Microsoft.NET\Framework\v4.0.30319). For more information, see [\<CompatSortNLSVersion> Element](https://go.microsoft.com/fwlink/?LinkId=205110).  
  
-   The following columns have been added to [sys.dm_clr_appdomains](/sql/relational-databases/system-dynamic-management-views/sys-dm-clr-appdomains-transact-sql): `total_processor_time_ms`, `total_allocated_memory_kb`, and `survived_memory_kb`.  
  
  
