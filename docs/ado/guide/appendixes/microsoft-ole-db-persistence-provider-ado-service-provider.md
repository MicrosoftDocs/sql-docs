---
title: "Microsoft OLE DB Persistence Provider (ADO Service Provider) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "providers [ADO], OLE DB persistence provider"
  - "persistence provider [ADO]"
  - "OLE DB persistence provider [ADO]"
ms.assetid: e75ef0dc-2016-4fcc-8918-23311c0d4e02
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft OLE DB Persistence Provider Overview
The Microsoft OLE DB Persistence Provider enables you to save a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object into a file, and later restore that **Recordset** object from the file. Schema information, data, and pending changes are preserved.

 You can save the **Recordset** in either the proprietary Advanced Data Table Gram (ADTG) format, or the open Extensible Markup Language (XML) format.

## Provider Keyword
 To invoke this provider, specify the following keyword and value in the connection string.

```vb
"Provider=MSPersist"
```

## Errors
 The following errors issued by this provider can be detected in your application.

|Constant|Description|
|--------------|-----------------|
|E_BADSTREAM|The file opened does not have a valid format (that is, the format is not ADTG or XML).|
|E_CANTPERSISTROWSET|The **Recordset** object saved has characteristics that prevent it from being stored.|

## Remarks
 The Microsoft OLE DB Persistence Provider exposes no dynamic properties.

 Currently, only parameterized hierarchical **Recordset** objects cannot be saved.

 For more information about persistently storing **Recordset** objects, see [Recordset Persistence](../../../ado/guide/data/more-about-recordset-persistence.md).

 When a stream is used to open a **Recordset,** there should be no parameters specified other than the *Source* parameter of the **Open** method.

## See Also
[Microsoft OLE DB Persistence Provider (ADO Service Provider)](../../../ado/guide/appendixes/microsoft-ole-db-persistence-provider-ado-service-provider.md)
