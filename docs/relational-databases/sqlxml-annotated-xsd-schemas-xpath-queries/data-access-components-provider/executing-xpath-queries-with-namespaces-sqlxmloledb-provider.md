---
title: "Executing XPath Queries with Namespaces (SQLXMLOLEDB Provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLXMLOLEDB Provider, executing XPath queries"
  - "namespaces property"
  - "queries [SQLXML], SQLXMLOLEDB Provider"
  - "XPath queries [SQLXML], namespaces"
  - "XPath queries [SQLXML], SQLXMLOLEDB Provider"
  - "namespaces [SQLXML], XPath queries"
ms.assetid: 024a4b7d-435d-47ba-9e80-2c2f640108f5
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing XPath Queries with Namespaces (SQLXMLOLEDB Provider)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  XPath queries can include namespaces. If the schema elements are namespace qualified (that is, if they include a target namespace), XPath queries against the schema must specify this namespace.  
  
 Because using the wildcard character (*) is not supported in SQLXML 4.0, you must specify the XPath query by using a namespace prefix. To resolve this prefix, use the namespaces property to specify the namespace binding.  
  
 In the following example, the XPath query specifies namespaces by using the wildcard character (\*) and the local-name() and namespace-uri() XPath functions. This XPath query returns all the elements where the local name is **Contact** and the namespace URI is **urn:myschema:Contacts**.  
  
```  
/*[local-name() = 'Contact' and namespace-uri() = 'urn:myschema:Contacts']  
```  
  
 In SQLXML 4.0, this XPath query must be specified with a namespace prefix. An example is **x:Contact**, where **x** is the namespace prefix. Consider the following XSD schema:  
  
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
  
 Because this schema defines the target namespace, an XPath query (such as "Employee") against the schema must include the namespace.  
  
 This is a sample [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic application that executes an XPath query (x:Employee) against the preceding XSD schema. To resolve the prefix, the namespace binding is specified by using the namespaces property.  
  
> [!NOTE]  
>  In the code, you must provide the name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the connection string. Also, this example specifies the use of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client (SQLNCLI11) for the data provider, which requires additional network client software to be installed. For more information, see [System Requirements for SQL Server Native Client](../../../relational-databases/native-client/system-requirements-for-sql-server-native-client.md).  
  
```  
Option Explicit  
Private Sub Form_Load()  
    Dim con As New ADODB.Connection  
    Dim cmd As New ADODB.Command  
    Dim stm As New ADODB.Stream  
    con.Open "provider=SQLXMLOLEDB.4.0;Data Provider=SQLNCLI11;Data Source=SqlServerName;Initial Catalog=AdventureWorks;Integrated Security=SSPI;"  
    Set cmd.ActiveConnection = con  
    stm.Open  
    cmd.Properties("Output Stream").Value = stm  
    cmd.Properties("Output Encoding") = "utf-8"  
    cmd.Properties("Mapping schema") = "C:\DirectoryPath\con-ex.xml"  
    cmd.Properties("namespaces") = "xmlns:x='urn:myschema:Contacts'"  
    '  Debug.Print "Set Command Dialect to DBGUID_XPATH"  
    cmd.Dialect = "{ec2a4293-e898-11d2-b1b7-00c04f680c56}"  
    cmd.CommandText = "x:Contact"  
    cmd.Execute , , adExecuteStream   
    stm.Position = 0  
    Debug.Print stm.ReadText(adReadAll)  
End Sub  
```  
  
### To test this application  
  
1.  Save the sample XSD schema in a folder.  
  
2.  Create a Visual Basic executable project, and copy the code into it. Change the specified directory path as appropriate.  
  
3.  Add the following project reference:  
  
    ```  
    "Microsoft ActiveX Data Objects 2.8 Library"  
    ```  
  
4.  Execute the application.  
  
 This is the partial result:  
  
```  
<y0:Employee xmlns:y0="urn:myschema:Contacts"   
             LName="Achong" CID="1" FName="Gustavo"/>  
<y0:Employee xmlns:y0="urn:myschema:Employees"   
             LName="Abel" CID="2" FName="Catherine"/>  
```  
  
 The prefixes that are generated in the XML document are arbitrary, but they map to the same namespace.  
  
  
