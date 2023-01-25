---
title: "UpdateRule Property (ADOX)"
description: "UpdateRule Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Key::GetUpdateRule"
  - "_Key::get_UpdateRule"
  - "_Key::PutUpdateRule"
  - "_Key::put_UpdateRule"
  - "_Key::UpdateRule"
helpviewer_keywords:
  - "UpdateRule property [ADOX]"
apitype: "COM"
---
# UpdateRule Property (ADOX)
Indicates the action performed when a primary [Key](./key-object-adox.md) is updated.  
  
## Settings and Return Values  
 Sets and returns a **Long** value that can be one of the [RuleEnum](./ruleenum.md) constants. The default value is **adRINone**.  
  
## Remarks  
 This property is read-only on [Key](./key-object-adox.md) objects already appended to the collection.  
  
## Applies To  
 [Key Object (ADOX)](./key-object-adox.md)  
  
## See Also  
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)