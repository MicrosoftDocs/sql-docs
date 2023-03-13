---
title: "SetDisable Method (ServerNetworkProtocolIPAddress)"
description: "SetDisable Method (ServerNetworkProtocolIPAddress Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetDisable method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetDisable Method (ServerNetworkProtocolIPAddress Class)"
apitype: "MOFDef"
---
# SetDisable Method (ServerNetworkProtocolIPAddress Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Disables the IP address.  
  
## Syntax  
  
```  
  
object.SetDisable()  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocolIPAdress Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolipaddress-class/servernetworkprotocolipaddress-class.md) object that represents an IP address for the network protocol on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A uint32 value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
