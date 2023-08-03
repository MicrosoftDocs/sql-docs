---
title: "SubmitChanges Method (RDS)"
description: "SubmitChanges Method (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "SubmitChanges method [ADO]"
apitype: "COM"
---
# SubmitChanges Method (RDS)
Submits pending changes of the locally cached and updatable [Recordset](../ado-api/recordset-object-ado.md) to the data source specified in the [Connect](./connect-property-rds.md) property or the [URL](./url-property-rds.md) property.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
DataControl.SubmitChanges DataFactory.SubmitChanges Connection, Recordset  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](./datacontrol-object-rds.md) object.  
  
 *DataFactory*  
 An object variable that represents an [RDSServer.DataFactory](./datafactory-object-rdsserver.md) object.  
  
 *Connection*  
 A **String** value that represents the connection created with the **RDS.DataControl** object's [Connect](./connect-property-rds.md) property.  
  
 *Recordset*  
 An object variable that represents a **Recordset** object.  
  
## Remarks  
 The [Connect](./connect-property-rds.md), [Server](./server-property-rds.md), and [SQL](./sql-property.md) properties must be set before you can use the **SubmitChanges** method with the **RDS.DataControl** object.  
  
 If you call the [CancelUpdate](./cancelupdate-method-rds.md) method after you have called **SubmitChanges** for the same **Recordset** object, the **CancelUpdate** call fails because the changes have already been committed.  
  
 Only the changed records are sent for modification, and either all of the changes succeed or all the changes fail together.  
  
 You can use **SubmitChanges** only with the default **RDSServer.DataFactory** object. Custom business objects cannot use this method.  
  
 If the **URL** property has been set, **SubmitChanges** will submit changes to the location specified by the URL.  
  
## Applies To  

:::row:::
    :::column:::
        [DataControl Object (RDS)](./datacontrol-object-rds.md)  
    :::column-end:::
    :::column:::
        [DataFactory Object (RDSServer)](./datafactory-object-rdsserver.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [SubmitChanges Method Example (VBScript)](./submitchanges-method-example-vbscript.md)   
 [Address Book Command Buttons](../../guide/remote-data-service/address-book-command-buttons.md)   
 [CancelUpdate Method (RDS)](./cancelupdate-method-rds.md)   
 [Refresh Method (RDS)](./refresh-method-rds.md)