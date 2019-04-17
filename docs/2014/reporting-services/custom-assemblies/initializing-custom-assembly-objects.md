---
title: "Initializing Custom Assembly Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "initializing custom assemblies [Reporting Services]"
  - "custom assemblies [Reporting Services], initializing"
  - "OnInit method"
ms.assetid: 26fd74dc-d02f-40f7-aeb3-50ce05e9e6b9
author: markingmyname
ms.author: maghan
manager: kfile
---
# Initializing Custom Assembly Objects
  In some cases, you may need to initialize property and field values in your custom assembly classes when you instantiate them. You will most likely need to initialize your custom classes with values available to you from the report's global object collections. You do this by overriding the **OnInit** method of the **Code** object of a report. To access **OnInit**, use the **Code** element of the report definition. There are two techniques for initializing property or field values of the classes in a custom assembly that you plan to use in your report: You can either declare and create a new instance of your class using **OnInit**, or you can call a publicly available method using **OnInit**.  
  
## Global Object Collections and Initialization  
 Several collections are available to you for initializing your custom class variables. You can use the **Globals** and **User** collections. The **Parameters**, **Fields** and **ReportItems** collections are not available to you at the point in the report lifecycle when the **OnInit** method is invoked. To use the shared collections, **Globals** or **User**, you need to include the **Report** object reference. For example, to initialize your custom class based on the current language of the user accessing the report, your **Code** element might look like the following:  
  
```  
<Code>  
   Dim m_myClass As MyClass  
  
   Protected Overrides Sub OnInit()  
      m_myClass = new MyClass(Report.User!Language, _  
         Report.Globals!ExecutionTime)  
   End Sub  
</Code>  
```  
  
 One way to initialize the property and field values of a class as shown previously is to declare your class and create a new instance of it by calling an overridden constructor.  
  
 Another way to initialize the property and field values of the classes in your custom assemblies is to call a publicly available method that you define from the **OnInit** method. You first need to add an instance name for your class in the report definition file. Once you have added the appropriate assembly reference and instance name, you can call your initialization method to initialize property and field values for your class. Your **OnInit** method might look like the following:  
  
```  
<Code>  
   Protected Overrides Sub OnInit()  
      m_myClass.MyInitializationMethod(Report.User!Language, _  
         Report.Globals!ExecutionTime)  
   End Sub  
</Code>  
```  
  
 For more information about adding an assembly reference and instance name for your custom class, see [Add an Assembly Reference to a Report &#40;SSRS&#41;](../report-design/add-an-assembly-reference-to-a-report-ssrs.md).  
  
 For more information about the global object collections, see [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../report-design/built-in-collections-in-expressions-report-builder.md).  
  
## See Also  
 [Using Custom Assemblies with Reports](using-custom-assemblies-with-reports.md)  
  
  
