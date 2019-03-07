---
title: "NumberOfFlags Property (ClientNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "NumberOfFlags Property (ClientNetworkProtocol Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "NumberOfFlags property"
ms.assetid: 7a656644-2154-419f-9787-99877f597770
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# NumberOfFlags Property (ClientNetworkProtocol Class)
  Gets the number of flag options required by the client network protocol specified by the [SetOrderValue Method (ClientNetworkProtocol Class)](clientnetworkprotocol-class.md).  
  
## Syntax  
  
```  
  
object  
.NumberofFlags [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A `Uint32` value that specifies the number of flag options required by the client network protocol referenced by the `OrderValue` property.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx)  
  
  
