---
title: "AUTO Mode Heuristics in Shaping Returned XML | Microsoft Docs"
description: Learn how to use AUTO mode heuristics with the FOR XML clause to compare column values in adjacent rows and determine the shape of XML returned by a query.
ms.custom: "fresh2019may"
ms.date: 04/03/2020
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "AUTO FOR XML mode, heuristics in shaping returned XML"
ms.assetid: 6c5cb6c1-2921-4ba1-8100-0bf8074f9103
author: RothJa
ms.author: jroth
# monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"
---
# AUTO Mode Heuristics in Shaping Returned XML

[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

AUTO mode determines the shape of returned XML based on the query. In determining how elements are to be nested, AUTO mode heuristics compare column values in adjacent rows. Columns of all types, except **ntext**, **text**, **image**, and **xml**, are compared. Columns of type **(n)varchar(max)** and **varbinary(max)** are compared.  
  
 The following example illustrates the AUTO mode heuristics that determine the shape of the resulting XML:  
  
```sql
SELECT T1.Id, T2.Id, T1.Name  
FROM   T1, T2  
WHERE ...  
ORDER BY T1.Id
FOR XML AUTO;
```  
  
 To determine where a new <`T1`> element starts, all column values of T1, except **ntext**, **text**, **image** and **xml**, are compared if the key on the table T1 is not specified. Next, assume that the **Name** column is **nvarchar(40)** and the SELECT statement returns this rowset:  
  
```  
T1.Id  T1.Name  T2.Id  
-----------------------  
1       Andrew    2  
1       Andrew    3  
1       Nancy     4  
```  
  
 The AUTO mode heuristics compare all the values of table T1, the Id and Name columns. Because the first two rows have the same values for the Id and Name columns, one \<T1> element having two \<T2> child elements is added in the result.  
  
 Following is the XML that is returned:  
  
```xml
<T1 Id="1" Name="Andrew">  
    <T2 Id="2" />  
    <T2 Id="3" />  
</T1>  
<T1 Id="1" Name="Nancy" >  
      <T2 Id="4" />  
</T>  
```  
  
 Now assume that the Name column is of **text** type. The AUTO mode heuristics do not compare the values for this type. Instead, it assumes that the values are not the same. This results in XML generation as shown in the following:  
  
```xml
<T1 Id="1" Name="Andrew" >  
  <T2 Id="2" />  
</T1>  
<T1 Id="1" Name="Andrew" >  
  <T2 Id="3" />  
</T1>  
<T1 Id="1" Name="Nancy" >  
  <T2 Id="4" />  
</T1>  
```  
  
## See Also  
 [Use AUTO Mode with FOR XML](../../relational-databases/xml/use-auto-mode-with-for-xml.md)  
  
  
