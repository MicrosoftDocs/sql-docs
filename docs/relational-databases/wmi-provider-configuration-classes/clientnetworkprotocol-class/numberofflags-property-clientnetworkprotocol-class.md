---
title: "NumberOfFlags Property (ClientNetworkProtocol)"
description: "NumberOfFlags Property (ClientNetworkProtocol Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "NumberOfFlags property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "NumberOfFlags Property (ClientNetworkProtocol Class)"
apitype: "MOFDef"
---
# NumberOfFlags Property (ClientNetworkProtocol Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the number of flag options required by the client network protocol specified by the [SetOrderValue Method (ClientNetworkProtocol Class)](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/setordervalue-method-clientnetworkprotocol-class.md).  
  
## Syntax  
  
```  
  
object.NumberofFlags [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A **Uint32** value that specifies the number of flag options required by the client network protocol referenced by the **OrderValue** property.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
