---
title: "Properties Property (ServerNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Properties Property (ServerNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "Properties property"
ms.assetid: 6c971bfc-c277-4c1e-a06e-146dcc34e759
caps.latest.revision: 29
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Properties Property (ServerNetworkProtocol Class)
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
  