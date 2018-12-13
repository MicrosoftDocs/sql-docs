---
title: "Developing a User Interface for a Custom ForEach Enumerator | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "custom user interface [Integration Services], custom foreach enumerators"
  - "custom foreach enumerators [Integration Services], developing custom user interface"
ms.assetid: 8aa4aa80-c9ba-42b3-ba87-ae5ea5d3cac3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Developing a User Interface for a Custom ForEach Enumerator
  After you have overridden the implementation of the properties and methods of the base class to provide your custom functionality, you may want to create a custom user interface for your Foreach enumerator. If you do not create a custom user interface, users can only configure the new custom Foreach enumerator by using the Properties window.  
  
 In a custom user interface project or assembly, you create a class that implements <xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumeratorUI>. This class derives from System.Windows.Forms.UserControl, which is typically used to create a composite control to host other Windows Forms controls. The control that you create is displayed in the **Enumerator configuration** area of the **Collection** tab of the **Foreach Loop Editor**.  
  
> [!IMPORTANT]  
>  After signing and building your custom user interface and installing it in the global assembly cache as described in [Building, Deploying, and Debugging Custom Objects](../building-deploying-and-debugging-custom-objects.md), remember to provide the fully qualified name of this class in the <xref:Microsoft.SqlServer.Dts.Runtime.DtsForEachEnumeratorAttribute.UITypeName%2A> property of the <xref:Microsoft.SqlServer.Dts.Runtime.DtsForEachEnumeratorAttribute>.  
  
## Coding the User Interface Control Class  
  
### Initializing the User Interface  
 You override the <xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumeratorUI.Initialize%2A> method to cache references to the host object, and to the collections of connection managers and variables defined in the package.  
  
### Setting Properties on the User Interface Control  
 The UserControl class, from which the user interface class is derived, is intended for use as a composite control to host other Windows Forms controls. Because this class hosts other controls, you can design your custom user interface by dragging and dropping controls, arranging them, setting their properties, and responding at run time to their events as in any Windows Forms application.  
  
### Saving Settings  
 You override the <xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumeratorUI.SaveSettings%2A> method to copy the values selected by the user from the controls to the properties of the enumerator when the user closes the editor.  
  
![Integration Services icon (small)](../../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Creating a Custom Foreach Enumerator](creating-a-custom-foreach-enumerator.md)   
 [Coding a Custom Foreach Enumerator](coding-a-custom-foreach-enumerator.md)  
  
  
