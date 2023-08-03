---
title: "ADOX Fundamentals"
description: "ADOX Fundamentals"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADOX, fundamentals"
---
# ADOX Fundamentals
Microsoft® ActiveX® Data Objects Extensions for Data Definition Language and Security (ADOX) is an extension to the ADO objects and programming model. ADOX includes objects for schema creation and modification, as well as security. Because it is an object-based approach to schema manipulation, you can write code that will work against various data sources regardless of differences in their native syntaxes.  
  
 ADOX is a companion library to the core ADO objects. It exposes additional objects for creating, modifying, and deleting schema objects, such as tables and procedures. It also includes security objects to maintain users and groups and to grant and revoke permissions on objects.  
  
 To use ADOX with your development tool, you should establish a reference to the ADOX type library. The description of the ADOX library is "Microsoft ADO Ext. for DDL and Security." The ADOX library file name is Msadox.dll, and the program ID (ProgID) is "ADOX". For more information about establishing references to libraries, see the documentation of your development tool.  
  
 The Microsoft OLE DB Provider for the Microsoft Jet Database Engine fully supports ADOX. Certain features of ADOX may not be supported, depending on your data provider.  
  
 This document assumes a working knowledge of the Microsoft® Visual Basic® programming language and a general knowledge of ADO. For more information about ADO, see the [ADO Programmer's Guide](../ado-programmer-s-guide.md). For more overview information about ADOX, see the following topics:  
  
-   [ADOX Object Model](../../reference/adox-api/adox-object-model.md)  
  
-   [ADOX Objects](../../reference/adox-api/adox-objects.md)  
  
-   [ADOX Collections](../../reference/adox-api/adox-collections.md)  
  
-   [ADOX Properties](../../reference/adox-api/adox-properties.md)  
  
-   [ADOX Methods](../../reference/adox-api/adox-methods.md)  
  
-   [ADOX Examples](../../reference/adox-api/adox-code-examples.md)  
  
## See Also  
 [ADOX API Reference](../../reference/adox-api/adox-object-model.md)   
 [ADOX Code Examples](../../reference/adox-api/adox-code-examples.md)   
 [ADOX Collections](../../reference/adox-api/adox-collections.md)   
 [ADOX Enumerated Constants](../../reference/adox-api/adox-enumerated-constants.md)   
 [ADOX Methods](../../reference/adox-api/adox-methods.md)   
 [ADOX Object Model](../../reference/adox-api/adox-object-model.md)   
 [ADOX Objects](../../reference/adox-api/adox-objects.md)   
 [ADOX Properties](../../reference/adox-api/adox-properties.md)   
 [ADO (Multidimensional) (ADO MD)](../multidimensional/ado-multidimensional-ado-md.md)   
 [ADO Programmer's Guide](../ado-programmer-s-guide.md)
