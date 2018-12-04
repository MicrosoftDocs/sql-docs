---
title: "onReadyStateChange Event (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "onReadyStateChange event [ADO]"
ms.assetid: bf2ae3ac-bfe4-4709-b50a-ea7c282c3164
author: MightyPen
ms.author: genemi
manager: craigg
---
# onReadyStateChange Event (RDS)
The **onReadyStateChange** event is called whenever the value of the [ReadyState](../../../ado/reference/rds-api/readystate-property-rds.md) property changes.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
onReadyStateChange  
```  
  
#### Parameters  
 None.  
  
## Remarks  
 The **ReadyState** property reflects the progress of an [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object as it asynchronously retrieves data into its [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object. Use the **onReadyStateChange** event to monitor changes in the **ReadyState** property whenever they occur. This is more efficient than periodically checking the property's value.  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)


