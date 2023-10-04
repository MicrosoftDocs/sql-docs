---
title: "Fabricating Hierarchical Recordsets"
description: "Fabricating Hierarchical Recordsets"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Recordset fabrication [ADO]"
  - "hierarchical Recordsets [ADO]"
  - "fabricating hierarchical Recordsets [ADO]"
  - "data shaping [ADO], hierarchical Recordsets"
---
# Fabricating Hierarchical Recordsets
The following example shows how to fabricate a hierarchical Recordset without an underlying data source by using the data shaping grammar to define columns for parent, child, and grandchild **Recordsets**.  
  
 To fabricate a hierarchical **Recordset**, you must specify the [Microsoft Data Shaping Service for OLE DB (ADO Service Provider)](../appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) (MSDataShape), and you can specify a Data Provider value of NONE in the connection string parameter of the [Open](../../reference/ado-api/open-method-ado-connection.md) method of the [Connection](../../reference/ado-api/connection-object-ado.md) object. For more information, see [Required Providers for Data Shaping](./required-providers-for-data-shaping.md).  
  
```  
Dim cn As New ADODB.Connection  
Dim rsCustomers As New ADODB.Recordset  
  
cn.Open "Provider=MSDataShape;Data Provider=NONE;"  
  
strShape = _  
"SHAPE APPEND NEW adInteger AS CustID," & _  
            " NEW adChar(25) AS FirstName," & _  
            " NEW adChar(25) AS LastName," & _  
            " NEW adChar(12) AS SSN," & _  
            " NEW adChar(50) AS Address," & _  
         " ((SHAPE APPEND NEW adChar(80) AS VIN_NO," & _  
                        " NEW adInteger AS CustID," & _  
                        " NEW adChar(20) AS BodyColor, " & _  
                     " ((SHAPE APPEND NEW adChar(80) AS VIN_NO," & _  
                                    " NEW adChar(20) AS Make, " & _  
                                    " NEW adChar(20) AS Model," & _  
                                    " NEW adChar(4) AS Year) " & _  
                        " AS VINS RELATE VIN_NO TO VIN_NO))" & _  
            " AS Vehicles RELATE CustID TO CustID) "  
  
rsCustomers.Open strShape, cn, adOpenStatic, adLockOptimistic, -1  
```  
  
 As soon as the **Recordset** has been fabricated, it can be populated, manipulated, or persisted to a file.  
  
## See Also  
 [Accessing Rows in a Hierarchical Recordset](./accessing-rows-in-a-hierarchical-recordset.md)   
 [Formal Shape Grammar](./formal-shape-grammar.md)   
 [Required Providers for Data Shaping](./required-providers-for-data-shaping.md)   
 [Shape APPEND Clause](./shape-append-clause.md)   
 [Shape Commands in General](./shape-commands-in-general.md)