---
title: "Connect to SQL Server with Proxy Server-SQL Server Config Manager | Microsoft Docs"
ms.custom: ""
ms.date: "12/15/2016"
ms.prod: sql
ms.prod_service: high-availability
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
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic describes how to connect to SQL Server through a proxy server in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using SQL Server Configuration Manager. To listen remotely by way of Remote WinSock (RWS), define the local address table (LAT) for the proxy server so that the listening node address is outside the range of LAT entries.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To enable connections to SQL Server through Microsoft Proxy Server  
  
1.  Follow the steps in [Configure a Server to Listen on a Specific TCP Port &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md) to determine which TCP/IP ports are used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or to configure the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to use a desired port.  
  
2.  In your proxy server define the local address table (LAT) for the proxy server so that the listening node address is outside the range of LAT entries. For more information, see your proxy server documentation.  
  
> [!NOTE]
>  This topic applies to on-premises [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. For connection issues related to [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], see [Troubleshoot connection issues to Azure SQL Database](https://docs.microsoft.com/azure/sql-database/sql-database-troubleshoot-common-connection-issues).  


