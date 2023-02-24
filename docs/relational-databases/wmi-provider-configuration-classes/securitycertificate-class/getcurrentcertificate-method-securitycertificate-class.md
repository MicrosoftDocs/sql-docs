---
description: "GetCurrentCertificate Method (SecurityCertificate Class)"
title: "GetCurrentCertificate Method (SecurityCertificate)"
ms.custom: seo-lt-2019
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "GetCurrentCertificate Method (SecurityCertificate Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "GetCurrentCertificate method"
ms.assetid: 987a2671-1801-45c4-93e6-29f883c58720
author: markingmyname
ms.author: maghan
---
# GetCurrentCertificate Method (SecurityCertificate Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the current security certificate.  
  
## Syntax  
  
```  
  
object.GetCurrentCertificate(SHA , SQLInstance)  
```  
  
## Parts  
 *object*  
 A [SecurityCertificate Class](../../../relational-databases/wmi-provider-configuration-classes/securitycertificate-class/securitycertificate-class.md) object that represents a security certificate.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*SHA*|A string value (output parameter) that specifies the current security certificate SHA thumbprint after the method completes.|  
|*SQLInstance*|A string value that specifies the instance for which the certificate is required.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
