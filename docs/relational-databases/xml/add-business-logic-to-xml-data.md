---
title: "Add Business Logic to XML Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "business logic [XML]"
ms.assetid: 0877fb38-f1a2-43d8-86cf-4754be224dc1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Add Business Logic to XML Data
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Your business logic can be added to XML data in several ways:  
  
-   You can write row or column constraints to enforce domain-specific constraints during insertion and modification of XML data.  
  
-   You can write a trigger on the XML column that fires when you insert or update values in the column. The trigger can contain domain-specific validation rules or populate property tables.  
  
-   The Database Engine includes the ability execute managed code. You can use this common language runtime (CLR) integration to write functions in managed code to which you pass XML values, and use XML processing capabilities provided by the System.Xml namespace. An example is to apply XSL transformation to XML data. Alternatively, you can deserialize the XML into one or more managed classes and operate on them by using managed code.  
  
-   You can write Transact-SQL stored procedures and functions that start the processing on the XML column for your business needs.  
  
## Example: Applying XSL Transformation  
 Consider a CLR function **TransformXml()** that accepts an **xml** data type instance and an XSL transformation stored in a file, applies the transformation to the XML data, and then returns the transformed XML in the result. Following is a skeleton function that is written in C#:  
  
```  
public static SqlXml TransformXml (SqlXml XmlData, string xslPath) {  
   // Load XSL transformation  
   XslCompiledTransform xform = new XslCompiledTransform();  
   XPathDocument xslDoc = new XPathDocument (xslPath);  
   xform.Load(xslDoc);  
  
   // Load XML data   
   XPathDocument xDoc = new XPathDocument (XmlData.CreateReader());  
  
   // Return the transformed value  
   MemoryStream xsltResult = new MemoryStream();  
   xform.Transform(xDoc, null, xsltResult);  
   SqlXml retSqlXml = new SqlXml(xsltResult);  
   return (retSqlXml);  
}   
```  
  
 After the assembly is registered and a user-defined [!INCLUDE[tsql](../../includes/tsql-md.md)] function is created, **SqlXslTransform()** corresponding to **TransformXml()**, the function can be invoked from Transact-SQL as shown in the following query:  
  
```  
SELECT SqlXslTransform (xCol, 'C:\MyFile\xsltransform.xsl')  
FROM    T  
WHERE  xCol.exist('/book/title/text()[contains(.,"custom")]') =1;  
```  
  
 The query result contains a rowset of the transformed XML.  
  
 The CLR integration into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] expands the possibilities for decomposing XML data into tables or property promotion, and querying XML data by using managed classes in the System.Xml namespace. For more information, see [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md).  
  
  
