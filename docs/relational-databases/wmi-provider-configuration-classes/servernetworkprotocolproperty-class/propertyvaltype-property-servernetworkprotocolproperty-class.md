---
description: "PropertyValType Property (ServerNetworkProtocolProperty Class)"
title: "PropertyValType Property (ServerNetworkProtocolProperty)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "PropertyValType Property (ServerNetworkProtocolProperty"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "PropertyValType property"
ms.assetid: fbd42e8e-0642-4a19-b3c8-6ce88345145f
author: markingmyname
ms.author: maghan
---
# PropertyValType Property (ServerNetworkProtocolProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the data type of the value stored in the referenced property.  
  
## Syntax  
  
```  
  
object.PropertyValType [= value]  
```  
  
## Parts  
 *object*  
 A [ServerNetworkProtocolProperty Class](../../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolproperty-class/servernetworkprotocolproperty-class.md) object that represents an attribute of the network protocol on the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A **uint32** value that specifies the data type of the property value. It returns 0 for a string value and 1 for a numerical type.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
