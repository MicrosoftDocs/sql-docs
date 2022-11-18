---
title: "SetPermissions Method (ADOX)"
description: "SetPermissions Method (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "User25::SetPermissions"
  - "User25::raw_SetPermissions"
  - "_Group25::SetPermissions"
  - "_Group25::raw_SetPermissions"
helpviewer_keywords:
  - "SetPermissions method [ADOX]"
apitype: "COM"
---
# SetPermissions Method (ADOX)
Specifies the permissions for a [group](./group-object-adox.md) or [user](./user-object-adox.md) on an object.  
  
## Syntax  
  
```  
  
GroupOrUser.SetPermissions Name, ObjectType, Action, Rights [, Inherit] [, ObjectTypeId]  
```  
  
#### Parameters  
 *Name*  
 A **String** value that specifies the name of the object for which to set permissions.  
  
 *ObjectType*  
 A **Long** value which can be one of the [ObjectTypeEnum](./objecttypeenum.md) constants, that specifies the type of the object for which to get permissions.  
  
 *Action*  
 A **Long** value which can be one of the [ActionEnum](./actionenum.md) constants that specifies the type of action to perform when setting permissions.  
  
 *Rights*  
 A **Long** value which can be a bitmask of one or more of the [RightsEnum](./rightsenum.md) constants, that indicates the rights to set.  
  
 *Inherit*  
 Optional. A **Long** value which can be one of the [InheritTypeEnum](./inherittypeenum.md) constants, that specifies how objects will inherit these permissions. The default value is **adInheritNone**.  
  
 *ObjectTypeId*  
 Optional. A **Variant** value that specifies the GUID for a provider object type that is not defined by the OLE DB specification. This parameter is required if *ObjectType* is set to **adPermObjProviderSpecific**; otherwise, it is not used.  
  
## Remarks  
 An error will occur if the provider does not support setting access rights for groups or users.  
  
> [!NOTE]
>  When calling **SetPermissions**, setting Actions to **adAccessRevoke** overrides any settings of the *Rights* parameter. Do not set *Actions* to **adAccessRevoke** if you want the rights specified in the *Rights* parameter to take effect.  
  
## Applies To  

:::row:::
    :::column:::
        [Group Object (ADOX)](./group-object-adox.md)  
    :::column-end:::
    :::column:::
        [User Object (ADOX)](./user-object-adox.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [GetPermissions and SetPermissions Methods Example (VB)](./getpermissions-and-setpermissions-methods-example-vb.md)   
 [GetPermissions Method (ADOX)](./getpermissions-method-adox.md)   
 [Name Property (ADOX)](./name-property-adox.md)