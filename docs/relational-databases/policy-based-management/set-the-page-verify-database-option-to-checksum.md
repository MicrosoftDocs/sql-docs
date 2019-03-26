---
title: "Set the PAGE_VERIFY Database Option to CHECKSUM | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 686b9a4a-ea61-4263-9ab8-f444a3077679
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Set the PAGE_VERIFY Database Option to CHECKSUM
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This rule checks whether PAGE_VERIFY database option is set to CHECKSUM. When CHECKSUM is enabled for the PAGE_VERIFY database option, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] calculates a checksum over the contents of the whole page, and stores the value in the page header when a page is written to disk. When the page is read from disk, the checksum is recomputed and compared to the checksum value that is stored in the page header. This helps provide a high level of data-file integrity.  
  
## Best Practices Recommendations  
 Set the PAGE_VERIFY database option to CHECKSUM.  
  
## For More Information  
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)  
  
  
