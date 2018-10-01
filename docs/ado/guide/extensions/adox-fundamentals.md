---
title: "ADOX Fundamentals | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "ADOX, fundamentals"
ms.assetid: 954476fc-5f72-4ada-ace5-d9acb27d18f8
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADOX Fundamentals
Microsoft® ActiveX® Data Objects Extensions for Data Definition Language and Security (ADOX) is an extension to the ADO objects and programming model. ADOX includes objects for schema creation and modification, as well as security. Because it is an object-based approach to schema manipulation, you can write code that will work against various data sources regardless of differences in their native syntaxes.  
  
 ADOX is a companion library to the core ADO objects. It exposes additional objects for creating, modifying, and deleting schema objects, such as tables and procedures. It also includes security objects to maintain users and groups and to grant and revoke permissions on objects.  
  
 To use ADOX with your development tool, you should establish a reference to the ADOX type library. The description of the ADOX library is "Microsoft ADO Ext. for DDL and Security." The ADOX library file name is Msadox.dll, and the program ID (ProgID) is "ADOX". For more information about establishing references to libraries, see the documentation of your development tool.  
  
 The Microsoft OLE DB Provider for the Microsoft Jet Database Engine fully supports ADOX. Certain features of ADOX may not be supported, depending on your data provider.  
  
 This document assumes a working knowledge of the Microsoft® Visual Basic® programming language and a general knowledge of ADO. For more information about ADO, see the [ADO Programmer's Guide](../../../ado/guide/ado-programmer-s-guide.md). For more overview information about ADOX, see the following topics:  
  
-   [ADOX Object Model](../../../ado/reference/adox-api/adox-object-model.md)  
  
-   [ADOX Objects](../../../ado/reference/adox-api/adox-objects.md)  
  
-   [ADOX Collections](../../../ado/reference/adox-api/adox-collections.md)  
  
-   [ADOX Properties](../../../ado/reference/adox-api/adox-properties.md)  
  
-   [ADOX Methods](../../../ado/reference/adox-api/adox-methods.md)  
  
-   [ADOX Examples](../../../ado/reference/adox-api/adox-code-examples.md)  
  
## See Also  
 [ADOX API Reference](../../../ado/reference/adox-api/adox-api-reference.md)   
 [ADOX Code Examples](../../../ado/reference/adox-api/adox-code-examples.md)   
 [ADOX Collections](../../../ado/reference/adox-api/adox-collections.md)   
 [ADOX Enumerated Constants](../../../ado/reference/adox-api/adox-enumerated-constants.md)   
 [ADOX Methods](../../../ado/reference/adox-api/adox-methods.md)   
 [ADOX Object Model](../../../ado/reference/adox-api/adox-object-model.md)   
 [ADOX Objects](../../../ado/reference/adox-api/adox-objects.md)   
 [ADOX Properties](../../../ado/reference/adox-api/adox-properties.md)   
 [ADO (Multidimensional) (ADO MD)](../../../ado/guide/multidimensional/ado-multidimensional-ado-md.md)   
 [ADO Programmer's Guide](../../../ado/guide/ado-programmer-s-guide.md)
