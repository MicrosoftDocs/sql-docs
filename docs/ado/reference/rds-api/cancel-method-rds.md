---
description: "Cancel Method (RDS)"
title: "Cancel Method (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Cancel method [RDS]"
ms.assetid: 560b5b3d-fba9-4275-8920-9c3e186134f7
author: rothja
ms.author: jroth
---
# Cancel Method (RDS)
Cancels execution of a pending, asynchronous method call.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
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