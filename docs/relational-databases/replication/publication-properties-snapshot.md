---
title: "Publication Properties, Snapshot | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.newpubwizard.pubproperties.snapshotformat.f1"
ms.assetid: 8e9133b1-fc37-4a85-8a7c-d5eaf172fbef
caps.latest.revision: 24
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Publication Properties, Snapshot
  The **Snapshot** page of the **Publication Properties** dialog box allows you to set the snapshot format, snapshot folder location, and scripts to run before and after the application of the snapshot. The snapshot folder must be designated as a share and have sufficient permissions for the agents that read and write files to the folder. For more information about securing the folder appropriately, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
> [!NOTE]  
>  Changes require a new snapshot for the publication. For more information, see [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
## Options  
 **Snapshot format**  
 Select native mode or character mode for the snapshot format.  
  
-   Select **Native SQL Server - all Subscribers must be servers running SQL Server** if all Subscribers are instances of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] other than [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssEW](../../includes/ssew-md.md)]. The native snapshot format provides the best performance.  
  
-   Select **Character - required if a Publisher or Subscriber is not running SQL Server** if any Subscribers are running [!INCLUDE[ssEW](../../includes/ssew-md.md)] or are non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.  
  
 **Location of snapshot files**  
 Select the location to store snapshot files. They can be stored in the default location; they can also be stored in an alternate location instead of, or in addition to, the default location. Files stored in an alternate location can be compressed.  
  
-   Select **Put files in the default folder** to use the default snapshot folder for the Publisher. The snapshot folder location is read-only in this dialog box, because it can only be changed for the Publisher in the **Distributor Properties** dialog box. For more information, see [Specify the Default Snapshot Location &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/specify-the-default-snapshot-location-sql-server-management-studio.md).  
  
-   Select **Put files in the following folder** to specify an alternate location instead of, or in addition to, the default location. Enter a path in the text box or click **Browse** and navigate to a location. Select **Compress snapshot files in this folder** to compress the files in the alternate snapshot location. The alternate location can be on another server, on a network drive, or on removable media such as a CD-ROM or removable disk. For more information, see [Alternate Snapshot Folder Locations](../../relational-databases/replication/alternate-snapshot-folder-locations.md) and [Compressed Snapshots](../../relational-databases/replication/compressed-snapshots.md).  
  
 **Run additional scripts**  
 Specify scripts to be executed before and after the snapshot is applied at the Subscriber. Scripts cannot be specified if **Snapshot format** is **Character**.  
  
 Scripts are optional, but they provide a convenient way to execute commands and apply administrative changes at Subscribers. For more information about executing scripts, see [Execute Scripts Before and After the Snapshot Is Applied](../../relational-databases/replication/execute-scripts-before-and-after-the-snapshot-is-applied.md).  
  
-   Enter a path in the **Before applying the snapshot, execute this script** text box or click **Browse** to specify a location for the script.  
  
-   Enter a path in the **After applying the snapshot, execute this script** text box or click **Browse** to specify a location for the script.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)   
 [Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  