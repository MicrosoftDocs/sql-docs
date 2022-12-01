---
title: "RuleEnum"
description: "RuleEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "RuleEnum"
helpviewer_keywords:
  - "RuleEnum enumeration [ADOX]"
apitype: "COM"
---
# RuleEnum
Specifies the rule to follow when a [Key](./key-object-adox.md) is deleted.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adRICascade**|1|Cascade changes.|  
|**adRINone**|0|Default. No action is taken.|  
|**adRISetDefault**|3|Foreign key value is set to the default.|  
|**adRISetNull**|2|Foreign key value is set to null.|  
  
## Applies To  
 [DeleteRule Property (ADOX)](./deleterule-property-adox.md)