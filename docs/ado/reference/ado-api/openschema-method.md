---
title: "OpenSchema Method"
description: "OpenSchema Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::OpenSchema"
  - "Connection15::raw_OpenSchema"
helpviewer_keywords:
  - "OpenSchema method [ADO]"
apitype: "COM"
---
# OpenSchema Method
Obtains database schema information from the provider.  
  
## Syntax  
  
```  
  
Set recordset = connection.OpenSchema(QueryType, Criteria, SchemaID)  
```  
  
## Return Value  
 Returns a [Recordset](./recordset-object-ado.md) object that contains schema information. The **Recordset** will be opened as a read-only, static cursor. The *QueryType* determines what columns appear in the **Recordset**.  
  
#### Parameters  
 *QueryType*  
 Any [SchemaEnum](./schemaenum.md) value that represents the type of schema query to run.  
  
 *Criteria*  
 Optional. An array of query constraints for each *QueryType* option, as listed in [SchemaEnum](./schemaenum.md).  
  
 *SchemaID*  
 The GUID for a provider-schema query not defined by the OLE DB specification. This parameter is required if *QueryType* is set to **adSchemaProviderSpecific**; otherwise, it is not used.  
  
## Remarks  
 The **OpenSchema** method returns self-descriptive information about the data source, such as what tables are in the data source, the columns in the tables, and the data types supported.  
  
 The *QueryType* argument is a GUID that indicates the columns (schemas) returned. The OLE DB specification has a full list of schemas.  
  
 The *Criteria* argument limits the results of a schema query. *Criteria* specifies an array of values that must occur in a corresponding subset of columns, called constraint columns, in the resulting **Recordset**.  
  
 The constant **adSchemaProviderSpecific** is used for the *QueryType* argument if the provider defines its own nonstandard schema queries outside those listed previously. When this constant is used, the *SchemaID* argument is required to pass the GUID of the schema query to execute. If *QueryType* is set to **adSchemaProviderSpecific** but *SchemaID* is not provided, an error will result.  
  
 Providers are not required to support all the OLE DB standard schema queries. Specifically, only **adSchemaTables**, **adSchemaColumns**, and **adSchemaProviderTypes** are required by the OLE DB specification. However, the provider is not required to support the *Criteria* constraints listed earlier for those schema queries.  
  
> [!NOTE]
>  **Remote Data Service Usage** The **OpenSchema** method is not available on a client-side [Connection](./connection-object-ado.md) object.  
  
> [!NOTE]
>  In Visual Basic, columns that have a four-byte unsigned integer (DBTYPE UI4) in the **Recordset** returned from the **OpenSchema** method on the **Connection** object cannot be compared to other variables. For more information about OLE DB data types, see [Data Types in OLE DB (OLE DB)](/previous-versions/windows/desktop/ms714931(v=vs.85)) and [Appendix A: Data Types](/previous-versions/windows/desktop/ms723969(v=vs.85)) in the Microsoft OLE DB Programmer's Reference.  
  
> [!NOTE]
>  **Visual C/C++ Users** When not using client-side cursors, retrieving the "ORDINAL_POSITION" of a column schema in ADO returns a variant of type VT_R8 in MDAC 2.7, MDAC 2.8, and Windows Data Access Components (Windows DAC) 6.0, while the type used in MDAC 2.6 was VT_I4. Programs written for MDAC 2.6 that only look for a variant returned of type VT_I4 would get a zero for every ordinal if run under MDAC 2.7, MDAC 2.8, and Windows DAC 6.0 without modification. This change was made because the data type that OLE DB returns is DBTYPE_UI4, and in the signed VT_I4 type there is not enough room to contain all possible values without possibly truncation occurring and thereby causing a loss of data.  
  
## Applies To  
 [Connection Object (ADO)](./connection-object-ado.md)  
  
## See Also  
 [OpenSchema Method Example (VB)](./openschema-method-example-vb.md)   
 [OpenSchema Method Example (VC++)](./openschema-method-example-vc.md)   
 [Open Method (ADO Connection)](./open-method-ado-connection.md)   
 [Open Method (ADO Record)](./open-method-ado-record.md)   
 [Open Method (ADO Recordset)](./open-method-ado-recordset.md)   
 [Open Method (ADO Stream)](./open-method-ado-stream.md)   
 [Appendix A: Providers](../../guide/appendixes/appendix-a-providers.md)