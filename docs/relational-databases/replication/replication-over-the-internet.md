---
title: "Replication over the Internet"
description: "Replication over the Internet"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Web publishing [SQL Server replication], about Web publishing"
  - "Web publishing [SQL Server replication]"
  - "Internet [SQL Server replication]"
  - "Internet [SQL Server replication], publishing"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication over the Internet
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Replicating data over the Internet allows remote, disconnected users to access data when they need it using a connection to the Internet. Replicate data over the Internet using:  
  
-   A Virtual Private Network (VPN). For more information, see [Publish Data over the Internet Using VPN](../../relational-databases/replication/publish-data-over-the-internet-using-vpn.md).  
  
-   The Web synchronization option for merge replication. For more information, see [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md).  
  
 All types of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication can replicate data over a VPN, but you should consider Web synchronization if you are using merge replication.  
  
  
