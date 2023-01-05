---
description: "Enabled Property (ServerNetworkProtocolIpAddress Class)"
title: "Enabled Property (ServerNetworkProtocolIpAddress)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "Enabled Property (ServerNetworkProtocolIpAddress Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "Enabled property"
ms.assetid: 870fd4d0-6c77-462a-b480-d42eb044b2e7
author: markingmyname
ms.author: maghan
---
# Enabled Property (ServerNetworkProtocolIpAddress Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the Boolean property that specifies whether an IP address is enabled.  
  
## Syntax  
  
```  
  
object.Enabled [= value]  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocolIPAdress Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolipaddress-class/servernetworkprotocolipaddress-class.md) object that represents an IP address for the network protocol on the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the IP address is enabled: **true** if the IP address is enabled, or **false** if the IP address is disabled.  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
