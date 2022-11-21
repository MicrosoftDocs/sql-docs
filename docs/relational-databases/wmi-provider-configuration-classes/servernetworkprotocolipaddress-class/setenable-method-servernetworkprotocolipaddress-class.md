---
description: "SetEnable Method (ServerNetworkProtocolIPAddress Class)"
title: "SetEnable Method (ServerNetworkProtocolIPAddress)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "SetEnable Method (ServerNetworkProtocolIPAddress Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetEnable method"
ms.assetid: baa86deb-95dd-416f-b2c7-cec1dfb91ab4
author: markingmyname
ms.author: maghan
---
# SetEnable Method (ServerNetworkProtocolIPAddress Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Enables the IP address.  
  
## Syntax  
  
```  
  
object.SetEnable()  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocolIPAdress Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolipaddress-class/servernetworkprotocolipaddress-class.md) object that represents an IP address for the network protocol on the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
