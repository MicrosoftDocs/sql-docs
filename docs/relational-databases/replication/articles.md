---
description: "Articles"
title: "Articles | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.articles.f1"
ms.assetid: 7c743dc6-6c6d-4c92-b711-842e1b0b273e
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Articles
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  On the **Articles** page, you specify which database objects to include as articles in the publication. If you are publishing a database object that depends on one or more other database objects, you must publish all referenced objects. For example, if you publish a view that depends on a table, you must publish the table also.  
  
 Objects that cannot be published have a red icon next to them, with an explanation in the information panel at the bottom of the wizard page. The following objects cannot be published:  
  
-   Encrypted objects.  
  
-   Tables without primary keys cannot be published in transactional publications.  
  
-   Tables cannot be published in both a merge publication and a transactional publication enabled for queued updating subscriptions. For more information about publishing an article in more than one publication, see the "Publishing Tables in More Than One Publication" section in [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md).  
  
## Oracle Publishers  
 There are additional considerations for Oracle Publishers:  
  
-   For a list of objects that can be published from Oracle, see [Design Considerations and Limitations for Oracle Publishers](../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md). Objects that cannot be published are not displayed.  
  
-   For a list of data types that can be published, see [Data Type Mapping for Oracle Publishers](../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md). Columns with data types that cannot be published are not displayed.  
  
## Column Filters  
 Filter columns on this page by expanding a table in the **Objects to publish** pane and then selecting only the columns required (rows can be filtered in the **Filter Table Rows** page of this wizard). Filtering columns is useful for a number of reasons, including security (preventing sensitive data from being replicated) and performance (avoiding replication of large binary large object (BLOB) columns, for example). For more information about column filtering, including a list of column types that cannot be filtered, see [Filter Published Data](../../relational-databases/replication/publish/filter-published-data.md).  
  
## Options  
 The **Objects to publish** pane allows you to:  
  
-   View all objects available for replication.  
  
-   Include an object in a publication by selecting the check box next to that object.  
  
-   Include all objects of a particular type (such as a table) in the publication by selecting the check box next to that object type (such as **Tables**).  
  
-   Expand table nodes to see the columns in the table.  
  
-   Filter table columns out of a publication by clearing the check box next to the column.  
  
-   Right-click an object in the pane to see a menu of commands for that object.  
  
 **Article Properties**  
 Click **Article Properties**, and then click one of the following:  
  
-   Click **Set Properties of Highlighted \<ObjectType> Article** to launch the **Article Properties - \<ObjectName>** dialog box; property changes made in this dialog box are applied only to the object that is highlighted in the object pane on the **Articles** page.  
  
-   Click **Set Properties of All \<ObjectType> Articles**, to launch the **Properties for All \<ObjectType> Articles** dialog box; property changes made in this dialog box are applied to all objects of that type in the object pane on the **Articles** page, including ones not yet selected for publication.  
  
    > [!NOTE]  
    >  Property changes made in the **Properties for All \<ObjectType> Articles** dialog box override any made previously in the **Article Properties - \<ObjectName>** dialog box. If, for example, you want to set a number of defaults for all articles of an object type, but also want to set some properties for individual objects, set the defaults for all articles first. Then set the properties for the individual objects.  
  
 **Highlighted table is download-only**  
 Merge replication only. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only. Select to specify that changes are disallowed at the Subscriber if a client subscription is used. Because download-only articles cannot be updated at the Subscriber, tracking metadata is not sent to Subscribers. This can lead to reduced storage on the Subscribers and a performance benefit, especially if the network connection is slow. This option corresponds to a value of **Download-only to Subscriber, prohibit Subscriber changes** for the option **Synchronization direction** in the **Article Properties** dialog box. For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).  
  
 **Show only checked objects in the list**  
 Select this check box to show only those articles that are selected in the object pane.  
  
## See Also  
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)  
  
  
