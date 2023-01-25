---
title: "Refresh Method (RDS)"
description: "Refresh Method (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Refresh"
  - "RDS.DataControl::Refresh"
  - "DataControl::Refresh"
helpviewer_keywords:
  - "Refresh method [RDS]"
apitype: "COM"
---
# Refresh Method (RDS)
Requeries the data source specified in the [Connect](./connect-property-rds.md) property and updates the query results.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
DataControl.Refresh  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](./datacontrol-object-rds.md) object.  
  
## Remarks  
 You must set the [Connect](./connect-property-rds.md), [Server](./server-property-rds.md), and [SQL](./sql-property.md) properties before you use the **Refresh** method. All data-bound controls on the form associated with an **RDS.DataControl** object will reflect the new set of records. Any pre-existing [Recordset](../ado-api/recordset-object-ado.md) object is released, and any unsaved changes are discarded. The **Refresh** method automatically makes the first record the current record.  
  
 It is a good idea to call the **Refresh** method periodically when you work with data. If you retrieve data, and then leave it on a client computer for a while, it is likely to become out of date. It is possible that any changes that you make will fail, because someone else might have changed the record and submitted changes before you.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [Refresh Method Example (VB)](../ado-api/refresh-method-example-vb.md)   
 [Refresh Method Example (VBScript)](./refresh-method-example-vbscript.md)   
 [Address Book Command Buttons](../../guide/remote-data-service/address-book-command-buttons.md)   
 [CancelUpdate Method (RDS)](./cancelupdate-method-rds.md)   
 [Refresh Method (ADO)](../ado-api/refresh-method-ado.md)   
 [SubmitChanges Method (RDS)](./submitchanges-method-rds.md)