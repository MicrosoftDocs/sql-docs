---
title: "Recordset-Related Error Information"
description: "Recordset-Related Error Information"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Recordset-related errors [ADO]"
  - "errors [ADO], Recordset-related"
---
# Recordset-Related Error Information
During batch processing, the **Status** property of the **Recordset** object provides information about the individual records in the **Recordset**. Before a batch update takes place, the **Status** property of the **Recordset** reflects information about records to be added, changed and deleted. After **UpdateBatch** has been called, the **Status** property indicates the success or failure of the operation. As you move from record to record in the **Recordset**, the value of the **Status** property changes to describe the status of the current record.
