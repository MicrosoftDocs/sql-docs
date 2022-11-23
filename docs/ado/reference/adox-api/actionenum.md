---
title: "ActionEnum"
description: "ActionEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ActionEnum"
helpviewer_keywords:
  - "ActionEnum enumeration [ADOX]"
apitype: "COM"
---
# ActionEnum
Specifies the type of action to be performed when [SetPermissions](./setpermissions-method-adox.md) is called.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adAccessDeny**|3|The group or user will be denied the specified permissions.|  
|**adAccessGrant**|1|The group or user will have at least the requested permissions.|  
|**adAccessRevoke**|4|Any explicit access rights of the group or user will be revoked.|  
|**adAccessSet**|2|The group or user will have exactly the requested permissions.|