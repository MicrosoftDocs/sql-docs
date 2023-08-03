---
title: "Adding Records Using AddNew"
description: "Adding Records Using AddNew Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "AddNew method [ADO]"
  - "ADO, adding data"
  - "editing data [ADO], AddNew method"
---
# Adding Records Using AddNew Method
This is the basic syntax of the **AddNew** method:

 *recordset*.AddNew *FieldList*, *Values*

 The *FieldList* and *Values* arguments are optional. *FieldList* is either a single name or an array of names or ordinal positions of the fields in the new record.

 The *Values* argument is either a single value or an array of values for the fields in the new record.

 Typically, when you intend to add a single record, you will call the **AddNew** method without any arguments. Specifically, you will call **AddNew**; set the **Value** of each field in the new record; and then call **Update** or **UpdateBatch**, or both. You can make sure that your **Recordset** supports adding new records by using the **Supports** property with the **adAddNew** enumerated constant.

 The following code uses this technique to add a new Shipper to the sample **Recordset**. SQL Server supplies the ShipperID field value automatically. Therefore, the code does not try to supply a field value for the new records.

```
'BeginAddNew1.1
If objRs.Supports(adAddNew) Then
    With objRs
        .AddNew
        .Fields("CompanyName") = "Sample Shipper"
        .Fields("Phone") = "(931) 555-6334"
        .Update
    End With
End If
'EndAddNew1.1
```

## Remarks
 Because this code uses a disconnected **Recordset** with a client-side cursor in batch mode, you must reconnect the **Recordset** to the data source with a new **Connection** object before you can call the **UpdateBatch** method to post changes to the database. This is easily done by using the new function **GetNewConnection**.
