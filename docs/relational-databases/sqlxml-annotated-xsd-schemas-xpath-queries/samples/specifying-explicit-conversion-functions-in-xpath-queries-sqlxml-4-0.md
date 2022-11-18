---
title: "Use conversion functions in XPath Queries (SQLXML)"
description: Learn how to specify the explicit conversion functions string() and number() in SQLXML 4.0 XPath queries.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "explicit conversion functions [SQLXML]"
  - "string function"
  - "number function"
  - "XPath queries [SQLXML], explicit conversion functions"
ms.assetid: 1111cb5d-2bd9-4bdb-8de2-dc0e47452dd6
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specifying Explicit Conversion Functions in XPath Queries (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  The following examples show how explicit conversion functions are specified in XPath queries. The XPath queries in these examples are specified against the mapping schema contained in SampleSchema1.xml. For information about this sample schema, see [Sample Annotated XSD Schema for XPath Examples &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md).  
  
## Examples  
  
### A. Use the number() explicit conversion function  
 The **number()** function converts an argument to a number.  
  
 Assuming the value of **ContactID** is nonnumeric, the following query converts **ContactID** to a number and compares it with the value 4. The query then returns all **\<Employee>** element children of the context node with the **ContactID** attribute that has a numeric value of 4:  
  
```  
/child::Contact[number(attribute::ContactID)= 4]  
```  
  
 A shortcut to the **attribute** axis (@) can be specified, and because the **child** axis is the default, it can be omitted from the query:  
  
```  
/Contact[number(@ContactID) = 4]  
```  
  
 In relational terms, the query returns an employee with a **ContactID** of 4.  
  
##### To test the XPath query against the mapping schema  
  
1.  Copy the [sample schema code](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md) and paste it into a text file. Save the file as SampleSchema1.xml.  
  
2.  Create the following template (ExplicitConversionA.xml) and save it in the directory where SampleSchema1.xml was saved.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="SampleSchema1.xml">  
        /Contact[number(@ContactID)=4]  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleSchema1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\SampleSchema1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 The result set for this template execution is:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Contact ContactID="4" LastName="Acevedo" FirstName="Humberto" Title="Sr." />   
</ROOT>  
```  
  
### B. Use the string() explicit conversion function  
 The **string()** function converts an argument to a string.  
  
 The following query converts **ContactID** to a string and compares it with the string value "4". The query returns all **\<Employee>** element children of the context node with a **ContactID** with a string value of "4":  
  
```  
/child::Contact[string(attribute::ContactID)="4"]  
```  
  
 A shortcut to the **attribute** axis (@) can be specified, and because the **child** axis is the default, it can be omitted from the query:  
  
```  
/Contact[string(@ContactID)="4"]  
```  
  
 Functionally, this query returns the same results as the previous example query, though the evaluation is done against a string value and not the numeric value (that is, the number 4).  
  
##### To test the XPath query against the mapping schema  
  
1.  Copy the [sample schema code](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md) and paste it into a text file. Save the file as SampleSchema1.xml.  
  
2.  Create the following template (ExplicitConversionB.xml) and save it in the directory where SampleSchema1.xml was saved.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="SampleSchema1.xml">  
        Contact[string(@ContactID)="4"]  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleSchema1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\SampleSchema1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the result set of the template execution:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Contact ContactID="4" LastName="Acevedo" FirstName="Humberto" Title="Sr." />   
</ROOT>  
```  
  
  
