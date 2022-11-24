---
title: "Subscription Validation Options (Merge)"
description: Describes the 'Subscription Validation Options' dialog box within SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.validate.mergeoptions.f1"
helpviewer_keywords: 
  - "Subscription Validation Options dialog box"
ms.assetid: 4958c4ab-2025-42ce-b836-6fb4e9e6f24d
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-current||>=sql-server-2016"
---
# Subscription Validation Options (Merge Subscriptions)
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  Use the **Subscription Validation Options** dialog box to specify whether validation should use a row count only or a row count and a binary checksum.  
  
## Options  
 **Verify the row counts only**  
 Select to validate whether the table at the Subscriber has the same number of rows as the table at the Publisher. This method does not validate that the content of the rows matches. Row count validation provides a lightweight approach to validation that can make you aware of issues with your data.  
  
 **Verify the row counts and compare checksums to verify the row data**  
 In addition to taking a count of rows at the Publisher and Subscriber, a checksum of all the data is calculated using the binary checksum algorithm. If the row count fails, the checksum is not performed. This option is not valid for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssEW](../../includes/ssew-md.md)].  
  
## See Also  
 [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md)   
 [Validate Replicated Data](../../relational-databases/replication/validate-data-at-the-subscriber.md)  
  
  
