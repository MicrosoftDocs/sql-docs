---
title: "DataMember Property"
description: "DataMember Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset20::DataMember"
helpviewer_keywords:
  - "DataMember property"
apitype: "COM"
---
# DataMember Property
Indicates the name of the data member that will be retrieved from the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) referenced by the [DataSource](../../../ado/reference/ado-api/datasource-property-ado.md) property.  
  
## Settings and Return Values  
 Sets or returns a **String** value. The name is not case sensitive.  
  
## Remarks  
 This property is used to create data-bound controls with the Data Environment. The Data Environment maintains collections of data (data sources) containing named objects (data members) that will be represented as a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
 The **DataMember** and **DataSource** properties must be used together.  
  
 The **DataMember** property determines which object specified by the **DataSource** property will be represented as a **Recordset** object. The **Recordset** object must be closed before this property is set. An error is generated if the **DataMember** property is not set before the **DataSource** property, or if the **DataMember** name is not recognized by the object specified in the **DataSource** property.  
  
## Usage  
  
```  
Dim rs as New ADODB.Recordset  
rs.DataMember = "Command"     'Name of the rowset to bind to  
Set rs.DataSource = myDE      'Name of the object containing an IRowset  
```  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [DataSource Property (ADO)](../../../ado/reference/ado-api/datasource-property-ado.md)
