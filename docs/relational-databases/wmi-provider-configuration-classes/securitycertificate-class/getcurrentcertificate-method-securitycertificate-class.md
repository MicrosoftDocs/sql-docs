---
title: "GetCurrentCertificate Method (SecurityCertificate)"
description: "GetCurrentCertificate Method (SecurityCertificate Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "GetCurrentCertificate method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "GetCurrentCertificate Method (SecurityCertificate Class)"
apitype: "MOFDef"
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
  
  
