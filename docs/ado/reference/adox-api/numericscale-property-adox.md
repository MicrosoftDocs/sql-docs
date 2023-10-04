---
title: "NumericScale Property (ADOX)"
description: "NumericScale Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Column::PutNumericScale"
  - "_Column::GetNumericScale"
  - "_Column::NumericScale"
  - "_Column::put_NumericScale"
  - "_Column::get_NumericScale"
helpviewer_keywords:
  - "NumericScale property [ADOX]"
apitype: "COM"
---
# NumericScale Property (ADOX)
Indicates the scale of a numeric value in the column.  
  
## Settings and Return Values  
 Sets and returns a **Byte** value that is the scale of data values in the column when the [Type](./type-property-column-adox.md) property is **adNumeric** or **adDecimal**. **NumericScale** is ignored for all other data types.  
  
## Remarks  
 The default value is zero (0).  
  
 **NumericScale** is read-only for [Column](./column-object-adox.md) objects already appended to a collection.  
  
## Applies To  
 [Column Object (ADOX)](./column-object-adox.md)  
  
## See Also  
 [ADOX Code Example: NumericScale and Precision Properties Example (VB)](./adox-code-example-numericscale-and-precision-properties-example-vb.md)   
 [Type Property (Column) (ADOX)](./type-property-column-adox.md)