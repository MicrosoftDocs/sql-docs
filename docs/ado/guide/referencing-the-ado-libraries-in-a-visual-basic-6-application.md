---
title: "Referencing the ADO Libraries In a Visual Basic 6 Application | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "libraries [ADO]"
  - "referencing libraries in a Visual Basic application[ADO]"
  - "ADO, libraries"
ms.assetid: cfd37a82-aad2-41cd-8d13-1566c43d95f0
author: MightyPen
ms.author: genemi
manager: craigg
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
