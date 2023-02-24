---
description: "PropertyValType Property (ClientNetworkProtocolProperty Class)"
title: "PropertyValType Property (ClientNetworkProtocolProperty)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "PropertyValType Property (ClientNetworkProtocolProperty"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "PropertyValType property"
ms.assetid: 624b9bdd-ed93-4140-bd4e-00d714a2558c
author: markingmyname
ms.author: maghan
---
# PropertyValType Property (ClientNetworkProtocolProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the data type of the value stored in the property referenced by the [PropertyIdx Property (ClientNetworkProtocolProperty Class)](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/propertyidx-property-clientnetworkprotocolproperty-class.md) value.  
  
## Syntax  
  
```  
  
object.PropertyValType [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/clientnetworkprotocolproperty-class.md) object that represents an attribute of the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A **uint32** value that specifies the data type of the property value.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
  
