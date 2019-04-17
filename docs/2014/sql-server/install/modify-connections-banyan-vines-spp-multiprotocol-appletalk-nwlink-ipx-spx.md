---
title: "Modify connections that use Banyan VINES Sequenced Packet Protocol (SPP), Multiprotocol, AppleTalk, or NWLink IPX SPX network protocols | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "network protocols [SQL Server], modifying connections"
  - "SPP [SQL Server]"
  - "NWLink IPX/SPX [SQL Server]"
  - "connections [SQL Server]"
  - "AppleTalk [SQL Server]"
  - "Sequenced Packet Protocol [SQL Server]"
  - "Multiprotocol Net-Library [SQL Server]"
  - "Banyan VINES Sequenced Package Protocol [SQL Server]"
  - "IPX/SPX [SQL Server]"
  - "protocols [SQL Server], modifying connections"
  - "RPC [SQL Server]"
ms.assetid: 5c5ae453-cc5b-4898-95c7-ad34157b1f60
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Modify connections that use Banyan VINES Sequenced Packet Protocol (SPP), Multiprotocol, AppleTalk, or NWLink IPX SPX network protocols
  The Upgrade Advisor has detected client server connectivity protocols that are not supported in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Client applications that use Banyan VINES Sequenced Packet Protocol (SPP), Multiprotocol (RPC), AppleTalk, or NWLink IPX/SPX network protocols must connect using a supported protocol.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] does not support the Banyan VINES Sequenced Packet Protocol (SPP), Multiprotocol, AppleTalk, or NWLink IPX/SPX network protocols. Clients previously connecting with these protocols must select a different protocol.  
  
## Corrective Action  
 Change the client applications to use a supported protocol when connecting to the server. If you have an alias setup that uses one of the unsupported protocols then the alias must be modified to use one of the supported protocols.  
  
 If your application connection string specifically uses or loads one of these protocols, by either specifying NETWORK=DBMSRPCN for RPC, NETWORK=DBMSADSN for Appletalk, or NETWORK=DBMSVINN for Banyan VINES property, or by using an explicit prefix such as "spx:*server\instance*" for SPX, "bv:*server*" for Banyan VINES, "adsp:*server*" for AppleTalk, or "rpc:*server*" for multiprotocol, then you must modify your application to use one of the supported protocols.  
  
 For more information, see "Choosing a Network Protocol" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
