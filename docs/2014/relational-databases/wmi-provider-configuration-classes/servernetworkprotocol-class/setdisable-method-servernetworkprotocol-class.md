---
title: "SetDisable Method (ServerNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "SetDisable Method (ServerNetworkProtocol Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetDisable method"
ms.assetid: 0ebbe0c5-07ad-4a76-a918-e379930adf71
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SetDisable Method (ServerNetworkProtocol Class)
  Disables the server network protocol.  
  
## Syntax  
  
```  
  
object  
.SetDisable()  
  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocol Class]servernetworkprotocol-class.md) object that represents the network protocol used by the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A uint32 value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
