---
description: "NumberOfFlags Property (ClientNetworkProtocol Class)"
title: "NumberOfFlags Property (ClientNetworkProtocol)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "NumberOfFlags Property (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "NumberOfFlags property"
ms.assetid: 7a656644-2154-419f-9787-99877f597770
author: markingmyname
ms.author: maghan
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
  
