---
title: "ReadyState Property (RDS)"
description: "ReadyState Property (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ReadyState property [ADO]"
apitype: "COM"
---
# ReadyState Property (RDS)
Indicates the progress of a [DataControl](./datacontrol-object-rds.md) object as it retrieves data into its [Recordset](../ado-api/recordset-object-ado.md) object.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Settings and Return Values  
 Sets or returns one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**adcReadyStateLoaded**|The current query is still executing and no rows have been fetched. The **DataControl** object's **Recordset** is not available for use.|  
|**adcReadyStateInteractive**|An initial set of rows retrieved by the current query has been stored in the **DataControl** object's **Recordset** and are available for use. The remaining rows are still being fetched.|  
|**adcReadyStateComplete**|All rows retrieved by the current query have been stored in the **DataControl** object's **Recordset** and are available for use.<br /><br /> This state will also exist if an operation aborted due to an error, or if the **Recordset** object is not initialized.|  
  
> [!NOTE]
>  Each client-side executable file that uses these constants must provide declarations for them. You can cut and paste the constant declarations you want from the file Adcvbs.inc, located in the default installation folder for the RDS library.  
  
## Remarks  
 Use the [onReadyStateChange](./onreadystatechange-event-rds.md) event to monitor changes in the **ReadyState** property during an asynchronous query operation. This is more efficient than periodically checking the value of the property.  
  
 If an error occurs during an asynchronous operation, the **ReadyState** property changes to **adcReadyStateComplete**, the [State](../ado-api/state-property-ado.md) property changes from **adStateExecuting** to **adStateClosed**, and the **Recordset** object [Value](../ado-api/value-property-ado.md) property remains *Nothing*.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [ReadyState Property Example (VBScript)](./readystate-property-example-vbscript.md)   
 [Cancel Method (RDS)](./cancel-method-rds.md)   
 [ExecuteOptions Property (RDS)](./executeoptions-property-rds.md)