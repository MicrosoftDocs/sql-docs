---
title: "Source Property (ADO Record) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Record::Source"
  - "_Record::PutRefSource"
  - "_Record::GetSource"
  - "_Record::put_Source"
  - "_Record::putref_Source"
  - "_Record::get_Source"
helpviewer_keywords: 
  - "Source property [ADO Record]"
ms.assetid: 2c18279e-6f35-4af0-b12e-8f1543d9ed20
author: MightyPen
ms.author: genemi
manager: craigg
---
# Source Property (ADO Record)
Indicates the data source or object represented by the [Record](../../../ado/reference/ado-api/record-object-ado.md).  
  
## Settings and Return Values  
 Sets or returns a **Variant** value that indicates the entity represented by the **Record**.  
  
## Remarks  
 The **Source** property returns the *Source* argument of the **Record** object [Open](../../../ado/reference/ado-api/open-method-ado-record.md) method. It can contain an absolute or relative URL string. An absolute URL can be used without setting the [ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md) property to directly open the **Record** object. An implicit **Connection** object is created in this case.  
  
 The **Source** property can also contain a reference to an already open **Recordset**, which opens a **Record** object representing the current row in the **Recordset**.  
  
 The **Source** property could also contain a reference to a [Command](../../../ado/reference/ado-api/command-object-ado.md) object which returns a single row of data from the provider.  
  
 If the **ActiveConnection** property is also set, then the **Source** property must point to some object that exists within the scope of that connection. For example, in tree-structured namespaces, if the **Source** property contains an absolute URL, it must point to a node that exists inside the scope of the node identified by the URL in the connection string. If the **Source** property contains a relative URL, then it is validated within the context set by the **ActiveConnection** property.  
  
 The **Source** property is read/write while the **Record** object is closed, and is read-only while the **Record** object is open.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)  
  
## See Also  
 [Source Property (ADO Error)](../../../ado/reference/ado-api/source-property-ado-error.md)   
 [Source Property (ADO Recordset)](../../../ado/reference/ado-api/source-property-ado-recordset.md)
