---
title: "Query Expressions and Uniform Resource Names | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
helpviewer_keywords: 
  - "query expressions"
  - "unique resource names"
  - "URN"
ms.assetid: e0d30dbe-7daf-47eb-8412-1b96792b6fb9
author: stevestein
ms.author: sstein
manager: craigg
---
# Query Expressions and Uniform Resource Names
  The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Management Object (SMO) models and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] PowerShell snap-ins use two types of expression strings that are similar to XPath expressions. Query expressions are strings that specify a set of criteria used to enumerate one or more objects in an object model hierarchy. A Uniform Resource Name (URN) is a specific type of query expression string that uniquely identifies a single object.  
  
## Syntax  
  
```  
  
      Object1  
      [<FilterExpression1>]/ ... /ObjectN[<FilterExpressionN>]<FilterExpression>::=  
<PropertyExpression> [and <PropertyExpression>][...n]  
  
<PropertyExpression>::=@BooleanPropertyName=true()  
 | @BooleanPropertyName=false()  
 | contains(@StringPropertyName, 'PatternString')  
  | @StringPropertyName='String'  
 | @DatePropertyName=datetime('DateString')  
 | is_null(@PropertyName)  
 | not(<PropertyExpression>)  
```  
  
## Arguments  
 *Object*  
 Specifies the type of object that is represented at that node of the expression string. Each object represents a collection class from these SMO object model namespaces:  
  
 <xref:Microsoft.SqlServer.Management.Smo>  
  
 <xref:Microsoft.SqlServer.Management.Smo.Agent>  
  
 <xref:Microsoft.SqlServer.Management.Smo.Broker>  
  
 <xref:Microsoft.SqlServer.Management.Smo.Mail>  
  
 <xref:Microsoft.SqlServer.Management.Dmf>  
  
 <xref:Microsoft.SqlServer.Management.Facets>  
  
 <xref:Microsoft.SqlServer.Management.RegisteredServers>  
  
 <xref:Microsoft.SqlServer.Management.Smo.RegSvrEnum>  
  
 For example, specify Server for the **ServerCollection** class, Database for the **DatabaseCollection** class.  
  
 \@*PropertyName*  
 Specifies the name of one of the properties of the class that is associated with the object specified in *Object*. The name of the property must be prefixed with the \@ character. For example, specify \@IsAnsiNull for the **Database** class property **IsAnsiNull**.  
  
 \@*BooleanPropertyName*=true()  
 Enumerates all objects where the specified Boolean property is set to TRUE.  
  
 \@*BooleanPropertyName*=false()  
 Enumerates all objects where the specified Boolean property is set to FALSE.  
  
 contains(\@*StringPropertyName*, '*PatternString*')  
 Enumerates all objects where the specified string property contains at least one occurrence of the set of characters that is specified in '*PatternString*'.  
  
 \@*StringPropertyName*='*PatternString*'  
 Enumerates all objects where the value of the specified string property is exactly the same as the character pattern that is specified in '*PatternString*'.  
  
 \@*DatePropertyName*= datetime('*DateString*')  
 Enumerates all objects where the value of the specified date property matches the date that is specified in '*DateString*'. *DateString* must follow the format yyyy-mm-dd hh:mi:ss.mmm  
  
|||  
|-|-|  
|yyyy|Four digit year.|  
|mm|Two digit month (01 through 12).|  
|dd|Two digit date (01 through 31).|  
|hh|Two digit hour using a 24 hour clock (01 through 23).|  
|mi|Two digit minutes (01 through 59).|  
|ss|Two digit seconds (01 through 59).|  
|mmm|Number of milliseconds (001 through 999).|  
  
 The dates that are specified in this format can be evaluated against any date format that is stored in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 is_null(\@*PropertyName*)  
 Enumerates all objects where the specified property has a value of NULL.  
  
 not(\<*PropertyExpression*>)  
 Negates the evaluation value of the *PropertyExpression*, enumerating all objects that do not match the condition specified in *PropertyExpression*. For example, not(contains(\@Name, 'xyz')) enumerates all objects that do not have the string xyz in their names.  
  
## Remarks  
 Query expressions are strings that enumerate the nodes in an SMO model hierarchy. Each node has a filter expression that specifies the criteria for determining which objects at that node are enumerated. Query expressions are modeled on the XPath expression language. Query expressions implement a small subset of the expressions that are supported by XPath, and also have some extensions that are not found in XPath. XPath expressions are strings that specify a set of criteria that are used to enumerate one or more of the tags in an XML document. For more information about XPath, see [W3C XPath Language](http://www.w3.org/TR/xpath20/).  
  
 Query expressions must start with an absolute reference to the Server object. Relative expressions with a leading / are not allowed. The sequence of objects that are specified in a query expression must follow the hierarchy of collection objects in the associated object model. For example, a query expression that references objects in the Microsoft.SqlServer.Management.Smo namespace must start with a Server node followed by a Database node, and so on.  
  
 If a *\<FilterExpression>* is not specified for an object, all the objects at that node are enumerated.  
  
## Uniform Resource Names (URN)  
 URNs are a subset of query expressions. Each URN forms a fully-qualified reference to a single object. A typical URN uses the Name property to identify a single object at each node. For example, this URN refers to a specific column:  
  
```  
Server[@Name='MYCOMPUTER']/Database[@Name='AdventureWorks2012']/Table[@Name='SalesPerson' and @Schema='Sales']/Column[@Name='SalesPersonID']  
```  
  
## Examples  
  
### A. Enumerating objects using false()  
 This query expression enumerates all the databases that have the **AutoClose** attribute set to false in the default instance on **MyComputer**.  
  
```  
Server[@Name='MYCOMPUTER']/Database[@AutoClose=false()]  
```  
  
### B. Enumerating objects using contains  
 This query expression enumerates all the databases that are case-insensitive and have the character 'm' in their name.  
  
```  
Server[@Name='MYCOMPUTER']/Database[@CaseSensitive=false() and contains(@Name, 'm')]   
```  
  
### C. Enumerating objects using not  
 This query expression enumerates all of [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] tables that are not in the **Production** schema and contain the word History in the table name:  
  
```  
Server[@Name='MYCOMPUTER']/Database[@Name='AdventureWorks2012']/Table[not(@Schema='Production') and contains(@Name, 'History')]  
```  
  
### D. Not supplying a filter expression for the final node  
 This query expression enumerates all the columns in the **AdventureWorks2012.Sales.SalesPerson** table:  
  
```  
Server[@Name='MYCOMPUTER']/Database[@Name='AdventureWorks2012"]/Table[@Schema='Sales' and @Name='SalesPerson']/Columns  
```  
  
### E. Enumerating objects using datetime  
 This query expression enumerates all the tables that are created in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database at a specific time:  
  
```  
Server[@Name='MYCOMPUTER']/Database[@Name='AdventureWorks2012"]/Table[@CreateDate=datetime('2008-03-21 19:49:32.647')]  
```  
  
### F. Enumerating objects using is_null  
 This query expression enumerates all the tables in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database that do not have NULL for their date last modified property:  
  
```  
Server[@Name='MYCOMPUTER']/Database[@Name='AdventureWorks2012"]/Table[Not(is_null(@DateLastModified))]  
```  
  
## See Also  
 [Invoke-PolicyEvaluation cmdlet](../database-engine/invoke-policyevaluation-cmdlet.md)   
 [SQL Server Audit &#40;Database Engine&#41;](../relational-databases/security/auditing/sql-server-audit-database-engine.md)  
  
  
