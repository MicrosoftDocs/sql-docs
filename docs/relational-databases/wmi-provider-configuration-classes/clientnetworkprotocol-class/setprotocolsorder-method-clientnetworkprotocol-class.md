---
title: "SetProtocolsOrder Method (ClientNetworkProtocol Class) | Microsoft Docs"
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
  - "SetProtocolsOrder Method (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetProtocolsOrder method"
ms.assetid: b86d98b9-aae4-4e74-b4da-1ec984d5c8b4
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SetProtocolsOrder Method (ClientNetworkProtocol Class)
  Changes the order of the protocol list.  
  
## Syntax  
  
```  
  
object.SetProtocolsOrder(ProtocolOrderList)  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*ProtocolOrderList*|A string[] array that lists the client network protocols in the new order.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](http://technet.microsoft.com/library/ms181035.aspx)   
 [Configuring Client Network Protocols and Net-Libraries](http://technet.microsoft.com/library/ms181035.aspx)  
  
  