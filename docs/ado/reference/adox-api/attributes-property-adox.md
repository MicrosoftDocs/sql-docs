---
description: "Attributes Property (ADOX)"
title: "Attributes Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Column::put_Attributes"
  - "_Column::Attributes"
  - "_Column::PutAttributes"
  - "_Column::get_Attributes"
  - "_Column::GetAttributes"
helpviewer_keywords: 
  - "Attributes property [ADOX]"
ms.assetid: e3abb359-79a3-4c22-b3a8-2900817e0d23
author: rothja
ms.author: jroth
---
# Attributes Property (ADOX)
Describes column characteristics.  
  
## Settings and Return Values  
 Sets or returns a **Long** value. The value specifies characteristics of the table that is represented by the [Column](./column-object-adox.md) object. The value can be a combination of [ColumnAttributesEnum](./columnattributesenum.md) constants. The default value is zero (**0**), which is neither **adColFixed** nor **adColNullable**.  
  
## Applies To  
  
- [Column Object (ADOX)](./column-object-adox.md)  
  
## See Also  
 [Attributes Property Example (VB)](./attributes-property-example-vb.md)