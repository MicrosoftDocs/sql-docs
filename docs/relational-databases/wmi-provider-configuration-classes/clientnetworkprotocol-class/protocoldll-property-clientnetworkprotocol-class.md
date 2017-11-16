---
title: "ProtocolDLL Property (ClientNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "wmi"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ProtocolDLL Property (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ProtocolDLL property"
ms.assetid: fe8650d5-7b9d-46f8-bf74-baf1d9d2a06a
caps.latest.revision: 35
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
ms.workload: "Inactive"
---
# ProtocolDLL Property (ClientNetworkProtocol Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the name of the .dll file required by the network protocol specified by the [Configure Client Protocols](http://technet.microsoft.com/library/ms181035.aspx).  
  
## Syntax  
  
```  
  
object.ProtocolDLL [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A string value that specifies the protocol .dll file required by the client network protocol.  
  
## Remarks  
  
## See Also  
 [Configuring Client Network Protocols and Net-Libraries](http://technet.microsoft.com/library/ms181035.aspx)  
  
  
