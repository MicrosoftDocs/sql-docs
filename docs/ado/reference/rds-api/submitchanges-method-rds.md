---
title: "SubmitChanges Method (RDS) | Microsoft Docs"
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "SubmitChanges method [ADO]"
ms.assetid: 250062a4-13c4-4bed-807d-8b9ad81536d4
author: MightyPen
ms.author: genemi
manager: craigg
---
# SubmitChanges Method (RDS)
Submits pending changes of the locally cached and updatable [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) to the data source specified in the [Connect](../../../ado/reference/rds-api/connect-property-rds.md) property or the [URL](../../../ado/reference/rds-api/url-property-rds.md) property.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
DataControl.SubmitChanges DataFactory.SubmitChanges Connection, Recordset  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object.  
  
 *DataFactory*  
 An object variable that represents an [RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md) object.  
  
 *Connection*  
 A **String** value that represents the connection created with the **RDS.DataControl** object's [Connect](../../../ado/reference/rds-api/connect-property-rds.md) property.  
  
 *Recordset*  
 An object variable that represents a **Recordset** object.  
  
## Remarks  
 The [Connect](../../../ado/reference/rds-api/connect-property-rds.md), [Server](../../../ado/reference/rds-api/server-property-rds.md), and [SQL](../../../ado/reference/rds-api/sql-property.md) properties must be set before you can use the **SubmitChanges** method with the **RDS.DataControl** object.  
  
 If you call the [CancelUpdate](../../../ado/reference/rds-api/cancelupdate-method-rds.md) method after you have called **SubmitChanges** for the same **Recordset** object, the **CancelUpdate** call fails because the changes have already been committed.  
  
 Only the changed records are sent for modification, and either all of the changes succeed or all the changes fail together.  
  
 You can use **SubmitChanges** only with the default **RDSServer.DataFactory** object. Custom business objects cannot use this method.  
  
 If the **URL** property has been set, **SubmitChanges** will submit changes to the location specified by the URL.  
  
## Applies To  
  
|||  
|-|-|  
|[DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)|[DataFactory Object (RDSServer)](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)|  
  
## See Also  
 [SubmitChanges Method Example (VBScript)](../../../ado/reference/rds-api/submitchanges-method-example-vbscript.md)   
 [Address Book Command Buttons](../../../ado/guide/remote-data-service/address-book-command-buttons.md)   
 [CancelUpdate Method (RDS)](../../../ado/reference/rds-api/cancelupdate-method-rds.md)   
 [Refresh Method (RDS)](../../../ado/reference/rds-api/refresh-method-rds.md)



