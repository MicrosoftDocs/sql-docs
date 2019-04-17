---
title: "SetObjectOwner Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Catalog::SetObjectOwner"
  - "_Catalog::raw_SetObjectOwner"
helpviewer_keywords: 
  - "SetObjectOwner method [ADOX]"
ms.assetid: e5170a37-9d6e-43db-bfb6-9b6631fa3048
author: MightyPen
ms.author: genemi
manager: craigg
---
# SetObjectOwner Method
Specifies the owner of an object in a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md).  
  
## Syntax  
  
```  
  
Catalog.SetObjectOwner ObjectName, ObjectType, OwnerName [,ObjectTypeId]  
```  
  
#### Parameters  
 *ObjectName*  
 A **String** value that specifies the name of the object for which to specify the owner.  
  
 *ObjectType*  
 A **Long** value which can be one of the [ObjectTypeEnum](../../../ado/reference/adox-api/objecttypeenum.md) constants that specifies the owner type.  
  
 *OwnerName*  
 A **String** value that specifies the [Name](../../../ado/reference/adox-api/name-property-adox.md) of the [User](../../../ado/reference/adox-api/user-object-adox.md) or [Group](../../../ado/reference/adox-api/group-object-adox.md) to own the object.  
  
 *ObjectTypeId*  
 Optional. A **Variant** value that specifies the GUID for a provider object type that is not defined by the OLE DB specification. This parameter is required if *ObjectType* is set to **adPermObjProviderSpecific**; otherwise, it is not used.  
  
## Remarks  
 An error will occur if the provider does not support specifying object owners.  
  
## Applies To  
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)  
  
## See Also  
 [GetObjectOwner and SetObjectOwner Methods Example (VB)](../../../ado/reference/adox-api/getobjectowner-and-setobjectowner-methods-example-vb.md)   
 [GetObjectOwner Method (ADOX)](../../../ado/reference/adox-api/getobjectowner-method-adox.md)
