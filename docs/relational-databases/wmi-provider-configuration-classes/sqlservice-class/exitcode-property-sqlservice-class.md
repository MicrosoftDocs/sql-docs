---
title: "ExitCode Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "ExitCode Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ExitCode property"
ms.assetid: e6b8bff2-946f-4abe-bd50-1f7bb11fdddf
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# ExitCode Property (SqlService Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets or sets the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Win32 error code that defines problems encountered when starting and stopping the service.  
  
## Syntax  
  
```  
  
object.ExitCode [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A **uint32** value that specifies the exit code.  
  
## Remarks  
 This property is set to ERROR_SERVICE_SPECIFIC_ERROR (1066) when the error is unique to the service represented by this class. The service sets this value to NO_ERROR when running, and again upon normal termination.  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
