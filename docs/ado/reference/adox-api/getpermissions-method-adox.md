---
title: "GetPermissions Method (ADOX)"
description: "GetPermissions Method (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_User25::GetPermissions"
  - "_Group25::raw_GetPermissions"
  - "_Group25::GetPermissions"
  - "_User25::raw_GetPermissions"
helpviewer_keywords:
  - "GetPermissions method [ADOX]"
apitype: "COM"
---
# GetPermissions Method (ADOX)
Returns the permissions for a [group](./group-object-adox.md) or [user](./user-object-adox.md) on an object or object container.  
  
## Syntax  
  
```  
  
ReturnValue=GroupOrUser.GetPermissions(Name, ObjectType    [,ObjectTypeId])  
```  
  
## Return Value  
 Returns a **Long** value that specifies a bitmask containing the permissions that the group or user has on the object. This value can be one or more of the [RightsEnum](./rightsenum.md) constants.  
  
#### Parameters  
 *Name*  
 A **Variant** value that specifies the name of the object for which to set permissions. Set *Name* to a null value if you want to get the permissions for the object container.  
  
 *ObjectType*  
 A **Long** value which can be one of the [ObjectTypeEnum](./objecttypeenum.md) constants, that specifies the type of the object for which to get permissions.  
  
 *ObjectTypeId*  
 Optional. A **Variant** value that specifies the GUID for a provider object type not defined by the OLE DB specification. This parameter is required if *ObjectType* is set to **adPermObjProviderSpecific**; otherwise, it is not used.  
  
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
 [Name Property (ADOX)](./name-property-adox.md)   
 [SetPermissions Method (ADOX)](./setpermissions-method-adox.md)