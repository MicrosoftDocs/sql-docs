---
title: "RuleEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "RuleEnum"
helpviewer_keywords: 
  - "RuleEnum enumeration [ADOX]"
ms.assetid: 738fd3ff-3daf-483d-a0b9-88bef1be54c1
author: MightyPen
ms.author: genemi
manager: craigg
---
# RuleEnum
Specifies the rule to follow when a [Key](../../../ado/reference/adox-api/key-object-adox.md) is deleted.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adRICascade**|1|Cascade changes.|  
|**adRINone**|0|Default. No action is taken.|  
|**adRISetDefault**|3|Foreign key value is set to the default.|  
|**adRISetNull**|2|Foreign key value is set to null.|  
  
## Applies To  
 [DeleteRule Property (ADOX)](../../../ado/reference/adox-api/deleterule-property-adox.md)
