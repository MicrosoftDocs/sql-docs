---
title: Optimize Download-only Article Performance (Merge)
description: Optimize merge replication performance with download-only articles in SQL Server. Compare article types, review requirements, and choose the best fit for your app.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "merge replication [SQL Server replication], download-only articles"
  - "articles [SQL Server replication], download-only"
  - "download-only articles"
---
# Optimize merge replication performance with download-only articles
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Merge replication offers two different article types to address different application needs. Publications can contain one or more of each of these article types as appropriate for the application:  
  
-   Standard articles  
  
-   Download-only articles  
  
 Download-only articles provide performance advantages over standard articles and should be used where appropriate.  
  
> [!NOTE]  
>  To use download-only articles, the compatibility level of the publication must be at least 90RTM.  
  
## Standard articles  
 Standard articles are the default, offering the full range of merge replication features, including rich conflict detection and resolution. Standard articles are appropriate for tables that are updated by multiple Subscribers. Objects other than tables, such as stored procedures and views, are always published as standard 
  
## Download-only articles  
 Download-only articles are designed for applications with data that isn't updated at Subscribers, such as a set of articles that are contained in a product catalog. A product catalog is typically updated at the Publisher, but not at the Subscribers. Because download-only articles can't be updated at the Subscriber, they don't send tracking metadata to Subscribers. This characteristic can reduce storage on the Subscribers and provides a performance benefit, especially if the network connection is slow.  
  
 Download-only articles work in conjunction with client subscriptions: if an article is designated as download-only, Subscribers who use client subscriptions can't insert, update, or delete rows for that article. Publishers and Subscribers that use the server subscription type (typically Subscribers that republish data to other Subscribers) can insert, update, and delete data. For more information about client subscriptions, see [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md).  
  
 To specify that an article is download-only, see [Specify Merge Replication properties](../../../relational-databases/replication/merge/specify-merge-replication-properties.md).  
  
## Using different article types in your applications  
 By understanding the requirements of your application, you can make tradeoffs between maximum flexibility and optimal performance. For example, applications with numerous conflicts and changes at both the Publisher and Subscribers use a publication made up of standard articles. Some applications, such as a sales force automation application, might have articles with a potential for conflicts, and other articles that function as lookup tables, which you can specify as download-only. Data entry applications, such as point of sales systems and field force automation applications, often strictly partition data in a way that conflicts are eliminated, and data from one Subscriber never goes to another. In these situations, a combination of nonoverlapping partitions, download-only articles, and precomputed partitions provides maximum performance and scalability. For more information about nonoverlapping partitions and precomputed partitions, see [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
## Related content

- [Article Options for Merge Replication](article-options-for-merge-replication.md)
- [Optimize Merge Replication Performance with Conditional Delete Tracking](optimize-merge-replication-performance-with-conditional-delete-tracking.md)
