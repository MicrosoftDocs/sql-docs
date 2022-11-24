---
description: "SetEnable Method (ClientNetworkProtocol Class)"
title: "SetEnable Method (ClientNetworkProtocol)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "SetEnable Method (ClientNetworkProtocol Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetEnable method"
ms.assetid: a66c756a-1311-4f4a-8088-818f8ed90056
author: markingmyname
ms.author: maghan
---
# SetEnable Method (ClientNetworkProtocol Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Enables the client network protocol that is specified by the [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md).  
  
## Syntax  
  
```  
  
object.SetEnableMethod()  
```  
  
## Parts  
 *object*  
 A [ClientNetworkProtocol Class](../../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) object that represents the network protocol used by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client.  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Client Network Protocols and Net-Libraries](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
