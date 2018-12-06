---
title: "Using ADO with Microsoft Visual Basic | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs:
  - "VB"
helpviewer_keywords:
  - "ADO, Visual Basic"
  - "Visual Basic [ADO]"
ms.assetid: 9dfb6784-037d-4f9d-bb7f-b506b4498573
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using ADO with Microsoft Visual Basic and Visual Basic for Applications
Setting up an ADO project and writing ADO code is similar whether you use Visual Basic or Visual Basic for Applications. This topic addresses using ADO with both Visual Basic and Visual Basic for Applications and notes any differences.

## Referencing the ADO Library
 The ADO library must be referenced by your project.

#### To reference ADO from Microsoft Visual Basic

1.  In Visual Basic, from the **Project** menu, select **References...**.

2.  Select **Microsoft ActiveX Data Objects x.x Library** from the list. Verify that at least the following libraries are also selected:

    -   Visual Basic for Applications

    -   Visual Basic runtime objects and procedures

    -   Visual Basic objects and procedures

    -   OLE Automation

3.  Click **OK**.

 You can use ADO just as easily with Visual Basic for Applications, by using Microsoft Access, for example.

#### To reference ADO from Microsoft Access

1.  In Microsoft Access, select or create a module from the **Modules** tab in the **Database** window.

2.  On the **Tools** menu, select **References...**.

3.  Select **Microsoft ActiveX Data Objects x.x Library** from the list. Verify that at least the following libraries are also selected:

    -   Visual Basic for Applications

    -   Microsoft Access 8.0 Object Library (or later)

    -   Microsoft DAO 3.5 Object Library (or later)

4.  Click **OK**.

## Creating ADO Objects in Visual Basic
 To create an automation variable and an instance of an object for that variable, you can use two methods: **Dim** or **CreateObject**.

### Dim
 You can use the **New** keyword with **Dim** to declare and create instances of ADO objects in one step:

```
Dim conn As New ADODB.Connection
```

 Alternatively, the **Dim** statement declaration and object instantiation can also be two steps:

```
Dim conn As ADODB.Connection
Set conn = New ADODB.Connection
```

> [!NOTE]
>  It is not required to explicitly use the `ADODB` progid with the **Dim** statement, assuming you have correctly referenced the ADO library in your project. However, using it ensures that you will not have naming conflicts with other libraries.

> [!NOTE]
>  For example, if you include references to both ADO and DAO in the same project, you should include a qualifier to specify which object model to use when instantiating **Recordset** objects, as in the following code:

```
Dim adoRS As ADODB.Recordset
Dim daoRS As DAO.Recordset
```

### CreateObject
 With the **CreateObject** method, the declaration and object instantiation must be two discrete steps:

```
Dim conn1
Set conn1 = CreateObject("ADODB.Connection") As Object
```

 Objects instantiated with **CreateObject** are late-bound, which means that they are not strongly typed and command-line completion is disabled. However, it does allow you to skip referencing the ADO library from your project, and enables you to instantiate specific versions of objects. For example:

```
Set conn1 = CreateObject("ADODB.Connection.2.0") As Object
```

 You could also accomplish this by specifically creating a reference to the ADO version 2.0 type library and creating the object.

 Instantiating objects by using the **CreateObject** method is typically slower than using the **Dim** statement.

## Handling Events
 In order to handle ADO events in Microsoft Visual Basic, you must declare a module-level variable using the **WithEvents** keyword. The variable can be declared only as part of a class module and must be declared at the module level. For a more thorough discussion of handling ADO events, see [Handling ADO Events](../../../ado/guide/data/handling-ado-events.md).

## Visual Basic Examples
 Many Visual Basic examples are included with the ADO documentation. For more information, see [ADO Code Examples in Microsoft Visual Basic](../../../ado/reference/ado-api/ado-code-examples-in-visual-basic.md).

## See Also
 [Microsoft ActiveX Data Objects (ADO)](../../../ado/microsoft-activex-data-objects-ado.md)
 [Using ADO with Microsoft Visual C++](../../../ado/guide/appendixes/using-ado-with-microsoft-visual-c.md)
 [Using ADO with Scripting Languages](../../../ado/guide/appendixes/using-ado-with-scripting-languages.md)
