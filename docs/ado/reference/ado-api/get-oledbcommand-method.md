---
title: "get_OLEDBCommand Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "get_OLEDBCommand method [ADO]"
ms.assetid: 23d551f5-3d5b-434b-ade6-fef15f1710e7
author: MightyPen
ms.author: genemi
manager: craigg
---
# get_OLEDBCommand Method
Returns the underlying OLE DB Command, first propagating any parameter information set on the ADO Command to the OLE DB Command.  
  
## Syntax  
  
```  
  
HRESULT get_OLEDBCommand(  
      IUnknown **ppOLEDBCommand  
);  
```  
  
#### Parameters  
 *ppOLEDBCommand*  
 [out] A pointer to a pointer location where the IUnknown pointer for the underlying OLE DB Command will be written.  
  
## Applies To  
 [IADOCommandConstruction](https://msdn.microsoft.com/d8e54333-00eb-4b72-bf4a-ca92c7ca5f86)
