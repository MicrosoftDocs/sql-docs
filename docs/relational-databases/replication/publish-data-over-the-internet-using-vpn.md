---
title: "Publish Data over the Internet Using VPN | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "VPNs [SQL Server replication]"
  - "Web publishing [SQL Server replication], VPNs"
  - "Internet [SQL Server replication], VPNs"
ms.assetid: 9ffb6546-9973-4574-aaa0-8fe0017e3601
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Publish Data over the Internet Using VPN
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Virtual Private Networking (VPN) technology allows users working at home, branch offices, remote clients, and other companies to connect to a corporate network over the Internet, while maintaining secure communications. Users can use Windows Authentication as though they were on a Local Area Network (LAN). All types of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication can replicate data over a VPN, but consider using Web synchronization if you are using merge replication, because Web synchronization eliminates the need for a VPN. For more information, see [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md).  
  
 A VPN includes client software so that computers connect over the Internet (or in special cases, even an intranet) to software in a dedicated computer or a server. Optionally, encryption at both ends, as well as user authentication methods, are used. The VPN connection over the Internet logically operates as a Wide Area Network (WAN) link between the sites.  
  
 A VPN connects the components of a network using another network. To connect, the user tunnels through the Internet or another public network using a protocol such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] Point-to-Point Tunneling Protocol (PPTP) or Layer Two Tunneling Protocol (L2TP). This process provides the same security and features previously available only in a private network. PPTP is available with the Microsoft Windows NT version 4.0 and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows 2000 (and later) operating systems; L2TP is available with Windows 2000 and later.  
  
 For the user, the intermediate routing infrastructure of the Internet is not visible and it appears as though the data is being sent over a dedicated private link. As far as users are concerned, the VPN is a point-to-point connection between the user computer and a corporate server.  
  
 After you have your remote client configured to connect using a VPN, and the client has Internet access and is logged in to the corporate LAN, you can configure replication as though the remote client is connected directly on the LAN. For security reasons, it is possible to have different network resources available to users connected over VPN and to those connected directly on the LAN.  
  
 For more information about setting up a VPN, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows documentation.  
  
## See Also  
 [Replication over the Internet](../../relational-databases/replication/replication-over-the-internet.md)  
  
  
