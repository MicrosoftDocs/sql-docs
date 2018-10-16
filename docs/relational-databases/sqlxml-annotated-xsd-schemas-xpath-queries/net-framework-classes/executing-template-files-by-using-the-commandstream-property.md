---
title: "Executing Template Files by Using the CommandStream Property | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "Managed Classes [SQLXML], executing template files"
  - "SQLXML Managed Classes, executing template files"
  - "templates [SQLXML], SQLXML Managed Classes"
  - "CommandStream property"
ms.assetid: 55c564e3-56d1-4d85-bcaa-703e2905dd57
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing Template Files by Using the CommandStream Property
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This example illustrates how template files that consist of SQL or XPath queries can be specified by using the CommandStream property of the SqlXmlCommand object. In this application, a FileStreamobject is opened for a command file, and the file stream is assigned as the CommandStream that is executed.  
  
 In the following example, the CommandType property is specified as SqlXmlCommandType.Template (not as TemplateFile).  
  
 This is the sample XML template:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query>  
    SELECT TOP 2 ContactID, FirstName, LastName   
    FROM   Person.Contact  
    FOR XML AUTO  
  </sql:query>  
</ROOT>  
```  
  
 This is the sample C# application. To test the application, save the template (TemplateFile.xml) and then execute the application. The application executes the query that is specified in the XML template and displays the XML document that is generated on the screen.  
  
> [!NOTE]  
>  In the code, you must provide the name of the instance of Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the connection string.  
  
```  
using System;  
using Microsoft.Data.SqlXml;  
using System.IO;  
  
class Test  
{  
      static string ConnString = "Provider=SQLOLEDB;Server=(local);database=AdventureWorks;Integrated Security=SSPI";  
      public static int testParams()  
      {  
         //Stream strm;  
         MemoryStream ms = new MemoryStream();  
         StreamWriter sw = new StreamWriter(ms);  
         ms.Position = 0;  
         SqlXmlCommand cmd = new SqlXmlCommand(ConnString);  
         cmd.CommandStream = new FileStream("TemplateFile.xml", FileMode.Open, FileAccess.Read);  
         cmd.CommandType = SqlXmlCommandType.Template;  
         using (Stream strm = cmd.ExecuteStream())  
         {  
            using (StreamReader sr = new StreamReader(strm)){  
               Console.WriteLine(sr.ReadToEnd());  
            }  
         }  
         return 0;        
      }  
  
      public static int Main(String[] args)  
      {  
         testParams();     
         return 0;  
      }  
   }  
```  
  
### To test the application  
  
1.  Save the XML template (TemplateFile.xml) that is provided in this example in a folder.  
  
2.  Save the C# code (DocSample.cs) that is provided in this example in the same folder in which the schema is stored. (If you store the files in a different folder, you will have to edit the code and specify the appropriate directory path for the mapping schema.)  
  
3.  Compile the code. To compile the code at the command prompt, use:  
  
    ```  
    csc /reference:Microsoft.Data.SqlXML.dll DocSample.cs  
    ```  
  
     This creates an executable (DocSample.exe).  
  
4.  At the command prompt, execute DocSample.exe.  
  
  
