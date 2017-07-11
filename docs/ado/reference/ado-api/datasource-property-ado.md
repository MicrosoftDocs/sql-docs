---
title: "DataSource Property (ADO) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
apitype: "COM"
f1_keywords: 
  - "Recordset20::DataSource"
helpviewer_keywords: 
  - "DataSource property [ADO]"
ms.assetid: 300a702a-3544-48c5-b759-83b511fe97e0
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# DataSource Property (ADO)
Indicates an object that contains data to be represented as a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
## Remarks  
 This property is used to create data-bound controls with the Data Environment. The Data Environment maintains collections of data (data sources) containing named objects (data members) that will be represented as a **Recordset** object*.*  
  
 The [DataMember](../../../ado/reference/ado-api/datamember-property.md) and **DataSource** properties must be used in conjunction.  
  
 The object referenced must implement the **IDataSource** interface and must contain an **IRowset** interface.  
  
## Usage  
  
```  
Dim rs as New ADODB.Recordset  
rs.DataMember = "Command"     'Name of the rowset to bind to.  
Set rs.DataSource = myDE      'Name of the object containing an IRowset.  
```  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [DataMember Property](../../../ado/reference/ado-api/datamember-property.md)