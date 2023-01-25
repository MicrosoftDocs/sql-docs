---
title: "AddNew Method (ADO)"
description: "AddNew Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::AddNew"
  - "Recordset15::raw_AddNew"
helpviewer_keywords:
  - "AddNew method [ADO]"
apitype: "COM"
---
# AddNew Method (ADO)
Creates a new record for an updatable [Recordset](./recordset-object-ado.md) object.  
  
## Syntax  
  
```  
  
recordset.AddNew FieldList, Values  
```  
  
#### Parameters  
 *recordset*  
 A **Recordset** object.  
  
 *FieldList*  
 Optional. A single name, or an array of names or ordinal positions of the fields in the new record.  
  
 *Values*  
 Optional. A single value, or an array of values for the fields in the new record. If *Fieldlist* is an array, *Values* must also be an array with the same number of members; otherwise, an error occurs. The order of field names must match the order of field values in each array.  
  
## Remarks  
 Use the **AddNew** method to create and initialize a new record. Use the [Supports](./supports-method.md) method with **adAddNew** (a [CursorOptionEnum](./cursoroptionenum.md) value) to verify whether you can add records to the current **Recordset** object.  
  
 After you call the **AddNew** method, the new record becomes the current record and remains current after you call the [Update](./update-method.md) method. Since the new record is appended to the **Recordset**, a call to **MoveNext** following the Update will move past the end of the **Recordset**, making **EOF** True. If the **Recordset** object does not support bookmarks, you may not be able to access the new record once you move to another record. Depending on your cursor type, you may need to call the [Requery](./requery-method.md) method to make the new record accessible.  
  
 If you call **AddNew** while editing the current record or while adding a new record, ADO calls the **Update** method to save any changes and then creates the new record.  
  
 The behavior of the **AddNew** method depends on the updating mode of the **Recordset** object and whether you pass the *Fieldlist* and *Values* arguments.  
  
 In *immediate update mode* (in which the provider writes changes to the underlying data source once you call the **Update** method), calling the **AddNew** method without arguments sets the [EditMode](./editmode-property.md) property to **adEditAdd** (an [EditModeEnum](./editmodeenum.md) value). The provider caches any field value changes locally. Calling the **Update** method posts the new record to the database and resets the **EditMode** property to **adEditNone** (an **EditModeEnum** value). If you pass the *Fieldlist* and *Values* arguments, ADO immediately posts the new record to the database (no **Update** call is necessary); the **EditMode** property value does not change (**adEditNone**).  
  
 In *batch update mode* (in which the provider caches multiple changes and writes them to the underlying data source only when you call the [UpdateBatch](./updatebatch-method.md) method), calling the **AddNew** method without arguments sets the **EditMode** property to **adEditAdd**. The provider caches any field value changes locally. Calling the **Update** method adds the new record to the current **Recordset**, but the provider does not post the changes to the underlying database, or reset the **EditMode** to **adEditNone**, until you call the **UpdateBatch** method. If you pass the *Fieldlist* and *Values* arguments, ADO sends the new record to the provider for storage in a cache and sets the **EditMode** to **adEditAdd**; you need to call the **UpdateBatch** method to post the new record to the underlying database.  
  
## Example  
 The following example shows how to use the AddNew method with the field list and value list included, to see how to include the field list and value list as arrays.  
  
```  
create table aa1 (intf int, charf char(10))  
insert into aa1 values (2, 'aa')  
  
Dim cn As New adodb.Connection  
Dim rs As New adodb.Recordset  
Dim cmd As New adodb.Command  
  
cn.ConnectionString = "Provider=SQLOLEDB;Data Source=alexverb2;uid=sa;pwd=foo$bar00;"  
  
cn.Open  
rs.Open "select * from xxx..aa1", cn, adOpenKeyset, adLockOptimistic  
  
Dim fieldsArray(1) As Variant  
fieldsArray(0) = "intf"  
fieldsArray(1) = "charf"  
Dim values(1) As Variant  
values(0) = 4  
values(1) = "as"  
rs.AddNew fieldsArray, values  
rs.Update  
```  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [AddNew Method Example (VB)](./addnew-method-example-vb.md)   
 [AddNew Method Example (VBScript)](./addnew-method-example-vbscript.md)   
 [AddNew Method Example (VC++)](./addnew-method-example-vc.md)   
 [CancelUpdate Method (ADO)](./cancelupdate-method-ado.md)   
 [EditMode Property](./editmode-property.md)   
 [Requery Method](./requery-method.md)   
 [Supports Method](./supports-method.md)   
 [Update Method](./update-method.md)   
 [UpdateBatch Method](./updatebatch-method.md)