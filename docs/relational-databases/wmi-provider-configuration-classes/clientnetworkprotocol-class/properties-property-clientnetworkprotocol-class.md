---
title: "Properties Property (ClientNetworkProtocol)"
description: "Properties Property (ClientNetworkProtocol Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "Properties property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "Properties Property (ClientNetworkProtocol Class)"
apitype: "MOFDef"
---
# Properties Property (ClientNetworkProtocol Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the properties associated with the current client network protocol specified by the [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md).  
  
## Syntax  
  
```  
  
object.Properties [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 An array of [ClientNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/clientnetworkprotocolproperty-class.md) objects that represent the properties supported by the current client network protocol that is referenced by the **OrderValue** property.  
  
## Remarks  
  
## See Also  
 [Configuring Client Network Protocols and Net-Libraries](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
