---
title: "Referencing the ADO Libraries In a Visual Basic 6 Application"
description: "Referencing the ADO Libraries In a Visual Basic 6 Application"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "libraries [ADO]"
  - "referencing libraries in a Visual Basic application[ADO]"
  - "ADO, libraries"
dev_langs:
  - "VB"
---
# Referencing the ADO Libraries In a Visual Basic 6 Application
To import the ADO libraries into a Microsoft Visual Basic 6 application, you must set a reference in the Visual Basic project.  
  
### To set a reference to the ADO libraries in a Visual Basic project  
  
1.  Create a new or open an existing Visual Basic project.  
  
2.  Click the **Project** menu item and then select **References...** from the drop-down menu panel.  
  
3.  From **Available References**, check the box for **Microsoft ActiveX Data Objects *n.n* Library**, where ***n.n*** represents the latest version number. The **Location** field below should identify your choice as *$installDir\msado15.dll*, where *$installDir* represents the path of the directory in which the ADO library has been installed.  
  
4.  If you intend to use ADO MD, repeat step 3 to select **Microsoft ActiveX Data Objects (Multi-dimensional) *n.n* Library**. The **Location** field should identify this choice as *$installDir\msadomd.dll*.  
  
5.  If you intend to use ADOX, repeat step 3 to select **Microsoft ADO Ext. *n.n* for DDL and Security**. The **Location** field should identify this choice as *$installDir\msadox.dll*.  
  
6.  Click **OK** to finish setting the references.  
  
## Backward Compatibility  
 Installing ADO also copies the following type libraries of earlier versions:  
  
-   *msado27.tlb*, ADO 2.7 Type Library  
  
-   *msado26.tlb*, ADO 2.6 Type Library  
  
-   *msado25.tlb*, ADO 2.5 Type Library  
  
-   *msado21.tlb*, ADO 2.1 Type Library  
  
-   *msado20.tlb*, ADO 2.0 Type Library  
  
 If your application must use any of these ADO libraries for reasons of backward compatibility, you need to import the appropriate version of the type library. To do this, follow the procedures in the previous section, replacing *msado15.dll* by *msadoXX.tlb*, where *XX* represents the version number you need to import.
