---
title: "ADOStreamConstruction Interface | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADOStreamConstruction"
helpviewer_keywords: 
  - "ADOStreamConstruction interface [ADO]"
ms.assetid: 92f5a939-3e1a-4b14-a9dd-90e6ce2dec74
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADOStreamConstruction Interface
The **ADOStreamConstruction** interface is used to construct an ADO **Stream** object from an OLE DB **IStream** object in a C/C++ application.  
  
## Properties  
  
|||  
|-|-|  
|[Stream Property](../../../ado/reference/ado-api/stream-property.md)|Read/Write. Gets/sets an OLE DB **Stream** object.|  
  
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
 [ADO API Reference](../../../ado/reference/ado-api/ado-api-reference.md)
