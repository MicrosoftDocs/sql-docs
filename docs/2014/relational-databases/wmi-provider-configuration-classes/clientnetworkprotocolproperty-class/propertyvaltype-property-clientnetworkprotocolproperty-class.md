---
title: "PropertyValType Property (ClientNetworkProtocolProperty Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "PropertyValType Property (ClientNetworkProtocolProperty Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "PropertyValType property"
ms.assetid: 624b9bdd-ed93-4140-bd4e-00d714a2558c
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# PropertyValType Property (ClientNetworkProtocolProperty Class)
  Gets the data type of the value stored in the property referenced by the [PropertyIdx Property (ClientNetworkProtocolProperty Class)](clientnetworkprotocolproperty-class.md) value.  
  
## Syntax  
  
```  
  
object  
.PropertyValType [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocolProperty Class](clientnetworkprotocolproperty-class.md) object that represents an attribute of the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A `uint32` value that specifies the data type of the property value.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
  
