---
title: "Editing Existing Records"
description: "Editing Existing Records"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "editing data [ADO], existing records"
---
# Editing Existing Records
To edit existing records, move to the row you want to edit and change the **Value** property of the fields you want to change. For more information about the **Field** object's **Value** property, see [Examining Data](./examining-data.md). Depending on your cursor type, you will use **Update** or **UpdateBatch** to send changes back to the data source. For more information, see [Updating and Persisting Data](./updating-and-persisting-data.md).  
  
 It is usually more efficient to use a stored procedure with a command object to perform updates, as well as to perform other operations, because a stored procedure does not require the creation of a cursor. For more information about cursors, see [Understanding Cursors and Locks](./understanding-cursors-and-locks.md).