---
title: "ADORecordsetConstruction Interface | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADORecordsetConstruction"
helpviewer_keywords: 
  - "ADORecordsetConstruction interface [ADO]"
ms.assetid: 08386eba-f1f7-4879-8ffd-8733930ecb2f
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADORecordsetConstruction Interface
The **ADORecordsetConstruction** interface is used to construct an ADO **Recordset** object from an OLE DB **Rowset** object in a C/C++ application.  
  
 This interface supports the following properties:  
  
## Properties  
  
|||  
|-|-|  
|[Chapter](../../../ado/reference/ado-api/chapter-property-ado.md)|Read/Write.<br />Gets/sets an OLE DB **Chapter** object from/on this ADO **Recordset** object.|  
|[RowPosition](../../../ado/reference/ado-api/rowposition-property-ado.md)|Read/Write.<br />Gets/sets an OLE DB **RowPosition** object from/on this ADO **Recordset** object.|  
|[Rowset](../../../ado/reference/ado-api/rowset-property-ado.md)|Read/Write.<br />Gets/sets an OLE DB **Rowset** object from/on this ADO **Recordset** object.|  
  
## Methods  
 None.  
  
## Events  
 None.  
  
## Remarks  
 Given an OLE DB **Rowset** object (`pRowset`), the construction of an ADO **Recordset** object (`adoRs`) amounts to the following three basic operations:  
  
1.  Create an ADO **Recordset** object:  
  
    ```  
    Recordset20Ptr adoRs;  
    adoRs.CreateInstance(__uuidof(Recordset));  
    ```  
  
2.  Query the **IADORecordsetConstruction** interface on the **Recordset** object:  
  
    ```  
    adoRecordsetConstructionPtr adoRsConstruct=NULL;  
    adoRs->QueryInterface(__uuidof(ADORecordsetConstruction),  
                         (void**)&adoRsConstruct);  
    ```  
  
3.  Call the `IADORecordsetConstruction::put_Rowset` property method to set the OLE DB `Rowset` object on the ADO `Recordset` object:  
  
    ```  
    IUnknown *pUnk=NULL;  
    pRowset->QueryInterface(IID_IUnknown, (void**)&pUnk);  
    adoRsConstruct->put_Rowset(pUnk);  
    ```  
  
 The resultant `adoRs` object now represents the ADO **Recordset** object constructed from the OLE DB **Rowset** object.  
  
 You can also construct an ADO **Recordset** object from an OLE DB **Chapter** or **RowPosition** object.  
  
## Requirements  
 **Version:** ADO 2.0 and later  
  
 **Library:** msado15.dll  
  
 **UUID:** 00000283-0000-0010-8000-00AA006D2EA4  
  
## See Also  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)   
 [Rowset Property (ADO)](../../../ado/reference/ado-api/rowset-property-ado.md)
