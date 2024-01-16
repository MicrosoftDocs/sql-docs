---
title: "Initializing custom assembly objects"
description: Learn to initialize custom classes with values available to you from the report's global object collections.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: custom-assemblies
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "initializing custom assemblies [Reporting Services]"
  - "custom assemblies [Reporting Services], initializing"
  - "OnInit method"
---
# Initializing custom assembly objects
  In some cases, you might need to initialize property and field values in your custom assembly classes when you instantiate them. You most likely need to initialize your custom classes with values available to you from the report's global object collections. You do this by overriding the **OnInit** method of the **Code** object of a report. To access **OnInit**, use the **Code** element of the report definition. There are two techniques for initializing property or field values of the classes in a custom assembly that you plan to use in your report: You can either declare and create a new instance of your class using **OnInit**, or you can call a publicly available method using **OnInit**.  
  
## Global object collections and initialization  
 Several collections are available to you for initializing your custom class variables. You can use the **Globals** and **User** collections. The **Parameters**, **Fields** and **ReportItems** collections aren't available to you at the point in the report lifecycle when the **OnInit** method is invoked. To use the shared collections, **Globals** or **User**, you need to include the **Report** object reference. For example, to initialize your custom class based on the current language of the user accessing the report, your **Code** element might look like the following:  
  
```vbnet
Dim m_myClass As MyClass  

Protected Overrides Sub OnInit()  
   m_myClass = new MyClass(Report.User!Language, _  
      Report.Globals!ExecutionTime)  
End Sub  
```  
  
 One way to initialize the property and field values of a class as shown previously is to declare your class and create a new instance of it by calling an overridden constructor.  
  
 Another way to initialize the property and field values of the classes in your custom assemblies is to call a publicly available method that you define from the **OnInit** method. You first need to add an instance name for your class in the report definition file. Once you add the appropriate assembly reference and instance name, you can call your initialization method to initialize property and field values for your class. Your **OnInit** method might look like the following:  
  
```vbnet
Protected Overrides Sub OnInit()  
   m_myClass.MyInitializationMethod(Report.User!Language, _  
      Report.Globals!ExecutionTime)  
End Sub  
```  
  
 For more information about adding an assembly reference and instance name for your custom class, see [Add an Assembly Reference to a Report &#40;SSRS&#41;](../../reporting-services/report-design/add-an-assembly-reference-to-a-report-ssrs.md).  
  
 For more information about the global object collections, see [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-in-expressions-report-builder.md).  
  
## Related content  
 [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)  
  
  
