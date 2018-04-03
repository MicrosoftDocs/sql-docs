---
title: "PropertyIdx Property (ClientNetworkProtocolProperty Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "PropertyIdx Property (ClientNetworkProtocolProperty Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "PropertyIdx property"
ms.assetid: d7845962-ac68-4435-9c59-70ec450fec88
caps.latest.revision: 29
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# PropertyIdx Property (ClientNetworkProtocolProperty Class)
  Gets or sets the index value of the property in the property array referenced by the [Properties Property (ClientNetworkProtocol Class)](../../../2014/database-engine/dev-guide/properties-property-clientnetworkprotocol-class.md) of the [ClientNetworkProtocol Class](../../../2014/database-engine/dev-guide/clientnetworkprotocol-class.md) object.  
  
## Syntax  
  
```  
  
object  
.PropertyIdx [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocolProperty Class](../../../2014/database-engine/dev-guide/clientnetworkprotocolproperty-class.md) object that represents an attribute of the network protocol used by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A `uint32` value that specifies the array index value of the current property.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../2014/database-engine/configure-client-protocols.md)  
  
  