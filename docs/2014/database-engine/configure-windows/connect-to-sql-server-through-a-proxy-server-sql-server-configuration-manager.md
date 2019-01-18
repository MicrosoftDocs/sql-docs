---
title: "Connect to SQL Server Through a Proxy Server (SQL Server Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/22/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "Remote WinSock"
  - "RWS"
  - "LATs"
  - "proxy servers [SQL Server]"
  - "connections [SQL Server], proxy server"
  - "Microsoft Proxy Server [SQL Server]"
  - "local address tables [SQL Server]"
ms.assetid: 39714de0-2a1f-4179-9091-5c3fa4612545
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Connect to SQL Server Through a Proxy Server (SQL Server Configuration Manager)
  This topic describes how to connect to SQL Server through a proxy server in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using SQL Server Configuration Manager. To listen remotely by way of Remote WinSock (RWS), define the local address table (LAT) for the proxy server so that the listening node address is outside the range of LAT entries.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To enable connections to SQL Server through Microsoft Proxy Server  
  
1.  Follow the steps in [Configure a Server to Listen on a Specific TCP Port &#40;SQL Server Configuration Manager&#41;](configure-a-server-to-listen-on-a-specific-tcp-port.md) to determine which TCP/IP ports are used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or to configure the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to use a desired port.  
  
2.  In your proxy server define the local address table (LAT) for the proxy server so that the listening node address is outside the range of LAT entries. For more information, see your proxy server documentation.  
  
  
