---
title: "ExecuteOptions Property (RDS)"
description: "ExecuteOptions Property (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ExecuteOptions property [ADO], VBScript example"
apitype: "COM"
---
# ExecuteOptions Property (RDS)
Indicates whether asynchronous execution is enabled.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Settings and Return Values  
 Sets or returns one of the following values.  
  
|Constant|Description|  
|--------------|-----------------|  
|**adcExecSync**|Executes the next refresh of the [Recordset](../ado-api/recordset-object-ado.md) synchronously.|  
|**adcExecAsync**|Default. Executes the next refresh of the **Recordset** asynchronously.|  
  
> [!NOTE]
>  Each executable file that uses these constants must provide declarations for them. You can cut and paste the constant declarations that you want from the file Adcvbs.inc, located in the default installation folder for the RDS library.  
  
## Remarks  
 If **ExecuteOptions** is set to **adcExecAsync**, then this asynchronously executes the next **Refresh** call on the [RDS.DataControl](./datacontrol-object-rds.md) object's **Recordset**.  
  
 If you try to call [Reset](./reset-method-rds.md), [Refresh](./refresh-method-rds.md), [SubmitChanges](./submitchanges-method-rds.md), [CancelUpdate](../ado-api/cancelupdate-method-ado.md), or [Recordset](./recordset-sourcerecordset-properties-rds.md) while another asynchronous operation that might change the [RDS.DataControl](./datacontrol-object-rds.md) object's **Recordset** is executing, an error occurs.  
  
 If an error occurs during an asynchronous operation, the **RDS.DataControl** object's [ReadyState](./readystate-property-rds.md) value changes from **adcReadyStateLoaded** to **adcReadyStateComplete**, and the **Recordset** property value remains *Nothing*.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [ExecuteOptions and FetchOptions Properties Example (VBScript)](./executeoptions-and-fetchoptions-properties-example-vbscript.md)   
 [Cancel Method (RDS)](./cancel-method-rds.md)