---
title: "ActionEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ActionEnum"
helpviewer_keywords: 
  - "ActionEnum enumeration [ADOX]"
ms.assetid: f948febd-c885-4621-823b-421e116fec4e
author: MightyPen
ms.author: genemi
manager: craigg
---
# ActionEnum
Specifies the type of action to be performed when [SetPermissions](../../../ado/reference/adox-api/setpermissions-method-adox.md) is called.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adAccessDeny**|3|The group or user will be denied the specified permissions.|  
|**adAccessGrant**|1|The group or user will have at least the requested permissions.|  
|**adAccessRevoke**|4|Any explicit access rights of the group or user will be revoked.|  
|**adAccessSet**|2|The group or user will have exactly the requested permissions.|
