---
title: "Specifying XPath Variables in XPath Queries (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XPath queries [SQLXML], XPath variables"
  - "XPath variables [SQLXML]"
ms.assetid: c11ab816-11b8-4131-8b77-c03fe500fa10
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specifying XPath Variables in XPath Queries (SQLXML 4.0)
  The following examples show how XPath variables are passed in XPath queries. The XPath queries in these examples are specified against the mapping schema contained in SampleSchema1.xml. For information about this sample schema, see [Sample Annotated XSD Schema for XPath Examples &#40;SQLXML 4.0&#41;](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md).  
  
## Examples  
  
### A. Use the XPath variables  
 A sample template consists of two XPath queries. Each of the XPath queries takes one parameter. The template also specifies default values for these parameters. The default values are used if parameter values are not specified. Two parameters with default values are specified in **\<sql:header>**.  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:header>  
     <sql:param name='CustomerID'>1</sql:param>  
     <sql:param name='ContactID'>1</sql:param>   
  </sql:header>  
  <sql:xpath-query mapping-schema="SampleSchema1.xml">  
    Customer[@CustomerID=$CustomerID]   
  </sql:xpath-query >  
  <sql:xpath-query mapping-schema="SampleSchema1.xml">  
   Contact[@ContactID=$ContactID]   
  </sql:xpath-query>  
</ROOT>  
```  
  
##### To test the XPath query against the mapping schema  
  
1.  Copy the [sample schema code](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md) and paste it into a text file. Save the file as SampleSchema1.xml.  
  
2.  Create the following template (XPathVariables.xml) and save it in the directory where:  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:header>  
         <sql:param name='CustomerID'>1</sql:param>  
         <sql:param name='ContactID'>1</sql:param>   
      </sql:header>  
      <sql:xpath-query mapping-schema="SampleSchema1.xml">  
        Customer[@CustomerID=$CustomerID]   
      </sql:xpath-query >  
      <sql:xpath-query mapping-schema="SampleSchema1.xml">  
       Contact[@ContactID=$ContactID]   
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleSchema1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\SampleSchema1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template. For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
> [!NOTE]  
>  In this example, no parameters are passed. Therefore, the default parameter values are used.  
  
  
