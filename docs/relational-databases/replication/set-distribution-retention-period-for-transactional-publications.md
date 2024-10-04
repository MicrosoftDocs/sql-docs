---
title: "Set Distribution retention period"
description: Set the retention period for data within the Distribution Database in SQL Server Management Studio (SSMS).
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "transaction retention periods [SQL Server replication]"
  - "retention periods [SQL Server replication]"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Set Distribution Retention Period for Transactional Publications
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Specify the minimum distribution retention period and maximum distribution retention period on the **Distribution Database Properties - \<DistributionDatabase>** dialog box. This is available from the **General** page of the **Distributor Properties - \<Distributor>** dialog box. For more information about accessing this dialog box, see [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
### To specify the distribution retention period  
  
1.  On the **General** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**...**) for the distribution database.  
  
2.  To specify the minimum distribution retention period, enter a value in the **At least** box; to specify the maximum distribution retention period, enter a value in the **But not more than** box.  
  
3.  Select **OK**.

## Related content

- [Configure Distribution](../../relational-databases/replication/configure-distribution.md)
- [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md)
