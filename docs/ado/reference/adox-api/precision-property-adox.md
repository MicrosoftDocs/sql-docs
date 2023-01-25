---
title: "Precision Property (ADOX)"
description: "Precision Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Column::put_Precision"
  - "_Column::PutPrecision"
  - "_Column::GetPrecision"
  - "_Column::get_Precision"
  - "_Column::Precision"
helpviewer_keywords:
  - "Precision property [ADOX]"
apitype: "COM"
---
# Precision Property (ADOX)
Indicates the maximum precision of data values in the [Column](./column-object-adox.md).  
  
## Settings and Return Values  
 Sets and returns a **Long** value that is the maximum precision of data values in the column when the [Type](./type-property-column-adox.md) property is a numeric type. **Precision** is ignored for all other data types.  
  
## Remarks  
 The default value is zero (**0**).  
  
 This property is read-only for [Column](./column-object-adox.md) objects already appended to a collection.  
  
## Applies To  
 [Column Object (ADOX)](./column-object-adox.md)  
  
## See Also  
 [ADOX Code Example: NumericScale and Precision Properties Example (VB)](./adox-code-example-numericscale-and-precision-properties-example-vb.md)   
 [Type Property (Column) (ADOX)](./type-property-column-adox.md)   
 [Column Object (ADOX)](./column-object-adox.md)