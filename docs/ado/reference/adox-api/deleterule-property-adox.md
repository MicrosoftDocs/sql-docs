---
description: "DeleteRule Property (ADOX)"
title: "DeleteRule Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Key::put_DeleteRule"
  - "_Key::DeleteRule"
  - "_Key::GetDeleteRule"
  - "_Key::PutDeleteRule"
  - "_Key::get_DeleteRule"
helpviewer_keywords: 
  - "DeleteRule property [ADOX]"
ms.assetid: 87bd4c0a-cae3-4007-a939-4193acaa00ac
author: rothja
ms.author: jroth
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