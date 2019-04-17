---
title: "NumberOfFlags Property (ClientNetworkProtocol Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "NumberOfFlags Property (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "NumberOfFlags property"
ms.assetid: 7a656644-2154-419f-9787-99877f597770
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# NumberOfFlags Property (ClientNetworkProtocol Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the number of flag options required by the client network protocol specified by the [SetOrderValue Method (ClientNetworkProtocol Class)](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/setordervalue-method-clientnetworkprotocol-class.md).  
  
## Syntax  
  
```  
  
object.NumberofFlags [= value]  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A **Uint32** value that specifies the number of flag options required by the client network protocol referenced by the **OrderValue** property.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx)  
  
  
