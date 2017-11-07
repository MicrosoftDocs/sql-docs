---
title: "Handling errors in the TOM API (Analysis Services AMO-TOM) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: ec44daa0-a90e-42ad-b70d-6a7a7a4e4b7b
caps.latest.revision: 4
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Handling errors in the TOM API (Analysis Services AMO-TOM)

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../../includes/ssas-appliesto-sql2016-later-aas.md)]

A common practice for managed libraries like Analysis Services Management Objects (AMO) Tabular Object Model (TOM) is to use exceptions as a mechanism for reporting error conditions to the user.  

When an error is detected in AMO-TOM, besides throwing few standard .NET exceptions like **ArgumentException** and **InvalidOperationException**, TOM also can throw several TOM-specific exceptions.  

TOM exceptions are derived from [AmoException Class](http://msdn.microsoft.com/library/microsoft.analysisservices.amoexception.aspx), covering both AMO- and TOM-specific exceptions. 

To illustrate exception handling in TOM, let’s review one of the more common exceptions, which is [OperationException Class](http://msdn.microsoft.com/library/microsoft.analysisservices.operationexception.aspx).

**OperationException** is thrown when a user initiates an operation on the Analysis Services server and the server fails to perform an operation, either because the action was illegal, or because of another internal or external error. 

When thrown,** OperationException** object will contain a list of XMLA errors returned by the server. 

Note that the server will not accept changes that are invalid. If this occurs, revert the **Model** tree back into the last known good state using the [UndoLocalChanges Method](http://msdn.microsoft.com/library/microsoft.analysisservices.tabular.model.undolocalchanges.aspx), correct the model, then resubmit. 

## Code Example: handle exceptions 
 
```
 try 
 { 
  // Change the Model, for example create a table. 
  // … 
   model.saveChanges(); 
 } 
  catch(operationException ex) 
 { 
  foreach(XmlaError err in ex.Results.OfType<XmlaError>().cast<XmlaError>()) 
  { 
   Console.WriteLine(“Error returned from the server:” + err.Messsage ); 
  } 
 } 
```

## Next steps

Other relevant exceptions include the following:

- [TomInternalException Class](http://msdn.microsoft.com/library/microsoft.analysisservices.tabular.tominternalexception.aspx)
- [TomValidationException Class](http://msdn.microsoft.com/library/microsoft.analysisservices.tabular.tomvalidationexception.aspx)
- [JsonSerializationException class](http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonSerializationException.htm)
