---
title: "Connecting Using IPv6 | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "Internet Protocol"
  - "IPv4"
  - "IPv6"
ms.assetid: 2669098c-f5f1-43da-aec6-e91003ac89f6
author: "stevestein"
ms.author: "sstein"
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Connecting Using IPv6
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client fully support both Internet Protocol version 4 (IPv4) and Internet Protocol version 6 (IPv6). When Windows is configured with IPv6 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], components automatically recognize the existence of IPv6. No special [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configuration is necessary.  
  
 Support includes but is not limited to the following:  
  
-   The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and the other server components can listen on both IPv4 and IPv6 addresses at the same time. When both IPv4 and IPv6 are present, you can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to configure the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to listen only on IPv4 addresses or only on IPv6 addresses.  
  
-   When the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service running on a machine that supports both IPv4 and IPv6 is queried on an IPv4 address, it responds with an IPv4 address and the first IPv4 TCP port in its list. When queried on an IPv6 address, it responds with an IPv6 address and the first IPv6 TCP port in its list. To avoid inconsistency, we recommend that the IPv4 and IPv6 listeners be configured to listen to the same port.  
  
-   Tools such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager accept both IPv4 and IPv6 formats for IP addresses. In most cases, the connection string does not need to be modified if the \<*computer_name*>\\<*instance_name*> is specified using server hostname or fully qualified domain name (FQDN). If the server computer has both IPv4 and IPv6, its hostname or FQDN will be resolved into multiple IP addresses, including at least one IPv4 address and multiple IPv6 addresses. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client attempts to establish connections using these IP addresses in the order received from TCP/IP and uses the first connection that succeeds. Because the order cannot be predicted by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, this should be regarded as random order. IPv4 addresses are attempted first if both IPv4 and IPv6 addresses are present. This logic is transparent to the users of ODBC, OLE DB, or ADO.NET.  
  
    > [!NOTE]  
    >  If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is not listening on IPv4, the attempted IPv4 connection must wait for the time-out period before the IPv6 address is attempted. To avoid this, connect directly to the IPv6 IP address or configure an alias on the client with the IPv6 address.  
  
## See Also  
 [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)  
  
  
