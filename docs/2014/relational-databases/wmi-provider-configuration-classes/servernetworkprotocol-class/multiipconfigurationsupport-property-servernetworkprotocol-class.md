---
title: "MultiIpConfigurationSupport Property (ServerNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "MultiIpConfigurationSupport Property (ServerNetworkProtocol Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MultiIpConfigurationSupport property"
ms.assetid: 442c6133-4038-42db-a67d-2631285ac76b
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# MultiIpConfigurationSupport Property (ServerNetworkProtocol Class)
  Gets the Boolean property that specifies whether multiple IP addresses are supported by a server network protocol.  
  
## Syntax  
  
```  
  
object  
.MultiIpConfigurationSupport [= value]  
```  
  
## Parts  
 *object*  
 A [ProtocolName Property (ServerNetworkProtocol Class)](servernetworkprotocol-class.md) object that represents the network protocol used by the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A Boolean value that specifies whether multiple IP addresses are supported by the server network protocol: `true` if multiple IP addresses are supported by the server network protocol, or `false` if multiple IP addresses are not supported by the server network protocol.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
