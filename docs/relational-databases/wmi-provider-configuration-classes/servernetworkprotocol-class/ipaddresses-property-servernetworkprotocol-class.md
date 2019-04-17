---
title: "IpAddresses Property (ServerNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "IpAddresses Property (ServerNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "IpAddresses property"
ms.assetid: e5d66f7e-9719-436e-b723-12d56f914a05
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# IpAddresses Property (ServerNetworkProtocol Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the IP addresses associated with the server network protocol.  
  
## Syntax  
  
```  
  
object.IpAddresses [= value]  
```  
  
## Parts  
 *object*  
 A **ServerNetworkProtocol** object that represents the network protocol used by the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 An array of [ServerNetworkProtocolIPAdress Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolipaddress-class/servernetworkprotocolipaddress-class.md) objects that represent the IP addresses supported by the server network protocol.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
