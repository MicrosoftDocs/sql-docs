---
description: "UpdateRule Property (ADOX)"
title: "UpdateRule Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Key::GetUpdateRule"
  - "_Key::get_UpdateRule"
  - "_Key::PutUpdateRule"
  - "_Key::put_UpdateRule"
  - "_Key::UpdateRule"
helpviewer_keywords: 
  - "UpdateRule property [ADOX]"
ms.assetid: f4e21060-40cb-4790-8611-4086a092dda2
author: rothja
ms.author: jroth
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