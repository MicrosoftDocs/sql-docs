---
description: "ProtocolName Property (ClientNetworkProtocol Class)"
title: "ProtocolName Property (ClientNetworkProtocol)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "ProtocolName Property (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ProtocolName property"
ms.assetid: f8527121-fbcd-4d30-9b4a-1461149cb5a8
author: markingmyname
ms.author: maghan
---
# ProtocolName Property (ClientNetworkProtocol Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the name of the current network protocol specified by the [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md).  
  
## Syntax  
  
```  
  
object.ProtocolName [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A string value that specifies the name of the current client network protocol referenced by the [SetOrderValue Method (ClientNetworkProtocol Class)](./setordervalue-method-clientnetworkprotocol-class.md).  
  
## Remarks  
  
## See Also  
 [Configuring Client Network Protocols and Net-Libraries](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
