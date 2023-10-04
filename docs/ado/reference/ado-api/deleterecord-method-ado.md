---
title: "DeleteRecord Method (ADO)"
description: "DeleteRecord Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Record::raw_DeleteRecord"
  - "_Record::DeleteRecord"
helpviewer_keywords:
  - "DeleteRecord method [ADO]"
apitype: "COM"
---
# DeleteRecord Method (ADO)
Deletes an entity represented by a [Record](../../../ado/reference/ado-api/record-object-ado.md).  
  
## Syntax  
  
```  
  
Record.DeleteRecord Source, Async  
```  
  
#### Parameters  
 *Source*  
 Optional. A **String** value that contains a URL identifying the entity (for example, the file or directory) to be deleted. If *Source* is omitted or specifies an empty string, the entity represented by the current [Record](../../../ado/reference/ado-api/record-object-ado.md) is deleted. If the Record is a collection record ([RecordType](../../../ado/reference/ado-api/recordtype-property-ado.md) of **adCollectionRecord**, such as a directory) all children (for example, subdirectories) will also be deleted.  
  
 *Async*  
 Optional. A **Boolean** value that, when **True**, specifies that the delete operation is asychronous.  
  
## Remarks  
 Operations on the object represented by this **Record** may fail after this method completes. After calling **DeleteRecord**, the **Record** should be closed because the behavior of the **Record** may become unpredictable depending upon when the provider updates the **Record** with the data source.  
  
 If this **Record** was obtained from a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md), then the results of this operation will not be reflected immediately in the **Recordset**. Refresh the **Recordset** by closing and re-opening it, or by executing the **Recordset** [Requery](../../../ado/reference/ado-api/requery-method.md) method, the [Update](../../../ado/reference/ado-api/update-method.md) method, or the [Resync](../../../ado/reference/ado-api/resync-method.md) method.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)  
  
## See Also  
 [Delete Method (ADO Fields Collection)](../../../ado/reference/ado-api/delete-method-ado-fields-collection.md)   
 [Delete Method (ADO Parameters Collection)](../../../ado/reference/ado-api/delete-method-ado-parameters-collection.md)   
 [Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)
