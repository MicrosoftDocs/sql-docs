---
title: "Use the Schema Generation Wizard (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Schema Generation Wizard, steps"
  - "relational schema [Analysis Services], Schema Generation Wizard"
ms.assetid: 8c710745-d41d-4c31-b6a2-2956229df75a
author: minewiskan
ms.author: owend
manager: craigg
---
# Use the Schema Generation Wizard (Analysis Services)
  The Schema Generation Wizard requires a limited amount of information during the generation phase. Most of the information that the Schema Generation Wizard requires for generating relational schemas is extracted from the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cubes and dimensions that you already created in the project. Additionally, you can customize how the subject area database schema is generated and how objects in the schema are named.  
  
## Start the Wizard  
 You can open the Schema Generation Wizard from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] in several different ways:  
  
-   Right-click the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project object and then click **Generate Relational Schema** from the context menu.  
  
-   Click the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project object, and then click **Generate Relational Schema** from the **Database** menu.  
  
-   Start the wizard from within the Dimension Wizard by clicking the **Generate Schema Now** check box on the last page of the wizard.  
  
## Step 1: Specify Targets  
 You must specify the data source view (DSV) in which you want the Schema Generation Wizard to generate the schema for the subject area database. Although you can select an existing DSV, typically you create a new one based on a data source. You can create the data source based on an existing or a new connection, or based on another object. The Schema Generation Wizard generates the schema for the subject area database in the database referenced by the data source, as well as the data source view. The Schema Generation Wizard does not create the subject area database itself; instead, the wizard creates the relational schema to support the cubes and dimensions in an existing database that you specify.  
  
 When the Schema Generation Wizard generates the underlying objects, it binds the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] dimensions and cubes to the generated tables and columns by using data source view-style bindings.  
  
> [!NOTE]  
>  To unbind [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] dimensions and cubes from previously generated objects, delete the data source view to which the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cubes and dimensions are bound, and then define a new data source view for the cubes and dimensions by using the Schema Generation Wizard.  
  
## Step 3: Specify Schema Options for the Subject Area Database  
 The Schema Generation Wizard provides a number of options for defining the schema that is generated for the subject area database. You can specify these options on the **Subject Area Database Schema Options** page of the wizard.  
  
### Specifying the Schema Owner  
 You can specify the owner of the schema by setting the value of **Owning schema** to a valid string. The default owner of the schema is the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, but you can specify any desired schema owner.  
  
### Specifying Primary Keys, Indexes, and Constraints  
 The Schema Generation Wizard by default creates a primary key constraint in each dimension table in the subject area database. The primary key corresponds to the attribute that is designated as the key attribute in the corresponding [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] dimension. This constraint improves processing performance in most environments, with minimal cost. Logical primary keys are always created in the data source view, even if you choose not to create the primary key in the subject area database. To define primary key constraints on dimension tables, select **Create primary keys on dimension tables**.  
  
 The wizard by default also creates indexes on the foreign key columns in each fact table. These indexes improve processing performance in most environments. Performance is typically improved because the processing queries that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] generates to retrieve new data from the subject area database typically include a significant number of join statements between the fact table and the dimension tables. To define indexes on the foreign key columns in each fact table, select **Create indexes**.  
  
 Finally, the wizard by default enforces referential integrity between the fact table and each of the dimension tables. If you choose not to enforce referential integrity, the Schema Generation Wizard still creates these relationships in the database and the data source view. To enforce referential integrity, select **Enforce referential integrity**.  
  
### Preserving Data for Incremental Generation  
 The Schema Generation Wizard by default attempts to preserve data when the database schema is regenerated. If the Schema Generation Wizard has to delete any rows because of a schema change, you receive a warning before the rows are deleted. For example, rows may have to be deleted to solve referential integrity issues because you dropped a dimension or because a data type changed when you changed a dimension attribute. To preserve data when the database schema is regenerated, select **Preserve data on regeneration**.  
  
## Step 4: Specify Naming Conventions  
 You can define the naming conventions that the Schema Generation Wizard uses when generating certain objects in the subject area database on the **Specify Naming Conventions** page of the wizard. For more information about the options available on the **Specify Naming Conventions** page, see [Specify Naming Conventions &#40;Schema Generation Wizard&#41; &#40;Analysis Services - Multidimensional Data&#41;](../specify-naming-conventions-schema-generation-analysis-services-multidimensional-data.md).  
  
  
