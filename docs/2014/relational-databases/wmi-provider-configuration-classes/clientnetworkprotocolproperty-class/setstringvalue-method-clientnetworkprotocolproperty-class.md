---
title: "SetStringValue Method (ClientNetworkProtocolProperty Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "SetStringValue Method (ClientNetworkProtocolProperty Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetStringValue method"
ms.assetid: 88d67b22-0eea-48c9-ab73-e0b4907953df
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SetStringValue Method (ClientNetworkProtocolProperty Class)
  Sets the string value of the current property referenced by the [PropertyIdx Property (ClientNetworkProtocolProperty Class)](clientnetworkprotocolproperty-class.md) value.  
  
## Syntax  
  
```  
  
object  
.SetStringValue(  
StrValue  
)  
  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocolProperty Class](clientnetworkprotocolproperty-class.md) object that represents an attribute of the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*StrValue*|A string value that specifies the new value of the current property.|  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
  
