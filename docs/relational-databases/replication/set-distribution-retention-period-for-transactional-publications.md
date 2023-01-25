---
title: "Set Distribution retention period"
description: Set the retention period for data within the Distribution Database in SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "transaction retention periods [SQL Server replication]"
  - "retention periods [SQL Server replication]"
ms.assetid: 9a98c53a-fea5-4235-b23d-6c69587c5676
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Set Distribution Retention Period for Transactional Publications
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Specify the minimum distribution retention period and maximum distribution retention period on the **Distribution Database Properties - \<DistributionDatabase>** dialog box. This is available from the **General** page of the **Distributor Properties - \<Distributor>** dialog box. For more information about accessing this dialog box, see [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
### To specify the distribution retention period  
  
1.  On the **General** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**...**) for the distribution database.  
  
2.  To specify the minimum distribution retention period, enter a value in the **At least** box; to specify the maximum distribution retention period, enter a value in the **But not more than** box.  
  
3.  Select **OK**.

## See Also  
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md)  
  
  
