---
title: "Required Providers for Data Shaping"
description: "Required Providers for Data Shaping"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "providers [ADO], data shaping"
  - "data shaping [ADO], providers required"
---
# Required Providers for Data Shaping
Data shaping typically requires two providers. The service provider, [Data Shaping Service for OLE DB](../../../ado/guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md), supplies the data shaping functionality, and a data provider, such as the OLE DB Provider for SQL Server, supplies rows of data to populate the shaped [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
 The name of the service provider (MSDataShape) can be specified as the value of the [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property or the connection string keyword "Provider=MSDataShape;".  
  
 The name of the data provider can be specified as the value of the **Data Provider** dynamic property, which is added to the **Connection** object [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection by the Data Shaping Service for OLE DB, or the connection string keyword "**Data Provider =** _provider_".  
  
 No data provider is required if the **Recordset** is not populated (for example, as in a fabricated **Recordset** where columns are created with the NEW keyword). In that case, specify "**Data Provider=** none;".  
  
## Example  
  
```  
Dim cnn As New ADODB.Connection  
cnn.Provider = "MSDataShape"  
cnn.Open "Data Provider=SQLOLEDB;Integrated Security=SSPI;Database=Northwind"  
```  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)
