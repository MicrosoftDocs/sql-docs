---
title: "ADO and WFC Programming | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "ADO, ADO/WFC"
  - "ADO/WFC [ADO]"
ms.assetid: 1fdfa42e-897e-4770-b320-ab3720adabcc
caps.latest.revision: 9
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# ADO and WFC Programming
For Microsoft Visual J++ 6.0, ADO has been extended to work with Windows Foundation Classes (WFC) in the following ways. First, a set of Java classes has been implemented that extends the ADO interfaces and creates notifications interesting to the Java programmer; the Java classes also expose functions that return Java types to the user. To improve performance, the Java class directly accesses the native data types in the OLE DB rowset object, and returns them to the Java programmer as Java types without first converting them to and from a variant. ADO has also been extended to work with event notifications in the WFC framework.  
  
 ADO for Windows Foundation Classes (ADO/WFC) supports all the standard ADO methods, properties, objects, and events. However, operations that require a variant as a parameter and show excellent performance in a language such as Microsoft Visual Basic, display lesser performance in a language such as Visual J++. For that reason, ADO/WFC also provides accessor functions on the [Field](../../../ado/reference/ado-api/field-object.md) object that take native Java data types instead of the variant data type.  
  
 For more detailed information within the ADO documentation about ADO/WFC packages, see [the ADO/WFC Syntax Index](../../../ado/reference/ado-api/ado-wfc-syntax-index.md).  
  
 For related information within the Visual J++ documentation, see [Converting Visual Basic ADO Examples to Visual J++](http://go.microsoft.com/fwlink/?LinkId=5684).  
  
## Referencing the Library  
 To import the ADO/WFC data classes into your project, include the following line in your code:  
  
```  
import com.ms.wfc.data.*;  
```  
  
## See Also  
 [ADO Event Instantiation by Language](../../../ado/guide/data/ado-event-instantiation-by-language.md)   
 [ADO - WFC Syntax Index](../../../ado/reference/ado-api/ado-wfc-syntax-index.md)   
 [Using the Java Type Library Wizard](../../../ado/guide/appendixes/using-the-java-type-library-wizard.md)   
 [Using the Microsoft SDK for Java](../../../ado/guide/appendixes/using-the-microsoft-sdk-for-java.md)