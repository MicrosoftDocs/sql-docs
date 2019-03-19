---
title: "Handling Namespaces in XQuery | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "declaring namespaces"
  - "namespaces [XQuery]"
  - "XQuery, namespaces"
ms.assetid: 542b63da-4d3d-4ad5-acea-f577730688f1
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Handling Namespaces in XQuery
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  This topic provides samples for handling namespaces in queries.  
  
## Examples  
  
### A. Declaring a namespace  
 The following query retrieves the manufacturing steps of a specific product model.  
  
```  
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        /AWMI:root/AWMI:Location[1]/AWMI:step  
    ') as x  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 This is the partial result:  
  
```  
<AWMI:step xmlns:AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">Insert <AWMI:material>aluminum sheet MS-2341</AWMI:material> into the <AWMI:tool>T-85A framing tool</AWMI:tool>. </AWMI:step>  
...  
```  
  
 Note that the **namespace** keyword is used to define a new namespace prefix, "AWMI:". This prefix then must be used in the query for all elements that fall within the scope of that namespace.  
  
### B. Declaring a default namespace  
 In the previous query, a new namespace prefix was defined. That prefix then had to be used in the query to select the intended XML structures. Alternatively, you can declare a namespace as the default namespace, as shown in the following modified query:  
  
```  
SELECT Instructions.query('  
     declare default element namespace "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        /root/Location[1]/step  
    ') as x  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 This is the result  
  
```  
<step xmlns="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">Insert <material>aluminum sheet MS-2341</material> into the <tool>T-85A framing tool</tool>. </step>  
...  
```  
  
 Note in this example that the namespace defined, `"https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"`, is made to override the default, or empty, namespace. Because of this, you no longer have a namespace prefix in the path expression that is used to query. You also no longer have a namespace prefix in the element names that appear in the results. Additionally, the default namespace is applied to all elements, but not to their attributes.  
  
### C. Using namespaces in XML construction  
 When you define new namespaces, they are brought into scope not only for the query, but for the construction. For example, in constructing XML, you can define a new namespace by using the "`declare namespace ...`" declaration and then use that namespace with any elements and attributes that you construct to appear within the query results.  
  
```  
SELECT CatalogDescription.query('  
     declare default element namespace "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     declare namespace myNS="uri:SomeNamespace";<myNS:Result>  
          { /ProductDescription/Summary }  
       </myNS:Result>  
  
    ') as Result  
FROM Production.ProductModel  
where ProductModelID=19  
```  
  
 This is the result:  
  
```  
  
      <myNS:Result xmlns:myNS="uri:SomeNamespace">  
  <Summary xmlns="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">  
   <p1:p xmlns:p1="http://www.w3.org/1999/xhtml">  
     Our top-of-the-line competition mountain bike. Performance-enhancing   
     options include the innovative HL Frame, super-smooth front   
     suspension, and traction for all terrain.</p1:p>  
  </Summary>  
</myNS:Result>  
```  
  
 Alternatively, you can also define the namespace explicitly at each point where it is used as part of the XML construction, as shown in the following query:  
  
```  
SELECT CatalogDescription.query('  
     declare default element namespace "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
       <myNS:Result xmlns:myNS="uri:SomeNamespace">  
          { /ProductDescription/Summary }  
       </myNS:Result>  
    ') as Result  
FROM Production.ProductModel  
where ProductModelID=19  
```  
  
### D. Construction using default namespaces  
 You can also define a default namespace for use in constructed XML. For example, the following query shows how you can specify a default namespace,  "uri:SomeNamespace"\\, to use as the default for the locally named elements that are constructed, such as the `<Result>` element.  
  
```  
SELECT CatalogDescription.query('  
      declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
      declare default element namespace "uri:SomeNamespace";<Result>  
          { /PD:ProductDescription/PD:Summary }  
       </Result>  
  
    ') as Result  
FROM Production.ProductModel  
where ProductModelID=19  
```  
  
 This is the result:  
  
```  
  
      <Result xmlns="uri:SomeNamespace">  
  <PD:Summary xmlns:PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">  
   <p1:p xmlns:p1="http://www.w3.org/1999/xhtml">  
         Our top-of-the-line competition mountain bike. Performance-  
         enhancing options include the innovative HL Frame, super-smooth   
         front suspension, and traction for all terrain.</p1:p>  
  </PD:Summary>  
</Result>  
```  
  
 Note that by overriding the default element namespace or empty namespace, all the locally named elements in the constructed XML are subsequently bound to the overriding default namespace. Therefore, if you require flexibility in constructing XML to take advantage of the empty namespace, do not override the default element namespace.  
  
## See Also  
 [Add Namespaces to Queries with WITH XMLNAMESPACES](../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)   
 [XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)   
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)  
  
  
