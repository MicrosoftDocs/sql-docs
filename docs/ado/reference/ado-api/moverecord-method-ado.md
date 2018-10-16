---
title: "MoveRecord Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Record::MoveRecord"
  - "_Record::raw_MoveRecord"
helpviewer_keywords: 
  - "MoveRecord method [ADO]"
ms.assetid: 6d2807b0-b861-4583-bcaf-fb0b82e0f2d0
author: MightyPen
ms.author: genemi
manager: craigg
---
# MoveRecord Method (ADO)
Moves the entity represented by a [Record](../../../ado/reference/ado-api/record-object-ado.md) to another location.  
  
## Syntax  
  
```  
  
Record.MoveRecord (Source, Destination, UserName, Password, Options, Async)  
```  
  
#### Parameters  
 *Source*  
 Optional. A **String** value that contains a URL identifying the **Record** to be moved. If *Source* is omitted or specifies an empty string, the object represented by this **Record** is moved. For example, if the **Record** represents a file, the contents of the file are moved to the location specified by *Destination*.  
  
 *Destination*  
 Optional. A **String** value that contains a URL specifying the location where *Source* will be moved.  
  
 *UserName*  
 Optional. A **String** value that contains the user ID that, if needed, authorizes access to *Destination*.  
  
 *Password*  
 Optional. A **String** that contains the password that, if needed, verifies *UserName*.  
  
 *Options*  
 Optional. A [MoveRecordOptionsEnum](../../../ado/reference/ado-api/moverecordoptionsenum.md) value whose default value is **adMoveUnspecified**. Specifies the behavior of this method.  
  
 *Async*  
 Optional. A **Boolean** value that, when **True**, specifies this operation should be asynchronous.  
  
## Return Value  
 A **String** value. Typically, the value of *Destination* is returned. However, the exact value returned is provider-dependent.  
  
## Remarks  
 The values of *Source* and *Destination* must not be identical; otherwise, a run-time error occurs. At least the server, path, and resource names must differ.  
  
 For files moved using the Internet Publishing Provider, this method updates all hypertext links in files being moved unless otherwise specified by *Options*. This method fails if *Destination* identifies an existing object (for example, a file or directory), unless **adMoveOverWrite** is specified.  
  
> [!NOTE]
>  Use the **adMoveOverWrite** option judiciously. For example, specifying this option when moving a file to a directory will delete the directory and replace it with the file.  
  
 Certain attributes of the **Record** object, such as the [ParentURL](../../../ado/reference/ado-api/parenturl-property-ado.md) property, will not be updated after this operation completes. Refresh the **Record** object's properties by closing the **Record**, then re-opening it with the URL of the location where the file or directory was moved.  
  
 If this **Record** was obtained from a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md), the new location of the moved file or directory will not be reflected immediately in the **Recordset**. Refresh the **Recordset** by closing and re-opening it.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)  
  
## See Also  
 [Move Method (ADO)](../../../ado/reference/ado-api/move-method-ado.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (ADO)](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (RDS)](../../../ado/reference/rds-api/movefirst-movelast-movenext-and-moveprevious-methods-rds.md)
