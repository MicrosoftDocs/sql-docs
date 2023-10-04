---
title: "DataSource Property (ADO)"
description: "DataSource Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset20::DataSource"
helpviewer_keywords:
  - "DataSource property [ADO]"
apitype: "COM"
---
# DataSource Property (ADO)
Indicates an object that contains data to be represented as a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
## Remarks  
 This property is used to create data-bound controls with the Data Environment. The Data Environment maintains collections of data (data sources) containing named objects (data members) that will be represented as a **Recordset** object.  
  
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
