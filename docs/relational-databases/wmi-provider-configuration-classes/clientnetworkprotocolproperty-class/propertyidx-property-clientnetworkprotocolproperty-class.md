---
title: "PropertyIdx Property (ClientNetworkProtocolProperty)"
description: "PropertyIdx Property (ClientNetworkProtocolProperty Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "PropertyIdx property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "PropertyIdx Property (ClientNetworkProtocolProperty Class)"
apitype: "MOFDef"
---
# PropertyIdx Property (ClientNetworkProtocolProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets or sets the index value of the property in the property array referenced by the [Properties Property (ClientNetworkProtocol Class)](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/properties-property-clientnetworkprotocol-class.md) of the [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object.  
  
## Syntax  
  
```  
  
object.PropertyIdx [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/clientnetworkprotocolproperty-class.md) object that represents an attribute of the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A **uint32** value that specifies the array index value of the current property.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
  
