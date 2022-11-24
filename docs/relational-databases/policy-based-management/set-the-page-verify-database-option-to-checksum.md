---
title: "Set the PAGE_VERIFY Database Option to CHECKSUM | Microsoft Docs"
description: Check whether PAGE_VERIFY option is CHECKSUM, which controls whether the SQL Server Database Engine calculates a checksum to help provide data-file integrity.
ms.custom: ""
ms.date: "08/19/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 686b9a4a-ea61-4263-9ab8-f444a3077679
author: VanMSFT
ms.author: vanto
---
# Set the PAGE_VERIFY Database Option to CHECKSUM
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether PAGE_VERIFY database option is set to CHECKSUM. When CHECKSUM is enabled for the PAGE_VERIFY database option, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] calculates a checksum over the contents of the whole page, and stores the value in the page header when a page is written to disk. When the page is read from disk, the checksum is recomputed and compared to the checksum value that is stored in the page header. This helps provide a high level of data-file integrity.  If you use the PAGE VERIFY CHECKSUM option for a database, when SQL Server detects a page has been altered after it has been written to disk, SQL Server reports [Msg 824](../errors-events/mssqlserver-824-database-engine-error.md) after reading the page back from disk. 
  
## Best Practices Recommendations  
 Set the PAGE_VERIFY database option to CHECKSUM. Using the PAGE_VERIFY CHECKSUM database option can provide the most robust detection of database consistency problems caused by the system I/O path.
  
## For More Information  
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)  
  
  
