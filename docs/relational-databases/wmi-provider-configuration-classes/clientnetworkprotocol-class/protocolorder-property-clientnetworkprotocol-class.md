---
title: "ProtocolOrder Property (ClientNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "ProtocolOrder Property (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ProtocolOrder property"
ms.assetid: aa09b8ab-37db-4406-8973-acf503855fd2
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# ProtocolOrder Property (ClientNetworkProtocol Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the order number of the currently referenced client network protocol as specified by the [SetOrderValue Method (ClientNetworkProtocol Class)](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/setordervalue-method-clientnetworkprotocol-class.md) method.  
  
## Syntax  
  
```  
  
object.ProtocolOrder [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A **uint32** value that specifies the order number of the currently referenced client network protocol as set by the **OrderValue** method. If the client network protocol is disabled, this value will be zero.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx)   
 [Configuring Client Network Protocols and Net-Libraries](https://technet.microsoft.com/library/ms181035.aspx)  
  
  
