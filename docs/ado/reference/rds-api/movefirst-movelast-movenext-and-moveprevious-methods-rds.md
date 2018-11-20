---
title: "MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "MoveLast method [RDS]"
  - "MovePrevious method [RDS]"
  - "MoveFirst method [RDS]"
  - "MoveNext method [RDS]"
ms.assetid: 45c80bb5-136f-4204-9df2-78740fa55574
author: MightyPen
ms.author: genemi
manager: craigg
---
# MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (RDS)
Moves to the first, last, next, or previous record in a specified [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
DataControl.Recordset.{MoveFirst | MoveLast | MoveNext | MovePrevious}  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object.  
  
## Remarks  
 You can use the **Move** methods with the **RDS.DataControl** object to navigate through the data records in the data-bound controls on a Web page. For example, suppose you display a **Recordset** in a grid by binding to an **RDS.DataControl** object. You can then include First, Last, Next, and Previous buttons that users can click to move to the first, last, next, or previous record in the displayed **Recordset**. You do this by calling the **MoveFirst**, **MoveLast**, **MoveNext**, and **MovePrevious** methods of the **RDS.DataControl** object in the onClick procedures for the First, Last, Next, and Previous buttons, respectively. The [Address Book example](../../../ado/guide/remote-data-service/address-book-navigation-buttons.md) shows how to do this.  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [Move Method (ADO)](../../../ado/reference/ado-api/move-method-ado.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (ADO)](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)   
 [MoveRecord Method (ADO)](../../../ado/reference/ado-api/moverecord-method-ado.md)


