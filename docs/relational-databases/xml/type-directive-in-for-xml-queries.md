---
title: "TYPE Directive in FOR XML Queries | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "FOR XML clause, TYPE directive"
  - "TYPE directive"
ms.assetid: a3df6c30-1f25-45dc-b5a9-bd0e41921293
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# TYPE Directive in FOR XML Queries
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support for the [xml &#40;Transact-SQL&#41;](../../t-sql/xml/xml-transact-sql.md) enables you to optionally request that the result of a FOR XML query be returned as **xml** data type by specifying the TYPE directive. This allows you to process the result of a FOR XML query on the server. For example, you can specify an XQuery against it, assign the result to an **xml** type variable, or write [Nested FOR XML queries](../../relational-databases/xml/use-nested-for-xml-queries.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns XML data type instance data to the client as a result of different server-constructs such as FOR XML queries that use the TYPE directive, or where the **xml** data type is used to return XML instance data values from SQL table columns and output parameters. In client application code, the ADO.NET provider requests this XML data type information to be sent in a binary encoding from the server. However, if you are using FOR XML without the TYPE directive, the XML data comes back as a string type. In any case, the client provider will always be able to handle either form of XML. Note that top-level FOR XML without the TYPE directive cannot be used with cursors.  
  
## Examples  
 The following examples illustrate the use of the TYPE directive with FOR XML queries.  
  
### Retrieving FOR XML query results as xml type  
 The following query retrieves customer contact information from the `Contacts` table. Because the `TYPE` directive is specified in `FOR XML`, the result is returned as **xml** type.  
  
```  
USE AdventureWorks2012;  
Go  
SELECT BusinessEntityID, FirstName, LastName  
FROM Person.Person  
ORDER BY BusinessEntityID  
FOR XML AUTO, TYPE;  
```  
  
 This is the partial result:  
  
 `<Person.Person BusinessEntityID="1" FirstName="Ken" LastName="Sánchez"/>`  
  
 `<Person.Person BusinessEntityID="2" FirstName="Terri" LastName="Duffy"/>`  
  
 `...`  
  
### Assigning FOR XML query results to an xml type variable  
 In the following example, a FOR XML result is assigned to an **xml** type variable, `@x`. The query retrieves contact information, such as the `BusinessEntityID`, `FirstName`, `LastName`, and additional telephone numbers, from the `AdditionalContactInfo` column of **xml**`TYPE`. Because the `FOR XML` clause specifies `TYPE` directive, the XML is returned as **xml** type and is assigned to a variable.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @x xml;  
SET @x = (  
   SELECT BusinessEntityID,   
          FirstName,   
          LastName,   
          AdditionalContactInfo.query('  
declare namespace aci="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo";  
declare namespace act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes";  
              //act:telephoneNumber/act:number') as MorePhoneNumbers  
   FROM Person.Person  
   FOR XML AUTO, TYPE);  
SELECT @x;  
GO  
```  
  
### Querying results of a FOR XML query  
 The FOR XML queries return XML. Therefore, you can apply **xml** type methods, such as **query()** and **value()**, to the XML result returned by FOR XML queries.  
  
 In the following query, the `query()` method of the **xml** data type is used to query the result of the `FOR XML` query. For more information, see [query&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/query-method-xml-data-type.md).  
  
```  
USE AdventureWorks2012;  
GO  
SELECT (SELECT BusinessEntityID, FirstName, LastName, AdditionalContactInfo.query('  
DECLARE namespace aci="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo";  
DECLARE namespace act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes";  
 //act:telephoneNumber/act:number  
') AS PhoneNumbers  
FROM Person.Person  
FOR XML AUTO, TYPE).query('/Person.Person[1]');  
```  
  
 The inner `SELECT ... FOR XML` query returns an **xml** type result to which the outer `SELECT` applies the `query()` method to the **xml** type. Note the `TYPE` directive specified.  
  
 This is the result:  
  
 `<Person.Person BusinessEntityID="1" FirstName="Ken" LastName="Sánchez">`  
  
 `<PhoneNumbers>`  
  
 `<act:number xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">111-111-1111</act:number>`  
  
 `<act:number xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">112-111-1111</act:number>`  
  
 `</PhoneNumbers>`  
  
 `</Person.Person>`  
  
 In the following query, the `value()` method of the **xml** data type is used to retrieve a value from the XML result returned by the `SELECT...FOR XML` query. For more information, see [value&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/value-method-xml-data-type.md).  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @FirstPhoneFromAdditionalContactInfo varchar(40);  
SELECT @FirstPhoneFromAdditionalContactInfo =   
 ( SELECT BusinessEntityID, FirstName, LastName, AdditionalContactInfo.query('  
declare namespace aci="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo";  
declare namespace act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes";  
   //act:telephoneNumber/act:number  
   ') AS PhoneNumbers  
   FROM Person.Person Contact  
   FOR XML AUTO, TYPE).value('  
declare namespace act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes";  
  /Contact[@BusinessEntityID="1"][1]/PhoneNumbers[1]/act:number[1]', 'varchar(40)'  
 )  
SELECT @FirstPhoneFromAdditionalContactInfo;  
```  
  
 The XQuery path expression in the `value()` method retrieves the first telephone number of a customer contact whose `BusinessEntityID` is `1`.  
  
> [!NOTE]  
>  If the TYPE directive is not specified, the FOR XML query result is returned as type **nvarchar(max)**.  
  
### Using FOR XML query results in INSERT, UPDATE, and DELETE (Transact-SQL DML)  
 The following example demonstrates how FOR XML queries can be used in Data Manipulation Language (DML) statements. In the example, the `FOR XML` returns an instance of **xml** type. The `INSERT` statement inserts this XML into a table.  
  
```  
CREATE TABLE T1(intCol int, XmlCol xml);  
GO  
INSERT INTO T1   
VALUES(1, '<Root><ProductDescription ProductModelID="1" /></Root>');  
GO  
  
CREATE TABLE T2(XmlCol xml)  
GO  
INSERT INTO T2(XmlCol)   
SELECT (SELECT XmlCol.query('/Root')   
        FROM T1   
        FOR XML AUTO,TYPE);   
GO  
```  
  
## See Also  
 [FOR XML &#40;SQL Server&#41;](../../relational-databases/xml/for-xml-sql-server.md)  
  
  
