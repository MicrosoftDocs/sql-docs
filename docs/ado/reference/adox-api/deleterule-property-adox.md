---
title: "DeleteRule Property (ADOX) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
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
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# DeleteRule Property (ADOX)
Indicates the action performed when a primary key is deleted.  
  
## Settings and Return Values  
 Sets and returns a **Long** value that can be one of the [RuleEnum](../../../ado/reference/adox-api/ruleenum.md) constants. The default value is **adRINone**.  
  
## Remarks  
 This property is read-only on [Key](../../../ado/reference/adox-api/key-object-adox.md) objects already appended to a collection.  
  
## Applies To  
 [Key Object (ADOX)](../../../ado/reference/adox-api/key-object-adox.md)  
  
## See Also  
 [DeleteRule Property Example (VB)](../../../ado/reference/adox-api/deleterule-property-example-vb.md)