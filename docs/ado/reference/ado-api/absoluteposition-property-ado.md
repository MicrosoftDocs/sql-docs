---
title: "AbsolutePosition Property (ADO)"
description: "AbsolutePosition Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::AbsolutePosition"
helpviewer_keywords:
  - "AbsolutePosition property [ADO]"
apitype: "COM"
---
# AbsolutePosition Property (ADO)
Indicates the ordinal position of a [Recordset](./recordset-object-ado.md) object's current record.  
  
## Settings and Return Values  
 For 32-bit code, sets or returns a **Long** value from 1 to the number of records in the **Recordset** object ([RecordCount](./recordcount-property-ado.md)), or returns one of the [PositionEnum](./positionenum.md) values.  
  
 For 64-bit code, use a data type that provides for storage of a 64-bit value. For example, you might use either Long or another value that is 64-bit length such as DBORDINAL. Do not use **PositionEnum** values since they are limited to 32-bit length.  
  
## Remarks  
 In order to set the **AbsolutePosition** property, ADO requires that the OLE DB provider you are using implement the [IRowsetLocate:IRowset](/previous-versions/windows/desktop/ms721190(v=vs.85)) interface.  
  
 Accessing the **AbsolutePosition** property of a **Recordset** that was opened with either a forward-only or dynamic cursor raises the error **adErrFeatureNotAvailable**. With other cursor types, the correct position will be returned as long as the OLE DB provider supports the **IRowsetScroll:IRowsetLocate** interface. If the provider does not support the **IRowsetScroll** interface, the property is set to **adPosUnknown**. See the documentation for your provider to determine whether it supports **IRowsetScroll**.  
  
 Use the **AbsolutePosition** property to move to a record based on its ordinal position in the **Recordset** object, or to determine the ordinal position of the current record. The provider must support the appropriate functionality for this property to be available.  
  
 Like the [AbsolutePage](./absolutepage-property-ado.md) property, **AbsolutePosition** is 1-based and equals 1 when the current record is the first record in the **Recordset**. You can obtain the total number of records in the **Recordset** object from the [RecordCount](./recordcount-property-ado.md) property.  
  
 When you set the **AbsolutePosition** property, even if it is to a record in the current cache, ADO reloads the cache with a new group of records starting with the record you specified. The [CacheSize](./cachesize-property-ado.md) property determines the size of this group.  
  
> [!NOTE]
>  You should not use the **AbsolutePosition** property as a surrogate record number. The position of a given record changes when you delete a preceding record. There is also no assurance that a given record will have the same **AbsolutePosition** if the **Recordset** object is requeried or reopened. Bookmarks are still the recommended way of retaining and returning to a given position and are the only way of positioning across all types of **Recordset** objects.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [AbsolutePosition and CursorLocation Properties Example (VB)](./absoluteposition-and-cursorlocation-properties-example-vb.md)   
 [AbsolutePosition and CursorLocation Properties Example (VC++)](./absoluteposition-and-cursorlocation-properties-example-vc.md)   
 [AbsolutePage Property (ADO)](./absolutepage-property-ado.md)   
 [RecordCount Property (ADO)](./recordcount-property-ado.md)