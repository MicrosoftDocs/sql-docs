---
title: "FOR Clause (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/08/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FOR"
  - "FOR CLAUSE"
  - "FOR_TSQL"
  - "FOR_CLAUSE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "XML option [SQL Server]"
  - "BROWSE option"
  - "FOR clause [Transact-SQL]"
ms.assetid: 08a6f084-8f73-4f2a-bae4-3c7513dc99b9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SELECT - FOR Clause (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Use the FOR clause to specify one of the following options for query results.
  
-   Allow updates while viewing query results in a browse mode cursor by specifying **FOR BROWSE**.  
  
-   Format query results as XML by specifying **FOR XML**.  
  
-   Format query results as JSON by specifying **FOR JSON**.  

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```
[ FOR { BROWSE | <XML> | <JSON>} ]  
  
<XML> ::=  
XML   
{   
    { RAW [ ( 'ElementName' ) ] | AUTO }   
    [   
        <CommonDirectivesForXML>   
        [ , { XMLDATA | XMLSCHEMA [ ( 'TargetNameSpaceURI' ) ] } ]   
        [ , ELEMENTS [ XSINIL | ABSENT ]   
    ]  
  | EXPLICIT   
    [   
        <CommonDirectivesForXML>   
        [ , XMLDATA ]   
    ]  
  | PATH [ ( 'ElementName' ) ]   
    [  
        <CommonDirectivesForXML>   
        [ , ELEMENTS [ XSINIL | ABSENT ] ]  
    ]  
}   
  
<CommonDirectivesForXML> ::=   
[ , BINARY BASE64 ]  
[ , TYPE ]  
[ , ROOT [ ( 'RootName' ) ] ]  
  
<JSON> ::=  
JSON   
{   
    { AUTO | PATH }   
    [   
        [ , ROOT [ ( 'RootName' ) ] ]  
        [ , INCLUDE_NULL_VALUES ]  
        [ , WITHOUT_ARRAY_WRAPPER ]  
    ]  
  
}
```
  
## FOR BROWSE

 BROWSE  
 Specifies that updates be allowed while viewing the data in a DB-Library browse mode cursor. A table can be browsed in an application if the table includes a **timestamp** column, the table has a unique index, and the FOR BROWSE option is at the end of the SELECT statements sent to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]
> You cannot use the \<lock_hint> HOLDLOCK in a SELECT statement that includes the FOR BROWSE option.
  
 FOR BROWSE cannot appear in SELECT statements that are joined by the UNION operator.  
  
> [!NOTE]
> When the unique index key columns of a table are nullable, and the table is on the inner side of an outer join, the index is not supported by browse mode.  
  
 The browse mode lets you scan the rows in your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and update the data in your table one row at a time. To access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table in your application in the browse mode, you must use one of the following two options:  
  
-   The SELECT statement that you use to access the data from your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table must end with the keywords **FOR BROWSE**. When you turn on the **FOR BROWSE** option to use browse mode, temporary tables are created.  
  
-   You must run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement to turn on the browse mode by using the **NO_BROWSETABLE** option:  
  
    ```sql
    SET NO_BROWSETABLE ON  
    ```  
  
     When you turn on the **NO_BROWSETABLE** option, all the SELECT statements behave as if the **FOR BROWSE** option is appended to the statements. However, the **NO_BROWSETABLE** option does not create the temporary tables that the **FOR BROWSE** option generally uses to send the results to your application.  
  
 When you try to access the data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables in browse mode by using a SELECT query that involves an outer join statement, and when a unique index is defined on the table that is present on the inner side of an outer join statement, the browse mode does not support the unique index. The browse mode supports the unique index only when all the unique index key columns can accept null values. The browse mode does not support the unique index if the following conditions are true:  
  
-   You try to access the data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables in browse mode by using a SELECT query that involves an outer join statement.  
  
-   A unique index is defined on the table that is present on the inner side of an outer join statement.  
  
 To reproduce this behavior in the browse mode, follow these steps:  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], create a database, named SampleDB.  
  
2.  In the SampleDB database, create a tleft table and a tright table that both contain a single column that is named c1. Define a unique index on the c1 column in the tleft table, and set the column to accept null values. To do this, run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in an appropriate query window:  
  
    ```sql
    CREATE TABLE tleft(c1 INT NULL UNIQUE) ;  
    GO   
    CREATE TABLE tright(c1 INT NULL) ;  
    GO  
    ```  
  
3.  Insert several values in the tleft table and the tright table. Make sure that you insert a null value in the tleft table. To do this, run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in the query window:  
  
    ```sql
    INSERT INTO tleft VALUES(2) ;  
    INSERT INTO tleft VALUES(NULL) ;  
    INSERT INTO tright VALUES(1) ;  
    INSERT INTO tright VALUES(3) ;  
    INSERT INTO tright VALUES(NULL) ;  
    GO  
    ```  
  
4.  Turn on the **NO_BROWSETABLE** option. To do this, run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in the query window:  
  
    ```sql
    SET NO_BROWSETABLE ON ;  
    GO  
    ```  
  
5.  Access the data in the tleft table and the tright table by using an outer join statement in the SELECT query. Make sure that the tleft table is on the inner side of the outer join statement. To do this, run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in the query window:  
  
    ```sql
    SELECT tleft.c1   
    FROM tleft   
    RIGHT JOIN tright   
    ON tleft.c1 = tright.c1   
    WHERE tright.c1 <> 2 ;
    ```  
  
     Notice the following output in the Results pane:  
  
     c1  
  
     ---\-  
  
     NULL  
  
     NULL  
  
 After you run the SELECT query to access the tables in the browse mode, the result set of the SELECT query contains two null values for the c1 column in the tleft table because of the definition of the right outer join statement. Therefore, in the result set, you cannot distinguish between the null values that came from the table and the null values that the right outer join statement introduced. You might receive incorrect results if you must ignore the null values from the result set.  
  
> [!NOTE]
> If the columns that are included in the unique index do not accept null values, all the null values in the result set were introduced by the right outer join statement.  
  
## FOR XML

 XML  
 Specifies that the results of a query are to be returned as an XML document. One of the following XML modes must be specified: RAW, AUTO, EXPLICIT. For more information about XML data and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [FOR XML &#40;SQL Server&#41;](../../relational-databases/xml/for-xml-sql-server.md).  
  
 RAW [ **('**_ElementName_**')** ]  
 Takes the query result and transforms each row in the result set into an XML element with a generic identifier \<row /> as the element tag. You can optionally specify a name for the row element. The resulting XML output uses the specified *ElementName* as the row element generated for each row. For more information, see [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md).
  
 AUTO  
 Returns query results in a simple, nested XML tree. Each table in the FROM clause, for which at least one column is listed in the SELECT clause, is represented as an XML element. The columns listed in the SELECT clause are mapped to the appropriate element attributes. For more information, see [Use AUTO Mode with FOR XML](../../relational-databases/xml/use-auto-mode-with-for-xml.md).  
  
 EXPLICIT  
 Specifies that the shape of the resulting XML tree is defined explicitly. Using this mode, queries must be written in a particular way so that additional information about the desired nesting is specified explicitly. For more information, see [Use EXPLICIT Mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md).  
  
 XMLDATA  
 Returns inline XDR schema, but does not add the root element to the result. If XMLDATA is specified, XDR schema is appended to the document.  
  
> [!IMPORTANT]
> The XMLDATA directive is **deprecated**. Use XSD generation in the case of RAW and AUTO modes. There is no replacement for the XMLDATA directive in EXPLICIT mode. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  

_Suppress unwanted line breaks:_ You might use SQL Server Management Studio (SSMS) to issue a query that uses the FOR XML clause. Sometimes a large amount of XML is returned and displayed in one grid cell. The XML string could be longer than one SSMS grid cell can hold on a single line. In these cases, SSMS might insert line break characters between long segments of the whole XML string. Such line breaks might occur in the middle of a substring that should not be split across lines. You can prevent the line breaks by using a cast AS XMLDATA. This solution can also apply when you use FOR JSON PATH. The technique is discussed on Stack Overflow, and is shown in the following Transact-SQL sample SELECT statement:

- [Using SQL Server FOR XML: Convert Result Datatype to Text/varchar/string whatever?](https://stackoverflow.com/questions/5655332/using-sql-server-for-xml-convert-result-datatype-to-text-varchar-string-whate/5658758#5658758)

    ```sql
    SELECT CAST(
        (SELECT column1, column2
            FROM my_table
            FOR XML PATH('')
        )
            AS VARCHAR(MAX)
    ) AS XMLDATA ;
    ```

<!-- The preceding Stack Overflow example is per MicrosoftDocs/sql-docs Issue 1501.  2019-01-06 -->

 XMLSCHEMA [ **('**_TargetNameSpaceURI_**')** ]  
 Returns inline XSD schema. You can optionally specify a target namespace URI when you specify this directive, which returns the specified namespace in the schema. For more information, see [Generate an Inline XSD Schema](../../relational-databases/xml/generate-an-inline-xsd-schema.md).  
  
 ELEMENTS  
 Specifies that the columns are returned as subelements. Otherwise, they are mapped to XML attributes. This option is supported in RAW, AUTO and PATH modes only. For more information, see [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md).  
  
 XSINIL  
 Specifies that an element with **xsi:nil** attribute set to **True** be created for NULL column values. This option can only be specified with ELEMENTS directive. For more information, see [Generate Elements for NULL Values with the XSINIL Parameter](../../relational-databases/xml/generate-elements-for-null-values-with-the-xsinil-parameter.md).  
  
 ABSENT  
 Indicates that for null column values, corresponding XML elements will not be added in the XML result. Specify this option only with ELEMENTS.  
  
 PATH [ **('**_ElementName_**')** ]  
 Generates a \<row> element wrapper for each row in the result set. You can optionally specify an element name for the \<row> element wrapper. If an empty string is provided, such as FOR XML PATH (**''**) ), a wrapper element is not generated. Using PATH may provide a simpler alternative to queries written using the EXPLICIT directive. For more information, see [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md).  
  
 BINARY BASE64  
 Specifies that the query returns the binary data in binary base64-encoded format. When you retrieve binary data by using RAW and EXPLICIT mode, this option must be specified. This is the default in AUTO mode.  
  
 TYPE  
 Specifies that the query returns results as **xml** type. For more information, see [TYPE Directive in FOR XML Queries](../../relational-databases/xml/type-directive-in-for-xml-queries.md).  
  
 ROOT [ **('**_RootName_**')** ]  
 Specifies that a single top-level element be added to the resulting XML. You can optionally specify the root element name to generate. If the optional root name is not specified, the default \<root> element is added.  
  
 For more info, see [FOR XML &#40;SQL Server&#41;](../../relational-databases/xml/for-xml-sql-server.md).  
  
 **FOR XML Example**  
  
 The following example specifies `FOR XML AUTO` with the `TYPE` and `XMLSCHEMA` options. Because of the `TYPE` option, the result set is returned to the client as an **xml** type. The `XMLSCHEMA` option specifies that the inline XSD schema is included in the XML data returned, and the `ELEMENTS` option specifies that the XML result is element-centric.  
  
```sql
USE AdventureWorks2012;  
GO  
SELECT p.BusinessEntityID, FirstName, LastName, PhoneNumber AS Phone  
FROM Person.Person AS p  
JOIN Person.PersonPhone AS pph ON p.BusinessEntityID  = pph.BusinessEntityID  
WHERE LastName LIKE 'G%'  
ORDER BY LastName, FirstName   
FOR XML AUTO, TYPE, XMLSCHEMA, ELEMENTS XSINIL;  
```  
  
## FOR JSON

 JSON  
 Specify FOR JSON to return the results of a query formatted as JSON text. You also have to specify one of the following JSON modes : AUTO or PATH. For more information about the **FOR JSON** clause, see [Format Query Results as JSON with FOR JSON &#40;SQL Server&#41;](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md).  
  
 AUTO  
 Format the JSON output automatically based on the structure of the **SELECT** statement  
            by specifying **FOR JSON AUTO**. For more info and examples, see [Format JSON Output Automatically with AUTO Mode &#40;SQL Server&#41;](../../relational-databases/json/format-json-output-automatically-with-auto-mode-sql-server.md).  
  
 PATH  
 Get full control over the format of the JSON output by specifying   
            **FOR JSON PATH**. **PATH** mode lets you create wrapper objects and nest complex properties. For more info and examples, see [Format Nested JSON Output with PATH Mode &#40;SQL Server&#41;](../../relational-databases/json/format-nested-json-output-with-path-mode-sql-server.md).  
  
 INCLUDE_NULL_VALUES  
 Include null values in the JSON output by specifying the **INCLUDE_NULL_VALUES** option with the **FOR JSON** clause. If you don't specify this option, the output does not include JSON properties for null values in the query results. For more info and examples, see [Include Null Values in JSON Output with the INCLUDE_NULL_VALUES Option &#40;SQL Server&#41;](../../relational-databases/json/include-null-values-in-json-include-null-values-option.md).  
  
 ROOT [ **('**_RootName_**')** ]  
 Add a single, top-level element to the JSON output by specifying the **ROOT** option with the **FOR JSON** clause. If you don't specify the **ROOT** option, the JSON output doesn't have a root element. For more info and examples, see [Add a Root Node to JSON Output with the ROOT Option &#40;SQL Server&#41;](../../relational-databases/json/add-a-root-node-to-json-output-with-the-root-option-sql-server.md).  
  
 WITHOUT_ARRAY_WRAPPER  
 Remove the square brackets that surround the JSON output by default by specifying the **WITHOUT_ARRAY_WRAPPER** option with the **FOR JSON** clause. If you don't specify this option, the JSON output is enclosed within square brackets. Use the **WITHOUT_ARRAY_WRAPPER** option to generate a single JSON object as output.  For more info, see [Remove Square Brackets from JSON Output with the WITHOUT_ARRAY_WRAPPER Option &#40;SQL Server&#41;](../../relational-databases/json/remove-square-brackets-from-json-without-array-wrapper-option.md).  
  
 For more info, see [Format Query Results as JSON with FOR JSON &#40;SQL Server&#41;](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md).  
  
## See Also

 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)

