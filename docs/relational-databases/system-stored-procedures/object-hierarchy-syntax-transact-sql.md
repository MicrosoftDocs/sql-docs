---
title: "Object Hierarchy Syntax (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "objects [SQL Server], hierarchy syntax"
ms.assetid: 7ed8df86-9fd2-4e09-96bc-5381fec85f65
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Object Hierarchy Syntax (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The *propertyname* parameter of sp_OAGetProperty and sp_OASetProperty and the *methodname* parameter of sp_OAMethod support an object hierarchy syntax that is similar to that of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]. When this special syntax is used, these parameters have the following general form.  
  
## Syntax  
  
```  
  
'TraversedObject.PropertyOrMethod'  
```  
  
## Arguments  
 *TraversedObject*  
 Is an OLE object in the hierarchy under the *objecttoken* specified in the stored procedure. Use [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] syntax to specify a series of collections, object properties, and methods that return objects. Each object specifier in the series must be separated by a period (.).  
  
 An item in the series can be the name of a collection. Use this syntax to specify a collection:  
  
 Collection("*item*")  
  
 The double quotation marks (") are required. The [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] exclamation point (!) syntax for collections is not supported.  
  
 *PropertyOrMethod*  
 Is the name of a property or method of the *TraversedObject*.  
  
 To specify all index or method parameters by using sp_OAGetProperty, sp_OASetProperty, or sp_OAMethod parameters (including support for sp_OAMethod output parameters), use the following syntax:  
  
 *PropertyOrMethod*  
  
 To specify all index or method parameters inside the parentheses (causing all index or method parameters of sp_OAGetProperty, sp_OASetProperty, or sp_OAMethod to be ignored) use the following syntax:  
  
 *PropertyOrMethod*( [ *ParameterName*:= ] "*parameter*" [ , ... ] )  
  
 The double quotation marks (") are required. All named parameters must be specified after all positional parameters are specified.  
  
## Remarks  
 If *TraversedObject* is not specified, *PropertyOrMethod* is required.  
  
 If *PropertyOrMethod* is not specified, the *TraversedObject* is returned as an object token output parameter from the OLE Automation stored procedure. If *PropertyOrMethod* is specified, the property or method of the *TraversedObject* is called, and the property value or method return value is returned as an output parameter from the OLE Automation stored procedure.  
  
 If any item in the *TraversedObject* list does not return an OLE object, an error is raised.  
  
 For more information about [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] OLE object syntax, see the [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] documentation.  
  
 For more information about HRESULT Return Codes, see [sp_OACreate &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oacreate-transact-sql.md).  
  
## Examples  
 The following are examples of object hierarchy syntax that use a SQL-DMO SQLServer object.  
  
```  
-- Get the AdventureWorks2012 Person.Address Table object.  
EXEC @hr = sp_OAGetProperty @object,  
   'Databases("AdventureWorks2012").Tables("Person.Address")',  
   @table OUT  
  
-- Get the Rows property of the AdventureWorks2012 Person.Address table.  
EXEC @hr = sp_OAGetProperty @object,  
   'Databases("AdventureWorks2012").Tables("Person.Address").Rows',  
   @rows OUT  
  
-- Call the CheckTable method to validate the   
-- AdventureWorks2012 Person.Address table.  
EXEC @hr = sp_OAMethod @object,  
   'Databases("AdventureWorks2012").Tables("Person.Address").CheckTable',  
   @checkoutput OUT  
```  
  
## See Also  
 [OLE Automation Sample Script](../../relational-databases/stored-procedures/ole-automation-sample-script.md)   
 [OLE Automation Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/ole-automation-stored-procedures-transact-sql.md)  
  
  
