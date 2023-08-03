---
title: "SetObjectOwner Method"
description: "SetObjectOwner Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Catalog::SetObjectOwner"
  - "_Catalog::raw_SetObjectOwner"
helpviewer_keywords:
  - "SetObjectOwner method [ADOX]"
apitype: "COM"
---
# SetObjectOwner Method
Specifies the owner of an object in a [Catalog](./catalog-object-adox.md).  
  
## Syntax  
  
```  
  
Catalog.SetObjectOwner ObjectName, ObjectType, OwnerName [,ObjectTypeId]  
```  
  
#### Parameters  
 *ObjectName*  
 A **String** value that specifies the name of the object for which to specify the owner.  
  
 *ObjectType*  
 A **Long** value which can be one of the [ObjectTypeEnum](./objecttypeenum.md) constants that specifies the owner type.  
  
 *OwnerName*  
 A **String** value that specifies the [Name](./name-property-adox.md) of the [User](./user-object-adox.md) or [Group](./group-object-adox.md) to own the object.  
  
 *ObjectTypeId*  
 Optional. A **Variant** value that specifies the GUID for a provider object type that is not defined by the OLE DB specification. This parameter is required if *ObjectType* is set to **adPermObjProviderSpecific**; otherwise, it is not used.  
  
## Remarks  
 An error will occur if the provider does not support specifying object owners.  
  
## Applies To  
 [Catalog Object (ADOX)](./catalog-object-adox.md)  
  
## See Also  
 [GetObjectOwner and SetObjectOwner Methods Example (VB)](./getobjectowner-and-setobjectowner-methods-example-vb.md)   
 [GetObjectOwner Method (ADOX)](./getobjectowner-method-adox.md)