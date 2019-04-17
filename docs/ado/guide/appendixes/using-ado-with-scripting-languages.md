---
title: "Using ADO with Scripting Languages | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "scripting languages [ADO]"
  - "ADO, scripting languages"
ms.assetid: 76fc4d00-0c9f-422b-af5c-af6ed8fb29d8
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using ADO with Scripting Languages
Within a scripting environment, ADO allows you to expose data by way of server-side scripting. In this scenario, ADO, the underlying OLE DB provider that it uses, and any other components needed to reference a given data store are installed on a server running Internet Information Services (IIS). Using Active Server Pages (ASP), ADO is a component referenced in a script that can generate HTML, for example. This HTML content can be passed via HTTP to a client Web browser. By using scripting, the Web page can send actions back to the server-side script, allowing you to update, traverse, or view specific data.  
  
 Before you use an ActiveX object in a Web page, it is important to know whether the object is safe for scripting. When an object is considered safe for scripting, it means that the control cannot take any harmful action on the user's computer and therefore can be executed without requesting the user's approval. The following table lists the ADO objects and indicates whether they are safe for scripting.  
  
|Object|Safe for Scripting?|  
|------------|-------------------------|  
|ADO Connection|Yes|  
|ADO Command|No|  
|ADO Parameter|No|  
|ADO Recordset|Yes|  
|ADO Record|Yes|  
|ADO Stream|Yes|  
|ADO Error|No|  
|ADOX Catalog|No|  
|ADOX CellSet|No|  
|RDS DataControl|Yes|  
|RDS DataSpace|Yes|  
|RDS DataFactory|No|  
  
 The following table lists the providers included with Windows DAC/MDAC, and indicates whether they are safe for scripting.  
  
|Provider|Safe for Scripting?|  
|--------------|-------------------------|  
|Shape|Yes|  
|Persist|Yes|  
|Remote|Yes|  
|OLE DB Provider for SQL Server (SQLOLEDB)|No|  
|OLE DB Provider for ODBC (MSDASQL)|No|  
  
## ODBC Data Sources  
 One notable difference between scripting and non-scripting ADO code is the ODBC Data Source, if used. For non-scripting applications, you can create a User DSN in the ODBC Data Source Administrator. For scripts that are running under IIS, you must create a System DSN; otherwise your scripts will not recognize the data source you created. This applies to any ADO scripting application using the Microsoft OLE DB Provider for ODBC through Microsoft IIS.  
  
## Referencing the ADO Library  
 Not applicable with scripting languages.  
  
## Handling events  
 Not applicable with scripting languages.  
  
 The following topics contain more specific information about using ADO with scripting languages:  
  
-   [VBScript ADO Programming](../../../ado/guide/appendixes/vbscript-ado-programming.md)  
  
-   [JScript ADO Programming](../../../ado/guide/appendixes/jscript-ado-programming.md)  
  
## See Also  
 [Microsoft ActiveX Data Objects (ADO)](../../../ado/microsoft-activex-data-objects-ado.md)   
 [Using ADO with Microsoft Visual Basic](../../../ado/guide/appendixes/using-ado-with-microsoft-visual-basic.md)   
 [Using ADO with Microsoft Visual C++](../../../ado/guide/appendixes/using-ado-with-microsoft-visual-c.md)   
