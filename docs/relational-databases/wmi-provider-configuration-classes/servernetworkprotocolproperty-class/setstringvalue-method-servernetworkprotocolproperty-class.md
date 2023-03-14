---
title: "SetStringValue Method (ServerNetworkProtocolProperty)"
description: "SetStringValue Method (ServerNetworkProtocolProperty Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetStringValue method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetStringValue Method (ServerNetworkProtocolProperty Class)"
apitype: "MOFDef"
---
# SetStringValue Method (ServerNetworkProtocolProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets the string value of the referenced property.  
  
## Syntax  
  
```  
  
object.SetStringValue(StrValue)  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolproperty-class/servernetworkprotocolproperty-class.md) object that represents an attribute of the network protocol on the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*StrValue*|A string value that specifies the new value of the current property.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
