---
title: "ADO Dynamic Properties"
description: "ADO Dynamic Properties"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "dynamic properties [ADO]"
  - "properties [ADO], dynamic"
---
# ADO Dynamic Properties
Dynamic properties can be added to the [Properties](./properties-collection-ado.md) collections of the [Connection](./connection-object-ado.md), [Command](./command-object-ado.md), or [Recordset](./recordset-object-ado.md) objects. The source for these properties is either a data provider, such as the [OLE DB Provider for SQL Server](../../guide/appendixes/microsoft-ole-db-provider-for-sql-server.md), or a service provider, such as the [Microsoft Cursor Service for OLE DB](../../guide/appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md). Refer to the appropriate data provider or service provider documentation for more information about a specific dynamic property.  
  
 The [ADO Dynamic Property Index](./ado-dynamic-property-index.md) provides a cross-reference between the ADO and OLE DB names for each standard OLE DB provider dynamic property.  
  
 The following dynamic properties are especially interesting, and are also documented in the sources that were mentioned earlier. Special functionality with ADO is documented in the ADO help topics in the following list.  
  
|Dynamic property|Description|  
|-|-|  
|[Optimize](./optimize-property-dynamic-ado.md)|Specifies whether an index should be created on this field.|  
|[Prompt](./prompt-property-dynamic-ado.md)|Specifies whether the OLE DB provider should prompt the user for initialization information.|  
|[Reshape Name](./reshape-name-property-dynamic-ado.md)|Specifies a name for the **Recordset** object.|  
|[Resync Command](./resync-command-property-dynamic-ado.md)|Specifies a user-supplied command string that the **Resync** method issues to refresh the data in the table named in the **Unique Table** dynamic property.|  
|[Unique Table, Unique Schema, Unique Catalog](./unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md)|**Unique Table** Specifies the name of the base table upon which updates, insertions, and deletions are allowed.<br /><br /> **Unique Schema** Specifies the schema, or name of the owner of the table.<br /><br /> **Unique Catalog** Specifies the catalog, or name of the database that contains the table.|  
|[Update Resync](./update-resync-property-dynamic-ado.md)|Specifies whether the **UpdateBatch** method is followed by an implicit **Resync** method operation, and if so, the scope of that operation.|

## See Also  
 [ADO API Reference](./ado-api-reference.md)   
 [ADO Collections](./ado-collections.md)   
 [ADO Enumerated Constants](./ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](./ado-events.md)   
 [ADO Methods](./ado-methods.md)   
 [ADO Object Model](./ado-object-model.md)   
 [ADO Objects and Interfaces](./ado-objects-and-interfaces.md)   
 [ADO Properties](./ado-properties.md)