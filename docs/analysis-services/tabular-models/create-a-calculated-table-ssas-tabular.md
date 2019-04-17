---
title: "Create a calculated table in Analysis Services tabular models | Microsoft Docs"
ms.date: 12/19/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create a calculated table 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  A *calculated table* is a computed object, based on either a DAX query or expression, derived from all or part of other tables in the same model.  
  
 A common design problem that calculated tables can solve is surfacing a role-playing dimension in a specific context so that you can expose it as a query structure in client applications.  You might recall that  a role-playing dimension  is simply a table surfaced in multiple  contexts -- a classic example is the Date table, manifested as OrderDate, ShipDate, or DueDate, depending on the foreign key relationship. By creating a calculated table for ShipDate explicitly, you get a standalone table that is available for queries, as fully operable as any other table. Another use includes configuring a filtered rowset, a subset, or superset of columns from other existing tables. This allows you to keep the original table intact while creating variations of that table to support specific scenarios.  
  
 Using calculated tables to best advantage will require that you know at least some DAX. As you work with expressions for your table, it might help to know that a calculated table contains a single partition with a DAXSource, where the expression is a DAX expression.  
There is one CalculatedTableColumn for each column returned by the expression, where the SourceColumn is the name of the column returned (similar to DataColumns on non-calculated tables). 

At least one table must already exist before you can create a calculated table. If you are creating a calculated table as a standalone computed table object, you can first create a table by importing from a file data source (csv, xls, xml). The file you import from can have a single column and single value. You can then hide that table. 
  
## How to create a calculated table  
  
1.  First, verify the tabular model has a compatibility level of 1200 or higher. You can check the **Compatibility Level** property on the model in SSDT.  
  
2.  Switch to the Data View. You can't create a calculated table in Diagram View.  
  
3.  Select **Table** > **New calculated table**.  
  
4.  Type or paste  a DAX expression (see below for some ideas).  
  
5.  Name the table.  
  
6.  Create relationships to other tables in the model. See [Create a Relationship Between Two Tables](../../analysis-services/tabular-models/create-a-relationship-between-two-tables-ssas-tabular.md) if you need help with this step.  
  
7.  Reference the table in calculations or expressions in your model or use **Analyze in Excel** for ad hoc data exploration.  
  
### Replicate a role-playing dimension  
 In the Formula bar, enter a DAX formula that gets a copy of another table. After the calculated table is populated, give it a descriptive name and then set up a relationship that uses the foreign key specific to the role. For example, in the Adventure Works database, you might create a calculated table for Due Date and use the DueDateKey as the basis of a relationship to the fact table.  
  
```  
=DimDate  
```  
  
### Summarized or filtered dataset  
 In the Formula bar, enter a DAX expression that filters, summarizes, or otherwise manipulates a dataset to contain the rows you want. This example groups by sales by color and currency.  
  
```  
=SUMMARIZECOLUMNS(DimProduct[Color]  
, DimCurrency[CurrencyName]   
, "Sales" , SUM(FactInternetSales[SalesAmount])  
)  
```  
  
### Superset using columns from multiple tables  
 In the Formula bar, enter a DAX expression that combines columns from multiple tables. In this case, query output lists  product category for each currency.  
  
```  
=CROSSJOIN(DimProductCategory, DimCurrency)  
```  
  
## See also  
 [Compatibility level](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md)   
 [Data Analysis Expressions &#40;DAX&#41; in Analysis Services](http://msdn.microsoft.com/library/abb336c9-3346-4cab-b91b-90f93f4575e5)   
 [Understanding DAX in tabular models](../../analysis-services/tabular-models/understanding-dax-in-tabular-models-ssas-tabular.md)  
  
  
