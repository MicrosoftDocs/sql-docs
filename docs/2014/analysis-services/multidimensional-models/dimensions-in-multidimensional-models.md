---
title: "Dimensions in Multidimensional Models | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "OLAP [Analysis Services], dimensions"
  - "dimensions [Analysis Services], about dimensions"
  - "OLAP objects [Analysis Services], dimensions"
ms.assetid: 2b62b05c-00fd-4e60-b77f-f707ba83a19b
author: minewiskan
ms.author: owend
manager: craigg
---
# Dimensions in Multidimensional Models
  A database dimension is a collection of related objects, called attributes, which can be used to provide information about fact data in one or more cubes. For example, typical attributes in a product dimension might be product name, product category, product line, product size, and product price. These objects are bound to one or more columns in one or more tables in a data source view. By default, these attributes are visible as attribute hierarchies and can be used to understand the fact data in a cube. Attributes can be organized into user-defined hierarchies that provide navigational paths to assist users when browsing the data in a cube.  
  
 Cubes contain all the dimensions on which users base their analyses of fact data. An instance of a database dimension in a cube is called a cube dimension and relates to one or more measure groups in the cube. A database dimension can be used multiple times in a cube. For example, a fact table can have multiple time-related facts, and a separate cube dimension can be defined to assist in analyzing each time-related fact. However, only one time-related database dimension needs to exist, which also means that only one time-related relational database table needs to exist to support multiple cube dimensions based on time.  
  
> [!NOTE]  
>  For performance issues related to dimension design, see the [SQL Server 2008 R2 Analysis Services Performance Guide](https://go.microsoft.com/fwlink/?LinkId=306717).  
  
## Defining Dimensions, Attributes, and Hierarchies  
 The simplest method for defining database and cube dimensions, attributes, and hierarchies is to use the Cube Wizard to create dimensions at the same time that you define the cube. The Cube Wizard will create dimensions based on the dimension tables in the data source view that the wizard identifies or that you specify for use in the cube. The wizard then creates the database dimensions and adds them to the new cube, creating cube dimensions.  
  
 When you create a cube, you can also add to the new cube any dimensions that already exist in the database. These dimensions may have been previously defined for another cube or by the Dimension Wizard. After a database dimension has been defined, you can modify and configure the database dimension in Dimension Designer. You can also customize the cube dimension, to a limited extent, in Cube Designer.  
  
> [!NOTE]  
>  You can also design and configure dimensions, attributes, and hierarchies programmatically by using either XMLA or Analysis Management Objects (AMO). For more information, see [Analysis Services Scripting Language &#40;ASSL&#41; Reference](https://docs.microsoft.com/bi-reference/assl/analysis-services-scripting-language-assl-for-xmla) and [Developing with Analysis Management Objects &#40;AMO&#41;](https://docs.microsoft.com/bi-reference/amo/developing-with-analysis-management-objects-amo).  
  
## In This Section  
 The following table describes the topics in this section.  
  
 [Define Database Dimensions](define-database-dimensions.md)  
 Describes how to modify and configure a database dimension by using Dimension Designer.  
  
 [Dimension Attribute Properties Reference](dimension-attribute-properties-reference.md)  
 Describes how to define, modify, and configure a database dimension attribute by using Dimension Designer.  
  
 [Define Attribute Relationships](attribute-relationships-define.md)  
 Describes how to define, modify, and configure an attribute relationship by using Dimension Designer.  
  
 [Create User-Defined Hierarchies](user-defined-hierarchies-create.md)  
 Describes how to define, modify, and configure a user-defined hierarchy of dimension attributes by using Dimension Designer.  
  
 [Use the Business Intelligence Wizard to Enhance Dimensions](../use-the-business-intelligence-wizard-to-enhance-dimensions.md)  
 Describes how to enhance a database dimension by using the Business Intelligence Wizard.  
  
## See Also  
 [Cubes in Multidimensional Models](cubes-in-multidimensional-models.md)  
  
  
