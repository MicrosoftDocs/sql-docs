---
title: "Relationships in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Relationships 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  In tabular models, a relationship is a connection between two tables of data. The relationship establishes how the data in the two tables should be correlated. For example, a Customers table and an Orders table can be related in order to show the customer name that is associated with each order.  
  
 When using the Table Import Wizard to import from the same data source, relationships that already exist in tables (at the data source) that you choose to import will be re-created in the model. You can view relationships that were detected and re-created automatically by using the model designer in Diagram View or by using the Manage Relationships dialog box. You can also create new relationships between tables manually by using the model designer in Diagram View or by using the Create Relationship or Manage Relationships dialog box.  
  
 After relationships between tables have been defined, either automatically during import or created manually, you will be able to filter data by using related columns and look up values in related tables.  
  
> [!TIP]  
>  If your model contains many relationships, Diagram View can better help you better visualize and create new relationships between tables.  
  
  
##  <a name="what"></a> Benefits  
 A relationship is a connection between two tables of data, based on one or more columns in each table. To see why relationships are useful, imagine that you track data for customer orders in your business. You could track all the data in a single table that has a structure like the following:  
  
|CustomerID|Name|EMail|DiscountRate|OrderID|OrderDate|Product|Quantity|  
|----------------|----------|-----------|------------------|-------------|---------------|-------------|--------------|  
|1|Ashton|chris.ashton@contoso.com|.05|256|2010-01-07|Compact Digital|11|  
|1|Ashton|chris.ashton@contoso.com|.05|255|2010-01-03|SLR Camera|15|  
|2|Jaworski|michal.jaworski@contoso.com|.10|254|2010-01-03|Budget Movie-Maker|27|  
  
 This approach can work, but it involves storing a lot of redundant data, such as the customer e-mail address for every order. Storage is cheap, but you have to make sure you update every row for that customer if the e-mail address changes. One solution to this problem is to split the data into multiple tables and define relationships between those tables. This is the approach used in *relational databases* like SQL Server. For example, a database that you import into a model might represent order data by using three related tables:  
  
### Customers  
  
|[CustomerID]|Name|Email|  
|--------------------|----------|-----------|  
|1|Ashton|chris.ashton@contoso.com|  
|2|Jaworski|michal.jaworski@contoso.com|  
  
### CustomerDiscounts  
  
|[CustomerID]|DiscountRate|  
|--------------------|------------------|  
|1|.05|  
|2|.10|  
  
### Orders  
  
|[CustomerID]|OrderID|OrderDate|Product|Quantity|  
|--------------------|-------------|---------------|-------------|--------------|  
|1|256|2010-01-07|Compact Digital|11|  
|1|255|2010-01-03|SLR Camera|15|  
|2|254|2010-01-03|Budget Movie-Maker|27|  
  
 If you import these tables from the same database, the Table Import Wizard can detect the relationships between the tables based on the columns that are in [brackets], and can reproduce these relationships in the model designer. For more information, see [Automatic Detection and Inference of Relationships](#detection) in this topic. If you import tables from multiple sources, you can manually create relationships as described in [Create a Relationship Between Two Tables](../../analysis-services/tabular-models/create-a-relationship-between-two-tables-ssas-tabular.md).  
  
### Columns and keys  
 Relationships are based on columns in each table that contain the same data. For example, the Customers and Orders tables can be related to each other because they both contain a column that stores a customer ID. In the example, the column names are the same, but this is not a requirement. One could be CustomerID and another CustomerNumber, as long as all of the rows in the Orders table contain an ID that is also stored in the Customers table.  
  
 In a relational database, there are several types of *keys*, which are typically just columns with special properties. The following four types of keys can be used in relational databases:  
  
-   *Primary key*: uniquely identifies a row in a table, such as CustomerID in the Customers table.  
  
-   *Alternate key* (or *candidate key*): a column other than the primary key that is unique. For example, an Employees table might store an employee ID and a social security number, both of which are unique.  
  
-   *Foreign key*: a column that refers to a unique column in another table, such as CustomerID in the Orders table, which refers to CustomerID in the Customers table.  
  
-   *Composite key*: a key that is composed of more than one column. Composite keys are not supported in tabular models. For more information, see "Composite Keys and Lookup Columns" in this topic.  
  
 In tabular models, the primary key or alternate key is referred to as the *related lookup column*, or just *lookup column*. If a table has both a primary and alternate key, you can use either as the lookup column. The foreign key is referred to as the *source column* or just *column*. In our example, a relationship would be defined between CustomerID in the Orders table (the column) and CustomerID (the lookup column) in the Customers table. If you import data from a relational database, by default the model designer chooses the foreign key from one table and the corresponding primary key from the other table. However, you can use any column that has unique values for the lookup column.  
  
### Types of relationships  
 The relationship between Customers and Orders is a *one-to-many relationship*. Every customer can have multiple orders, but an order cannot have multiple customers. The other types of relationships are *one-to-one* and *many-to-many*. The CustomerDiscounts table, which defines a single discount rate for each customer, is in a one-to-one relationship with the Customers table. An example of a many-to-many relationship is a direct relationship between Products and Customers, in which a customer can buy many products and the same product can be bought by many customers. The model designer does not support many-to-many relationships in the user interface. For more information, see "[Many-to-Many Relationships](#bkmk_many_to_many)" in this topic.  
  
 The following table shows the relationships between the three tables:  
  
|Relationship|Type|Lookup Column|Column|  
|------------------|----------|-------------------|------------|  
|Customers-CustomerDiscounts|one-to-one|Customers.CustomerID|CustomerDiscounts.CustomerID|  
|Customers-Orders|one-to-many|Customers.CustomerID|Orders.CustomerID|  
  
### Relationships and performance  
 After any relationship has been created, the model designer typically must recalculate any formulas that use columns from tables in the newly created relationship. Processing can take some time depending on the amount of data and the complexity of the relationships.  
  
##  <a name="requirements"></a> Requirements for relationships  
 The model designer has several requirements that must be followed when creating relationships:  
  
### Single Active Relationship between tables  
 Multiple relationships could result in ambiguous dependencies between tables. To create accurate calculations, you need a single path from one table to the next. Therefore, there can be only one active relationship between each pair of tables. For example, in AdventureWorks DW 2012, the table, DimDate, contains a column, DateKey, that is related to three different columns in the table, FactInternetSales: OrderDate, DueDate, and ShipDate. If you attempt to import these tables, the first relationship is created successfully, but you will receive the following error on successive relationships that involve the same column:  
  
 \* Relationship: table[column 1]-> table[column 2]   - Status: error   - Reason: A relationship cannot be created between tables \<table 1> and \<table 2>. Only one direct or indirect relationship can exist between two tables.  
  
 If you have two tables and multiple relationships between them, then you will need to import multiple copies of the table that contains the lookup column, and create one relationship between each pair of tables.  
  
 There can be many inactive relationships between tables. The path to use between tables is specified by the reporting client at query time.  
  
### One relationship for each source column  
 A source column cannot participate in multiple relationships. If you have used a column as a source column in one relationship already, but want to use that column to connect to another related lookup column in a different table, you can create a copy of the column, and use that column for the new relationship.  
  
 It is easy to create a copy of a column that has the exact same values, by using a DAX formula in a calculated column. For more information, see [Create a Calculated Column](../../analysis-services/tabular-models/ssas-calculated-columns-create-a-calculated-column.md).  
  
### Unique identifier for each table  
 Each table must have a single column that uniquely identifies each row in that table. This column is often referred to as the primary key.  
  
### Unique lookup columns  
 The data values in the lookup column must be unique. In other words, the column cannot contain duplicates. In Tabular models, nulls and empty strings are equivalent to a blank, which is a distinct data value. This means that you cannot have multiple nulls in the lookup column.  
  
### Compatible data types  
 The data types in the source column and lookup column must be compatible. For more information about data types, see [Data Types Supported](../../analysis-services/tabular-models/data-types-supported-ssas-tabular.md).  
  
### Composite keys and lookup columns  
 You cannot use composite keys in a tabular model; you must always have one column that uniquely identifies each row in the table. If you try to import tables that have an existing relationship based on a composite key, the Table Import Wizard will ignore that relationship because it cannot be created in the tabular model.  
  
 If you want to create a relationship between two tables in the model designer, and there are multiple columns defining the primary and foreign keys, you must combine the values to create a single key column before creating the relationship. You can do this before you import the data, or you can do this in the model designer by creating a calculated column.  
  
###  <a name="bkmk_many_to_many"></a> Many-to-Many relationships  
 Tabular models do not support many-to-many relationships, and you cannot add *junction tables* in the model designer. However, you can use DAX functions to model many-to-many relationships.  
  
 You can also try setting up a bi-directional cross filter to see if it achieves the same purpose. Sometimes the requirement of many-to-many relationship can be satisfied through cross filters that persist a filter context across multiple table relationships. See [Bi-directional cross filters for tabular models in SQL Server 2016 Analysis Services](../../analysis-services/tabular-models/bi-directional-cross-filters-tabular-models-analysis-services.md) for details.  
  
### Self-joins and loops  
 Self-joins are not permitted in tabular model tables. A self-join is a recursive relationship between a table and itself. Self-joins are often used to define parent-child hierarchies. For example, you could join an Employees table to itself to produce a hierarchy that shows the management chain at a business.  
  
 The model designer does not allow loops to be created among relationships in a model. In other words, the following set of relationships is prohibited.  
  
 Table 1, column a   to   Table 2, column f  
  
 Table 2, column f   to   Table 3, column n  
  
 Table 3, column n   to   Table 1, column a  
  
 If you try to create a relationship that would result in a loop being created, an error is generated.  
  
##  <a name="detection"></a> Inference of relationships  
 In some cases, relationships between tables are automatically chained. For example, if you create a relationship between the first two sets of tables below, a relationship is inferred to exist between the other two tables, and a relationship is automatically established.  
  
 Products and Category -- created manually  
  
 Category and SubCategory -- created manually  
  
 Products and SubCategory -- relationship is inferred  
  
 In order for relationships to be automatically chained, the relationships must go in one direction, as shown above. If the initial relationships were between, for example, Sales and Products, and Sales and Customers, a relationship is not inferred. This is because the relationship between Products and Customers is a many-to-many relationship.  
  
##  <a name="bkmk_detection"></a> Detection of relationships when importing data  
 When you import from a relational data source table, the Table Import Wizard detects existing relationships in those source tables based on the source schema data. If related tables are imported, those relationships will be duplicated in the model.  
  
##  <a name="bkmk_manually_create"></a> Manually create relationships  
 While most relationships between tables in a single relational data source will be detected automatically, and created in the tabular model, there are also many instances where you must manually create relationships between model tables.  
  
 If your model contains data from multiple sources, you will likely have to manually create relationships. For example, you may import Customers, CustomerDiscounts, and Orders tables from a relational data source. Relationships existing between those tables at the source are automatically created in the model. You may then add another table from a different source, for example, you import region data from a Geography table in a Microsoft Excel workbook. You can then manually create a relationship between a column in the Customers table and a column in the Geography table.  
  
 To manually create relationships in a tabular model, you can use the model designer in Diagram View or by using the Manage Relationships dialog box. The diagram view displays tables, with relationships between them, in a graphical format. You can click a column in one table and drag the cursor to another table to easily create a relationship, in the correct order, between the tables. The Manage Relationships dialog box displays relationships between tables in a simple table format. To learn how to manually create relationships, see [Create a Relationship Between Two Tables](../../analysis-services/tabular-models/create-a-relationship-between-two-tables-ssas-tabular.md).  
  
##  <a name="bkmk_dupl_errors"></a> Duplicate values and other errors  
 If you choose a column that cannot be used in the relationship, a red X appears next to the column. You can pause the cursor over the error icon to view a message that provides more information about the problem. Problems that can make it impossible to create a relationship between the selected columns include the following:  
  
|Problem or message|Resolution|  
|------------------------|----------------|  
|The relationship cannot be created because both columns selected contain duplicate values.|To create a valid relationship, at least one column of the pair that you select must contain only unique values.<br /><br /> You can either edit the columns to remove duplicates, or you can reverse the order of the columns so that the column that contains the unique values is used as the **Related Lookup Column**.|  
|The column contains a null or empty value.|Data columns cannot be joined to each other on a null value. For every row, there must be a value in both of the columns that are used in a relationship.|  
  
##  <a name="bkmk_related_tasks"></a> Related tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create a Relationship Between Two Tables](../../analysis-services/tabular-models/create-a-relationship-between-two-tables-ssas-tabular.md)|Describes how to manually create a relationship between two tables.|  
|[Delete Relationships](../../analysis-services/tabular-models/delete-relationships-ssas-tabular.md)|Describes how to delete a relationship and the ramifications of deleting relationships.|  
|[Bi-directional cross filters for tabular models in SQL Server 2016 Analysis Services](../../analysis-services/tabular-models/bi-directional-cross-filters-tabular-models-analysis-services.md)|Describes two-way cross filtering for related tables. A filter context of one table relationship can be used when querying across a second table relationship if tables are related and bi-directional cross filters are define.|  
  
## See also  
 [Tables and Columns](../../analysis-services/tabular-models/tables-and-columns-ssas-tabular.md)   
 [Import Data](http://msdn.microsoft.com/library/6617b2a2-9f69-433e-89e0-4c5dc92982cf)  
  
  
