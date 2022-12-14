---
title: "Cancel Method (RDS)"
description: "Cancel Method (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Cancel method [RDS]"
apitype: "COM"
---
# Cancel Method (RDS)
Cancels execution of a pending, asynchronous method call.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
RDS.DataControl.Cancel  
```  
  
## Remarks  
 When you call **Cancel**, [ReadyState](./readystate-property-rds.md) is automatically set to **adcReadyStateLoaded**, and the [Recordset](../ado-api/recordset-object-ado.md) will be empty.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [Cancel Method Example (VBScript)](./cancel-method-example-vbscript.md)   
 [Cancel Method (ADO)](../ado-api/cancel-method-ado.md)   
 [CancelBatch Method (ADO)](../ado-api/cancelbatch-method-ado.md)   
 [CancelUpdate Method (ADO)](../ado-api/cancelupdate-method-ado.md)   
 [CancelUpdate Method (RDS)](./cancelupdate-method-rds.md)   
 [ExecuteOptions Property (RDS)](./executeoptions-property-rds.md)