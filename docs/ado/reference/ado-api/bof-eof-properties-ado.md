---
title: "BOF, EOF Properties (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::BOF"
  - "Recordset15::EOF"
helpviewer_keywords: 
  - "EOF property [ADO]"
  - "BOF property [ADO]"
ms.assetid: 36c31ab2-f3b6-4281-89b6-db7e04e38fd2
author: MightyPen
ms.author: genemi
manager: craigg
---
# BOF, EOF Properties (ADO)
-   **BOF** Indicates that the current record position is before the first record in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
-   **EOF** Indicates that the current record position is after the last record in a **Recordset** object.  
  
## Return Value  
 The **BOF** and **EOF** properties return **Boolean** values.  
  
## Remarks  
 Use the **BOF** and **EOF** properties to determine whether a **Recordset** object contains records or whether you have gone beyond the limits of a **Recordset** object when you move from record to record.  
  
 The **BOF** property returns **True** (-1) if the current record position is before the first record and **False** (0) if the current record position is on or after the first record.  
  
 The **EOF** property returns **True** if the current record position is after the last record and **False** if the current record position is on or before the last record.  
  
 If either the **BOF** or **EOF** property is **True**, there is no current record.  
  
 If you open a **Recordset** object containing no records, the **BOF** and **EOF** properties are set to **True** (see the [RecordCount](../../../ado/reference/ado-api/recordcount-property-ado.md) property for more information about this state of a **Recordset**). When you open a **Recordset** object that contains at least one record, the first record is the current record and the **BOF** and **EOF** properties are **False**.  
  
 If you delete the last remaining record in the **Recordset** object, the **BOF** and **EOF** properties may remain **False** until you attempt to reposition the current record.  
  
 This table shows which **Move** methods are allowed with different combinations of the **BOF** and **EOF** properties.  
  
||MoveFirst,<br /><br /> MoveLast|MovePrevious,<br /><br /> Move < 0|Move 0|MoveNext,<br /><br /> Move > 0|  
|------|-----------------------------|---------------------------------|------------|-----------------------------|  
|**BOF**=**True**, **EOF**=**False**|Allowed|Error|Error|Allowed|  
|**BOF**=**False**, **EOF**=**True**|Allowed|Allowed|Error|Error|  
|Both **True**|Error|Error|Error|Error|  
|Both **False**|Allowed|Allowed|Allowed|Allowed|  
  
 Allowing a **Move** method does not guarantee that the method will successfully locate a record; it only means that calling the specified **Move** method will not generate an error.  
  
 The following table shows what happens to the **BOF** and **EOF** property settings when you call various **Move** methods but are unable to successfully locate a record.  
  
||BOF|EOF|  
|------|---------|---------|  
|**MoveFirst**, **MoveLast**|Set to **True**|Set to **True**|  
|**Move** 0|No change|No change|  
|**MovePrevious**, **Move** < 0|Set to **True**|No change|  
|**MoveNext**, **Move** > 0|No change|Set to **True**|  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [BOF, EOF, and Bookmark Properties Example (VB)](../../../ado/reference/ado-api/bof-eof-and-bookmark-properties-example-vb.md)   
 [BOF, EOF, and Bookmark Properties Example (VC++)](../../../ado/reference/ado-api/bof-eof-and-bookmark-properties-example-vc.md)   
