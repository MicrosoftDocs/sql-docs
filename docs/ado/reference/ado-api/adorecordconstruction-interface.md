---
title: "ADORecordConstruction Interface | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADORecordConstruction"
helpviewer_keywords: 
  - "ADORecordConstruction interface [ADO]"
ms.assetid: 52a5429e-5829-455e-be3b-31f05cbecf2d
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADORecordConstruction Interface
The **ADORecordConstruction**interface is used to construct an ADO **Record** object from an OLE DB **Row** object in a C/C++ application.  
  
 This interface supports the following properties:  
  
## Properties  
  
|||  
|-|-|  
|[ParentRow](../../../ado/reference/ado-api/parentrow-property-ado.md)|Write-only.<br />Sets the container of an OLE DB **Row** object on this ADO **Record** object.|  
|[Row](../../../ado/reference/ado-api/row-property-ado.md)|Read/Write.<br />Gets/sets an OLE DB **Row** object from/on this ADO **Record** object.|  
  
## Methods  
 None.  
  
## Events  
 None.  
  
## Remarks  
 Given an OLE DB **Row** object (`pRow`), the construction of an ADO **Record** object (`adoR`), amounts to the following three basic operations:  
  
1.  Create an ADO **Record** object:  
  
    ```  
    _RecordPtr adoR;  
    adoRs.CreateInstance(__uuidof(_Record));  
    ```  
  
2.  Query the **IADORecordConstruction** interface on the **Record** object:  
  
    ```  
    adoRecordConstructionPtr adoRConstruct=NULL;  
    adoR->QueryInterface(__uuidof(ADORecordConstruction),  
                        (void**)&adoRConstruct);  
    ```  
  
3.  Call the **IADORecordConstruction::put_Row** property method to set the OLE DB **Row** object on the ADO **Record** object:  
  
    ```  
    IUnknown *pUnk=NULL;  
    pRow->QueryInterface(IID_IUnknown, (void**)&pUnk);  
    adoRConstruct->put_Row(pUnk);  
    ```  
  
 The resultant **adoR** object now represents the ADO **Record** object constructed from the OLE DB **Row** object.  
  
 An ADO **Record** object can also be constructed from the container of an OLE DB **Row** object.  
  
## Requirements  
 **Version:** ADO 2.0 and later  
  
 **Library:** msado15.dll  
  
 **UUID:** 00000567-0000-0010-8000-00AA006D2EA4
