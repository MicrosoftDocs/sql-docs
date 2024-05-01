---
title: "Set history retention period (SSMS)"
description: Learn how to set the distribution database history retention period in SQL Server Management Studio (SSMS).
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "history retention periods [SQL Server replication]"
  - "retention periods [SQL Server replication]"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Set the History Retention Period (SQL Server Management Studio)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Specify the history retention period on the **General** page of the **Distribution Database Properties - \<DistributionDatabase>** dialog box. This setting controls how long replication agent history is stored. This page is available from the **General** page of the **Distributor Properties - \<Distributor>** dialog box. For more information about accessing this dialog box, see [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
### To specify the history retention period  
  
1.  On the **General** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**…**) for the distribution database.  
  
2.  Enter a value in the **Store replication performance history at least** box.  
  
3.  Select **OK**.
  
## See Also  
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
  
  
