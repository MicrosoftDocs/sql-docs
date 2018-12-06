---
title: "FOR XML Support for the timestamp Data Type | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "timestamp data type"
ms.assetid: 4e1920e1-e7a4-4069-965e-3f6039a6099e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# FOR XML Support for the timestamp Data Type
  In the FOR XML transformation, **timestamp** type values are treated as **varbinary(8)** data and will always be base 64 encoded. The XSD or XDR schema, if requested, reflects this type.  
  
```  
drop table t  
go  
create table t  
(c1 int,  
 c2 timestamp)  
go  
  
insert t values(1, null)  
go  
select * from t  
for xml auto, xmldata  
go  
```  
  
 This is the result:  
  
```  
<Schema name="Schema1"   
        xmlns="urn:schemas-microsoft-com:xml-data"   
        xmlns:dt="urn:schemas-microsoft-com:datatypes">  
  <ElementType name="t" content="empty" model="closed">  
    <AttributeType name="c1" dt:type="i4" />  
    <AttributeType name="c2" dt:type="bin.base64" />  
    <attribute type="c1" />  
    <attribute type="c2" />  
  </ElementType>  
</Schema>  
<t xmlns="x-schema:#Schema1" c1="1" c2="AAAAAAAAH04=" />  
```  
  
## See Also  
 [FOR XML Support for Various SQL Server Data Types](for-xml-support-for-various-sql-server-data-types.md)  
  
  
