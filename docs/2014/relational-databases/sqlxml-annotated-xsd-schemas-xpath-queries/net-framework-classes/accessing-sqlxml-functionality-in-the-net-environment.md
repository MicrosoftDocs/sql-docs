---
title: "Accessing SQLXML Functionality in the .NET Environment | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "Managed Classes [SQLXML], accessing SQLXML functionality"
  - "SQLXML Managed Classes, accessing SQLXML functionality"
  - "DiffGrams [SQLXML], accessing SQLXML functionality"
  - ".NET Framework [SQLXML], accessing SQLXML functionality"
ms.assetid: 74744535-2945-414d-9a5b-7e8cc363953a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Accessing SQLXML Functionality in the .NET Environment
  This example shows:  
  
-   How to use [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML Managed Classes (Microsoft.Data.SqlXml) to access Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework environment.  
  
-   How DiffGrams that are generated in the .NET Framework environment can apply data updates to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tables.  
  
 In this application, an XPath query is executed against an XSD schema. The execution of the XPath query returns an XML document that consists of contact data (**FirstName**, **LastName**). The application loads the XML document in the dataset in the .NET Framework environment. The data in the dataset is modified: the contact's first name is changed to "Susan" for the first contact in the dataset. The DiffGram is generated from the dataset, and the update that is specified in the DiffGram (the change in the employee's first name) is then applied to the Person.Contact table.  
  
> [!NOTE]  
>  In the code, you must provide the name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the connection string.  
  
```  
using System;  
using System.Data;  
using Microsoft.Data.SqlXml;  
using System.IO;  
class Test  
{  
   static string ConnString = "Provider=SQLOLEDB;Server=SqlServerName;database=AdventureWorks;Integrated Security=SSPI;";  
   public static int testParams()  
   {  
      DataRow row;  
      SqlXmlAdapter ad;  
      //need a memory stream to hold diff gram temporarily  
      MemoryStream ms = new MemoryStream();  
      SqlXmlCommand cmd = new SqlXmlCommand(ConnString);  
      cmd.RootTag = "ROOT";  
      cmd.CommandText = "Con";  
      cmd.CommandType = SqlXmlCommandType.XPath;  
      cmd.SchemaPath = "MySchema.xml";  
      //load data set  
      DataSet ds = new DataSet();  
      ad = new SqlXmlAdapter(cmd);  
      ad.Fill(ds);  
      row = ds.Tables["Con"].Rows[0];  
      row["FName"] = "Susan";  
      ad.Update(ds);  
      return 0;  
   }  
   public static int Main(String[] args)  
   {  
      testParams();  
      return 0;  
   }  
}  
```  
  
 **To test the example:**  
  
 To test this example, you must have the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework installed on your computer.  
  
1.  Save this XSD schema (MySchema.xml) in a folder:  
  
    ```  
    <xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
                xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
      <xsd:element name="Con" sql:relation="Person.Contact" >  
       <xsd:complexType>  
         <xsd:sequence>  
            <xsd:element name="FName"    
                         sql:field="FirstName"   
                         type="xsd:string" />   
            <xsd:element name="LName"    
                         sql:field="LastName"    
                         type="xsd:string" />  
         </xsd:sequence>  
         <xsd:attribute name="ContactID" type="xsd:integer" />  
        </xsd:complexType>  
      </xsd:element>  
    </xsd:schema>  
    ```  
  
2.  Save the C# code (DocSample.cs) provided in this example in the same folder in which the schema is stored. (If you store the files in a different folder, you will have to edit the code and specify the appropriate directory path for the mapping schema.)  
  
3.  Compile the code. To compile the code at the command prompt, use:  
  
    ```  
    csc /reference:Microsoft.Data.SqlXML.dll DocSample.cs  
    ```  
  
     This creates an executable (DocSample.exe).  
  
 At the command prompt, execute DocSample.exe.  
  
  
