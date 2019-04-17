---
title: "RightsEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "RightsEnum"
helpviewer_keywords: 
  - "RightsEnum enumeration [ADOX]"
ms.assetid: 55ee67c7-a583-42aa-849a-78264b4cb614
author: MightyPen
ms.author: genemi
manager: craigg
---
# RightsEnum
Specifies the rights or permissions for a group or user on an object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adRightCreate**|16384 (&H4000)|The user or group has permission to create new objects of this type.|  
|**adRightDelete**|65536 (&H10000)|The user or group has permission to delete data from an object. For objects such as **Tables**, the user has permission to delete data values from records.|  
|**adRightDrop**|256 (&H100)|The user or group has permission to remove objects from the catalog. For example, **Tables** can be deleted by a DROP TABLE SQL command.|  
|**adRightExclusive**|512 (&H200)|The user or group has permission to access the object exclusively.|  
|**adRightExecute**|536870912 (&H20000000)|The user or group has permission to execute the object.|  
|**adRightFull**|268435456 (&H10000000)|The user or group has all permissions on the object.|  
|**adRightInsert**|32768 (&H8000)|The user or group has permission to insert the object. For objects such as **Tables**, the user has permission to insert data into the table.|  
|**adRightMaximumAllowed**|33554432 (&H2000000)|The user or group has the maximum number of permissions allowed by the provider. Specific permissions are provider-dependent.|  
|**adRightNone**|0|The user or group has no permissions for the object.|  
|**adRightRead**|-2147483648 (&H80000000)|The user or group has permission to read the object. For objects such as [Tables](../../../ado/reference/adox-api/table-object-adox.md), the user has permission to read the data in the table.|  
|**adRightReadDesign**|1024 (&H400)|The user or group has permission to read the design for the object.|  
|**adRightReadPermissions**|131072 (&H20000)|The user or group can view, but not change, the specific permissions for an object in the catalog.|  
|**adRightReference**|8192 (&H2000)|The user or group has permission to reference the object.|  
|**adRightUpdate**|1073741824 (&H40000000)|The user or group has permission to update the object. For objects such as **Tables**, the user has permission to update the data in the table.|  
|**adRightWithGrant**|4096 (&H1000)|The user or group has permission to grant permissions on the object.|  
|**adRightWriteDesign**|2048 (&H800)|The user or group has permission to modify the design for the object.|  
|**adRightWriteOwner**|524288 (&H80000)|The user or group has permission to modify the owner of the object.|  
|**adRightWritePermissions**|262144 (&H40000)|The user or group can modify the specific permissions for an object in the catalog.|  
  
## Applies To  
  
|||  
|-|-|  
|[GetPermissions Method (ADOX)](../../../ado/reference/adox-api/getpermissions-method-adox.md)|[SetPermissions Method (ADOX)](../../../ado/reference/adox-api/setpermissions-method-adox.md)|
