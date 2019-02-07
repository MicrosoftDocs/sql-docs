---
title: "Expression Context and Query Evaluation (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "expression context [XQuery]"
  - "XQuery, expression context"
  - "XQuery, query evaluation"
  - "static context"
  - "dynamic context [XQuery]"
ms.assetid: 5059f858-086a-40d4-811e-81fedaa18b06
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Expression Context and Query Evaluation (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The context of an expression is the information that is used to analyze and evaluate it. Following are the two phases in which XQuery is evaluated:  
  
-   **Static context** - This is the query compilation phase. Based on the information available, errors are sometimes raised during this static analysis of the query.  
  
-   **Dynamic context** - This is the query execution phase. Even if a query has no static errors, such as errors during query compilation, the query may return errors during its execution.  
  
## Static Context  
 Static context initialization refers to the process of putting together all the information for static analysis of the expression. As part of static context initialization, the following is completed:  
  
-   The **boundary white space** policy is set to strip. Therefore, the boundary white space is not preserved by the **any element** and **attribute** constructors in the query. For example:  
  
    ```  
    declare @x xml  
    set @x=''  
    select @x.query('<a>  {"Hello"}  </a>,  
  
        <b> {"Hello2"}  </b>')  
    ```  
  
     This query returns the following result, because the boundary space is stripped away during parsing of the XQuery expression:  
  
    ```  
    <a>Hello</a><b>Hello2</b>  
    ```  
  
-   The prefix and the namespace binding are initialized for the following:  
  
    -   A set of predefined namespaces.  
  
    -   Any namespaces defined using WITH XMLNAMESPACES. For more information, see [Add Namespaces to Queries with WITH XMLNAMESPACES](../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)).  
  
    -   Any namespaces defined in the query prolog. Note that the namespace declarations in the prolog may override the namespace declaration in the WITH XMLNAMESPACES. For example, in the following query, WITH XMLNAMESPACES declares a prefix (pd) that binds it to namespace (`https://someURI`). However, in the WHERE clause, the query prolog overrides the binding.  
  
        ```  
        WITH XMLNAMESPACES ('https://someURI' AS pd)  
        SELECT ProductModelID, CatalogDescription.query('  
            <Product   
                ProductModelID= "{ sql:column("ProductModelID") }"   
                />  
        ') AS Result  
        FROM Production.ProductModel  
        WHERE CatalogDescription.exist('  
            declare namespace  pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
             /pd:ProductDescription[(pd:Specifications)]'  
            ) = 1  
        ```  
  
     All these namespace bindings are resolved during static context initialization.  
  
-   If querying a typed **xml** column or variable, the components of the XML schema collection associated with the column or variable are imported into the static context. For more information, see [Compare Typed XML to Untyped XML](../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).  
  
-   For every atomic type in the imported schemas, a casting function is also made available in the static context. This is illustrated in the following example. In this example, a query is specified against a typed **xml** variable. The XML schema collection associated with this variable defines an atomic type, myType. Corresponding to this type, a casting function, **myType()**, is available during the static analysis. The query expression (`ns:myType(0)`) returns a value of myType.  
  
    ```  
    -- DROP XML SCHEMA COLLECTION SC  
    -- go  
    CREATE XML SCHEMA COLLECTION SC AS '<schema xmlns="http://www.w3.org/2001/XMLSchema"   
    targetNamespace="myNS" xmlns:ns="myNS"  
    xmlns:s="https://schemas.microsoft.com/sqlserver/2004/sqltypes">  
          <import namespace="https://schemas.microsoft.com/sqlserver/2004/sqltypes"/>  
          <simpleType name="myType">  
                <restriction base="int">  
                 <enumeration value="0" />  
                  <enumeration value="1"/>  
                </restriction>  
          </simpleType>  
          <element name="root" type="ns:myType"/>  
    </schema>'  
    go  
  
    DECLARE @var XML(SC)  
    SET @var = '<root xmlns="myNS">0</root>'  
    -- specify myType() casting function in the query  
    SELECT @var.query('declare namespace ns="myNS"; ns:myType(0)')  
    ```  
  
     In the following example, the casting function for the **int** built-in XML type is specified in the expression.  
  
    ```  
    declare @x xml  
    set @x = ''  
    select @x.query('xs:int(5)')  
    go  
    ```  
  
 After the static context is initialized, the query expression is analyzed (compiled). The static analysis involves the following:  
  
1.  Query parsing.  
  
2.  Resolving the function and type names specified in the expression.  
  
3.  Static typing of the query. This makes sure that the query is type safe. For example, the following query returns a static error, because the **+** operator requires numeric primitive type arguments:  
  
    ```  
    declare @x xml  
    set @x=''  
    SELECT @x.query('"x" + 4')  
    ```  
  
     In the following example, the **value()** operator requires a singleton. As specified in the XML schema, there can be multiple \<Elem> elements. Static analysis of the expression determines that it is not type safe and a static error is returned. To resolve the error, the expression must be rewritten to explicitly specify a singleton (`data(/x:Elem)[1]`).  
  
    ```  
    DROP XML SCHEMA COLLECTION SC  
    go  
    CREATE XML SCHEMA COLLECTION SC AS '<schema xmlns="http://www.w3.org/2001/XMLSchema"   
    targetNamespace="myNS" xmlns:ns="myNS"  
    xmlns:s="https://schemas.microsoft.com/sqlserver/2004/sqltypes">  
          <import namespace="https://schemas.microsoft.com/sqlserver/2004/sqltypes"/>  
          <element name="Elem" type="string"/>  
    </schema>'  
    go  
  
    declare @x xml (SC)  
    set @x='<Elem xmlns="myNS">test</Elem><Elem xmlns="myNS">test2</Elem>'  
    SELECT @x.value('declare namespace x="myNS"; data(/x:Elem)[1]','varchar(20)')  
    ```  
  
     For more information, see [XQuery and Static Typing](../xquery/xquery-and-static-typing.md).  
  
### Implementation Restrictions  
 Following are the limitations related to static context:  
  
-   XPath compatibility mode is not supported.  
  
-   For XML construction, only the strip construction mode is supported. This is the default setting. Therefore, the type of the constructed element node is of **xdt:untyped** type and the attributes are of **xdt:untypedAtomic** type.  
  
-   Only ordered ordering mode is supported.  
  
-   Only strip XML space policy is supported.  
  
-   Base URI functionality is not supported.  
  
-   **fn:doc()** is not supported.  
  
-   **fn:collection()** is not supported.  
  
-   XQuery static flagger is not provided.  
  
-   The collation associated with the **xml** data type is used. This collation is always set to the Unicode codepoint collation.  
  
## Dynamic Context  
 Dynamic context refers to the information that must be available at the time the expression is executed. In addition to the static context, the following information is initialized as part of dynamic context:  
  
-   The expression focus, such as the context item, context position, and context size, is initialized as shown in the following. Note that all these values can be overridden by the [nodes() method](../t-sql/xml/nodes-method-xml-data-type.md).  
  
    -   The **xml** data type sets the context item, the node being processed, to the document node.  
  
    -   The context position, the position of the context item relative to the nodes being processed, is first set to 1.  
  
    -   The context size, the number of items in the sequence being processed, is first set to 1, because there is always one document node.  
  
### Implementation Restrictions  
 Following are the limitations related to dynamic context:  
  
-   The **Current date and time** context functions, **fn:current-date**, **fn:current-time**, and **fn:current-dateTime**, are not supported.  
  
-   The **implicit timezone** is fixed to UTC+0 and cannot be changed.  
  
-   The **fn:doc()** function is not supported. All queries are executed against **xml** type columns or variables.  
  
-   The **fn:collection()** function is not supported.  
  
## See Also  
 [XQuery Basics](../xquery/xquery-basics.md)   
 [Compare Typed XML to Untyped XML](../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [XML Schema Collections &#40;SQL Server&#41;](../relational-databases/xml/xml-schema-collections-sql-server.md)  
  
  
