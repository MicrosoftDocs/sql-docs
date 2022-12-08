---
description: "Filter Published Data for Merge Replication"
title: "Filter Published Data for Merge Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication [SQL Server replication], filtering published data"
  - "replication [SQL Server], filtering published data"
ms.assetid: 46c5023d-7a3b-4455-becc-e159fcb5d6c4
author: "MashaMSFT"
ms.author: "mathoma"
---
# Filter Published Data for Merge Replication
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  In addition to the static row filters and column filters you can define with other types of replication, merge replication offers parameterized row filters and join filters. For more information about static row filters and column filters, see [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md).  
  
 Merge replication is used in many applications that support mobile users; these applications usually have a large number of subscriptions with each subscription receiving a unique data set. Parameterized filters combined with join filters allow an administrator to set up one publication (or at most a small number of publications) and yet provide different data sets to users, reducing the management overhead introduced by creating multiple publications.  
  
-   Parameterized filters allow different partitions of data to be sent to different Subscribers without requiring multiple publications to be created. For example, a table can be filtered so that data for a given sales representative is replicated only to that representative. Parameterized filters have a variety of options that allow you to tailor filtering to optimize performance and best match your data and application requirements. For more information, see [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
-   Join filters are typically used in conjunction with parameterized filters to extend filtering to related tables (they can also be used in conjunction with static filters). For example, the sales representative typically has data in other tables such as customer and order tables. This data can be filtered so the sales representative receives only the data on her customers and her customers' orders. For more information, see [Join Filters](../../../relational-databases/replication/merge/join-filters.md).  
  
 A filter must not include the **rowguidcol** used by replication to identify rows. By default this is the column added at the time you set up merge replication and is named **rowguid**.  
  
## See Also  
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
