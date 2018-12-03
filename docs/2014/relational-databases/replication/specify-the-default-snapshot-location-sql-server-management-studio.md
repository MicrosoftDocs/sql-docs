---
title: "Specify the Default Snapshot Location (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshots [SQL Server replication], default locations"
  - "default snapshot locations"
ms.assetid: 27c5d9ad-a915-4c59-a8b7-82e3af61ac4d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Specify the Default Snapshot Location (SQL Server Management Studio)
  Specify the default snapshot location on the **Snapshot Folder** page of the Configure Distribution Wizard. For more information about using this wizard, see [Configure Publishing and Distribution](configure-publishing-and-distribution.md). If you create a publication on a server that is not configured as a Distributor, specify a default snapshot location on the **Snapshot Folder** page of the New Publication Wizard. For more information about using this wizard, see [Create a Publication](publish/create-a-publication.md).  
  
 Modify the default snapshot location on the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box. For more information, see [View and Modify Distributor and Publisher Properties](view-and-modify-distributor-and-publisher-properties.md). Set the snapshot folder for each publication in the **Publication Properties - \<Publication>** dialog box. For more information, see [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md).  
  
### To modify the default snapshot location  
  
1.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**...**) for the Publisher for which you want to change the default snapshot location.  
  
2.  In the **Publisher Properties - \<Publisher>** dialog box, enter a value for the **Default Snapshot Folder** property.  
  
    > [!NOTE]  
    >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md).  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Alternate Snapshot Folder Locations](alternate-snapshot-folder-locations.md)   
 [Initialize a Subscription with a Snapshot](initialize-a-subscription-with-a-snapshot.md)  
  
  
