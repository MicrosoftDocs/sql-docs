---
title: "Conditional Expressions (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "XQuery, conditional expressions"
  - "expressions [XQuery], conditional"
  - "effective Boolean value [XQuery]"
  - "if-then-else statement [XQuery]"
  - "conditional expressions [XQuery]"
  - "EBV"
ms.assetid: b280dd96-c80f-4c51-bc06-a88d42174acb
author: rothja
ms.author: jroth
manager: craigg
---
# Conditional Expressions (XQuery)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  XQuery supports the following conditional **if-then-else** statement:  
  
```  
if (<expression1>)  
then  
  <expression2>  
else  
  <expression3>  
```  
  
 Depending on the effective Boolean value of `expression1`, either `expression2` or `expression3` is evaluated. For example:  
  
-   If the test expression, `expression1`, results in an empty sequence, the result is False.  
  
-   If the test expression, `expression1`, results in a simple Boolean value, this value is the result of the expression.  
  
-   If the test expression, `expression1`, results in a sequence of one or more nodes, the result of the expression is True.  
  
-   Otherwise, a static error is raised.  
  
 Also note the following:  
  
-   The test expression must be enclosed between parentheses.  
  
-   The **else** expression is required. If you do not need it, you can return " ( ) ", as illustrated in the examples in this topic.  
  
 For example, the following query is specified against the **xml** type variable. The **if** condition tests the value of the SQL variable (@v) inside the XQuery expression by using the [sql:variable() function](../xquery/xquery-extension-functions-sql-variable.md) extension function. If the variable value is "FirstName", it returns the <`FirstName`> element. Otherwise, it returns the <`LastName`> element.  
  
```  
declare @x xml  
declare @v varchar(20)  
set @v='FirstName'  
set @x='  
<ROOT rootID="2">  
  <FirstName>fname</FirstName>  
  <LastName>lname</LastName>  
</ROOT>'  
SELECT @x.query('  
if ( sql:variable("@v")="FirstName" ) then  
  /ROOT/FirstName  
 else  
   /ROOT/LastName  
')  
```  
  
 This is the result:  
  
```  
<FirstName>fname</FirstName>  
```  
  
 The following query retrieves the first two feature descriptions from the product catalog description of a specific product model. If there are more features in the document, it adds a <`there-is-more`> element with empty content.  
  
```  
SELECT CatalogDescription.query('  
     declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     <Product>   
          { /p1:ProductDescription/@ProductModelID }  
          { /p1:ProductDescription/@ProductModelName }   
          {  
            for $f in /p1:ProductDescription/p1:Features/*[position()\<=2]  
            return  
            $f   
          }  
          {  
            if (count(/p1:ProductDescription/p1:Features/*) > 2)  
            then \<there-is-more/>  
            else ()  
          }   
     </Product>          
') as x  
FROM Production.ProductModel  
WHERE ProductModelID = 19  
```  
  
 In the previous query, the condition in the **if** expression checks whether there are more than two child elements in <`Features`>. If yes, it returns the `\<there-is-more/>` element in the result.  
  
 This is the result:  
  
```  
<Product ProductModelID="19" ProductModelName="Mountain 100">  
  \<p1:Warranty xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
    \<p1:WarrantyPeriod>3 years\</p1:WarrantyPeriod>  
    \<p1:Description>parts and labor\</p1:Description>  
  \</p1:Warranty>  
  \<p2:Maintenance xmlns:p2="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
    \<p2:NoOfYears>10 years\</p2:NoOfYears>  
    \<p2:Description>maintenance contract available through your dealer or any AdventureWorks retail store.\</p2:Description>  
  \</p2:Maintenance>  
  \<there-is-more />  
</Product>  
```  
  
 In the following query, a <`Location`> element with a LocationID attribute is returned if the work center location does not specify the setup hours.  
  
```  
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        for $WC in //AWMI:root/AWMI:Location  
        return  
        if ( $WC[not(@SetupHours)] )  
        then  
          <WorkCenterLocation>  
             { $WC/@LocationID }   
          </WorkCenterLocation>  
         else  
           ()  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 This is the result:  
  
```  
<WorkCenterLocation LocationID="30" />  
<WorkCenterLocation LocationID="45" />  
<WorkCenterLocation LocationID="60" />  
```  
  
 This query can be written without the **if** clause, as shown in the following example:  
  
```  
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
for $WC in //AWMI:root/AWMI:Location[not(@SetupHours)]   
        return  
          <Location>  
             { $WC/@LocationID }   
          </Location>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
## See Also  
 [XQuery Expressions](../xquery/xquery-expressions.md)  
  
  
