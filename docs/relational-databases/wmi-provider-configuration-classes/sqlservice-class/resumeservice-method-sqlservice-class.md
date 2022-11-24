---
description: "ResumeService Method (SqlService Class)"
title: "ResumeService Method (SqlService)"
ms.custom: seo-lt-2019
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "ResumeService Method (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ResumeService method"
ms.assetid: 0b0a5f08-b95e-4626-bf81-309da7a0aacd
author: markingmyname
ms.author: maghan
---
# ResumeService Method (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Attempts to place the service in the resumed state.  
  
## Syntax  
  
```  
  
object.ResumeService()  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A uint32 value, which is 0 if the **ResumeService** request was accepted, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
