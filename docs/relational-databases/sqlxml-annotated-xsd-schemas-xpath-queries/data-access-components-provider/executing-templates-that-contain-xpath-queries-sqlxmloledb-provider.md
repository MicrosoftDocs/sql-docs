---
title: "Execute templates containing XPath Queries (SQLXMLOLEDB)"
description: View an example of an ADO application that uses the SQLXMLOLEDB Provider to execute a template containing XPath queries.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "SQLXMLOLEDB Provider, executing template files"
  - "Base Path property"
  - "templates [SQLXML], XPath queries"
  - "XPath queries [SQLXML], templates"
  - "XPath queries [SQLXML], SQLXMLOLEDB Provider"
  - "Mapping Schema property"
  - "XML templates [SQLXML]"
ms.assetid: 7368c188-607e-459e-8254-8f23352dfa01
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing Templates That Contain XPath Queries (SQLXMLOLEDB Provider)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  This example shows how to use the following SQLXMLOLEDB Provider-specific properties:  
  
-   ClientSideXML  
  
-   Base Path  
  
-   Mapping Schema  
  
 In this sample ADO application, an XML template that consists of an XPath query (root) is specified against the XSD mapping schema (MySchema.xml) that is described in [Executing XPath Queries &#40;SQLXMLOLEDB Provider&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/data-access-components-provider/executing-xpath-queries-sqlxmloledb-provider.md).  
  
 The Mapping Schema property provides the XSD mapping schema against which the XPath query is executed. The Base Path property provides the file path to the mapping schema.  
  
 The ClientSideXML property is set to True. Therefore, the XML document is generated on the client.  
  
 In the application, an XPath query is specified directly. Therefore, the dialect {5d531cb2-e6ed-11d2-b252-00c04f681b71} must be included.  
  
> [!NOTE]  
>  In the code, you must provide the name of the instance of Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the connection string. Also, this example specifies the use of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client (SQLNCLI11) for the data provider which requires additional network client software to be installed. For more information, see [System Requirements for SQL Server Native Client](../../../relational-databases/native-client/system-requirements-for-sql-server-native-client.md).  
  
```  
Option Explicit  
Sub Main()  
  
   Dim oTestStream As New ADODB.Stream  
   Dim oTestConnection As New ADODB.Connection  
   Dim oTestCommand As New ADODB.Command  
  
   oTestConnection.Open "provider=SQLXMLOLEDB.4.0;data provider=SQLNCLI11;data source=SqlServerName;initial catalog=AdventureWorks;Integrated Security=SSPI;"  
  
   oTestCommand.ActiveConnection = oTestConnection  
   oTestCommand.Properties("ClientSideXML") = "False"  
  
   oTestCommand.CommandText = "<ROOT xmlns:sql='urn:schemas-microsoft-com:xml-sql'> " & _  
        " <sql:xpath-query mapping-schema='mySchema.xml' > " & _  
        "   root " & _  
        "   </sql:xpath-query> " & _  
        " </ROOT> "  
   oTestStream.Open  
   ' You need the dialect if you are executing a template.  
   oTestCommand.Dialect = "{5d531cb2-e6ed-11d2-b252-00c04f681b71}"  
   oTestCommand.Properties("Output Stream").Value = oTestStream  
   oTestCommand.Properties("Base Path").Value = "c:\Schemas\SQLXML4\TemplateWithXPath\"  
   oTestCommand.Properties("Mapping Schema").Value = "mySchema.xml"  
   oTestCommand.Properties("Output Encoding") = "utf-8"  
   oTestCommand.Execute , , adExecuteStream  
   oTestStream.Position = 0  
   oTestStream.Charset = "utf-8"  
   Debug.Print oTestStream.ReadText(adReadAll)  
  
End Sub  
  
Sub Form_Load()  
   Main  
End Sub  
```  
  
 This is the schema:  
  
```  
<xsd:schema xmlns:xsd='http://www.w3.org/2001/XMLSchema'  
   xmlns:sql='urn:schemas-microsoft-com:mapping-schema'>  
 <xsd:element name= 'root' sql:is-constant='1'>   
    <xsd:complexType>  
       <xsd:sequence>  
         <xsd:element ref = 'Contact'/>  
       </xsd:sequence>  
    </xsd:complexType>  
  </xsd:element>  
  
  <xsd:element name='Contact' sql:relation='Person.Contact'>  
     <xsd:complexType>  
          <xsd:attribute name='ContactID' type='xsd:integer' />  
          <xsd:attribute name='FirstName' type='xsd:string'/>   
          <xsd:attribute name='LastName' type='xsd:string' />   
     </xsd:complexType>  
   </xsd:element>  
</xsd:schema>  
```  
  
  
