---
title: "Properties Property (ServerNetworkProtocol)"
description: "Properties Property (ServerNetworkProtocol Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "Properties property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "Properties Property (ServerNetworkProtocol Class)"
apitype: "MOFDef"
---
# Properties Property (ServerNetworkProtocol Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the properties associated with the server network protocol.  
  
## Syntax  
  
```  
  
object.Properties [= value]  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocol-class/servernetworkprotocol-class.md) object that represents the network protocol used by the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 An array of [ServerNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolproperty-class/servernetworkprotocolproperty-class.md) objects that represent the properties supported by the server network protocol.  
  
## Remarks  
  
