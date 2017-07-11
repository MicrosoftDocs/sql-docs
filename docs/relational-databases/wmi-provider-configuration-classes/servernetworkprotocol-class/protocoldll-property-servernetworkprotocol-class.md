---
title: "ProtocolDLL Property (ServerNetworkProtocol Class) | Microsoft Docs"
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
  - "ProtocolDLL Property (ServerNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ProtocolDLL property"
ms.assetid: ac386558-392e-46f3-97f8-382f267b7fca
caps.latest.revision: 31
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# ProtocolDLL Property (ServerNetworkProtocol Class)
  Gets the name of the .dll file that is required by a server network protocol.  
  
## Syntax  
  
```  
  
object.ProtocolDLL [= value]  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocol-class/servernetworkprotocol-class.md) object that represents the network protocol used by the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A string value that specifies the protocol .dll file that is required by the server network protocol.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](http://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  