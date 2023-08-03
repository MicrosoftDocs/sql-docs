---
title: "onReadyStateChange Event (RDS)"
description: "onReadyStateChange Event (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "onReadyStateChange event [ADO]"
apitype: "COM"
---
# onReadyStateChange Event (RDS)
The **onReadyStateChange** event is called whenever the value of the [ReadyState](./readystate-property-rds.md) property changes.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
onReadyStateChange  
```  
  
#### Parameters  
 None.  
  
## Remarks  
 The **ReadyState** property reflects the progress of an [RDS.DataControl](./datacontrol-object-rds.md) object as it asynchronously retrieves data into its [Recordset](../ado-api/recordset-object-ado.md) object. Use the **onReadyStateChange** event to monitor changes in the **ReadyState** property whenever they occur. This is more efficient than periodically checking the property's value.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [ADO Events Model Example (VC++)](../ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../guide/data/ado-event-handler-summary.md)