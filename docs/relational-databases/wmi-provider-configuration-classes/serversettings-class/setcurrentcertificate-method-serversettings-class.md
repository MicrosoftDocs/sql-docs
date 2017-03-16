---
title: "SetCurrentCertificate Method (ServerSettings Class) | Microsoft Docs"
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
  - "SetCurrentCertificate Method (ServerSettings Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetCurrentCertificate method"
ms.assetid: f9c6e172-11be-42de-b19b-a5b3436e84da
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SetCurrentCertificate Method (ServerSettings Class)
  Sets the current security certificate.  
  
## Syntax  
  
```  
  
object.SetCurrentCertificate(SHA)  
```  
  
## Parts  
 *object*  
 A [ServerSettings Class](../../../relational-databases/wmi-provider-configuration-classes/serversettings-class/serversettings-class.md) object that represents the server setting on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*SHA*|A string value that specifies the current security certificate.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](http://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  