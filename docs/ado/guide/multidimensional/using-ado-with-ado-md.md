---
title: "Using ADO with ADO MD"
description: "Using ADO with ADO MD"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, using with ADO MD"
---
# Using ADO with ADO MD
ADO and ADO MD are related but separate object models. ADO provides objects for connecting to data sources, executing commands, retrieving tabular data and schema metadata in a tabular format, and viewing provider error information. ADO MD provides objects for retrieving multidimensional data and viewing multidimensional schema metadata.  
  
 When you work with an MDP, you can choose to use ADO, ADO MD, or both with your application. By referencing both libraries within your project, you will have full access to the functionality provided by your MDP.  
  
 It is frequently useful for consumers to get a flattened, tabular view of a multidimensional dataset. You can do this by using the ADO [Recordset](../../reference/ado-api/recordset-object-ado.md) object. Specify the source for your [Cellset](../../reference/ado-md-api/cellset-object-ado-md.md) as the ***Source*** parameter for the [Open](../../reference/ado-api/open-method-ado-recordset.md) method of a **Recordset**, rather than as the source for an ADO MD **Cellset**.  
  
 It may also be useful to view the schema metadata in a tabular view rather than as a hierarchy of objects. The ADO [OpenSchema](../../reference/ado-api/openschema-method.md) method on the [Connection](../../reference/ado-api/connection-object-ado.md) object lets the user open a **Recordset** that contains schema information. The ***QueryType*** parameter of the **OpenSchema** method has several [SchemaEnum](../../reference/ado-api/schemaenum.md) values that relate specifically to MDPs. These values are as follows:  
  
-   **adSchemaCubes**  
  
-   **adSchemaDimensions**  
  
-   **adSchemaHierarchies**  
  
-   **adSchemaLevels**  
  
-   **adSchemaMeasures**  
  
-   **adSchemaMembers**  
  
 To use ADO enumeration values with ADO MD properties or methods, your project must reference both the ADO and ADO MD libraries. For example, you can use the ADO **adState** enumeration values with the ADO MD [State](../../reference/ado-md-api/state-property-ado-md.md) property. For more information about how to establish references to libraries, see the documentation of your development tool.  
  
 For more information about the ADO objects and methods, see the [ADO API Reference](../../reference/ado-api/ado-api-reference.md).  
  
## See Also  
 [ADO MD Object Model](../../reference/ado-md-api/ado-md-object-model.md)   
 [ADO (Multidimensional) (ADO MD)](./ado-multidimensional-ado-md.md)   
 [Overview of Multidimensional Schemas and Data](./overview-of-multidimensional-schemas-and-data.md)   
 [Programming with ADO MD](./programming-with-ado-md.md)   
 [Working with Multidimensional Data](./working-with-multidimensional-data.md)