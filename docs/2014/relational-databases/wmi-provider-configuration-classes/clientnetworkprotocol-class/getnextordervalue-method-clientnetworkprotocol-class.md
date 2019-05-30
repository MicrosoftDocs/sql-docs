---
title: "GetNextOrderValue Method (ClientNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "GetNextOrderValue Method (ClientNetworkProtocol Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "GetNextOrderValue method"
ms.assetid: d741dc5c-c225-43d9-a730-7ad664ac525f
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# GetNextOrderValue Method (ClientNetworkProtocol Class)
  Selects the protocol that is in the next position in the list of protocols.  
  
## Syntax  
  
```  
  
object  
.GetNextOrderValue()  
  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx)   
 [Configuring Client Network Protocols and Net-Libraries](https://technet.microsoft.com/library/ms181035.aspx)  
  
  
