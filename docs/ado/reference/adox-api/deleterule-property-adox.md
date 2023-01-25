---
title: "DeleteRule Property (ADOX)"
description: "DeleteRule Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Key::put_DeleteRule"
  - "_Key::DeleteRule"
  - "_Key::GetDeleteRule"
  - "_Key::PutDeleteRule"
  - "_Key::get_DeleteRule"
helpviewer_keywords:
  - "DeleteRule property [ADOX]"
apitype: "COM"
---
# DeleteRule Property (ADOX)
Indicates the action performed when a primary key is deleted.  
  
## Settings and Return Values  
 Sets and returns a **Long** value that can be one of the [RuleEnum](./ruleenum.md) constants. The default value is **adRINone**.  
  
## Remarks  
 This property is read-only on [Key](./key-object-adox.md) objects already appended to a collection.  
  
## Applies To  
 [Key Object (ADOX)](./key-object-adox.md)  
  
## See Also  
 [DeleteRule Property Example (VB)](./deleterule-property-example-vb.md)