---
title: "Determining Edit Mode | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "editing data [ADO], edit mode"
  - "ADO, editing data"
ms.assetid: 4c7e010d-08cd-4e22-9b32-23c36f02f88c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Determining Edit Mode
ADO maintains an editing buffer associated with the current record. The **EditMode** property indicates whether changes have been made to this buffer or whether a new record has been created. Use **EditMode** to determine the editing status of the current record. You can test for pending changes if an editing process has been interrupted and determine whether you need to use the **Update** or **CancelUpdate** method.  
  
 **EditMode** returns one of the **EditModeEnum** constants, which are listed in the following table.  
  
|Constant|Description|  
|--------------|-----------------|  
|**adEditNone**|Indicates that no editing operation is in progress.|  
|**adEditInProgress**|Indicates that data in the current record has been modified but not saved.|  
|**adEditAdd**|Indicates that the **AddNew** method has been called, and the current record in the copy buffer is a new record that has not been saved to the database.|  
|**adEditDelete**|Indicates that the current record has been deleted.|  
  
 **EditMode** can return a valid value only if there is a current record. **EditMode** will return an error if **BOF** or **EOF** is **True** or if the current record has been deleted.
