---
title: "NumericScale Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Column::PutNumericScale"
  - "_Column::GetNumericScale"
  - "_Column::NumericScale"
  - "_Column::put_NumericScale"
  - "_Column::get_NumericScale"
helpviewer_keywords: 
  - "NumericScale property [ADOX]"
ms.assetid: 573ee5d1-57c7-4a27-be79-a0e12944ad9b
author: MightyPen
ms.author: genemi
manager: craigg
---
# NumericScale Property (ADOX)
Indicates the scale of a numeric value in the column.  
  
## Settings and Return Values  
 Sets and returns a **Byte** value that is the scale of data values in the column when the [Type](../../../ado/reference/adox-api/type-property-column-adox.md) property is **adNumeric** or **adDecimal**. **NumericScale** is ignored for all other data types.  
  
## Remarks  
 The default value is zero (0).  
  
 **NumericScale** is read-only for [Column](../../../ado/reference/adox-api/column-object-adox.md) objects already appended to a collection.  
  
## Applies To  
 [Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)  
  
## See Also  
 [ADOX Code Example: NumericScale and Precision Properties Example (VB)](../../../ado/reference/adox-api/adox-code-example-numericscale-and-precision-properties-example-vb.md)   
 [Type Property (Column) (ADOX)](../../../ado/reference/adox-api/type-property-column-adox.md)
