---
title: "CSDLBI Concepts | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 2fbdf621-a94d-4a55-a088-3d56d65016ac
author: minewiskan
ms.author: owend
manager: craigg
---
# CSDLBI Concepts
  Conceptual Schema Definition Language with BI annotations (CSDLBI) is based on the Entity Data Framework, which is an abstraction for representing data in a way that enables disparate data sets to be programmatically accessed, queried, or exported. CSDLBI is used to represent data models created using [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] because it supports rich, data-driven reporting and applications.  
  
 This section explains how the CSDLBI representation maps to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data models (both tabular and multidimensional), along with examples of each model type.  
  
 Examples used to illustrate these concepts are taken from the AdventureWorks sample database, available on Codeplex. For more information about the samples, see [Adventure Works Samples for SQL Server](https://go.microsoft.com/fwlink/?linkID=220093).  
  
## Structure of a Tabular Model in CSDLBI  
 A CSDLBI document that describes a report model and its data begins with the xsd statement, followed by the definition of a model.  
  
 The model is a namespace, which contains the following major entities, associations, and properties:  
  
-   The `EntityContainer` lists the tables in the model.  
  
-   Each table is listed with the `EntityContainer` as an `EntitySet`.  
  
-   Each relationship between two tables is described as an `AssociationSet` that defines the relationship end points and the relationship roles.  
  
-   The `EntityType` element is extended for BISM to provide additional detail about the tables and the columns in them, including properties for sorting and display purposes.  
  
-   The `Measure` element defines calculations that can be used in the model. A measure can be turned into a KPI by adding a set of special display attributes, using the new `KPI` Element.  
  
-   There is no separate representation of perspectives. Columns and tables that are not included in a perspective are present in the CSDL but flagged with the `Hidden` attribute.  
  
### Entities, EntitySets, and EntityTypes  
 The notion of an entity in the Entity Data Framework is extended to represent columns and tables from the data model. The following excerpt shows the list of `EntitySet` elements in a simple model containing only three tables.  
  
```  
<EntityContainer Name="SimpleModel">  
<EntitySet Name="DimCustomer"EntityType="SimpleModel.DimCustomer">  
     <bi:EntitySet />  
   </EntitySet>  
<EntitySet Name="DimDate" EntityType="SimpleModel.DimDate">  
     <bi:EntitySet />  
   </EntitySet>  
<EntitySet Name="DimGeography" EntityType="SimpleModel.DimGeography">  
     <bi:EntitySet />  
   </EntitySet> />  
  
```  
  
 The `EntitySet` does not contain information about columns or data in the table. The detailed description of the columns and their properties is provided in the EntityType element.  
  
 The `EntitySet` element for each entity (table) includes a collection of properties that define the key column, the data type and length of the column, nullability, sorting behavior, and so forth. For example, the following CSDL excerpt describes three columns in the Customer table. The first column is a special hidden column used internally by the model.  
  
```  
<EntityType Name="Customer">  
  <Key>  
     <PropertyRef Name="RowNumber" />  
  </Key>  
    <Property Name="RowNumber" Type="Int64" Nullable="false">  
     <bi:Property Hidden="true" Contents="RowNumber"  
       Stability="RowNumber" />  
    </Property>  
    <Property Name="CustomerKey" Type="Int64" Nullable="false">  
      <bi:Property />  
    </Property>  
     <Property Name="FirstName" Type="String" MaxLength="Max" FixedLength="false">  
       <bi:Property />  
      </Property>  
  
```  
  
 To limit the size of the CSDLBI document that is generated, properties that appear more than once in an entity are specified by a reference to an existing property, so that the property need be listed only once for the `EntityType`. The client application can get the value of the property by finding the `EntityType` that matches the `OriginEntityType`.  
  
### Relationships  
 In the Entity Data Framework, relationships are defined as *associations* between entities.  
  
 Associations always have exactly two ends, each pointing to a field or column in a table. Therefore, multiple relationships are possible between two tables, if the relationships have different end points. A role name is assigned to the end points of the association, and indicates how the association is used in the context of the data model. An example of a role name might be **ShipTo**, when applied to a customer ID that is related to the customer ID in an Orders table.  
  
 The CSDLBI representation of the model also contains attributes on the association that determine how the entities are mapped to each other in terms of the *multiplicity* of the association. Multiplicity indicates whether the attribute or column at the end point of a relationship between tables is on the one side of a relationship, or on the many side. There is no separate value for one-to-one relationships. CSDLBI annotations support multiplicity of 0 (meaning the entity is not associated with anything) or 0..1, which means either a one-one relationship or a one-to-many relationship.  
  
 The following sample represents the CSDLBI output for a relationship between the tables, Date and ProductInventory, where the two tables are joined on the column DateAlternateKey. Notice that, by default, the name of the `AssociationSet` is the fully qualified name of the columns that are involved in the relationship. However, you can change this behavior when you design the model, to use a different naming format.  
  
```  
<AssociationSet Name="ProductInventory_Date_DateKey" Association="Model.ProductInventory_Date_DateKey">  
              <End EntitySet="ProductInventory" />  
              <End EntitySet="Date" />  
              <bi:AssociationSet />  
            </AssociationSet>  
  
```  
  
### Visualization and Navigation Properties  
 An important part of the CSDLBI annotations are the properties for defining presentation in the reporting layer, and for navigating the relationships among entities. Typically, when you are creating a data model, you do not consider it important to control how the data is ordered or grouped, or what the default value might be, on the assumption that the client application will specify ordering and other details of presentation. However, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tabular models are designed for integration with the [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] reporting client, and includes properties and attributes that support presentation of entities from the data model in the report design surface.  
  
 Extensions for visualization include attributes for specifying the default aggregation to use with numerical data, for indicating that a text field points to a URL of an image, or specifying the field used to sort the current field.  
  
### Name Properties and Naming Conventions  
 The CSDLBI schema provides that each entity has a unique name and an identifier that can be used as a key. In addition, some entities can have captions used for display purposes, and contextual names that change depending on where the entity is used.  
  
 The `Documentation` element provides the opportunity for report designers to furnish a description of the entity, to help business users understand the meaning of the data. Some entities also allow one or more `Annotation` attributes, which provide extra metadata for consumption by the application or by clients.  
  
 When you generate a model the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tools, the names that are created for objects follow the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] conventions for object naming and name uniqueness. However, because CSDLBI is based on the Entity Data Framework (EDF), which requires that names adhere to conventions for C# identifiers, when the server creates the CSDLBI output for a model, the server takes the names used within the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] schema and automatically creates new object names that comply with EDF requirements. The following table describes the operations by which the new names are generated.  
  
|Rule|Action|  
|----------|------------|  
|No forbidden characters|Forbidden characters are replaced by underscore.|  
|Names must be unique|If two strings are the same, one is made unique by appending underscore plus a number|  
  
> [!WARNING]  
>  Captions and qualifiers both have translations, and for a given language, one or the other might be present. This means that in cases where a qualifier and name or qualifier and caption are concatenated, the strings could be in two different languages.  
  
## Additions to Support Multidimensional Models  
 Version 1.0 of the CSDLBI annotations supported only tabular models. In version 1.1, support was added for multidimensional models (OLAP cubes) created using traditional BI development tools. Therefore, you can now issue an XML request to a multidimensional model and receive a CSDLBI definition of the model, for use in reporting.  
  
 **Cubes:** A SQL Server [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tabular database can contain only one mode. In contrast, each multidimensional database can contain multiple cubes, each database being associated with a default cube. Therefore, when issuing an XML request against a multidimensional server, it is necessary to specify the cube; otherwise, the XML for the default cube will be returned.  
  
 The representation of a cube is otherwise very much like that of a tabular model database. The cube name and cube correspond to the tabular database name and database identifier.  
  
 **Dimensions:** A dimension is represented in CSDLBI as an entity (table) with columns and properties. Note that even if not included in a perspective, a dimension that is included in the model will be represented in the CSDL output, marked as `Hidden`.  
  
 **Perspectives:** A client can request CSDL for individual perspectives. For more information, see [DISCOVER_CSDL_METADATA Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/xml/discover-csdl-metadata-rowset).  
  
 **Hierarchies:** Hierarchies are supported and represented in CSDLBI as a set of levels.  
  
 **Members:** Support for the default member has been added and default values are automatically added to the CSDLBI output.  
  
 **Calculated Members:** Multidimensional models support calculated members for child of **All** with a single real member.  
  
 **Dimension attributes:** In CSDLBI output, dimension attributes are supported and automatically marked as non-aggregatable.  
  
 **KPIs:** KPIs were supported in CSDLBI version 1.1, but the representation has changed. Before, a KPI was a property of a measure. In version 1.1, the KPI element can be added to a measure  
  
 **New properties:** Additional attributes were added to support DirectQuery models.  
  
 **Limitations:** Cell security is not supported.  
  
## See Also  
 [CSDL Annotations for Business Intelligence &#40;CSDLBI&#41;](https://docs.microsoft.com/bi-reference/csdl/csdl-annotations-for-business-intelligence-csdlbi)  
  
  
