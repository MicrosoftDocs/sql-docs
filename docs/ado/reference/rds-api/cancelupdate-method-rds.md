---
title: "CancelUpdate Method (RDS)"
description: "CancelUpdate Method (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "CancelUpdate method [RDS]"
apitype: "COM"
---
# CancelUpdate Method (RDS)
Cancels any changes made to the current or new row of a [Recordset](../ado-api/recordset-object-ado.md) object.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
DataControl.CancelUpdate  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](./datacontrol-object-rds.md) object.  
  
## Remarks  
 The Cursor Service for OLE DB keeps both a copy of the original values and a cache of changes. When you call **CancelUpdate**, the cache of changes is reset to empty, and any bound controls are refreshed with the original data.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [CancelUpdate Method Example (VBScript)](./cancelupdate-method-example-vbscript.md)   
 [Address Book Command Buttons](../../guide/remote-data-service/address-book-command-buttons.md)   
 [Cancel Method (ADO)](../ado-api/cancel-method-ado.md)   
 [Cancel Method (RDS)](./cancel-method-rds.md)   
 [CancelBatch Method (ADO)](../ado-api/cancelbatch-method-ado.md)   
 [CancelUpdate Method (ADO)](../ado-api/cancelupdate-method-ado.md)   
 [Refresh Method (RDS)](./refresh-method-rds.md)   
 [SubmitChanges Method (RDS)](./submitchanges-method-rds.md)