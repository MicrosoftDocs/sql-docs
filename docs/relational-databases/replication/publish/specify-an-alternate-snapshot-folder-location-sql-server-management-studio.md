---
title: "Specify an Alternate Snapshot Folder Location (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "snapshots [SQL Server replication], alternate folder locations"
  - "snapshot replication [SQL Server], alternate folder locations"
  - "alternate snapshot folders [SQL Server replication]"
ms.assetid: 9293f0eb-5531-47ec-b6e2-0392823ce5cc
caps.latest.revision: 42
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Specify an Alternate Snapshot Folder Location (SQL Server Management Studio)
  Specify an alternate snapshot location on the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
### To specify an alternate snapshot location  
  
1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:  
  
    1.  Select **Put files in the following folder**, and then click **Browse** to navigate to a directory, or enter the path to the directory in which the snapshot files should be stored.  
  
        > [!NOTE]  
        >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
    2.  Clear **Put files in the default folder** unless you require snapshot files to be written to both locations.  
  
     To compress the snapshot files, select **Compress snapshot files in this location**. Compression is typically used for low bandwidth connections and alternate snapshot locations on removable media, such as a CD-ROM.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
## See Also  
 [Alternate Snapshot Folder Locations](../../../relational-databases/replication/alternate-snapshot-folder-locations.md)   
 [Configure Snapshot Properties &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/publish/configure-snapshot-properties-replication-transact-sql-programming.md)   
 [Specify the Default Snapshot Location &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/specify-the-default-snapshot-location-sql-server-management-studio.md)   
 [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [Initialize a Subscription with a Snapshot](../../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)  
  
  