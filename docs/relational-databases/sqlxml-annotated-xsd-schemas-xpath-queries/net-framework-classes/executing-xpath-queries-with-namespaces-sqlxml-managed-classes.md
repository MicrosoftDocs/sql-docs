---
title: "Executing XPath Queries with Namespaces (SQLXML)"
description: Learn how to include namespaces in SQLXML XPath queries.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "namespaces property"
  - "XPath queries [SQLXML], SQLXML Managed Classes"
  - "queries [SQLXML], SQLXML Managed Classes"
  - "XPath queries [SQLXML], namespaces"
  - "Managed Classes [SQLXML], executing XPath queries"
  - "SQLXML Managed Classes, executing XPath queries"
  - "namespaces [SQLXML], XPath queries"
ms.assetid: c6fc46d8-6b42-4992-a8f1-a8d4b8886e6e
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing XPath Queries with Namespaces (SQLXML Managed Classes)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  XPath queries can include namespaces. If the schema elements are namespace-qualified (use a target namespace), the XPath queries against the schema must specify the namespace.  
  
 Because the wildcard character (*) is not supported in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML 4.0, you must specify the XPath query by using a namespace prefix. To resolve the prefix, use the namespaces property to specify the namespace binding.  
  
 In the following example, the XPath query specifies namespaces by using the wildcard character (\*) and the local-name() and namespace-uri() XPath functions. This XPath query returns all the elements where the local name is **Employee** and the namespace URI is **urn:myschema:Contacts**:  
  
```  
/*[local-name() = 'Contact' and namespace-uri() = 'urn:myschema:Contacts']  
```  
  
 In SQLXML 4.0, specify this XPath query with a namespace prefix. An example is **x:Contact**, where **x** is the namespace prefix. Consider the following XSD schema:  
  
```  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema"  
            xmlns:con="urn:myschema:Contacts"  
            targetNamespace="urn:myschema:Contacts">  
<complexType name="ContactType">  
  <attribute name="CID" sql:field="ContactID" type="ID"/>  
  <attribute name="FName" sql:field="FirstName" type="string"/>  
  <attribute name="LName" sql:field="LastName"/>   
</complexType>  
<element name="Contact" type="con:ContactType" sql:relation="Person.Contact"/>  
</schema>  
```  
  
 Because this schema defines the target namespace, an XPath query (such as "Employee") against this schema must include the namespace.  
  
 The following C# sample application executes an XPath query against the preceding XSD schema (MySchema.xml). To resolve the prefix, specify the namespace binding by using the Namespaces property of the SqlXmlCommand object.  
  
> [!NOTE]  
>  In the code, you must provide the name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the connection string.  
  
```  
using System;  
using Microsoft.Data.SqlXml;  
using System.IO;  
class Test  
{  
      static string ConnString = "Provider=SQLOLEDB;Server=(local);database=AdventureWorks;Integrated Security=SSPI";  
      public static int testXPath()  
      {  
         //Stream strm;  
         SqlXmlCommand cmd = new SqlXmlCommand(ConnString);  
         cmd.CommandText = "x:Contact[@CID='1']";  
         cmd.CommandType = SqlXmlCommandType.XPath;  
         cmd.RootTag = "ROOT";  
         cmd.Namespaces = "xmlns:x='urn:myschema:Contacts'";  
         cmd.SchemaPath = "MySchema.xml";  
         using (Stream strm = cmd.ExecuteStream()){  
            using (StreamReader sr = new StreamReader(strm)){  
               Console.WriteLine(sr.ReadToEnd());  
            }  
         }  
         return 0;  
      }  
      public static int Main(String[] args)  
      {  
         testXPath();  
         return 0;  
      }  
   }  
```  
  
 To test this example, you must have the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework installed on your computer.  
  
### To test the application  
  
1.  Save the XSD schema (MySchema.xml) that is provided in this example in a folder.  
  
2.  Save the C# code (DocSample.cs) that is provided in this example in the same folder in which the schema is stored. (If you store the files in a different folder, you will have to edit the code and specify the appropriate directory path for the mapping schema.)  
  
3.  Compile the code. To compile the code at the command prompt, use:  
  
    ```  
    csc /reference:Microsoft.Data.SqlXML.dll DocSample.cs  
    ```  
  
     This creates an executable (DocSample.exe).  
  
4.  At the command prompt, execute DocSample.exe.  

