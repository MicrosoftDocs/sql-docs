---
description: "Snapshot Folder"
title: "Snapshot Folder | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.replicationutilities.specifysnapshotfolder.f1"
ms.assetid: 3eb1b73f-ddb3-4d09-be6e-811c414698e9
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Snapshot Folder
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

The **Snapshot Folder** page appears in the Configure Distribution Wizard and in the New Publication Wizard. The location you specify for the snapshot folder will be used as the default for all Publishers enabled in this wizard (the default snapshot folder does not apply to Publishers that are later enabled using the **Distributor Properties** dialog box). You can override this default for any Publisher on the **Publishers** page of the Configure Distribution Wizard or the **Distributor Properties** dialog box.  
  
The snapshot folder is simply a directory that you have designated as a share; agents that read from and write to this folder must have sufficient permissions to access it. For more information about securing the folder appropriately, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md). Prior to implementing replication, test that the replication agents will be able to connect to the snapshot folder. Log on under the account that will be used by each agent and then attempt to access the snapshot folder.  

For Azure SQL Managed Instance, the snapshot folder must be an Azure file share. 
  
## Options  
 **Snapshot folder**  
 Enter the path for the folder where you want snapshot files stored.  
  
> [!NOTE]  
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you use a network share as a snapshot folder location. Local paths (those starting with a drive letter, such as C:\\) are not accessible to agents on other computers.  
  
## See Also  
 [Modify snapshot options](../../relational-databases/replication/snapshot-options.md)   
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)  
  
  
