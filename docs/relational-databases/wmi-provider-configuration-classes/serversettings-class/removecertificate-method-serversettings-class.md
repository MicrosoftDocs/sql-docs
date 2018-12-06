---
title: "RemoveCertificate Method (ServerSettings Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "RemoveCertificate Method (ServerSettings Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "RemoveCertificate method"
ms.assetid: 9ffdbc39-93f5-48fb-859a-86a3ad545827
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# RemoveCertificate Method (ServerSettings Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Removes the current security certificate from the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
object.RemoveCertificate()  
```  
  
## Parts  
 *object*  
 A [ServerSettings Class](../../../relational-databases/wmi-provider-configuration-classes/serversettings-class/serversettings-class.md) object that represents the server settings on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 A u**int32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
