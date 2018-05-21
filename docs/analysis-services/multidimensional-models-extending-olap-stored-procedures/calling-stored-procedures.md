---
title: "Calling Stored Procedures | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Calling Stored Procedures
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Stored procedures can be called on the server or from client application. In either case, stored procedures always run on the server, either the context of the server or of a database. There are no special permissions required to execute a stored procedure. Once a stored procedure is added by an assembly to the server or database context, any user can execute the stored procedure as long as the role for the user permits the actions performed by the stored procedure.  
  
 Calling a stored procedure in MDX is done in the same manner as calling an intrinsic MDX function. For a stored procedure that takes no parameters, the name of the procedure and an empty pair of parentheses are used, as shown here:  
  
```  
MyStoredProcedure()  
```  
  
 If the stored procedure takes one or more parameters, then the parameters are supplied, in order, separated by commas. The following example demonstrates a sample stored procedure with three parameters:  
  
```  
MyStoredProcedure("Parameter1", 2, 800)  
```  
  
## Calling Stored Procedures in MDX Queries  
 In all MDX queries, the stored procedure must return the syntactically correct type required by an MDX expression. If a stored procedure does not return the correct type, an MDX error occurs. The following examples demonstrate stored procedures that return a set, a member, and the result of a mathematical operation.  
  
### Returning a Set  
 The following examples implement a stored procedure, called MySproc, that returns a set. In the first example, MySproc returns the set directly in the SELECT expression. In the second two examples, MySproc returns the set as an argument for the Crossjoin and DrilldownLevel functions.  
  
```  
SELECT MySetProcedure(a,b,c) ON 0 FROM Sales  
SELECT Crossjoin(MySetProcedure(a,b,c)) ON 0 FROM Sales  
SELECT DrilldownLevel(MySetProcedure(a,b,c)) ON 0 FROM Sales  
```  
  
### Returning a Member  
 The following example shows a function MySproc function that returns a member:  
  
```  
SELECT Descendants(MySproc(a,b,c),3) ON 0 FROM Sales  
```  
  
### Returning the Result of a Math Operation  
  
```  
SELECT Country.Members on 0, MySproc(Measures.Sales) ON 1 FROM Sales  
```  
  
## Calling Stored Procedures with the Call Statement  
 Stored procedures can be called outside of the context of an MDX query using the MDX **Call** statement.  
  
 You can use this method to either instantiate the side effects of a stored query or for the application to get the results of a stored query. A common use of the **Call** statement would be to use Analysis Management Objects (AMO) to perform administrative functions that do not have a return result. For example, the following command calls a stored procedure:  
  
```  
Call MyStoredProcedure(a,b,c)  
```  
  
 The only supported type returned from stored procedure in a **Call** statement is a rowset. The serialization for a rowset is defined by XML for Analysis. If a stored procedure in a **Call** statement returns any other type, it is ignored and not returned in XML to the calling application. For more information about XML for Analysis rowsets, see, XML for Analysis Schema Rowsets.  
  
 If a stored procedure returns a .NET rowset, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] converts the result on the server to an XML for Analysis rowset. The XML for Analysis rowset is always returned by a stored procedure in the **Call** function. If a dataset contains features that cannot be expressed in the XML for Analysis rowset, a failure results.  
  
 Procedures that return void values (for example, subroutines in Visual Basic) can also be employed with the CALL keyword. If, for example, you wanted to use the function MyVoidFunction() in an MDX statement, the following syntax would be employed:  
  
```  
CALL(MyVoidFunction)  
```  
  
## See Also  
 [Multidimensional Model Assemblies Management](../../analysis-services/multidimensional-models/multidimensional-model-assemblies-management.md)   
 [Defining Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)  
  
  
