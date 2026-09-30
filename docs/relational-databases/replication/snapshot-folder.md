---
title: Snapshot Folder
description: Snapshot Folder settings let you set the default share where SQL Server replication stores snapshot files. Learn how to specify and secure the folder.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.replicationutilities.specifysnapshotfolder.f1"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Snapshot Folder
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

The **Snapshot Folder** page appears in the Configure Distribution Wizard and in the New Publication Wizard. The location you specify for the snapshot folder serves as the default for all Publishers you enable in this wizard. The default snapshot folder doesn't apply to Publishers that you later enable by using the **Distributor Properties** dialog box. You can override this default for any Publisher on the **Publishers** page of the Configure Distribution Wizard or in the **Distributor Properties** dialog box.  
  
The snapshot folder is a directory that you designate as a share. Agents that read from and write to this folder must have sufficient permissions to access it. For more information about securing the folder appropriately, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md). Before you implement replication, test that the replication agents can connect to the snapshot folder. Sign in by using the account that each agent uses, and then attempt to access the snapshot folder.  

For Azure SQL Managed Instance, the snapshot folder must be an Azure Files share. 
  
## Options  
 **Snapshot folder**  
 Enter the path for the folder where you want to store snapshot files.  
  
> [!NOTE]  
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you use a network share as a snapshot folder location. Local paths (those starting with a drive letter, such as C:\\) aren't accessible to agents on other computers.  
  
## Related content

- [Modify Snapshot Initialization Options for SQL Replication](snapshot-options.md)
- [Configure Distribution](configure-distribution.md)
- [Configure Publishing and Distribution](configure-publishing-and-distribution.md)
- [View and Modify Distributor and Publisher Properties](view-and-modify-distributor-and-publisher-properties.md)
- [Initialize a Subscription with a Snapshot for a New Publication](initialize-a-subscription-with-a-snapshot.md)
