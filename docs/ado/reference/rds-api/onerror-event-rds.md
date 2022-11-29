---
title: "onError Event (RDS)"
description: "onError Event (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "onError event [ADO]"
apitype: "COM"
---
# onError Event (RDS)
The **onError** event is called whenever an error occurs during an operation.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
onError SCode, Description, Source, CancelDisplay  
```  
  
#### Parameters  
 *SCode*  
 An integer that indicates the status code of the error.  
  
 *Description*  
 A **String** that indicates a description of the error.  
  
 *Source*  
 A **String** that indicates the query or command that caused the error.  
  
 *CancelDisplay*  
 A **Boolean** value, which if set to **True**, that prevents the error from being displayed in a dialog box.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [ADO Events Model Example (VC++)](../ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../guide/data/ado-event-handler-summary.md)