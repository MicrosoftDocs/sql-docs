---
title: "Data Sources and Bindings (SSAS Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data source views [Analysis Services], bindings"
  - "DSO, bindings"
  - "Analysis Services Scripting Language, data sources"
  - "cubes [Analysis Services], bindings"
  - "OLAP mining models [Analysis Services Scripting Language]"
  - "bindings [Analysis Services Scripting Language]"
  - "rebindings [Analysis Services Scripting Language]"
  - "ASSL, bindings"
  - "relational mining models [ASSL]"
  - "data sources [Analysis Services Scripting Language]"
  - "ASSL, data sources"
  - "dimensions [Analysis Services], bindings"
  - "measures [Analysis Services], bindings"
  - "relational data sources [Analysis Services Scripting Language]"
  - "Analysis Services Scripting Language, bindings"
  - "chaptered rowsets"
  - "granularity"
  - "mining models [Analysis Services], data sources"
  - "inline bindings [ASSL]"
  - "out-of-line bindings"
  - "measure groups [Analysis Services], bindings"
  - "partitions [Analysis Services], bindings"
ms.assetid: bc028030-dda2-4660-b818-c3160d79fd6d
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Sources and Bindings (SSAS Multidimensional)
  Cubes, dimensions, and other [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects can be bound to a data source. A data source can be one of the following objects:  
  
-   A relational data source.  
  
-   An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] pipeline that outputs a rowset (or chaptered rowsets).  
  
 The means of expressing the data source varies by the type of data source. For example, a relational data source is distinguished by the connection string. For more information about data sources, see [Data Sources in Multidimensional Models](data-sources-in-multidimensional-models.md).  
  
 Regardless of the data source used, the data source view (DSV) contains the metadata for the data source. Thus, the bindings for a cube or other [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects are expressed as bindings to the DSV. These bindings can include bindings to logical objects-objects such as views, calculated columns, and relationships that do not physically exist in the data source. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] adds a calculated column that encapsulates the expression to the DSV, and then binds the corresponding OLAP measure to that column in the DSV. For more information about DSVs, see [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md).  
  
 Each [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object binds to the data source in its own way. In addition, the data bindings for these objects and the definition of the data source can be provided inline with the definition of the databound object (for example, the dimension), or out-of-line as a separate set of definitions.  
  
## Analysis Services Data Types  
 The data types that are used in bindings must match the data types supported by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The following data types are defined in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:  
  
|Analysis Services Data Type|Description|  
|---------------------------------|-----------------|  
|BigInt|A 64-bit signed integer. This data type maps to the Int64 data type inside Microsoft [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_I8 data type inside OLE DB.|  
|Bool|A Boolean value. This data type maps to the Boolean data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_BOOL data type inside OLE DB.|  
|Currency|A currency value ranging from -263 (or -922,337,203,685,477.5808) to 263-1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit. This data type maps to the Decimal data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_CY data type inside OLE DB.|  
|Date|Date data, stored as a double-precision floating-point number. The whole portion is the number of days since December 30, 1899, while the fractional portion is a fraction of a day. This data type maps to the DateTime data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_DATE data type inside OLE DB.|  
|Double|A double-precision floating-point number within the range of -1.79E +308 through 1.79E +308. This data type maps to the Double data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_R8 data type inside OLE DB.|  
|Integer|A 32-bit signed integer. This data type maps to the Int32 data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_I4 data type inside OLE DB.|  
|Single|A single-precision floating-point number within the range of -3.40E +38 through 3.40E +38. This data type maps to the Single data type inside [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_R4 data type inside OLE DB.|  
|SmallInt|A 16-bit signed integer. This data type maps to the Int16 data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_I2 data type inside OLE DB.|  
|TinyInt|An 8-bit signed integer. This data type maps to the SByte data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_I1 data type inside OLE DB.<br /><br /> Note: If a data source contains fields that are of the tinyint datatype and the AutoIncrement property is set to True, then they will be converted to integers in the data source view.|  
|UnsignedBigInt|A 64-bit unsigned integer. This data type maps to the UInt64 data type inside [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_UI8 data type inside OLE DB.|  
|UnsignedInt|A 32-bit unsigned integer. This data type maps to the UInt32 data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_UI4 data type inside OLE DB.|  
|UnsignedSmallInt|A 16-bit unsigned integer. This data type maps to the UInt16 data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_UI2 data type inside OLE DB.|  
|WChar|A null-terminated stream of Unicode characters. This data type maps to the String data type inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and the DBTYPE_WSTR data type inside OLE DB.|  
  
 All data that is received from the data source is converted to the [!INCLUDE[ssAS](../../includes/ssas-md.md)] type specified in the binding (usually during processing). An error is raised if the conversion cannot be performed (for example, String to Int). [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] usually sets the data type in the binding to the one that best matches the source type in the data source. For example, the SQL types Date, DateTime, SmallDateTime, DateTime2, DateTimeOffset are mapped to [!INCLUDE[ssAS](../../includes/ssas-md.md)] Date, and the SQL type Time is mapped to String.  
  
## Bindings for Dimensions  
 Each attribute of a dimension is bound to a column in a DSV. All the attributes of a dimension must come from a single data source. However, the attributes can be bound to columns in different tables. The relationships between the tables are defined in the DSV. In the case where more than one set of relationships exists to the same table, it might be necessary to introduce a named query in the DSV to act as an 'alias' table. Expressions and filters are defined in the DSV by using named calculations and named queries.  
  
## Bindings for MeasureGroups, Measures, and Partitions  
 Each measure group has the following default bindings:  
  
-   The measure group is bound to a table in a DSV (for example, `MeasureGroup.Source`).  
  
-   Each measure is bound to a column in that table (for example, `Measure.ValueColumn.Source`).  
  
-   Each measure group dimension has a set of *granularity attributes* that define the granularity of the measure group. Each of these attributes must be bound to the column or columns in the fact table that contain the attribute key. (For more information about granularity attributes, see MeasureGroup Granularity Attributes later in this topic.)  
  
 These default bindings can be selectively overridden per partition. Each partition can specify a different data source, table or query name, or filter expression. The most common partitioning strategy is to override the table per partition, by using the same data source. Alternatives include applying a different filter per partition or changing the data source.  
  
 The default data source must be defined in the DSV, thereby providing the schema information, including the details of relationships. Any additional tables or queries specified at the partition level do not need to be listed in the DSV, but they must have the same schema as the default table defined for the measure group, or at least they must contain all the columns used by the measures or granularity attributes. The detailed bindings per measure and granularity attribute cannot be overridden at the partition level, and they are assumed to be to the same columns as defined for the measure group. Therefore, if the partition uses a data source that does in fact have a different schema, the `TableDefinition` query defined for the partition must result in the same schema as the schema used by the measure group.  
  
### MeasureGroup Granularity Attributes  
 When the granularity of a measure group matches the granularity known in the database, and there is a direct relationship from the fact table to the dimension table, the granularity attribute only needs to be bound to the appropriate foreign key column or columns on the fact table. For example, consider the following fact and dimension tables:  
  
 `Sales(RequestedDate, OrderedProductID, ReplacementProductID, Qty)`  
  
 `Product(ProductID, ProductName,Category)`  
  
 ``  
  
 `Relation: Sales.OrderedProductID -> Product.ProductID`  
  
 `Relation: Sales.ReplacementProductID -> Product.ProductID`  
  
 ``  
  
 If you analyze by the ordered product, for the Ordered Product on Sales dimension role, the Product granularity attribute would be bound to Sales.OrderedProductID.  
  
 However, there may be times when the `GranularityAttributes` might not exist as columns on the fact table. For example, the `GranularityAttributes` might not exist as columns in the following circumstances:  
  
-   The OLAP granularity is coarser than the granularity in the source.  
  
-   An intermediate table interposes between the fact table and the dimension table.  
  
-   The dimension key is not the same as the primary key in the dimension table.  
  
 In all such cases, the DSV must be defined so that the GranularityAttributes exist on the fact table. For example, a named query or calculated column can be introduced.  
  
 For example, in the same example tables as above, if the granularity were by Category, then a view of the Sales could be introduced:  
  
 `SalesWithCategory(RequestedDate, OrderedProductID, ReplacementProductID, Qty, OrderedProductCategory)`  
  
 `SELECT Sales.*, Product.Category AS OrderedProductCategory`  
  
 `FROM Sales INNER JOIN Product`  
  
 `ON Sales.OrderedProductID = Product.ProductID`  
  
 ``  
  
 In this case, the GranularityAttribute Category is bound to SalesWithCategory.OrderedProductCategory.  
  
### Migrating from Decision Support Objects  
 Decision Support Objects (DSO) 8.0 allows `PartitionMeasures` to be rebound. Therefore, the migration strategy in these cases is to construct the appropriate query.  
  
 Similarly, it is not possible to rebind the dimension attributes within a partition, although DSO 8.0 allows this rebinding also. The migration strategy in these cases is to define the necessary named queries in the DSV so that the same tables and columns exist in the DSV for the partition as the tables and columns that are used for the dimension. These cases may require the adoption of the simple migration, in which the From/Join/Filter clause is mapped to a single named query rather than to a structured set of related tables. As DSO 8.0 allows PartitionDimensions to be rebound even when the partition is using the same data source, migration may also require multiple DSVs for the same data source.  
  
 In DSO 8.0, the corresponding bindings can be expressed in two different ways, depending on whether optimized schemas are employed, by binding to either the primary key on the dimension table or the foreign key on the fact table. In ASSL, the two different forms are not distinguished.  
  
 The same approach to bindings applies even for a partition using a data source that does not contain the dimension tables, because the binding is made to the foreign key column in the fact table, not to the primary key column in the dimension table.  
  
## Bindings for Mining Models  
 A mining model is either relational or OLAP. The data bindings for a relational mining model are considerably different than the bindings for an OLAP mining model.  
  
### Bindings for a Relational Mining Model  
 A relational mining model relies on the relationships defined in the DSV to resolve any ambiguity regarding which columns are bound to which data sources. In a relational mining model, the data bindings follow these rules:  
  
-   Each non-nested table column is bound to a column either on the case table or a table related to the case table (following a many-to-one or one-to-one relationship). The DSV defines the relationships between the tables.  
  
-   Each nested-table column is bound to a source table. The columns owned by the nested-table column are then bound to columns on that source table or a table related to the source table. (Again, the binding follows a many-to-one or one-to-one relationship.) The mining model bindings do not provide the join path to the nested table. Instead, the relationships defined in the DSV provide this information.  
  
### Bindings for an OLAP Mining Model  
 OLAP mining models do not have the equivalent of a DSV. Therefore, the data bindings must provide any disambiguation between columns and data sources. For example, a mining model can be based on the Sales cube, and columns can be based on Qty, Amount, and Product Name. Alternatively, a mining model can be based on Product, and columns can be based on Product Name, Product Color, and a nested table with Sales Qty.  
  
 In an OLAP mining model, the data bindings follow these rules:  
  
-   Each non-nested table column is bound to a measure on a cube, to an attribute on a dimension of that cube (specifying the `CubeDimension` to disambiguate in the case of dimension roles), or to an attribute on a dimension.  
  
-   Each nested table column is bound to a `CubeDimension`. That is, it defines how to navigate from a dimension to a related cube or (in the less common case of nested tables) from a cube to one of its dimensions.  
  
## Out-of-Line Bindings  
 Out-of-Line bindings enable you to temporarily change the existing data bindings for the duration of a command. Out-of-line bindings refer to bindings that are included in a command and are not persisted. Out-of-line bindings apply only while that particular command executes. In contrast, inline bindings are contained in the ASSL object definition, and persist with the object definition within server metadata.  
  
 ASSL allows out-of-line bindings to be specified on either a `Process` command, if it is not in a batch, or on a `Batch` command. If out-of-line bindings are specified on the `Batch` command, all bindings specified in the `Batch` command create a new binding context in which all `Process` commands of the batch run. This new binding context includes objects that are indirectly processed because of the `Process` command.  
  
 When out-of-line bindings are specified on a command, they override the inline bindings contained in the persisted DDL for the specified objects. These processed objects may include the object directly named in the `Process` command, or they may include other objects whose processing is automatically initiated as a part of the processing.  
  
 Out-of-line bindings are specified by including the optional `Bindings` collection object with the processing command. The optional `Bindings` collection contains the following elements.  
  
|Property|Cardinality|Type|Description|  
|--------------|-----------------|----------|-----------------|  
|`Binding`|0-n|`Binding`|Provides a collection of new bindings.|  
|`DataSource`|0-1|`DataSource`|Replaces `DataSource` from server that would have been used.|  
|`DataSourceView`|0-1|`DataSourceView`|Replaces the `DataSourceView` from the<br /><br /> server that would have been used.|  
  
 All elements that relate to out-of-line bindings are optional. For any elements not specified, ASSL uses the specification contained in the DDL of the persisted object. Specification of `DataSource` or `DataSourceView` in the `Process` command is optional. If `DataSource` or `DataSourceView` are specified, they are not instantiated and do not persist after the `Process` command has completed.  
  
### Definition of the Out-of-line Binding Type  
 Inside the out-of-line `Bindings` collection, ASSL allows a collection of bindings for multiple objects, each a `Binding`. Each `Binding` has an extended object reference, which is similar to the object reference, but it can refer to minor objects as well (for example, dimension attributes and measure group attributes). This object takes the flat form typical of the `Object` element in `Process` commands, except that the \<*Object*>\<*/Object*> tags are not present.  
  
 Each object for which the binding is specified is identified by an XML element of the form \<*object*>ID (for example, `DimensionID`). After you have identified the object as specifically as possible with the form \<*object*>ID, then you identify the element for which the binding is being specified, which is usually `Source`. A common case to note is that in which `Source` is a property on the `DataItem`, which is the case for column bindings in an attribute. In this case, you do not specify the `DataItem` tag; instead, you simply specify the `Source` property, as if it were directly on the column to be bound.  
  
 `KeyColumns` are identified by their ordering inside the `KeyColumns` collection. There it is not possible to specify, for example, only the first and third key columns of an attribute, because there is no way to indicate that the second key column is to be skipped. All of the key columns must be present in the out-of-line binding for a dimension attribute.  
  
 `Translations`, although they have no ID, are semantically identified by their language. Therefore, the `Translations` inside a `Binding` need to include their language identifier.  
  
 One additional element allowed within a `Binding` that does not exist directly in the DDL is `ParentColumnID`, which is used for nested tables for data mining. In this case, it is necessary to identify the parent column in the nested table for which the binding is being provided.  
  
  
