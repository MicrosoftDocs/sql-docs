---
title: "Subscription Validation Options (Transactional Subscriptions) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.validate.options.f1"
helpviewer_keywords: 
  - "Subscription Validation Options dialog box"
ms.assetid: fd66ad1f-df01-4240-9e89-8f41bff12c1e
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Subscription Validation Options (Transactional Subscriptions)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Use the **Subscription Validation Options** dialog box to specify whether validation should use a row count only, or a row count and a binary checksum.  
  
## Options  
 **Verify that the Subscriber has the same number of rows of replicated data as the Publisher**  
 Select the type of row count validation to perform. For Oracle publications, the only available option is **Compute an actual row count by querying the tables directly**.  
  
 **Compare checksums to verify row data**  
 In addition to taking a count of rows at the Publisher and Subscriber, a checksum of all the data is calculated using the binary checksum algorithm. If the row count fails, the checksum is not performed.  
  
 **Stop the Distribution Agent after the validation has completed**  
 By default, the Distribution Agent runs continuously. Select this option to stop the agent after validation is performed. This allows you to check whether validation was successful before continuing to replicate data to the Subscriber.  
  
## See Also  
 [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md)   
 [Validate Replicated Data](../../relational-databases/replication/validate-data-at-the-subscriber.md)  
  
  
