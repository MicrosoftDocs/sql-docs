---
title: "Adding Records"
description: "Adding Records to a Recordset"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "AddNew method [ADO]"
  - "ADO, editing data"
  - "editing data [ADO], AddNew method"
  - "editing data [ADO], adding data"
---
# Adding Records to a Recordset
Use the **AddNew** method to create and initialize a new record in an existing **Recordset**. You can use the **Supports** method with a **CursorOptionEnum** value of **adAddNew** to verify whether you can add records to the current **Recordset** object.

 After you call the **AddNew** method, the new record becomes the current record and remains current after you call the **Update** method. If the **Recordset** object does not support bookmarks, you might not be able to access the new record once you move to another record. Therefore, depending on your cursor type, you might need to call the **Requery** method to make the new record accessible.

 If you call **AddNew** while editing the current record or while adding a new record, ADO calls the **Update** method to save any changes and then creates the new record.

 This section contains the following topics.

-   [Adding Records Using AddNew](./adding-records-using-addnew.md)

-   [Adding Multiple Fields](./adding-multiple-fields.md)

-   [Determining Edit Mode](./determining-edit-mode.md)

-   [Using AddNew in Immediate and Batch Modes](./using-addnew-in-immediate-and-batch-modes.md)