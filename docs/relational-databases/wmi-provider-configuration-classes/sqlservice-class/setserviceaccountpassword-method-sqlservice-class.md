---
description: "SetServiceAccountPassword Method (SqlService Class)"
title: "SetServiceAccountPassword Method (SqlService)"
ms.custom: seo-lt-2019
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "SetServiceAccountPassword Method (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetServiceAccountPassword method"
ms.assetid: e577a1ac-985c-4799-bb38-9393efc3def2
author: markingmyname
ms.author: maghan
---
# SetServiceAccountPassword Method (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Modifies the password for the account that the referenced service runs under.  
  
## Syntax  
  
```  
  
object.SetServiceAccountPassword(AccountOldPassword , ServiceStartPassword)  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
#### Parameters  
 *AccountOldPassword*  
 A string value that specifies the existing password for the account.  
  
 *ServiceStartPassword*  
 A string value that specifies the new password for the account.  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
