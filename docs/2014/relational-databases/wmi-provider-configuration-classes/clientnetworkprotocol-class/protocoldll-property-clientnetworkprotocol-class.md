---
title: "ProtocolDLL Property (ClientNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "ProtocolDLL Property (ClientNetworkProtocol Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ProtocolDLL property"
ms.assetid: fe8650d5-7b9d-46f8-bf74-baf1d9d2a06a
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ProtocolDLL Property (ClientNetworkProtocol Class)
  Gets the name of the .dll file required by the network protocol specified by the [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx).  
  
## Syntax  
  
```  
  
object  
.ProtocolDLL [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A string value that specifies the protocol .dll file required by the client network protocol.  
  
## Remarks  
  
## See Also  
 [Configuring Client Network Protocols and Net-Libraries](https://technet.microsoft.com/library/ms181035.aspx)  
  
  
