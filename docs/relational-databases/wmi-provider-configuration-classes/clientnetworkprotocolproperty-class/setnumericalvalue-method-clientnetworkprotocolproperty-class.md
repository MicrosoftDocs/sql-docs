---
description: "SetNumericalValue Method (ClientNetworkProtocolProperty Class)"
title: "SetNumericalValue Method (ClientNetworkProtocolProperty)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "SetNumericalValue Method (ClientNetworkProtocolProperty"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetNumericalValue method"
ms.assetid: d4d6df52-9e68-4003-9e28-ece6716ba7f1
author: markingmyname
ms.author: maghan
---
# SetNumericalValue Method (ClientNetworkProtocolProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets the numeric value of the current property referenced by the [PropertyIdx Property (ClientNetworkProtocolProperty Class)](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/propertyidx-property-clientnetworkprotocolproperty-class.md) value.  
  
## Syntax  
  
```  
  
object.SetNumericalValue [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/clientnetworkprotocolproperty-class.md) object that represents an attribute of the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*value*|A **uint32** value that specifies the numeric value of the referenced property.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
  
