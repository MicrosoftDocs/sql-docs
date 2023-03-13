---
title: "SetDisable Method (ServerNetworkProtocol)"
description: "SetDisable Method (ServerNetworkProtocol Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetDisable method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetDisable Method (ServerNetworkProtocol Class)"
apitype: "MOFDef"
---
# SetDisable Method (ServerNetworkProtocol Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Disables the server network protocol.  
  
## Syntax  
  
```  
  
object.SetDisable()  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocol-class/servernetworkprotocol-class.md) object that represents the network protocol used by the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A uint32 value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
