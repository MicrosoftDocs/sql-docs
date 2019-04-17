---
title: "GetPermissions Method (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_User25::GetPermissions"
  - "_Group25::raw_GetPermissions"
  - "_Group25::GetPermissions"
  - "_User25::raw_GetPermissions"
helpviewer_keywords: 
  - "GetPermissions method [ADOX]"
ms.assetid: df201c1f-c76a-465d-98f0-83b7fc36e6e3
author: MightyPen
ms.author: genemi
manager: craigg
---
# GetPermissions Method (ADOX)
Returns the permissions for a [group](../../../ado/reference/adox-api/group-object-adox.md) or [user](../../../ado/reference/adox-api/user-object-adox.md) on an object or object container.  
  
## Syntax  
  
```  
  
ReturnValue=GroupOrUser.GetPermissions(Name, ObjectType    [,ObjectTypeId])  
```  
  
## Return Value  
 Returns a **Long** value that specifies a bitmask containing the permissions that the group or user has on the object. This value can be one or more of the [RightsEnum](../../../ado/reference/adox-api/rightsenum.md) constants.  
  
#### Parameters  
 *Name*  
 A **Variant** value that specifies the name of the object for which to set permissions. Set *Name* to a null value if you want to get the permissions for the object container.  
  
 *ObjectType*  
 A **Long** value which can be one of the [ObjectTypeEnum](../../../ado/reference/adox-api/objecttypeenum.md) constants, that specifies the type of the object for which to get permissions.  
  
 *ObjectTypeId*  
 Optional. A **Variant** value that specifies the GUID for a provider object type not defined by the OLE DB specification. This parameter is required if *ObjectType* is set to **adPermObjProviderSpecific**; otherwise, it is not used.  
  
## Applies To  
  
|||  
|-|-|  
|[Group Object (ADOX)](../../../ado/reference/adox-api/group-object-adox.md)|[User Object (ADOX)](../../../ado/reference/adox-api/user-object-adox.md)|  
  
## See Also  
 [GetPermissions and SetPermissions Methods Example (VB)](../../../ado/reference/adox-api/getpermissions-and-setpermissions-methods-example-vb.md)   
 [Name Property (ADOX)](../../../ado/reference/adox-api/name-property-adox.md)   
 [SetPermissions Method (ADOX)](../../../ado/reference/adox-api/setpermissions-method-adox.md)
