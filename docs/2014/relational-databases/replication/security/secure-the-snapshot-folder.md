---
title: "Secure the Snapshot Folder | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshots [SQL Server replication], security"
ms.assetid: 3cd877d1-ffb8-48fd-a72b-98eb948aad27
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Secure the Snapshot Folder
  The snapshot folder is a directory that stores snapshot files; it is recommended that you dedicate the directory for snapshot storage. Grant the Snapshot Agent write permission to the folder, and ensure that read permission is granted only to the Windows account that the Merge Agent or Distribution agent uses when accessing the folder. The Windows account associated with the agent must be a domain account to access a snapshot folder that is located on a remote computer.  
  
> [!NOTE]  
>  User Account Control (UAC)  helps administrators manage their elevated user rights (sometimes called *privileges*). When running on operating systems that have UAC enabled, administrators do not use their administrative rights. Instead, they perform most actions as standard (non-administrative) users, temporarily assuming their administrative rights only when it is required. UAC can prevent administrative access to the snapshot share. You must therefore explicitly grant snapshot share permissions to the Windows accounts that are used by the Snapshot Agent, Distribution Agent, and Merge Agent. You must do this even if the Windows accounts are members of the Administrators group.  
  
 When configuring a Distributor through the Configure Distribution Wizard or the New Publication Wizard, the snapshot folder defaults to a local path: X:\Program Files\Microsoft SQL Server\\*\<instance>*\MSSQL\ReplData. If you are using a remote Distributor or pull subscriptions, you must specify a UNC network share (such as \\\\<*computername>*\snapshot) rather than a local path.  
  
 When granting permissions to access the snapshot folder, you must grant them according to the way in which the folder is accessed. The following dialog box tabs are used in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows 2003:  
  
-   If you specify a local path, grant permissions through the **Security** tab of the **Properties** dialog box for the folder.  
  
-   If you specify a network share, grant permissions through the **Sharing** tab of the **Properties** dialog box for the folder.  
  
    > [!NOTE]  
    >  If the replication agent runs on the Distributor, use the **Security** tab of the **Properties** dialog box for the folder to grant permissions to the Windows account used to run the agent. Do this even when a network share is used. This applies to the Merge Agent and Distribution Agent for a push subscription and to the Snapshot Agent when the Publisher and Distributor are on the same computer.  
  
 For more information about setting permissions for local paths and network shares, see the Windows documentation.  
  
> [!NOTE]  
>  If a publication is dropped, replication attempts to remove the snapshot folder under the security context of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account. If this account does not have sufficient privileges, log in with an account that does have sufficient privileges and remove the folder manually. Removing a folder requires the **Modify** privilege if the folder is a local path or the **Full Control** privilege if the folder is a network path.  
  
## Delivering Snapshots Through FTP  
 It is recommended as a security best practice that snapshots be stored in a UNC share, but snapshots can be stored in an FTP share and then delivered to a Subscriber through FTP. When configuring the FTP server, ensure that the virtual directory exposes an underlying UNC share that permits write access by the Snapshot Agent for the publication.  
  
 To configure a Subscriber to retrieve the Snapshot via FTP, first set up an FTP server with an FTP login and password that allows Subscribers read (or "get") access to allow the snapshot files to be downloaded.  
  
 To deliver snapshots through FTP, see [Deliver a Snapshot Through FTP](../publish/deliver-a-snapshot-through-ftp.md).  
  
 For information about setting and changing the password for access to snapshots through FTP, see the section "FTP Snapshot Delivery" in the topic [Secure the Publisher](secure-the-publisher.md).  
  
## See Also  
 [Alternate Snapshot Folder Locations](../alternate-snapshot-folder-locations.md)   
 [Initialize a Subscription with a Snapshot](../initialize-a-subscription-with-a-snapshot.md)   
 [Replication Security Best Practices](replication-security-best-practices.md)   
 [SQL Server Replication Security](view-and-modify-replication-security-settings.md)   
 [Transfer Snapshots Through FTP](../transfer-snapshots-through-ftp.md)  
  
  
