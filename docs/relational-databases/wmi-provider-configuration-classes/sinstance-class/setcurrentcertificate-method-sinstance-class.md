---
description: "SetCurrentCertificate Method (SInstance Class)"
title: "SetCurrentCertificate Method (SInstance)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "SetCurrentCertificate Method (SInstance Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetCurrentCertificate method"
ms.assetid: 7349fb87-b973-4160-a2be-cab73abf5b31
author: markingmyname
ms.author: maghan
---
# SetCurrentCertificate Method (SInstance Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets the current security certificate.  
  
## Syntax  
  
```  
  
object.SetCurrentCertificate(SHA)  
```  
  
## Parts  
 *object*  
 An [SInstance Class](../../../relational-databases/wmi-provider-configuration-classes/sinstance-class/sinstance-class.md) object that represents the server setting on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*SHA*|A string value that specifies the current security certificate.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
