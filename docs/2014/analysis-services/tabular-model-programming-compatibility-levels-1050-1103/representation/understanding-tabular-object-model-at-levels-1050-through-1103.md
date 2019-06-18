---
title: "Understanding the Tabular Object Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
ms.assetid: 6077b7e8-cb3e-4480-a5de-bb602cf9d69a
author: minewiskan
ms.author: owend
manager: craigg
---
# Understanding the Tabular Object Model
  A tabular model is a logical representation of tables, relationships, hierarchies, perspectives, measures, and Key Performance. This section introduces the internal implementation using AMO. See [Developing with Analysis Management Objects &#40;AMO&#41;](https://docs.microsoft.com/bi-reference/amo/developing-with-analysis-management-objects-amo) if you haven't used AMO before.  
  
 The approach here is top-down, all relevant objects in the tabular model are logically mapped to AMO objects, and the required interaction or workflow explained. A source code sample to create a tabular model using AMO,  AMO to Tabular, is available from Codeplex. An important note about the code in the sample: it is provided only to support to the logical concepts explained here and should not be used in a production environment. The sample is provided without support or warranty.  
  
## Database Representation  
 A database provides the container object for the tabular model. All objects in a tabular model are contained in the database. In terms of AMO objects, a database representation has a one-to-one mapping relationship with <xref:Microsoft.AnalysisServices.Database>, and no other main AMO objects are required. It is important to note that this doesn't mean that all contained objects in the AMO database object can be used when modeling.  
  
 See [Database Representation&#40;Tabular&#41;](database-representation-tabular.md) for a detailed explanation on how to create and manipulate the database representation.  
  
## Connection Representation  
 A connection establishes the relationship between the data to be included in a tabular model solution and the model itself. In terms of AMO objects, a connection has a one-to-one mapping relationship with <xref:Microsoft.AnalysisServices.DataSource>, and no other main AMO objects are required. It is important to note that this doesn't mean that all contained objects in the AMO datasource object can be used when modeling.  
  
 See [Connection Representation &#40;Tabular&#41;](connection-representation-tabular.md) for a detailed explanation on how to create and manipulate the data source representation.  
  
## Table Representation  
 Tables are database objects that contain the data in the database. In terms of AMO objects, a table has a one-to-many mapping relationship. A table is represented by the usage of the following AMO objects: <xref:Microsoft.AnalysisServices.DataSourceView>, <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.CubeDimension>, <xref:Microsoft.AnalysisServices.MeasureGroup> and <xref:Microsoft.AnalysisServices.Partition> are the main required objects; however, it is important to note that this doesn't mean that all contained objects in the previously mentioned AMO objects can be used when modeling.  
  
 See [Tables Representation &#40;Tabular&#41;](tables-representation-tabular.md) for a detailed explanation on how to create and manipulate the table representation.  
  
### Calculated Column Representation  
 Calculated columns are evaluated expressions that generate a column in a table, where a new value is calculated and stored for each row in the table. In terms of AMO objects a calculated column has a one-to-many mapping relationship. A calculated column is represented by the usage of the following AMO objects: <xref:Microsoft.AnalysisServices.Dimension> and <xref:Microsoft.AnalysisServices.MeasureGroup> are the main required objects. It is important to note that this doesn't mean that all contained objects in the previously mentioned AMO objects can be used when modeling.  
  
 See [Calculated Column Representation &#40;Tabular&#41;](tables-calculated-column-representation.md) for a detailed explanation on how to create and manipulate the calculated column representation.  
  
### Calculated Measure Representation  
 Calculated Measures are stored expressions that are evaluated upon request once the model is deployed. In terms of AMO objects, a calculated measure has a one-to-many mapping relationship. A calculated column is represented by the usage of the following AMO objects: <xref:Microsoft.AnalysisServices.MdxScript.Commands%2A> and <xref:Microsoft.AnalysisServices.MdxScript.CalculationProperties%2A> are the main required objects. It is important to note that this doesn't mean that all contained objects in the previously mentioned AMO objects can be used when modeling.  
  
> [!NOTE]  
>  The <xref:Microsoft.AnalysisServices.Measure> objects have no relationship with the calculated measures in tabular models, and are not supported in tabular models.  
  
 See [Calculated Measure Representation &#40;Tabular&#41;](tables-calculated-measure-representation.md) for a detailed explanation on how to create and manipulate the calculated measure representation.  
  
### Hierarchy Representation  
 Hierarchies are a mechanism to provide a richer drill-up and drill-down experience to the end user. In terms of AMO objects, a hierarchy representation has a one-to-one mapping relationship with <xref:Microsoft.AnalysisServices.Hierarchy>, and no other main AMO objects are required. It is important to note that this doesn't mean that all contained objects in the AMO database object can be used when doing tabular modeling.  
  
 See [Hierarchy Representation &#40;Tabular&#41;](tables-hierarchy-representation.md) for a detailed explanation on how to create and manipulate the hierarchy representation.  
  
### Key Performance Indicator -KPI- Representation  
 A KPI is used to gauge performance of a value, defined by a Base measure, against a Target value. In terms of AMO objects, a KPI representation has a one-to-many mapping relationship. A KPI is represented by the usage of the following AMO objects: <xref:Microsoft.AnalysisServices.MdxScript.Commands%2A>and <xref:Microsoft.AnalysisServices.MdxScript.CalculationProperties%2A> are the main required objects.  It is important to note that this doesn't mean that all contained objects in the previously mentioned AMO objects can be used when modeling.  
  
> [!NOTE]  
>  Also, important distinction, the <xref:Microsoft.AnalysisServices.Kpi> objects have no relationship with the KPIs in tabular models. And, they are not supported in tabular models.  
  
 See [Key Performance Indicator Representation &#40;Tabular&#41;](tables-key-performance-indicator-representation.md) for a detailed explanation on how to create and manipulate the KPI representation.  
  
### Partition Representation  
 For operational purposes, a table can be divided in different subsets of rows that when combined together form the table. Each of those subsets is a partition of the table. In terms of AMO objects, a partition representation has a one-to-one mapping relationship with <xref:Microsoft.AnalysisServices.Partition> and no other main AMO objects are required. It is important to note that this doesn't mean that all contained objects in the AMO database object can be used when modeling.  
  
 See [Partition Representation &#40;Tabular&#41;](tables-partition-representation.md) for a detailed explanation on how to create and manipulate the partition representation.  
  
## Relationship Representation  
 A relationship is a connection between two tables of data. The relationship establishes how the data in the two tables should be correlated.  
  
 In tabular models, multiple relationships can be defined between two tables. When multiple relationships between two tables are defined, only one can be defined as the default Active relationship. All other relationships are Inactive.  
  
 In terms of AMO objects, all inactive relationships have a representation of a one-to-one mapping relationship with <xref:Microsoft.AnalysisServices.Relationship>, and no other main AMO objects are required. For the active relationship, other requirements exist and a mapping to the <xref:Microsoft.AnalysisServices.ReferenceMeasureGroupDimension> is also required. It is important to note that this doesn't mean that all contained objects in the AMO relationship or referenceMeasureGroupDimension object can be used when modeling.  
  
 See [Relationship Representation &#40;Tabular&#41;](relationship-representation-tabular.md) for a detailed explanation on how to create and manipulate the relationship representation.  
  
## Perspective Representation  
 A perspective is a mechanism to simplify or focus the mode. In terms of AMO objects, a relationship representation has a one-to-one mapping relationship with <xref:Microsoft.AnalysisServices.Perspective> and no other main AMO objects are required. It is important to note that this doesn't mean that all contained objects in the AMO perspective ob0ject can be used when doing tabular modeling.  
  
 See [Perspective Representation &#40;Tabular&#41;](perspective-representation-tabular.md) for a detailed explanation on how to create and manipulate the perspective representation.  
  
> [!WARNING]  
>  Perspectives are not a security mechanism; objects outside the perspective can still be accessed by the user through other interfaces.  
  
  
