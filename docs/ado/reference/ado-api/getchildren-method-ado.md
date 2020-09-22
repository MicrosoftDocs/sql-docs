---
description: "GetChildren Method (ADO)"
title: "GetChildren Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Record::raw_GetChildren"
  - "_Record::GetChildren"
helpviewer_keywords: 
  - "GetChildren method [ADO]"
ms.assetid: b3f09bac-4f66-49f6-aa5a-6fbb4fb28338
author: rothja
ms.author: jroth
---
# GetChildren Method (ADO)
Returns a [Recordset](./recordset-object-ado.md) whose rows represent the children of a collection [Record](./record-object-ado.md).  
  
## Syntax  
  
```  
  
Set recordset = record.GetChildren  
```  
  
## Return Value  
 A **Recordset** object for which each row represents a child of the current **Record** object. For example, the children of a **Record** that represents a directory would be the files and subdirectories contained within the parent directory.  
  
## Remarks  
 The provider determines what columns exist in the returned **Recordset**. For example, a document source provider always returns a resource **Recordset**.  
  
## Applies To  

:::row:::
    :::column:::
        [Record Object (ADO)](./record-object-ado.md)  
    :::column-end:::
    :::column:::
        [Recordset Object (ADO)](./recordset-object-ado.md)  
    :::column-end:::
:::row-end:::