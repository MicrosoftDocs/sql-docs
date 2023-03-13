---
title: "SupportAlias Property (ClientNetworkProtocol)"
description: "SupportAlias Property (ClientNetworkProtocol Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SupportAlias property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SupportAlias Property (ClientNetworkProtocol Class)"
apitype: "MOFDef"
---
# SupportAlias Property (ClientNetworkProtocol Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the Boolean property that specifies whether the current network protocol specified by the [SetOrderValue Method (ClientNetworkProtocol Class)](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/setordervalue-method-clientnetworkprotocol-class.md) support aliases.  
  
## Syntax  
  
```  
  
object.SupportAlias [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the client network protocol supports aliases.  
  
 If True, the client network protocol supports aliases.  
  
 If False, the client network protocol does not support aliases.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
