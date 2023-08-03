---
title: "InheritTypeEnum"
description: "InheritTypeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "InheritTypeEnum"
helpviewer_keywords:
  - "InheritTypeEnum enumeration [ADOX]"
apitype: "COM"
---
# InheritTypeEnum
Specifies how objects will inherit permissions set with [SetPermissions](./setpermissions-method-adox.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adInheritBoth**|3|Both objects and other containers contained by the primary object inherit the entry.|  
|**adInheritContainers**|2|Other containers that are contained by the primary object inherit the entry.|  
|**adInheritNone**|0|Default. No inheritance occurs.|  
|**adInheritNoPropagate**|4|The **adInheritObjects** and **adInheritContainers** flags are not propagated to an inherited entry.|  
|**adInheritObjects**|1|Non-container objects in the container inherit the permissions.|  
  
## Applies To  
 [SetPermissions Method (ADOX)](./setpermissions-method-adox.md)