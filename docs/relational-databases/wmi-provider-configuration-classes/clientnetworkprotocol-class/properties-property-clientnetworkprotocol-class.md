---
title: "Properties Property (ClientNetworkProtocol Class) | Microsoft Docs"
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
  - "Properties Property (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "Properties property"
ms.assetid: 7e0a4e38-4555-4750-8fd3-4425b29e6aa1
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Properties Property (ClientNetworkProtocol Class)
  Gets the properties associated with the current client network protocol specified by the [Configure Client Protocols](http://technet.microsoft.com/library/ms181035.aspx).  
  
## Syntax  
  
```  
  
object.Properties [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 An array of [ClientNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/clientnetworkprotocolproperty-class.md) objects that represent the properties supported by the current client network protocol that is referenced by the **OrderValue** property.  
  
## Remarks  
  
## See Also  
 [Configuring Client Network Protocols and Net-Libraries](http://technet.microsoft.com/library/ms181035.aspx)  
  
  