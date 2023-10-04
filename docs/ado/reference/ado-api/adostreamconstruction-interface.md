---
title: "ADOStreamConstruction Interface"
description: "ADOStreamConstruction Interface"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADOStreamConstruction"
helpviewer_keywords:
  - "ADOStreamConstruction interface [ADO]"
apitype: "COM"
---
# ADOStreamConstruction Interface
The **ADOStreamConstruction** interface is used to construct an ADO **Stream** object from an OLE DB **IStream** object in a C/C++ application.  
  
## Properties  
  
|Property|Description|  
|-|-|  
|[Stream](./stream-property.md)|Read/Write. Gets/sets an OLE DB **Stream** object.|  
  
## Methods  
 None.  
  
## Events  
 None.  
  
## Remarks  
 Given an OLE DB **IStream** object (`pStream`), the construction of an ADO **Stream** object (`adoStr`) amounts to the following three basic operations:  
  
1.  Create an ADO **Stream** object:  
  
    ```  
    Stream20Ptr adoStr;  
    adoStr.CreateInstance(__uuidof(Stream));  
    ```  
  
2.  Query the **IADOStreamConstruction** interface on the **Stream** object:  
  
    ```  
    adoStreamConstructionPtr adoStrConstruct=NULL;  
    adoStr->QueryInterface(__uuidof(ADOStreamConstruction),  
                         (void**)&adoStrConstruct);  
    ```  
  
 Call the `IADOStreamConstruction::get_Stream` property method to set the OLE DB **IStream** object on the ADO **Stream** object:  
  
```  
IUnknown *pUnk=NULL;  
pRowset->QueryInterface(IID_IUnknown, (void**)&pUnk);  
adoStrConstruct->put_Stream(pUnk);  
```  
  
 The resultant `adoStr` object now represents the ADO **Stream** object constructed from the OLE DB **IStream** object.  
  
## Requirements  
 **Version:** ADO 2.0 or a later version  
  
 **Library:** msado15.dll  
  
 **UUID:** 00000283-0000-0010-8000-00AA006D2EA4  
  
## See Also  
 [ADO API Reference](./ado-api-reference.md)