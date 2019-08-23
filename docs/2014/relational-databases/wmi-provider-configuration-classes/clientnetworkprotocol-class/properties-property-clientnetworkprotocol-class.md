---
title: "Properties Property (ClientNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "Properties Property (ClientNetworkProtocol Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Properties property"
ms.assetid: 7e0a4e38-4555-4750-8fd3-4425b29e6aa1
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Properties Property (ClientNetworkProtocol Class)
  Gets the properties associated with the current client network protocol specified by the [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx).  
  
## Syntax  
  
```  
  
object  
.Properties [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 An array of [ClientNetworkProtocolProperty Class](../clientnetworkprotocolproperty-class/clientnetworkprotocolproperty-class.md) objects that represent the properties supported by the current client network protocol that is referenced by the `OrderValue` property.  
  
## Remarks  
  
## See Also  
 [Configuring Client Network Protocols and Net-Libraries](https://technet.microsoft.com/library/ms181035.aspx)  
  
  
