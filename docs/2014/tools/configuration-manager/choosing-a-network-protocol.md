---
title: "Choosing a Network Protocol | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "configmgr-client"
ms.topic: conceptual
helpviewer_keywords: 
  - "shared memory [SQL Server]"
  - "Named Pipes [SQL Server]"
  - "TCP [SQL Server]"
  - "network protocols [SQL Server], choosing"
  - "protocols [SQL Server], choosing"
  - "NWLink IPX/SPX [SQL Server]"
  - "client configuration [SQL Server], protocols"
  - "VIA"
  - "Multiprotocol Net-Library [SQL Server]"
  - "IPX/SPX [SQL Server]"
  - "Banyan VINES"
  - "protocols [SQL Server], client configuration"
ms.assetid: 6565fb7d-b076-4447-be90-e10d0dec359a
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Choosing a Network Protocol
  To connect to [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] you must have a network protocol enabled. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can service requests on several protocols at the same time. Clients connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with a single protocol. If the client program does not know which protocol [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is listening on, configure the client to sequentially try multiple protocols. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to enable, disable, and configure network protocols.  
  
## Shared Memory  
 Shared memory is the simplest protocol to use and has no configurable settings. Because clients using the shared memory protocol can only connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance running on the same computer, it is not useful for most database activity. Use the shared memory protocol for troubleshooting when you suspect the other protocols are configured incorrectly.  
  
> [!NOTE]  
>  Clients that use MDAC 2.8 or earlier cannot use shared memory protocol. If these clients try this, they are automatically switched to the named pipes protocol.  
  
## TCP/IP  
 TCP/IP is a common protocol widely used over the Internet. It communicates across interconnected networks of computers that have diverse hardware architectures and various operating systems. TCP/IP includes standards for routing network traffic and offers advanced security features. It is the most popular protocol that is used in business today. Configuring your computer to use TCP/IP can be complex, but most networked computers are already correctly configured. To configure the TCP/IP settings that are not exposed in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows documentation.  
  
## Named Pipes  
 Named Pipes is a protocol developed for local area networks. A part of memory is used by one process to pass information to another process, so that the output of one is the input of the other. The second process can be local (on the same computer as the first) or remote (on a networked computer).  
  
## Named Pipes vs. TCP/IP Sockets  
 In a fast local area network (LAN) environment, Transmission Control Protocol/Internet Protocol (TCP/IP) Sockets and Named Pipes clients are comparable with regard to performance. However, the performance difference between the TCP/IP Sockets and Named Pipes clients becomes apparent with slower networks, such as across wide area networks (WANs) or dial-up networks. This is because of the different ways the interprocess communication (IPC) mechanisms communicate between peers.  
  
 For named pipes, network communications are typically more interactive. A peer does not send data until another peer asks for it using a read command. A network read typically involves a series of peek named pipes messages before it starts to read the data. These can be very costly in a slow network and cause excessive network traffic, which in turn affects other network clients.  
  
 It is also important to clarify if you are talking about local pipes or network pipes. If the server application is running locally on the computer that is running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the local Named Pipes protocol is an option. Local named pipes runs in kernel mode and is very fast.  
  
 For TCP/IP Sockets, data transmissions are more streamlined and have less overhead. Data transmissions can also take advantage of TCP/IP Sockets performance enhancement mechanisms such as windowing, delayed acknowledgements, and so on. This can be very helpful in a slow network. Depending on the type of applications, such performance differences can be significant.  
  
 TCP/IP Sockets also support a backlog queue. This can provide a limited smoothing effect compared to named pipes that could lead to pipe-busy errors when you are trying to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Generally, TCP/IP is preferred in a slow LAN, WAN, or dial-up network, whereas named pipes can be a better choice when network speed is not the issue, as it offers more functionality, ease of use, and configuration options.  
  
## Enabling the Protocol  
 The protocol must be enabled on both the client and server to work. The server can listen for requests on all enabled protocols at the same time. Client computers can pick one, or try the protocols in the order listed in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
 For a short tutorial about how to configure protocols and connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)], see [Tutorial: Getting Started with the Database Engine](../../relational-databases/tutorial-getting-started-with-the-database-engine.md).  
  
  
