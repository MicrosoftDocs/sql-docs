---
title: "FOR XML Security Considerations (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "NESTED mode"
  - "client-side XML formatting"
  - "FOR XML clause, security"
  - "server-side XML formatting"
  - "AUTO mode"
  - "security [SQLXML], FOR XML"
ms.assetid: facba279-df93-475b-ad43-0043dc5bae03
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# FOR XML Security Considerations (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The FOR XML AUTO mode generates an XML hierarchy in which element names map to table names and attribute names map to column names. This exposes the database table and column information. You can hide the database information when you use AUTO mode (server-side formatting) by specifying table and column aliases in the query. These aliases are returned in the resulting XML document as element and attribute names.  
  
 For example, the following query specifies AUTO mode; therefore, the XML formatting is done on the server:  
  
```  
SELECT C.FirstName as F,C.LastName as L   
FROM Person.Contact C   
FOR XML AUTO  
```  
  
 In the resulting XML document, the aliases are used for element and attribute names:  
  
```  
<?xml version="1.0" encoding="utf-8" ?>   
<root>  
  <C F="Nancy" L="Fuller" />   
  <CE F="Andrew" L="Peacock" />   
  <C F="Janet" L="Leverling" />   
  ...  
</root>  
```  
  
 When you use NESTED mode (client-side formatting), aliases are returned only for attributes in the resulting XML document. The names of the base tables are always returned as element names. For example, the following query specifies NESTED mode.  
  
```  
SELECT C.FirstName as F,C.LastName as L   
FROM Person.Contact C   
FOR XML AUTO  
```  
  
 In the resulting XML document, the names of the base tables are returned as element names and table aliases are not used:  
  
```  
<?xml version="1.0" encoding="utf-8" ?>   
<root>  
  <Person.Contact F="Nancy" L="Fuller" />   
  <Person.Contact F="Andrew" L="Peacock" />   
  <Person.Contact F="Janet" L="Leverling" />   
       ...  
</root>  
```  
  
  
