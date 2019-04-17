---
title: "Transfer Snapshots Through FTP | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshots [SQL Server replication], FTP snapshots"
  - "FTP snapshots [SQL Server replication]"
  - "snapshot replication [SQL Server], FTP"
ms.assetid: 55c30791-cd2a-420b-8ba7-5700e005cb45
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Transfer Snapshots Through FTP
  By default, snapshots are stored in folders defined as Universal Naming Convention (UNC) shares. Replication also allows you to specify a File Transfer Protocol (FTP) share instead of a UNC share. To use FTP, you must configure an FTP server and then configure a publication and one or more subscriptions to use FTP. For information about how to configure an FTP server, see the Internet Information Services (IIS) documentation. If you specify FTP information for a publication, subscriptions to that publication use FTP by default. FTP is only used with Web synchronization when the computer that is running IIS is separated from the Distributor by a firewall. In this case, FTP can be used to transfer the snapshot from the Distributor and the computer that is running IIS. (The snapshot is always transferred to the Subscriber by using HTTPS.)  
  
> [!IMPORTANT]  
>  We recommend that you use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication and a UNC share rather than an FTP share because FTP passwords must be stored, and the password is sent from the Subscriber or the computer that is running IIS when it uses Web synchronization to the FTP server in plain text. Additionally, because a single account controls access to the snapshot share, it is not possible to ensure that a Subscriber to a filtered merge publication only has access to the snapshot files from their data partition.  
  
 To deliver a snapshot through FTP, see [Deliver a Snapshot Through FTP](publish/deliver-a-snapshot-through-ftp.md).  
  
## See Also  
 [Web Synchronization for Merge Replication](web-synchronization-for-merge-replication.md)   
 [Initialize a Subscription with a Snapshot](initialize-a-subscription-with-a-snapshot.md)   
 [Snapshot Options](snapshot-options.md)  
  
  
