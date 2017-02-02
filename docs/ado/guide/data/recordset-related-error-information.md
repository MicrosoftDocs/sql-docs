---
title: "Recordset-Related Error Information | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Recordset-related errors [ADO]"
  - "errors [ADO], Recordset-related"
ms.assetid: 7e103574-59ad-4790-b5f9-fa8d715e711e
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Recordset-Related Error Information
During batch processing, the **Status** property of the **Recordset** object provides information about the individual records in the **Recordset**. Before a batch update takes place, the **Status** property of the **Recordset** reflects information about records to be added, changed and deleted. After **UpdateBatch** has been called, the **Status** property indicates the success or failure of the operation. As you move from record to record in the **Recordset**, the value of the **Status** property changes to describe the status of the current record.