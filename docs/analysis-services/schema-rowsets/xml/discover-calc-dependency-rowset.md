---
title: "DISCOVER_CALC_DEPENDENCY Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_CALC_DEPENDENCIES rowset"
ms.assetid: f39dde72-fa5c-4c82-8b4e-88358aa2e422
caps.latest.revision: 22
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_CALC_DEPENDENCY Rowset
  Reports on the dependencies between calculations and on the objects referenced in those calculations. You can use this information in a client application to report on problems with complex formulas, or to warn when related objects are deleted or modified. You can also use the rowset to extract the DAX expressions used in measures or calculated columns.  
  
 **Applies to:** tabular models  
  
## Rowset Columns  
 The **DISCOVER_CALC_DEPENDENCY** rowset contains the following columns. The table also specifies the data type, indicates whether the column can be restricted to limit the rows that are returned, and provides a description of each column.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|**DATABASE_NAME**|**DBTYPE_WSTR**|Yes|Specifies the database name that contains the object for which dependency analysis is requested. If omitted, the current database is used.<br /><br /> The **DISCOVER_DEPENDENCY_CALC** rowset can be restricted by using this column.|  
|**OBJECT_TYPE**|**DBTYPE_WSTR**|Yes|Indicates the type of the object for which dependency analysis is requested. The object must be one of the following types:<br /><br /> **ACTIVE_RELATIONSHIP**: an active relationship<br /><br /> **CALC_COLUMN**: Calculated column<br /><br /> **HIERARCHY**: a hierarchy<br /><br /> **MEASURE**: a measure<br /><br /> **RELATIONSHIP**: a relationship<br /><br /> **KPI**: a KPI (Key Performance Indicator)<br /><br /> <br /><br /> Note that the **DISCOVER_DEPENDENCY_CALC** rowset can be restricted by using this column.|  
|**QUERY**|**DBTYPE_WSTR**|Yes|For tabular models created in [!INCLUDE[ssSQL11SP1](../../../includes/sssql11sp1-md.md)], you can include a DAX query or expression to show the dependency graph for that query or expression. The QUERY restriction provides client applications with a way to determine which objects are used by a DAX query.<br /><br /> The **QUERY** restriction can be specified in XMLA or in the WHERE clause of a DMV query. See the examples section for more information.|  
|**TABLE**|**DBTYPE_WSTR**||The name of the table that contains the object for which dependency information is generated.|  
|**OBJECT**|**DBTYPE_WSTR**||The name of the object for which dependency information is generated. If the object is a measure or calculated column, use the name of the measure. If the object is a relationship, the name of the table (or cube dimension) that contains the column participating in the relationship.|  
|**EXPRESSION**|**DBTYPE_WSTR**||The formula that contains the object for which dependencies are sought.|  
|**REFERENCED_OBJECT_TYPE**|**DBTYPE_WSTR**||Returns the type of the object that has a dependency on the referenced object. Objects returned can be of the following types:<br /><br /> **CALC_COLUMN**:  a calculated column<br /><br /> **COLUMN**: A column of data<br /><br /> **MEASURE**: a measure<br /><br /> **RELATIONSHIP**: a relationship<br /><br /> **KPI**: a KPI (Key Performance Indicator)|  
|**REFERENCED_TABLE**|**DBTYPE_ WSTR**||The name of the table that contains the dependent object.|  
|**REFERENCED_OBJECT**|**DBTYPE_ WSTR**||The name of the object that has a dependency on the referenced object. For measures and calculated columns, the name of the measure or column. For relationships, the fully-qualified name of the table (or cube dimension) that contains the dependent object.|  
|**REFERENCED_EXPRESSION**|**DBTYPE_WSTR**||A formula, either in a calculated column or in a measure, that is dependent on the referenced object.|  
  
## Example  
 **Basic syntax**  
  
 The following query is a simple DMV query that returns values for all of the columns in this rowset, using the default database on the current connection. You can run this query in an MDX query window and view its results in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)]. You can also follow the techniques described in [Querying Power Pivot DMVs from Excel](http://go.microsoft.com/fwlink/?LinkID=235146) to view DMV query results in Excel.  
  
```  
SELECT * FROM $System.DISCOVER_CALC_DEPENDENCY  
```  
  
## Example  
 **Sort the results**  
  
 Add an ORDER BY to sort the rowset by table or another column.  
  
```  
SELECT * FROM $System.DISCOVER_CALC_DEPENDENCY ORDER BY [TABLE] ASC  
```  
  
## Example  
 **Filter using a WHERE clause**  
  
 The next query shows how to add a restriction using the WHERE clause. The following columns can be used as query filters in a WHERE clause: **Database_Name**, **Object_Type**, and **Query**.  
  
```  
SELECT * From $SYSTEM.DISCOVER_CALC_DEPENDENCY WHERE OBJECT_TYPE = 'RELATIONSHIP' OR OBJECT_TYPE = 'ACTIVE_RELATIONSHIP'  
```  
  
## Example  
 **Filter on measures and calculated columns to view underlying DAX expressions**  
  
 In this query, you can select just the measure or calculated column, and then view the DAX expression used in the calculation. The EXPRESSION column contains the DAX expressions. If you are using DISCOVER_CALC_DEPENDENCY to extract DAX expression used in the model, this query is sufficient for that purpose. It returns all expressions used in the model, in ascending order.  
  
```  
SELECT * From $SYSTEM.DISCOVER_CALC_DEPENDENCY WHERE OBJECT_TYPE = 'MEASURE' OR OBJECT_TYPE = 'CALC_COLUMN' ORDER BY [EXPRESSION] ASC  
```  
  
## Example  
 **Filter using QUERY**  
  
 Using the QUERY restriction, you can provide a DAX query to view all of the objects used in that query. Consider a simple query like ‘Evaluate Customer’. As written, this query returns rows of customer data, where row composition is based on the columns in the Customer table. If you now run DISCOVER_CALC_DEPENDENCY with a QUERY restriction of ‘Evaluate Customer’, you will get the columns (or objects) used in that query. In this case, it’s a list of the columns in the Customer table.  
  
 The next set of queries demonstrates syntax for the QUERY restriction. You can run these queries against the [AdventureWorks Tabular Model SQL Server 2012](http://msftdbprodsamples.codeplex.com/releases/view/55330) to view the result set.  
  
 The first query shows how to specify a QUERY restriction for object names that include spaces. The second query, borrowed from [Execute DAX queries through OLE DB and ADOMD.NET](http://go.microsoft.com/fwlink/?LinkId=254329), is a more complex query that includes objects from multiple tables.  
  
> [!NOTE]  
>  Although the queries appear to use double-quotes, in fact only single quotes are used. One pair of single quotation marks encloses ‘Evaluate \<Tablename>’, and single quotation marks used around the table name need to be escaped by doubling them up. Single quotation marks around a table name are needed only for table names that include a space.  
  
```  
SELECT * From $SYSTEM.DISCOVER_CALC_DEPENDENCY WHERE QUERY = 'EVALUATE ''Reseller Sales'''  
```  
  
```  
SELECT * from $system.DISCOVER_CALC_DEPENDENCY WHERE QUERY = 'EVALUATE CALCULATETABLE(VALUES(''Product Subcategory''[Product Subcategory Name]), ''Product Category''[Product Category Name] = "Bikes")'  
```  
  
## Example  
 **QUERY Restriction XMLA Example**  
  
 You can use an XMLA Discover command to return the query objects in a table. XMLA returns results as raw XML. You can use ADOMD.NET to parse the results in a more readable format.  
  
```  
<Discover xmlns="urn:schemas-microsoft-com:xml-analysis">  
   <RequestType>DISCOVER_CALC_DEPENDENCY</RequestType>  
     <Restrictions>  
        <RestrictionList>  
            <QUERY>Evaluate 'Reseller Sales'</QUERY>  
        </RestrictionList>  
    </Restrictions>  
    <Properties />  
</Discover>  
```  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd46-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|DependencyGraph|  
  
## See Also  
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)   
 [Use Dynamic Management Views &#40;DMVs&#41; to Monitor Analysis Services](../../../analysis-services/instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md)  
  
  