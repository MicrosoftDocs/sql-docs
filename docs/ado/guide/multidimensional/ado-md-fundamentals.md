---
title: "ADO MD Fundamentals"
description: "ADO MD Fundamentals"
author: rothja
ms.author: jroth
ms.date: "02/15/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO MD, fundamentals"
---
# ADO MD Fundamentals
Microsoft® ActiveX® Data Objects (Multidimensional) (ADO MD) provides easy access to multidimensional data from languages such as Microsoft Visual Basic®, Microsoft Visual C++®. ADO MD extends Microsoft ActiveX® Data Objects (ADO) to include objects specific to multidimensional data, such as the [CubeDef](../../reference/ado-md-api/cubedef-object-ado-md.md) and [Cellset](../../reference/ado-md-api/cellset-object-ado-md.md) objects. With ADO MD you can browse multidimensional schema, query a cube, and retrieve the results.  
  
 Like ADO, ADO MD uses an underlying OLE DB provider to gain access to data. To work with ADO MD, the provider must be a multidimensional data provider (MDP) as defined by the OLE DB for OLAP specification. An MDP presents data in multidimensional views instead of tabular views, which is how a tabular data provider (TDP) presents data. Refer to the documentation for your OLAP OLE DB provider for more information about the specific syntax and behavior supported by your provider.  
  
 This document assumes a working knowledge of the Visual Basic programming language and a general knowledge of ADO and OLAP. For more information, see the [ADO Programmer's Guide](../ado-programmer-s-guide.md) and [OLE DB for Online Analytical Processing (OLAP)](/previous-versions/windows/desktop/ms717005(v=vs.85)).  
  
 This section contains the following topics.  
  
-   [Overview of Multidimensional Schemas and Data](./overview-of-multidimensional-schemas-and-data.md)  
  
-   [Working with Multidimensional Data](./working-with-multidimensional-data.md)  
  
-   [Using ADO with ADO MD](./using-ado-with-ado-md.md)  
  
-   [Programming with ADO MD](./programming-with-ado-md.md)  
  
## See Also  
 [ADO MD Object Model](../../reference/ado-md-api/ado-md-object-model.md)   
 [ADO Programmer's Guide](../ado-programmer-s-guide.md)   
 [ADO Extensions for Data Definition Language and Security (ADOX)](../extensions/ado-extensions-for-data-definition-language-and-security-adox.md)   
 [Overview of Multidimensional Schemas and Data](./overview-of-multidimensional-schemas-and-data.md)   
 [Programming with ADO MD](./programming-with-ado-md.md)   
 [Using ADO with ADO MD](./using-ado-with-ado-md.md)   
 [Working with Multidimensional Data](./working-with-multidimensional-data.md)