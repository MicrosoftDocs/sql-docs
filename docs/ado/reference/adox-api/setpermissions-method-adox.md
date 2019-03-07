---
title: "SetPermissions Method (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "User25::SetPermissions"
  - "User25::raw_SetPermissions"
  - "_Group25::SetPermissions"
  - "_Group25::raw_SetPermissions"
helpviewer_keywords: 
  - "SetPermissions method [ADOX]"
ms.assetid: b7f925d7-b05c-4376-bb49-f8d2c17b8b24
author: MightyPen
ms.author: genemi
manager: craigg
---
# SetPermissions Method (ADOX)
Specifies the permissions for a [group](../../../ado/reference/adox-api/group-object-adox.md) or [user](../../../ado/reference/adox-api/user-object-adox.md) on an object.  
  
## Syntax  
  
```  
  
GroupOrUser.SetPermissions Name, ObjectType, Action, Rights [, Inherit] [, ObjectTypeId]  
```  
  
#### Parameters  
 *Name*  
 A **String** value that specifies the name of the object for which to set permissions.  
  
 *ObjectType*  
 A **Long** value which can be one of the [ObjectTypeEnum](../../../ado/reference/adox-api/objecttypeenum.md) constants, that specifies the type of the object for which to get permissions.  
  
 *Action*  
 A **Long** value which can be one of the [ActionEnum](../../../ado/reference/adox-api/actionenum.md) constants that specifies the type of action to perform when setting permissions.  
  
 *Rights*  
 A **Long** value which can be a bitmask of one or more of the [RightsEnum](../../../ado/reference/adox-api/rightsenum.md) constants, that indicates the rights to set.  
  
 *Inherit*  
 Optional. A **Long** value which can be one of the [InheritTypeEnum](../../../ado/reference/adox-api/inherittypeenum.md) constants, that specifies how objects will inherit these permissions. The default value is **adInheritNone**.  
  
 *ObjectTypeId*  
 Optional. A **Variant** value that specifies the GUID for a provider object type that is not defined by the OLE DB specification. This parameter is required if *ObjectType* is set to **adPermObjProviderSpecific**; otherwise, it is not used.  
  
## Remarks  
 An error will occur if the provider does not support setting access rights for groups or users.  
  
> [!NOTE]
>  When calling **SetPermissions**, setting Actions to **adAccessRevoke** overrides any settings of the *Rights* parameter. Do not set *Actions* to **adAccessRevoke** if you want the rights specified in the *Rights* parameter to take effect.  
  
## Applies To  
  
|||  
|-|-|  
|[Group Object (ADOX)](../../../ado/reference/adox-api/group-object-adox.md)|[User Object (ADOX)](../../../ado/reference/adox-api/user-object-adox.md)|  
  
## See Also  
 [GetPermissions and SetPermissions Methods Example (VB)](../../../ado/reference/adox-api/getpermissions-and-setpermissions-methods-example-vb.md)   
 [GetPermissions Method (ADOX)](../../../ado/reference/adox-api/getpermissions-method-adox.md)   
 [Name Property (ADOX)](../../../ado/reference/adox-api/name-property-adox.md)
