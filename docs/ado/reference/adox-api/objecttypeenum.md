---
title: "ObjectTypeEnum"
description: "ObjectTypeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ObjectTypeEnum"
helpviewer_keywords:
  - "ObjectTypeEnum enumeration [ADOX]"
apitype: "COM"
---
# ObjectTypeEnum
Specifies the type of database object for which to set permissions or ownership.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adPermObjColumn**|2|The object is a column.|  
|**adPermObjDatabase**|3|The object is a database.|  
|**adPermObjProcedure**|4|The object is a procedure.|  
|**adPermObjProviderSpecific**|-1|The object is a type defined by the provider. An error will occur if the *ObjectType* parameter is **adPermObjProviderSpecific** and an *ObjectTypeId* is not supplied.|  
|**adPermObjTable**|1|The object is a table.|  
|**adPermObjView**|5|The object is a view.|  
  
## Applies To  

:::row:::
    :::column:::
        [GetObjectOwner Method (ADOX)](./getobjectowner-method-adox.md)  
        [GetPermissions Method (ADOX)](./getpermissions-method-adox.md)  
    :::column-end:::
    :::column:::
        [SetObjectOwner Method](./setobjectowner-method.md)  
        [SetPermissions Method (ADOX)](./setpermissions-method-adox.md)  
    :::column-end:::
:::row-end:::